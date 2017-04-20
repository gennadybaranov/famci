// Calculater.cpp: определяет точку входа для приложения.
//
#include "stdafx.h"
#include "Calculator.h"

#define MAX_LOADSTRING 100

HWND hEditA, hEditB, hEditRes, hButtonC,
hRBPlus, hRBMinus, hRBMul, hRBDiv, hWhole, hResults;

double a, b, res;
wchar_t sign;
vector<wstring> results;

// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

												// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK    Dialog(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, reinterpret_cast<DLGPROC>(Dialog));
	return 0;
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

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_CALCULATOR));
	wcex.hCursor = LoadCursor(nullptr, IDC_CROSS);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_CALCULATOR);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

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

double ReadArg(HWND hEdit)
{
	WCHAR buf[100];
	GetWindowText(hEdit, buf, 99);
	double result = _wtof(buf);
	return result;
}

void Init(HWND hDlg)
{
	hEditA = GetDlgItem(hDlg, ID_A);
	hEditB = GetDlgItem(hDlg, ID_B);
	hEditRes = GetDlgItem(hDlg, ID_RES);
	EnableWindow(hEditRes, FALSE);
	hRBPlus = GetDlgItem(hDlg, ID_RB_PLUS);
	hRBMinus = GetDlgItem(hDlg, ID_RB_MINUS);
	hRBMul = GetDlgItem(hDlg, ID_RB_MUL);
	hRBDiv = GetDlgItem(hDlg, ID_RB_DIV);
	hWhole = GetDlgItem(hDlg, IDC_WHOLE);
	hResults = GetDlgItem(hDlg, IDC_RESULTS);
	EnableWindow(hWhole, FALSE);
	SendMessage(hRBPlus, BM_SETCHECK, (WPARAM)BST_CHECKED, 0);
	sign = '+';
}

void Calculate(HWND hDlg)
{
	a = ReadArg(hEditA);
	b = ReadArg(hEditB);
	res = 0;
	try
	{
		switch (sign)
		{
		case '+':
			res = a + b;
			break;
		case '-':
			res = a - b;
			break;
		case '*':
			res = a * b;
			break;
		case '/':
			if (fabs(b) < 1e-9) throw exception("Division by 0");
			if (IsDlgButtonChecked(hDlg, IDC_WHOLE)) res = int(a / b);
			else res = double(a / b);
			break;
		default:
			assert(false);
			break;
		}
		results.push_back(to_wstring(a) + sign + to_wstring(b) + L"=" + to_wstring(res) + L"\r\n");
		wstring all;
		for (int i = 0; i < results.size(); i++) all += results[i];
		SetWindowText(hResults, all.c_str());
	}
	catch (exception& e)
	{
		SetWindowText(hEditRes, L"Error!");
		WCHAR buf[100];
		mbstowcs(buf, e.what(), -1);
		MessageBox(NULL, (LPCWSTR)buf, L"Error", MB_OK);
	}
	CHAR buf[100];
	sprintf(buf, "%f", res);
	WCHAR buf1[100];
	mbstowcs(buf1, buf, -1);
	SetWindowText(hEditRes, buf1);
}

LRESULT CALLBACK Dialog(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	SendDlgItemMessage(hDlg, IDC_RESULTS, LB_RESETCONTENT, 0, 0);
	hInst = GetModuleHandle(NULL);
	HMENU hmenu;
	static OPENFILENAME ofn;
	static WCHAR szFileName[100];
	static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
		L"All files (*.*)\0*.*\0\0";
	static int success;
	switch (message)
	{
		case WM_INITDIALOG:

			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hDlg;
			ofn.lpstrFilter = szFilter;
			ofn.lpstrFile = szFileName;
			ofn.nMaxFile = sizeof(szFileName);
			ofn.lpstrDefExt = L"txt";
			hmenu = LoadMenu(hInst, MAKEINTRESOURCE(IDC_CALCULATOR));
			SetMenu(hDlg, hmenu);
			Init(hDlg);
			break;

		case WM_COMMAND:
			switch (LOWORD(wParam))
			{
			case ID_RB_PLUS:
				sign = '+';
				EnableWindow(hWhole, FALSE);
				break;
			case ID_RB_MINUS:
				sign = '-';
				EnableWindow(hWhole, FALSE);
				break;
			case ID_RB_MUL:
				sign = '*';
				EnableWindow(hWhole, FALSE);
				break;
			case ID_RB_DIV:
				sign = '/';
				EnableWindow(hWhole, TRUE);
				break;
			case ID_CALC:
			{
				Calculate(hDlg);
				break;
			}
			case ID_OPEN:
				wcscpy(szFileName, L"");
				success = GetOpenFileName(&ofn);
				if (success)
				{
					SendDlgItemMessage(hDlg, IDC_RESULTS, LB_RESETCONTENT, 0, 0);
					ifstream in(ofn.lpstrFile);
					results.clear();
					string data;
					wstring all;
					while (!in.eof())
					{
						getline(in, data, '\n');
						results.push_back(wstring(data.begin(), data.end()) + L"\r\n");
						all += results[results.size() - 1];
					}
					SetWindowText(hResults, all.c_str());
				}
				break;

			case ID_SAVE:
				wcscpy(szFileName, L"");
				success = GetOpenFileName(&ofn);
				if (success)
				{
					wofstream out(ofn.lpstrFile);
					for (int i = 0; i < results.size(); i++)
						out << results[i];
				}
				break;

			case IDM_EXIT:
				EndDialog(hDlg, 0);
				return TRUE;
				break;
			case IDOK:
				EndDialog(hDlg, 0);
				return TRUE;
				break;
			case WM_DESTROY:
				EndDialog(hDlg, 0);
				return TRUE;
				break;
			case IDM_ABOUT:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hDlg, About);
				break;
		}
		break;
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
		return (LRESULT)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (LRESULT)TRUE;
		}
		break;
	}
	return (LRESULT)FALSE;
}
