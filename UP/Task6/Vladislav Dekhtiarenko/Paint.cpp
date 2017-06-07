// Paint.cpp: определяет точку входа для приложения.
//

#include "stdafx.h"
#include "Paint.h"
#include "WindowsX.h"
#include "CommDlg.h"
#include <CommCtrl.h>
#include <vector>
#include <string>
#include <fstream>
#include <iostream>
#include <sstream>
#include <winuser.h>

#define MAX_LOADSTRING 100
	
struct Pixel
{
	int x, y;
	Pixel(int a, int b) : x(a), y(b) {};
};

// Глобальные переменные:
HINSTANCE hInst;								// текущий экземпляр
TCHAR szTitle[MAX_LOADSTRING];					// Текст строки заголовка
TCHAR szWindowClass[MAX_LOADSTRING];			// имя класса главного окна
static int xClient, yClient, xCenter, yCenter, xClick, yClick, curWidth = 1;
static std::string statusWidth;
static std::vector<Pixel> pixels;
static HCURSOR hcHand;
static COLORREF penColor;
static HPEN userPen;
static int penStyle;
CHOOSECOLOR cc;
static COLORREF acrCustClr[16];
static HPEN hPen;
static HBRUSH hBrush;
static DWORD rgbCurrent;
static RECT window;
static bool openFlag = false;


// Отправить объявления функций, включенных в этот модуль кода:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	SetPenWidth(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	SetPenStyle(HWND, UINT, WPARAM, LPARAM);


int APIENTRY _tWinMain(HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR    lpCmdLine,
	int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);
	// TODO: разместите код здесь.
	MSG msg;
	HACCEL hAccelTable;

	// Инициализация глобальных строк
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_PAINT, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Выполнить инициализацию приложения:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PAINT));

	// Цикл основного сообщения:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}




ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PAINT));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCE(IDC_PAINT);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

bool LoadAndBlitBitmap(LPCWSTR szFileName, HDC hWinDC)
{
	// Load the bitmap image file
	HBITMAP hBitmap;
	hBitmap = (HBITMAP)::LoadImage(NULL, szFileName, IMAGE_BITMAP, 0, 0,
		LR_LOADFROMFILE);
	// Verify that the image was loaded
	if (hBitmap == NULL) {
		::MessageBox(NULL, __T("LoadImage Failed"), __T("Error"), MB_OK);
		return false;
	}

	// Create a device context that is compatible with the window
	HDC hLocalDC;
	hLocalDC = ::CreateCompatibleDC(hWinDC);
	// Verify that the device context was created
	if (hLocalDC == NULL) {
		::MessageBox(NULL, __T("CreateCompatibleDC Failed"), __T("Error"), MB_OK);
		return false;
	}

	// Get the bitmap's parameters and verify the get
	BITMAP qBitmap;
	int iReturn = GetObject(reinterpret_cast<HGDIOBJ>(hBitmap), sizeof(BITMAP),
		reinterpret_cast<LPVOID>(&qBitmap));
	if (!iReturn) {
		::MessageBox(NULL, __T("GetObject Failed"), __T("Error"), MB_OK);
		return false;
	}

	// Select the loaded bitmap into the device context
	HBITMAP hOldBmp = (HBITMAP)::SelectObject(hLocalDC, hBitmap);
	if (hOldBmp == NULL) {
		::MessageBox(NULL, __T("SelectObject Failed"), __T("Error"), MB_OK);
		return false;
	}

	// Blit the dc which holds the bitmap onto the window's dc
	BOOL qRetBlit = ::BitBlt(hWinDC, 0, 0, qBitmap.bmWidth, qBitmap.bmHeight,
		hLocalDC, 0, 0, SRCCOPY);
	if (!qRetBlit) {
		::MessageBox(NULL, __T("Blit Failed"), __T("Error"), MB_OK);
		return false;
	}

	// Unitialize and deallocate resources
	::SelectObject(hLocalDC, hOldBmp);
	::DeleteDC(hLocalDC);
	::DeleteObject(hBitmap);
	return true;
}



BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;

	hInst = hInstance; // Сохранить дескриптор экземпляра в глобальной переменной

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

	if (!hWnd)
	{
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

HWND DoCreateStatusBar(HWND hwndParent, int idStatus, HINSTANCE
	hinst, int cParts)
{
	HWND hwndStatus;
	HDC hdc;
	RECT rcClient;
	HLOCAL hloc;
	PINT paParts;
	int i, nWidth;

	// Create the status bar.
	hwndStatus = CreateWindowEx(
		0,                       // no extended styles
		STATUSCLASSNAME,         // name of status bar class
		(PCTSTR)NULL,           // no text when first created
		SBARS_SIZEGRIP |         // includes a sizing grip
		WS_CHILD | WS_VISIBLE,   // creates a visible child window
		0, 0, 0, 0,              // ignores size and position
		hwndParent,              // handle to parent window
		(HMENU)idStatus,       // child window identifier
		hinst,                   // handle to application instance
		NULL);                   // no window creation data

								 // Get the coordinates of the parent window's client area.
	GetClientRect(hwndParent, &rcClient);

	// Allocate an array for holding the right edge coordinates.
	hloc = LocalAlloc(LHND, sizeof(int) * cParts);
	paParts = (PINT)LocalLock(hloc);

	// Calculate the right edge coordinate for each part, and
	// copy the coordinates to the array.
	nWidth = rcClient.right / cParts;
	int rightEdge = nWidth;
	for (i = 0; i < cParts; i++) {
		paParts[i] = rightEdge;
		rightEdge += nWidth;
		
	}

	// Tell the status bar to create the window parts.
	SendMessage(hwndStatus, SB_SETPARTS, (WPARAM)cParts, (LPARAM)paParts);

	SendMessage(hwndStatus, SB_SETTEXT, 0, (LPARAM)L"Status Bar");

	SendMessage(hwndStatus, SB_SETTEXT, 1, (LPARAM)L"Cells");
	statusWidth = curWidth + '0';
	//sprintf(statusWidth.c_str(), "%d", 3);
	SendMessage(hwndStatus, SB_SETTEXT, 2, (LPARAM)L"Current pen width : ");
	//SendMessage(hwndStatus, SB_SETTEXT, 2, (LPARAM)statusWidth.c_str());

	ShowWindow(hwndStatus, SW_SHOW);

	// Free the array, and return.
	LocalUnlock(hloc);
	LocalFree(hloc);
	return hwndStatus;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;
	int barWidth[3];
	static OPENFILENAME ofn;
	static WCHAR szFile[100];
	static WCHAR szFilter[] = L"Bmp Files (*.bmp)\0*.bmp\0"\
		L"Text Files (*.txt)\0*.txt\0";
	static BOOL ofnSuccess;
	static std::vector<Pixel> vpixel;
	static std::vector<std::vector<Pixel>> vpixels;
	char* buf = new char[80];
	static HWND hWndStatusBar;
	bool openFlag = false;
	static std::wstring style, width,click;

	switch (message)
	{
	case WM_CREATE:
		hdc = GetDC(hWnd);
		//DoCreateStatusBar(hWnd, 1, hInst, 3);
		hWndStatusBar = CreateWindowEx(0, STATUSCLASSNAME, NULL, WS_CHILD | WS_VISIBLE | SBARS_SIZEGRIP, 0, 0, 0, 0, hWnd, (HMENU)IDC_STATUSBAR, (HINSTANCE)GetWindowLong(hWnd, GWL_HINSTANCE), NULL);
		if (!hWndStatusBar)
		{
			MessageBox(NULL, L"Failed To Create The Status Bar", L"Error", MB_OK | MB_ICONERROR);
			return 0;
		}
		GetClientRect(hWnd, &window);
		barWidth[0] = window.right / 3;
		barWidth[1] = 2 * window.right / 3;
		barWidth[2] = window.right;
		if(penStyle == 0)
		{
			style = L"Pen Style : Solid";
		}
		if (penStyle == 1)
		{
			style = L"Pen Style : Dot";
		}
		if (penStyle == 2)
		{
			style = L"Pen Style : Dash";
		}
		if (penStyle == 3)
		{
			style = L"Pen Style : Dash & dot";
		}
		if (penStyle == 4)
		{
			style = L"Pen Style : Dash & dot dot";
		}
		width = L"Pen Width : " + std::to_wstring(curWidth) ;
		SendMessage(hWndStatusBar, SB_SETPARTS, 3, (LPARAM)barWidth);
		SendMessage(hWndStatusBar, SB_SETTEXT, 0, (LPARAM)style.c_str());
		SendMessage(hWndStatusBar, SB_SETTEXT, 1, (LPARAM)width.c_str());

		ShowWindow(hWndStatusBar, SW_SHOW);

		hcHand = LoadCursor(NULL, IDC_HAND);
		userPen = CreatePen(penStyle, curWidth, RGB(255, 0, 0));
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;
		ofn.lpstrFilter = szFilter;
		ofn.lpstrFile = szFile;
		ofn.nMaxFile = sizeof(szFile);
		ofn.lpstrDefExt = L"bmp";
		break;
	case WM_LBUTTONDOWN:
		if (wParam == MK_LBUTTON)
		{
			SetCursor(hcHand);
			xClick = GET_X_LPARAM(lParam);
			yClick = GET_Y_LPARAM(lParam);
			click = L"X: " + std::to_wstring(xClick) + L" Y: " + std::to_wstring(yClick);
			SendMessage(hWndStatusBar, SB_SETTEXT, 2, (LPARAM)click.c_str());

			Pixel p(xClick, yClick);
			pixels.push_back(p);
			vpixels.push_back(pixels);
		}
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	case WM_MOUSEMOVE:
		if (wParam == MK_LBUTTON)
		{
			SetCursor(hcHand);
			xClick = GET_X_LPARAM(lParam);
			yClick = GET_Y_LPARAM(lParam);
			click = L"X: " + std::to_wstring(xClick) + L" Y: " + std::to_wstring(yClick);
			SendMessage(hWndStatusBar, SB_SETTEXT, 2, (LPARAM)click.c_str());
			Pixel p(xClick, yClick);
			pixels.push_back(p);
			vpixels.push_back(pixels);
		}
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	case WM_LBUTTONUP:
		pixels.clear();
		break;
	case WM_SIZE:
		GetClientRect(hWnd, &window);
		barWidth[0] = window.right / 3;
		barWidth[1] = 2 * window.right / 3;
		barWidth[2] = window.right;
		SendMessage(hWndStatusBar, SB_SETPARTS, 3, (LPARAM)barWidth);
		xClient = LOWORD(lParam);
		yClient = HIWORD(lParam);
		xCenter = xClient / 2;
		yCenter = yClient / 2;
		break;
	case WM_SETTEXT:

	case WM_COMMAND:
		wmId = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Разобрать выбор в меню:
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case ID_OPEN:
			wcscpy_s(szFile, L"");
			ofnSuccess = GetOpenFileName(&ofn);
			if (ofnSuccess)
			{
				if (ofn.lpstrDefExt == L"bmp")
				{
					std::ifstream f(ofn.lpstrFile);
					std::string tmp;
					while (!f.eof())
					{
						getline(f, tmp);
						if (tmp != "END")
						{
							hdc = GetDC(hWnd);
							LoadAndBlitBitmap(ofn.lpstrFile, hdc);
							EndPaint(hWnd, &ps);
							ReleaseDC(hWnd, hdc);
						}
					}
					f.close();
				}
				if (ofn.lpstrDefExt == L"txt")
				{
					std::ifstream f(ofn.lpstrFile);
					int x, y, width, penStyle;
					COLORREF color;
					std::string tmp;
					while (!f.eof())
					{
						getline(f, tmp);
						if (tmp != "END")
						{
							hdc = GetDC(hWnd);
							f >> x >> y >> penColor >> curWidth >> penStyle;
							Pixel p(x, y);
							pixels.push_back(p);
						}
					}
					f.close();
				}
			}
			else
			{
				MessageBox(hWnd, L"File Open Error", L"Error", MB_ICONWARNING);
			}
			
			openFlag == true;
			break;
		case ID_SAVE_AS:
			wcscpy_s(szFile, L"");
			ofnSuccess = GetSaveFileName(&ofn);
			if (ofnSuccess)
			{
				MessageBox(hWnd, ofn.lpstrFile, L"File is saving...", MB_OK);
				std::ofstream f(ofn.lpstrFile);
				for (int i = 0; i < vpixels.size(); i++)
				{
					for (int j = 0; j < vpixels[i].size(); j++)
					{
						f << vpixels[i][j].x << " " << vpixels[i][j].y << " " << rgbCurrent << " " << curWidth << " " << penStyle <<  std::endl;
					}
					f << "END\n";
				}
				f.close();
			}
			else
			{
				MessageBox(hWnd, L"Save Open Error", L"Error", MB_ICONWARNING);
			}
			break;
		case IDM_CHOOSE_COLOR:
			ZeroMemory(&cc, sizeof(cc));
			cc.lStructSize = sizeof(cc);
			cc.hwndOwner = hWnd;
			cc.lpCustColors = (LPDWORD)acrCustClr;
			cc.rgbResult = rgbCurrent;
			cc.Flags = CC_FULLOPEN | CC_RGBINIT;
			if (ChooseColor(&cc) == TRUE)
			{
				userPen = CreatePen(penStyle, curWidth, cc.rgbResult);
				rgbCurrent = cc.rgbResult;
			}
			break;
		case IDM_PEN_SETWIDTH:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_SET_WIDTH), hWnd, SetPenWidth);
			width = L"Pen Width : " + std::to_wstring(curWidth);
			SendMessage(hWndStatusBar, SB_SETTEXT, 1, (LPARAM)width.c_str());
			break;
		case IDM_PEN_SETSTYLE:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_STYLE), hWnd, SetPenStyle);
			if (penStyle == 0)
			{
				style = L"Pen Style : Solid";
			}
			if (penStyle == 1)
			{
				style = L"Pen Style : Dot";
			}
			if (penStyle == 2)
			{
				style = L"Pen Style : Dash";
			}
			if (penStyle == 3)
			{
				style = L"Pen Style : Dash & dot";
			}
			if (penStyle == 4)
			{
				style = L"Pen Style : Dash & dot dot";
			}
			SendMessage(hWndStatusBar, SB_SETTEXT, 0, (LPARAM)style.c_str());
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		
		hdc = BeginPaint(hWnd, &ps);
		userPen = CreatePen(penStyle, curWidth, rgbCurrent);
		SelectObject(hdc, userPen);
		/*if(ofnSuccess)
		{
			LoadAndBlitBitmap(ofn.lpstrFile, hdc);
		}*/
		for (int i = 1; i < pixels.size(); i++)
		{
			MoveToEx(hdc, pixels[i - 1].x, pixels[i - 1].y, NULL);
			LineTo(hdc, pixels[i].x, pixels[i].y);
		}
		
		EndPaint(hWnd, &ps);
		ReleaseDC(hWnd, hdc);
		break;
	case WM_DESTROY:
		DeleteObject(hcHand);
		DeleteObject(hPen);
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// Обработчик сообщений для окна "О программе".
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}

		break;
	}
	return (INT_PTR)FALSE;
}


INT_PTR CALLBACK SetPenWidth(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	static std::wstring buf;
	static char* str;

	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{

			GetDlgItemText(hDlg, IDC_WIDTH, (LPWSTR)buf.c_str(), 10);
			str = new char[255];
			sprintf(str, "%ls", buf.c_str());
			curWidth = atoi(str);
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}

		break;
	}
	return (INT_PTR)FALSE;
}

INT_PTR CALLBACK SetPenStyle(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	static std::wstring buf;
	static char* str;

	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if(LOWORD(wParam) == IDC_SOLID)
		{
			penStyle = 0;
		}
		if (LOWORD(wParam) == IDC_DOT)
		{
			penStyle = 1;
		}
		if (LOWORD(wParam) == IDC_DASH)
		{
			penStyle = 2;
		}
		if (LOWORD(wParam) == IDC_DASHDOT)
		{
			penStyle = 3;
		}
		if (LOWORD(wParam) == IDC_DASHDOTDOT)
		{
			penStyle = 4;
		}
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}

		break;
	}
	return (INT_PTR)FALSE;
}

