// Calculater.cpp: определяет точку входа для приложения.
//
#include "stdafx.h"
#include "Calculater.h"

#define MAX_LOADSTRING 100

HWND hEditA, hEditB, hEditRes, hButtonC,
hRBPlus, hRBMinus, hRBMul, hRBDiv, hWhole, hResults;
wchar_t sign;
static int ok;
vector<wstring> results;

void openFile(HWND&, OPENFILENAME&);
void saveFile(HWND&, OPENFILENAME&);
void сalculate(HWND&);

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

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
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

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON1));
    wcex.hCursor        = LoadCursor(nullptr, IDC_CROSS);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_CALCULATER);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_ICON1));

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

LRESULT CALLBACK Dialog(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	HMENU hMenu;
	static int success;
	static vector<string> list;

	static OPENFILENAME ofn;
	static WCHAR szFileName[100];
	static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
		L"All files (*.*)\0*.*\0\0";

	switch (message)
	{
		case WM_INITDIALOG:

			hEditA   = GetDlgItem(hDlg, ID_EDITA);
			hEditB   = GetDlgItem(hDlg, ID_EDITB);
			hEditRes = GetDlgItem(hDlg, ID_EDITRES);
			EnableWindow(hEditRes, FALSE); 

			hButtonC = GetDlgItem(hDlg, ID_CAL); 

			hRBPlus  = GetDlgItem(hDlg, ID_RB_PLUS);
			hRBMinus = GetDlgItem(hDlg, ID_RB_MINUS);
			hRBMul   = GetDlgItem(hDlg, ID_RB_MUL);
			hRBDiv   = GetDlgItem(hDlg, ID_RB_DIV);
			hWhole   = GetDlgItem(hDlg, IDC_WHOLE);
			hResults = GetDlgItem(hDlg, IDC_RESULTS);
			EnableWindow(hWhole, FALSE);
			
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hDlg;
			ofn.lpstrFilter = szFilter;
			ofn.lpstrFile = szFileName;
			ofn.nMaxFile = sizeof(szFileName);
			ofn.lpstrDefExt = L"txt";

			SendMessage(hRBPlus, BM_SETCHECK, (WPARAM)BST_CHECKED, 0);
			hMenu = LoadMenu(hInst, MAKEINTRESOURCE(IDC_CALCULATER));
			SetMenu(hDlg, hMenu);
			sign = '+';
			break;

		case WM_COMMAND:
			switch (LOWORD(wParam))
			{
				case ID_OPEN: 
					wcscpy(szFileName, L"");
					openFile(hDlg, ofn);
					break;

				case ID_SAVE:
					wcscpy(szFileName, L"");
					saveFile(hDlg, ofn);
					break;

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

				case ID_CAL:
					сalculate(hDlg);
					break;

				case IDM_ABOUT:
					DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hDlg, About);
					break;

				case IDM_EXIT:
					DestroyWindow(hDlg);
					break;

				case IDOK:
					EndDialog(hDlg, LOWORD(wParam));
					return (LRESULT)TRUE;

				case IDCANCEL:
					EndDialog(hDlg, LOWORD(wParam));
					return (LRESULT)TRUE;

				default:
					return DefWindowProc(hDlg, message, wParam, lParam);
			}
			case WM_PAINT:
			{
				PAINTSTRUCT ps;
				HDC hdc = BeginPaint(hDlg, &ps);
				// TODO: Добавьте сюда любой код прорисовки, использующий HDC...
				EndPaint(hDlg, &ps);
			}
			break;
			case WM_DESTROY:
				PostQuitMessage(0);
				break;
	}
	return 0;
}

void openFile(HWND& hDlg, OPENFILENAME& ofn)
{
	ok = GetOpenFileName(&ofn);
	if ( ok )
	{
		SendDlgItemMessage(hDlg, IDC_RESULTS, LB_RESETCONTENT, 0, 0);
		ifstream input(ofn.lpstrFile);
		results.clear();
		string data;
		wstring result;
		while (input)
		{
			getline(input, data, '\n');
			results.push_back(wstring(data.begin(), data.end()) + L"\r\n");
			result += results[ results.size() - 1 ];
		}
		input.close();
		SetWindowText(hResults, result.c_str());
	}
}

void saveFile(HWND& hDlg, OPENFILENAME& ofn)
{
	ok = GetOpenFileName(&ofn);
	if ( ok )
	{
		wofstream out(ofn.lpstrFile);
		for (int i = 0; i < results.size(); i++)
		{
			out << results[i];
		}
		out.close();
	}
}

void сalculate(HWND& hDlg)
{
		double a = ReadArg(hEditA);
		double b = ReadArg(hEditB);
		double res = 0;
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
				if (b == 0) {
					throw exception("Division by 0");
				}
				if (IsDlgButtonChecked(hDlg, IDC_WHOLE)) {
					res = int(a / b);
				}
				else {
					res = double(a / b);
				}
				break;
			default:
				assert(false);
				break;
			}

			results.push_back(to_wstring(a) + sign + to_wstring(b) + L"=" + to_wstring(res) + L"\r\n");
			wstring str;
			for (int i = 0; i < results.size(); i++)
			{
				str += results[i];
			}
			SetWindowText(hResults, str.c_str());
		}
		catch (exception e)
		{
			SetWindowText(hEditRes, L"Error");
			WCHAR buffer[100];
			mbstowcs(buffer, e.what(), -1);
			MessageBox(NULL, (LPCWSTR)buffer, L"Error", MB_OK);
		}

		CHAR buf[100];
		sprintf(buf, "%f", res);
		WCHAR buf1[100];
		mbstowcs(buf1, buf, -1);
		SetWindowText(hEditRes, buf1);
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
