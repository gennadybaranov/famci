// !!УП.cpp: определяет точку входа для приложения.
//

#include "stdafx.h"
#include "!!УП.h"

#define MAX_LOADSTRING 100

// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна
bool check = false;

// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK DlgProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, reinterpret_cast<DLGPROC>(DlgProc));
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

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_MY));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_MY);
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
LRESULT CALLBACK DlgProc(HWND hWndDlg, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	
	hInst = GetModuleHandle(NULL);
	WCHAR var1[20], var2[20];
	GetDlgItemText(hWndDlg, IDC_A, var1, 10);
	GetDlgItemText(hWndDlg, IDC_B, var2, 10);
	static string sign, str;
	double a, b;
	static double res;
	static int ires;
	a = _wtoi(var1);
	b = _wtoi(var2);
	static wchar_t* szText, *szRes;
	static vector<string> list;

	HMENU hmenu;
	static OPENFILENAME ofn;
	static WCHAR szFileName[100];
	static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
		L"All files (*.*)\0*.*\0\0";
	static int success;
	switch (Msg)
	{
	case WM_INITDIALOG:
	{
		
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWndDlg;
		ofn.lpstrFilter = szFilter;
		ofn.lpstrFile = szFileName;
		ofn.nMaxFile = sizeof(szFileName);
		ofn.lpstrDefExt = L"txt";
		hmenu = LoadMenu(hInst, MAKEINTRESOURCE(IDC_MY));
		SetMenu(hWndDlg, hmenu);
		return TRUE;
	}
	case WM_COMMAND:
		switch (wParam)
		{		
		case IDC_DIVINT:
			sign = "/";

			if (!b)
			{
				str = "Error! Dividing by zero.";
				szRes = new wchar_t[str.size()];
				szText = new wchar_t[str.size()];
				mbstowcs(szRes, str.c_str(), str.size());
				mbstowcs(szText, str.c_str(), str.size());
				szRes[str.size()] = '\0';
				szText[str.size()] = '\0';
				str.clear();
				break;
			}
			res = int(a / b);
			str = to_string(res);
			szRes = new wchar_t[str.size()];
			mbstowcs(szRes, str.c_str(), str.size());
			szRes[str.size()] = '\0';
			str.clear();
			str = to_string(a) + sign + to_string(b) + "=" + to_string(res);
			szText = new wchar_t[str.size()];
			mbstowcs(szText, str.c_str(), str.size());
			szText[str.size()] = '\0';
				break;
			case IDC_PLUS:
				 sign = "+";
				 res=a+b;
				 str = to_string(res);
				 szRes = new wchar_t[str.size()];
				 mbstowcs(szRes, str.c_str(), str.size());
				 szRes[str.size()] = '\0';
				 str.clear();
				 str = to_string(a) + sign + to_string(b) + "=" + to_string(res);
				 szText = new wchar_t[str.size()];
				 mbstowcs(szText, str.c_str(), str.size());
				 szText[str.size()] = '\0';
				break;
			case IDC_MINUS:
				sign = "-";
				res=a - b;
				str = to_string(res);
				szRes = new wchar_t[str.size()];
				mbstowcs(szRes, str.c_str(), str.size());
				szRes[str.size()] = '\0';
				str.clear();
				str = to_string(a) + sign + to_string(b) + "=" + to_string(res);
				szText = new wchar_t[str.size()];
				mbstowcs(szText, str.c_str(), str.size());
				szText[str.size()] = '\0';
				break;
			case IDC_MUL:
				sign = "*";
				res=a * b;
				str = to_string(res);
				szRes = new wchar_t[str.size()];
				mbstowcs(szRes, str.c_str(), str.size());
				szRes[str.size()] = '\0';
				str.clear();
				str = to_string(a) + sign + to_string(b) + "=" + to_string(res);
				szText = new wchar_t[str.size()];
				mbstowcs(szText, str.c_str(), str.size());
				szText[str.size()] = '\0';
				break;
			case IDC_DIV:
				sign = "/";
				
				if (!b)
				{
					str = "Error! Dividing by zero.";
					szRes = new wchar_t[str.size()];
					szText = new wchar_t[str.size()];
					mbstowcs(szRes, str.c_str(), str.size());
					mbstowcs(szText, str.c_str(), str.size());
					szRes[str.size()] = '\0';
					szText[str.size()] = '\0';
					str.clear();
					break;
				}
				res = a / b;
				str = to_string(res);
				szRes = new wchar_t[str.size()];
				mbstowcs(szRes, str.c_str(), str.size());
				szRes[str.size()] = '\0';
				str.clear();
				str = to_string(a) + sign + to_string(b) + "=" + to_string(res);
				szText = new wchar_t[str.size()];
				mbstowcs(szText, str.c_str(), str.size());
				szText[str.size()] = '\0';
				break;
			case IDC_CALC:
				list.push_back(str);
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_RESETCONTENT, 0, 0);
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)szRes);
				SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szText);
				break;
			case IDM_OPEN:
				wcscpy_s(szFileName, L"");
				success = GetOpenFileName(&ofn);
				if (success) 
				{
					SendDlgItemMessage(hWndDlg, IDC_LIST, LB_RESETCONTENT, 0, 0);
					ifstream f(ofn.lpstrFile);
					while (!f.eof())
					{
						string tmp;
						getline(f, tmp);
						wchar_t* szStr = new wchar_t[tmp.size()];
						mbstowcs(szStr, tmp.c_str(), tmp.size());
						szStr[tmp.size()] = '\0';
						SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szStr);
					}
					f.close();
					MessageBox(hWndDlg, ofn.lpstrFile, L"File is opening...", MB_OK);
				}
				else 
				{
					MessageBox(hWndDlg, L"File Save Error", L"Error", MB_ICONWARNING);
				}
				break;
			case IDM_SAVE:
				wcscpy_s(szFileName, L"");
				success = GetSaveFileName(&ofn);
				if (success) 
				{
					MessageBox(hWndDlg, ofn.lpstrFile, L"File is saving...", MB_OK);
					wchar_t szOut[1000];
					GetDlgItemText(hWndDlg, IDC_LIST, szOut, 1000);
					wstring ws(szOut);
					string s(ws.begin(), ws.end());
					ofstream o(ofn.lpstrFile);
					for (int i = 0; i < list.size(); ++i)
						o << list[i] << endl;
					o.close();
					
				}
				else 
				{
					MessageBox(hWndDlg , L"Save Open Error", L"Error", MB_ICONWARNING);
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
		

		break;
	}
	return FALSE;
}

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
