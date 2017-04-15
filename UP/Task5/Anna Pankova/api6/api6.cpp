// api6.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "api6.h"

struct MyException {
	string msg;
	MyException(const string msg = "") : msg(msg){}
};

#define MAX_LOADSTRING 100
#define ID_EDITA 3001
#define ID_EDITB 3002
#define ID_EDITRES 3003
#define ID_HISTORY 3010
#define ID_BUTTON_C 3004
#define ID_BUTTON_D 3009
#define ID_RB_PLUS 3005
#define ID_RB_MINUS 3006
#define ID_RB_NUL 3007
#define ID_RB_DIV 3008


HWND hEditA, hEditB, hEditRes, hHistory, hButtonC, hButtonD,
hRBPlus, hRBMinus, hRBNul, hRBDiv;

wstring history;



// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);


void DoCalc(HWND hWnd)
{
	try {
		WCHAR buf[30];
		GetDlgItemText(hWnd, IDC_EDIT2, buf, 30);
		float res = _wtof(buf);
		GetDlgItemText(hWnd, IDC_EDIT1, buf, 30);
		float b = _wtof(buf);

		wstringstream line;
		line << res << " ";

		if (SendDlgItemMessage(hWnd, IDC_RADIO1, BM_GETCHECK, 0, 0) == BST_CHECKED) {
			res += b;
			line << "+ ";
		}
		else if (SendDlgItemMessage(hWnd, IDC_RADIO3, BM_GETCHECK, 0, 0) == BST_CHECKED) {
			res -= b;
			line << "- ";
		}
		else if (SendDlgItemMessage(hWnd, IDC_RADIO2, BM_GETCHECK, 0, 0) == BST_CHECKED) {
			res *= b;
			line << "* ";
		}
		else if ((SendDlgItemMessage(hWnd, IDC_RADIO4, BM_GETCHECK, 0, 0) == BST_CHECKED) &&
			SendDlgItemMessage(hWnd, IDC_CHECK1, BM_GETCHECK, 0, 0) == BST_CHECKED) {
			if (b == 0) throw MyException("Деление на 0!");
			line << ": ";
			res = (int)(res / b);
		}
		else if (SendDlgItemMessage(hWnd, IDC_RADIO4, BM_GETCHECK, 0, 0) == BST_CHECKED) {
			if (b == 0) throw MyException("Деление на 0!");
			line << ": ";
			res /= b;
		}
		else assert(false);

		line << b << " = " << res << "\r\n";
		history += line.str();

		SetDlgItemText(hWnd, IDC_HISTORY, history.c_str());

		WCHAR buf1[100];
		swprintf_s(buf, L"%d", res);
		SetWindowText(hEditRes, buf);

	}
	catch (MyException & e) {
		SetWindowText(hEditRes, L"Error!");
		MessageBox(NULL, (LPCWSTR)e.msg.c_str(), L"Error", MB_OK);
	}
}


LRESULT CALLBACK DialogProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
	case WM_INITDIALOG:
	{
		HMENU hMenu = LoadMenu(hInst, MAKEINTRESOURCE(IDC_API6));
		SetMenu(hWnd, hMenu);
		break;
	}
	case WM_COMMAND:
	{
		if (HIWORD(wParam) == 0 && LOWORD(wParam) == ID_FILE_OPEN) {
			OPENFILENAME ofn = { 0 };
			WCHAR filename[1000];
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFile = filename;
			ofn.nMaxFile = 1000;
			ofn.lpstrFile[0] = '\0';
			GetOpenFileName(&ofn);

			ifstream in(ofn.lpstrFile);
			string data;
			getline(in, data, '\0');
			history = wstring(data.begin(), data.end());
			in.close();

			SetDlgItemText(hWnd, IDC_HISTORY, history.c_str());
		}
		if (HIWORD(wParam) == 0 && LOWORD(wParam) == ID_FILE_SAVE) {
			OPENFILENAME ofn = { 0 };
			WCHAR filename[1000];
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFile = filename;
			ofn.nMaxFile = 1000;
			ofn.lpstrFile[0] = '\0';
			GetSaveFileName(&ofn);

			wofstream out(ofn.lpstrFile);
			out << history;
			out.close();
		}

		if (LOWORD(wParam) == IDC_BUTTON1) { 
			DoCalc(hWnd);
		}

		if (HIWORD(wParam) == 0 && LOWORD(wParam) == IDM_EXIT) {
			PostQuitMessage(0);
		}
		break;
	}
	case WM_CLOSE:
		PostQuitMessage(0);
		break;
	}
	return 0;
}


INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, reinterpret_cast<DLGPROC>(DialogProc));
	return 0;
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_API6));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+20);
    wcex.lpszMenuName   = MAKEINTRESOURCE(IDC_API6);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}
//
/*
void CreateControls(HWND hWnd) {
	CreateWindow(L"static", L"Первый аргумент", WS_CHILD | WS_VISIBLE | SS_LEFT,
		12, 12, 140, 20, hWnd, NULL, hInst, NULL);
	CreateWindow(L"static", L"Ворой аргумент", WS_CHILD | WS_VISIBLE | SS_LEFT,
		12, 42, 140, 20, hWnd, NULL, hInst, NULL);
	CreateWindow(L"static", L"Результат", WS_CHILD | WS_VISIBLE | SS_LEFT,
		12, 72, 140, 20, hWnd, NULL, hInst, NULL);

	hEditA = CreateWindow(L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
		155, 12, 100, 20, hWnd, (HMENU)ID_EDITA, hInst, NULL);

	hEditB = CreateWindow(L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
		155, 42, 100, 20, hWnd, (HMENU)ID_EDITB, hInst, NULL);

	hEditRes = CreateWindow(L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
		155, 72, 100, 20, hWnd, (HMENU)ID_EDITRES, hInst, NULL);

	hHistory = CreateWindow(L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_VSCROLL |
		ES_LEFT | ES_MULTILINE | ES_AUTOVSCROLL, 350, 12, 550, 240, hWnd, (HMENU)ID_HISTORY, hInst, NULL);

	EnableWindow(hEditRes, FALSE);
	EnableWindow(hHistory, FALSE);

	CreateWindow(L"static", L"", WS_CHILD | WS_VISIBLE | SS_LEFT,
		10, 110, 140, 40, hWnd, NULL, hInst, NULL);

	hButtonC = CreateWindow(L"Button", L"Вычислисть", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON, 
		155, 190, 150, 30, hWnd, (HMENU)ID_BUTTON_C, hInst, NULL);
	
	hButtonD = CreateWindow(L"Button", L"Нацело", WS_CHILD | WS_VISIBLE | BS_AUTOCHECKBOX,
		155, 140, 100, 30, hWnd, (HMENU)ID_BUTTON_D, hInst, NULL);
	
	hRBPlus = CreateWindow(L"Button", L"Сложить", WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
		12, 110, 100, 20, hWnd, (HMENU)ID_RB_PLUS, hInst, NULL);
	hRBMinus = CreateWindow(L"Button", L"Вычесть", WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
		12, 140, 100, 20, hWnd, (HMENU)ID_RB_MINUS, hInst, NULL);
	hRBNul= CreateWindow(L"Button", L"Умножить", WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
		12, 170, 100, 20, hWnd, (HMENU)ID_RB_NUL, hInst, NULL);
	hRBDiv = CreateWindow(L"Button", L"Разделить", WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
		12, 200, 100, 20, hWnd, (HMENU)ID_RB_DIV, hInst, NULL);

	SendMessage(hRBPlus, BM_SETCHECK, (WPARAM)BST_CHECKED, 0);
}

float ReadArg(HWND hEdit) {
	WCHAR buf[100];
	GetWindowText(hEdit, buf, 99);
	float result = _wtof(buf);
	return result;
}

int ReadArgEnt(HWND hEdit) {
	WCHAR buf[100];
	GetWindowText(hEdit, buf, 99);
	int result = _wtoi(buf);
	return result;
}*/

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	HWND dialogHWnd;
    switch (message)
    {		
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Message handler for about box.
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
