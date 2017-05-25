// Task6.cpp: определяет точку входа для приложения.
//

#include "stdafx.h"
#include "Task6.h"

#include <windows.h>
#include <fstream>
#include <iostream>
#include <vector>
#include <string>

using namespace std;

#define MAX_LOADSTRING 100

#define ID_LINE_COLOR                   32773
#define ID_LINE_WIDTH                   32774
#define ID_LINE_TYPE                    32775
#define ID_FILE_OPEN                    32776
#define ID_FILE_SAVE                    32778
#define ID_FILE_CLEAN                   32779
#define IDD_DIALOG1                     129
#define IDD_DIALOG2                     130
#define IDC_EDIT1                       1001

// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: разместите код здесь.

    // Инициализация глобальных строк
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_TASK6, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_TASK6));

    MSG msg;

    // Цикл основного сообщения:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  ФУНКЦИЯ: MyRegisterClass()
//
//  НАЗНАЧЕНИЕ: регистрирует класс окна.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_TASK6));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_TASK6);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   ФУНКЦИЯ: InitInstance(HINSTANCE, int)
//
//   НАЗНАЧЕНИЕ: сохраняет обработку экземпляра и создает главное окно.
//
//   КОММЕНТАРИИ:
//
//        В данной функции дескриптор экземпляра сохраняется в глобальной переменной, а также
//        создается и выводится на экран главное окно программы.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Сохранить дескриптор экземпляра в глобальной переменной

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  ФУНКЦИЯ: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  НАЗНАЧЕНИЕ:  обрабатывает сообщения в главном окне.
//
//  WM_COMMAND — обработать меню приложения
//  WM_PAINT — отрисовать главное окно
//  WM_DESTROY — отправить сообщение о выходе и вернуться
//
//

struct discrPoint {
	discrPoint(vector<POINT> _d, int _wid, int _sty, COLORREF _col) : d(_d), wid(_wid), sty(_sty), col(_col) {}
	vector<POINT> d;
	int wid;
	int sty;
	COLORREF col;
};

HWND hStatus;
int pParts[3];
short cx;
wstring buf1 = L"1", buf2, wBuf, sBuf, st = L"PS_SOLID";
static char* cBuf;
static char* str1;
static char* str2;
static int style = PS_SOLID, width = 1, _style;;
static POINT ptPrevious, ptPrevious2, ar;
static BOOL fDraw = FALSE;
static vector<POINT> dots;
static vector<POINT>::iterator it;
static vector <discrPoint> curves;

LRESULT CALLBACK DlgProc1(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK DlgProc2(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);


LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	
	static HDC hDC;
	static HDC hdc;
	PAINTSTRUCT ps;
	static OPENFILENAME ofn;
	static WCHAR szFile[100];
	static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
		L"All files (*.*)\0*.*\0\0";

	static CHOOSECOLOR cc = {0};
	static COLORREF textColor;
	static COLORREF acrCustClr[16];

	static BOOL ofnSucess;
	static HPEN hpen = CreatePen(style, width, RGB(0, 0, 0));

    switch (message)
    {
	case WM_CREATE:
		hDC = GetDC(hWnd);

		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;
		ofn.lpstrFilter = szFilter;
		ofn.lpstrFile = szFile;
		ofn.nMaxFile = sizeof(szFile);
		ofn.lpstrDefExt = L"txt";

		cc.lStructSize = sizeof(CHOOSECOLOR);
		cc.hwndOwner = hWnd;
		cc.Flags = CC_FULLOPEN | CC_RGBINIT;
		cc.rgbResult = RGB(130, 160, 150);
		cc.lpCustColors = acrCustClr;
		cc.lCustData = 0L;

		hStatus = CreateStatusWindow(WS_CHILD | WS_VISIBLE, (LPWSTR)L"PS_SOLID", hWnd, 10000);
		break;

    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            switch (wmId)
            {

			case ID_FILE_OPEN:
			{
				static char szFilter[] = "Text Files (*.TXT)\0*.txt\0 ";
				wcscpy_s(szFile, L"");
				ofnSucess = GetOpenFileName(&ofn);
				if (ofnSucess)
				{
					curves.clear();
					ifstream fin(ofn.lpstrFile);
					string wid, st, col, po, po1;
					int poin, poin1, i = 0, k = 0, st2;
					char c;
					bool f = true;
					discrPoint*p = NULL;
					while ((c = fin.peek()) != EOF)
					{
						if (c == ' ')
						{ fin.ignore(); continue; }
						if (c == '\n')
						{
							char mass3[6];
							strcpy(mass3, wid.c_str());
							width = atoi(mass3);
							char mass4[6];
							strcpy(mass4, st.c_str());
							st2 = atoi(mass4);
							switch (st2) {
							case 1:	style = PS_SOLID;
								break;
							case 2: style = PS_DASH;
								break;
							case 3: style = PS_DOT;
								break;
							case 4:	style = PS_DASHDOT;
								break;
							}
							char mass5[50];
							strcpy(mass5, col.c_str());
							textColor = atoi(mass5);
							p = new discrPoint(dots, width, style, textColor);
							curves.push_back(*p);
							f = true;
							dots.clear();
							fin.ignore();
							continue;
						}
						if (f)
						{
							if (i == 0)
							{ fin >> wid; i++; }
							if (i == 1)
							{fin >> st;	i++; }
							if (i == 2)
							{ fin >> col; f = false; i = 0;	}
							continue;
						}
						else
						{
							if (k == 0)
							{
								fin >> po;
								char mass[6];
								strcpy(mass, po.c_str());
								poin = atoi(mass);
								k++;
							}
							if (k == 1)
							{
								fin >> po1;
								char mass2[6];
								strcpy(mass2, po1.c_str());
								poin1 = atoi(mass2);
								POINT a;
								a.x = poin; a.y = poin1;
								dots.push_back((a));
								k = 0;
							}
							continue;
						}
					}
					InvalidateRect(hWnd, NULL, TRUE);
					UpdateWindow(hWnd);
				}
				else
					MessageBox(hWnd, L"Failure to choose or error.", L"Error", MB_ICONWARNING);
				break;
			}
			break;

			case ID_FILE_SAVE:
			{
				static char szFilter[] = "Text Files (*.TXT)\0*.txt\0 ";
				wcscpy_s(szFile, L"");
				ofnSucess = GetOpenFileName(&ofn);
				if (ofnSucess)
				{
					ofstream fout(ofn.lpstrFile);
					for (int i = 0; i < curves.size(); ++i)
					{
						fout << curves[i].wid << " " << curves[i].sty << " " << curves[i].col << " " ;
						it = curves[i].d.begin();
						for (it; it != curves[i].d.end(); ++it)
							fout << it->x << " " << it->y << " ";
						fout << endl;
					}
					MessageBox(hWnd, ofn.lpstrFile, L"Writing to file:", MB_OK);
				}
				else
					MessageBox(hWnd, L"Failure to choose or error.", L"Error", MB_ICONWARNING);
				break;
			}
			break;

			case ID_LINE_COLOR:
				if (ChooseColor(&cc)) {
					textColor = cc.rgbResult;
					InvalidateRect(hWnd, NULL, TRUE);
					UpdateWindow(hWnd);
				}
				break;

			case ID_LINE_WIDTH:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_DIALOG1), hWnd, reinterpret_cast<DLGPROC>(DlgProc1));
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
				break;

			case ID_LINE_TYPE:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_DIALOG2), hWnd, reinterpret_cast<DLGPROC>(DlgProc2));
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
				break;

			case ID_FILE_CLEAN:
				curves.clear();
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
				break;

            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;

            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;

            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;

	case WM_SIZE:
		MoveWindow(hStatus, 0, 0, 0, 0, TRUE);
		cx = LOWORD(lParam);
		pParts[0] = cx/2;
		pParts[1] = cx;
		SendMessage(hStatus, SB_SETPARTS, 2, (LPARAM)pParts);
		break;

	case WM_LBUTTONDOWN:
		fDraw = TRUE;
		ptPrevious.x = LOWORD(lParam);
		ptPrevious.y = HIWORD(lParam);
		return 0;

	case WM_LBUTTONUP:
		if (fDraw)
		{
			hdc = GetDC(hWnd);
			MoveToEx(hdc, ptPrevious.x, ptPrevious.y, NULL);
			LineTo(hdc, ar.x = LOWORD(lParam), ar.y = HIWORD(lParam));
			dots.push_back(POINT(ptPrevious));
			curves.push_back(discrPoint(dots, width, style, textColor));
			dots.clear();

			ReleaseDC(hWnd, hdc);
			InvalidateRect(hWnd, NULL, TRUE);
			UpdateWindow(hWnd);
		}
		fDraw = FALSE;
		return 0;

	case WM_MOUSEMOVE:
		if (fDraw)
		{
			hdc = GetDC(hWnd);
			MoveToEx(hdc, ptPrevious.x, ptPrevious.y, NULL);
			LineTo(hdc, ptPrevious.x = LOWORD(lParam), ptPrevious.y = HIWORD(lParam));
			dots.push_back(POINT(ptPrevious));
			ReleaseDC(hWnd, hdc);
		}
		return 0L;

    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
			
			SendMessage(hStatus, SB_SETTEXT, 1, (LPARAM)buf1.c_str());	

			if (_style == 1)
				SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)L"PS_SOLID");
			if (_style == 2)
				SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)L"PS_DASH");
			if (_style == 3)
				SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)L"PS_DOT");
			if (_style == 4)
				SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)L"PS_DASHDOT");

			for (int i = 0; i < curves.size(); ++i)
			{
				hpen = CreatePen(curves[i].sty, curves[i].wid, curves[i].col);
				SelectObject(hdc, hpen);
				it = curves[i].d.begin();
				MoveToEx(hdc, it->x, it->y, NULL);
				for (it + 1; it != curves[i].d.end(); ++it)
					LineTo(hdc, it->x, it->y);
			}
            // TODO: Добавьте сюда любой код прорисовки, использующий HDC...
            EndPaint(hWnd, &ps);
        }
        break;

    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

LRESULT CALLBACK DlgProc1(HWND hWndDlg, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	HMENU hmenu1;
	static int k = 0;
	HINSTANCE hinst = GetModuleHandle(NULL);
	switch (Msg)
	{
	case WM_INITDIALOG:
	{
		hmenu1 = LoadMenu(hinst, MAKEINTRESOURCE(IDD_DIALOG1));
		SetMenu(hWndDlg, hmenu1);
		return TRUE;
	}
	case WM_COMMAND:
		switch (wParam)
		{
		case IDOK:
			GetDlgItemText(hWndDlg, IDC_EDIT1, (LPWSTR)buf1.c_str(), 10);
			str1 = new char[255];
			sprintf(str1, "%ls", buf1.c_str());
			width = atoi(str1);
			EndDialog(hWndDlg, 0);
			return TRUE;
			break;

		case WM_DESTROY:
			EndDialog(hWndDlg, 0);
			return TRUE;
			break;
		}
	}
	return FALSE;
}

LRESULT CALLBACK DlgProc2(HWND hWndDlg2, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	HMENU hmenu2;
	HINSTANCE hinst2 = GetModuleHandle(NULL);
	switch (Msg)
	{
	case WM_INITDIALOG:
	{
		hmenu2 = LoadMenu(hinst2, MAKEINTRESOURCE(IDD_DIALOG2));
		SetMenu(hWndDlg2, hmenu2);
		return TRUE;
	}
	case WM_COMMAND:
		switch (wParam)
		{

		case IDOK:
			GetDlgItemText(hWndDlg2, IDC_EDIT1, (LPWSTR)buf2.c_str(), 10);
			str2 = new char[255];
			sprintf(str2, "%ls", buf2.c_str());
			_style = atoi(str2);
			if (_style == 1)
				style = PS_SOLID; 
			if (_style == 1)
				style = PS_DASH;
			if (_style == 1)
				style = PS_DOT;
			if (_style == 1)
				style = PS_DASHDOT;
			EndDialog(hWndDlg2, 0);
			return TRUE;
			break;

		case WM_DESTROY:
			EndDialog(hWndDlg2, 0);
			return TRUE;
			break;
		}
	}
	return FALSE;
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
