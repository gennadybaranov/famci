// Task5.cpp: определяет точку входа для приложения.
//

#include "stdafx.h"
#include "Task5.h"
#include <stdio.h>
#include "winbase.h"
#include "resource.h"
#include <windows.h>
#include <string>
#include <iostream>
#include <fstream>

using namespace std;

#define MAX_LOADSTRING 100
#define IDD_DIALOG1                     130
#define IDCANCEL                        1004
#define IDC_EDIT1                       1005
#define IDC_EDIT2                       1006
#define IDC_LIST1                       1007
#define IDC_LIST2                       1008
#define IDC_RADIO1                      1009
#define IDC_RADIO2                      1010
#define IDC_RADIO3                      1011
#define IDC_RADIO4                      1012
#define IDC_RADIO5                      1013
#define IDC_BUTTON1                     1014
#define ID_32771                        32771
#define ID_32772                        32772

HWND hWnd;
LRESULT CALLBACK DlgProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
// Глобальные переменные:
HINSTANCE hInst;
HINSTANCE hinst;  // текущий экземпляр                               // текущий экземпляр
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
    LoadStringW(hInstance, IDC_TASK5, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_TASK5));

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

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, reinterpret_cast<DLGPROC>(DlgProc));
	return 0;
}


LRESULT CALLBACK DlgProc(HWND hWndDlg, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	HANDLE file;
	static char buf1[10], buf2[10];
	INT a, b;
	BOOL sucess;
	static OPENFILENAME ofn;
	static char szFile[200];
	double doub;
	static string res;
	static int n = 1;
	static bool nac = true;
	static HWND hbuttonit;
	string szText, A, B, sign, r, out;
	HMENU hmenu;
	static char*fi;
	hinst = GetModuleHandle(NULL);
	switch (Msg)
	{
	case WM_INITDIALOG:
	{
		hmenu = LoadMenu(hinst, MAKEINTRESOURCE(IDC_TASK5));
		SetMenu(hWndDlg, hmenu);
		return TRUE;
	}
	case WM_COMMAND:
		switch (wParam)
		{
		case IDC_RADIO1:
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_RESETCONTENT, 0, 0);
			GetDlgItemText(hWndDlg, IDC_EDIT1, buf1, 10);
			GetDlgItemText(hWndDlg, IDC_EDIT2, buf2, 10);
			a = atoi(buf1);
			b = atoi(buf2);
			a = a + b;
			res = to_string((long double)a);
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_ADDSTRING, 0, (LPARAM)res.c_str());
			A = string(buf1);
			B = string(buf2);
			sign = " + ";
			r = " = ";
			out = A + sign + B + r + res;
			SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)out.c_str());
			break;

		case IDC_RADIO2:
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_RESETCONTENT, 0, 0);
			GetDlgItemText(hWndDlg, IDC_EDIT1, buf1, 10);
			GetDlgItemText(hWndDlg, IDC_EDIT2, buf2, 10);
			a = atoi(buf1);
			b = atoi(buf2);
			a = a - b;
			res = to_string((long double)a);
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_ADDSTRING, 0, (LPARAM)res.c_str());
			A = string(buf1);
			B = string(buf2);
			sign = " - ";
			r = " = ";
			out = A + sign + B + r + res;
			SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)out.c_str());
			break;

		case IDC_RADIO3:
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_RESETCONTENT, 0, 0);
			GetDlgItemText(hWndDlg, IDC_EDIT1, buf1, 10);
			GetDlgItemText(hWndDlg, IDC_EDIT2, buf2, 10);
			a = atoi(buf1);
			b = atoi(buf2);
			a = a * b;
			res = to_string((long double)a);
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_ADDSTRING, 0, (LPARAM)res.c_str());
			A = string(buf1);
			B = string(buf2);
			sign = " * ";
			r = " = ";
			out = A + sign + B + r + res;
			SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)out.c_str());
			break;

		case IDC_RADIO4:
		{
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_RESETCONTENT, 0, 0);
			GetDlgItemText(hWndDlg, IDC_EDIT1, buf1, 10);
			GetDlgItemText(hWndDlg, IDC_EDIT2, buf2, 10);
			a = atoi(buf1);
			b = atoi(buf2);
			if (b == 0)
				SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_ADDSTRING, 0, (LPARAM)"ERROR!!!");
			else
			{
				doub = (double)a / (double)b;
				res = to_string((long double)doub);
				SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_ADDSTRING, 0, (LPARAM)res.c_str());
				A = string(buf1);
				B = string(buf2);
				sign = " / ";
				r = " = ";
				out = A + sign + B + r + res;
				SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)out.c_str());
			}
		}
		break;

		case IDC_RADIO5:
		{
			SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_RESETCONTENT, 0, 0);
			GetDlgItemText(hWndDlg, IDC_EDIT1, buf1, 10);
			GetDlgItemText(hWndDlg, IDC_EDIT2, buf2, 10);
			a = atoi(buf1);
			b = atoi(buf2);
			if (b == 0)
				SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)"ERROR!!!");
			else
			{
				a = a / b;
				res = to_string((long double)a);
				SendDlgItemMessage(hWndDlg, IDC_LIST1, LB_ADDSTRING, 0, (LPARAM)res.c_str());
				A = string(buf1);
				B = string(buf2);
				sign = " / ";
				r = " = ";
				out = A + sign + B + r + res;
				SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)out.c_str());
			}
		}
		break;


		case ID_32771:
		{
			static char szFilter[] = "Text Files (*.TXT)\0*.txt\0 ";
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFile = szFile;
			ofn.nMaxFile = sizeof(szFile);
			ofn.lpstrFilter = szFilter;
			ofn.lpstrDefExt = "txt";
			strcpy(szFile, "");
			sucess = GetOpenFileName(&ofn);
			if (sucess)
			{
				SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_RESETCONTENT, 0, 0);
				HWND ListBox = GetDlgItem(hWndDlg, IDC_LIST2);
				int y = 0;
				ifstream myfile(ofn.lpstrFile);
				while (myfile.good())
				{
					string s;
					getline(myfile, s);
					SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_ADDSTRING, 0, (LPARAM)s.c_str());
				}
				myfile.close();
				MessageBox(hWnd, ofn.lpstrFile, "Считываем из файла:", MB_OK);
				//_lopen(ofn.lpstrFile, OF_READ);
			}
			else
				MessageBox(hWnd, "Отказ от выбора или ошибка.", "Ошибка", MB_ICONWARNING);
			break;
		}
		break;

		case ID_32772:
		{
			char* lpString = new char[0xFF];
			file = CreateFile("New.txt", GENERIC_ALL, FILE_SHARE_DELETE | FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
			DWORD dwCount = (DWORD)::SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_GETCOUNT, 0, 0);
			for (DWORD i = 0; i < dwCount; i++)
			{
				SendDlgItemMessage(hWndDlg, IDC_LIST2, LB_GETTEXT, i, (LPARAM)(LPSTR)lpString);
				lstrcat(lpString, "\xD\xA");
				DWORD dwBytesCount;
				WriteFile(file, (LPCVOID)lpString, lstrlen(lpString), &dwBytesCount, NULL);
			}
			MessageBox(hWnd, "Файл сохранен в папке проекта.", "Сохранение", MB_OK);
		}
			break;

		case IDM_EXIT:
			EndDialog(hWndDlg, 0);
			return TRUE;
			break;
		case IDOK:
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
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_TASK5));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_TASK5);
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
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Разобрать выбор в меню:
            switch (wmId)
            {
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
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
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
