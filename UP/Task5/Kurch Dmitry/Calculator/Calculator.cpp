// Calculator.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Calculator.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
static float a, b, result;
static wstring history, strResult, sign;

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
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
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_CALCULATOR));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_CALCULATOR);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

void Calculator(HWND hWnd)
{
	WCHAR temp[30];
	GetDlgItemText(hWnd, IDC_OPERAND1, temp, 30);
	a = _wtof(temp);
	GetDlgItemText(hWnd, IDC_OPERAND2, temp, 30);
	b = _wtof(temp);

	if (SendDlgItemMessage(hWnd, IDC_ADD, BM_GETCHECK, 0, 0) == BST_CHECKED) {
		result = a + b;
		sign = '+';
	}
	else if (SendDlgItemMessage(hWnd, IDC_SUB, BM_GETCHECK, 0, 0) == BST_CHECKED) {
		result = a - b;
		sign = '-';
	}
	else if (SendDlgItemMessage(hWnd, IDC_MUL, BM_GETCHECK, 0, 0) == BST_CHECKED) {
		result = a*b;
		sign = '*';
	}
	else if ((SendDlgItemMessage(hWnd, IDC_DIV, BM_GETCHECK, 0, 0) == BST_CHECKED) &&
		SendDlgItemMessage(hWnd, IDC_CHECK, BM_GETCHECK, 0, 0) == BST_CHECKED) {
		sign = '/';
		result = (int)(a / b);
	}
	else if (SendDlgItemMessage(hWnd, IDC_DIV, BM_GETCHECK, 0, 0) == BST_CHECKED) {
		sign = '/';
		result = a / b;
	}

	wstringstream out;
	out << a << "  " << sign << " " << b << " = " << result << "\r\n";

	history += out.str();
	SetDlgItemText(hWnd, IDC_RESUL, history.c_str());
	strResult = to_wstring(result);
	SetDlgItemText(hWnd, IDC_RES, strResult.c_str());

}

LRESULT CALLBACK DialogProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_INITDIALOG:
	{
		HMENU menu = LoadMenu(hInst, MAKEINTRESOURCE(IDC_CALCULATOR));
		SetMenu(hWnd, menu);
		break;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case ID_OPEN:
		{
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
			SetDlgItemText(hWnd, IDC_RESUL, history.c_str());
			break;
		}
		case ID_SAVE:
		{
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
			break;
		}
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		case IDC_COUNT:
			Calculator(hWnd);
		     break;
		}
		break;
	}
	case WM_CLOSE:
	{
		PostQuitMessage(0);
		break;
	}
	}
	return 0;
}

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_MAIN), 0, reinterpret_cast<DLGPROC>(DialogProc));
	return 0;
}
//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
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
