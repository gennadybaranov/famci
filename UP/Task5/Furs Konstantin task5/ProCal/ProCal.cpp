#include "stdafx.h"
#include "ProCal.h"

#define MAX_LOADSTRING 100

wchar_t operation;
vector<wstring> list;
void Calculate(HWND);
HWND del, zero;
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    DlgProc(HWND, UINT, WPARAM, LPARAM); //Объявление диалога
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	DialogBox(hInst, MAKEINTRESOURCE(IDD_DIALOG1), 0, DlgProc);
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
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PROCAL));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_PROCAL);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

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

double Read(HWND hwnd)
{
	WCHAR buf[100];
	GetWindowText(hwnd, buf, 99);
	double result = _wtof(buf);
	return result;
}

INT_PTR CALLBACK DlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	static OPENFILENAME ofn;
	wchar_t fn[1024];
	wchar_t fp[1024];
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		zero = GetDlgItem(hDlg, IDC_CHECK1);
		del = GetDlgItem(hDlg, IDC_EDIT3);
		EnableWindow(del, FALSE);
		EnableWindow(zero, FALSE);
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		int wmId = LOWORD(wParam);	
		switch (wmId)
		{
			case ID_32772:
				fn[0] = L'\0';
				ofn.lStructSize = sizeof(OPENFILENAMEW);
				ofn.hwndOwner = hDlg;
				ofn.lpstrFilter = L"Все файлы\0*.*\0\0";
				ofn.lpstrCustomFilter = NULL;
				ofn.nFilterIndex = 1;
				ofn.lpstrFile = fn;
				ofn.nMaxFile = 1024;
				ofn.lpstrFileTitle = NULL;
				ofn.lpstrInitialDir = NULL;
				ofn.lpstrTitle = NULL;
				ofn.Flags = OFN_EXPLORER;
				ofn.lpstrDefExt = NULL;
				ofn.FlagsEx = 0;
				if (GetOpenFileName(&ofn))
				{
					list.clear();
					string temp;
					ifstream in;
					in.open(fn);
					while (!in.eof()) {
						in >> temp;
						wstring wstr(temp.begin(), temp.end());
						list.push_back(wstr + L"\r\n");
					}
					wstring temp1;
					for (int i = 0; i < list.size(); i++)
					{
						temp1 += list[i];
					}
					SetWindowText(GetDlgItem(hDlg, IDC_EDIT4), temp1.c_str());
					in.close();
				}
				break;
			case ID_32773:
				fn[0] = L'\0';
				ofn.lStructSize = sizeof(OPENFILENAMEW);
				ofn.hwndOwner = hDlg;
				ofn.lpstrFilter = L"Все файлы\0*.*\0\0";
				ofn.lpstrCustomFilter = NULL;
				ofn.nFilterIndex = 1;
				ofn.lpstrFile = fn;
				ofn.nMaxFile = 1024;
				ofn.lpstrFileTitle = NULL;
				ofn.lpstrInitialDir = NULL;
				ofn.lpstrTitle = NULL;
				ofn.Flags = OFN_EXPLORER;
				ofn.lpstrDefExt = NULL;
				ofn.FlagsEx = 0;
				if (GetOpenFileName(&ofn)) {
					wofstream on(fn);
					for (int i = 0; i < list.size(); i++)
					{
						on << list[i];
					}
					on.close();
				}
				break;
			case IDC_RADIO1:
				operation = '+';
				EnableWindow(zero, FALSE);	
				break;
			case IDC_RADIO2:
				operation = '-';
				EnableWindow(zero, FALSE);
				break;
			case IDC_RADIO3:
				operation = '*';
				EnableWindow(zero, FALSE);
				break;
			case IDC_RADIO4:
				operation = '/';
				EnableWindow(zero, TRUE);
				break;
			case IDC_BUTTON1:
				if(operation != NULL)
				Calculate(hDlg);
				break;
			case IDM_ABOUT:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hDlg, About);
				break;
			case IDM_EXIT:
				DestroyWindow(hDlg);
				break;
			default:
				return DefWindowProc(hDlg, message, wParam, lParam);
		}
		break;
	}
	return (INT_PTR)FALSE;
}

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

void Calculate(HWND hDlg) {
	double A = Read(GetDlgItem(hDlg, firstARG));
	double B = Read(GetDlgItem(hDlg, secondARG));
	double result = 0;
	try 
	{
		switch (operation) {
		case '+':
			result = A + B;
			break;
		case '-':
			result = A - B;
			break;
		case '*':
			result = A * B;
			break;
		case '/':
			if (B == 0) 
				throw exception("Division by 0");
			if (IsDlgButtonChecked(hDlg, IDC_CHECK1))
				result = int(A / B);
			else
				result = double(A / B);
			break;
		default:
			break;
		}
		list.push_back(to_wstring(A) + operation + to_wstring(B) + L"=" + to_wstring(result) + L"\r\n");
		wstring temp;
		for (int i = 0; i < list.size(); i++)
		{
			temp += list[i];
		}
		SetWindowText(GetDlgItem(hDlg, IDC_EDIT4), temp.c_str());
	}
	catch (exception a) {
		WCHAR buffer[100];
		mbstowcs(buffer, a.what(), -1);
		MessageBox(NULL, (LPCWSTR)buffer, L"Error", MB_OK);
	}
	CHAR buf1[100];
	sprintf(buf1, "%f", result);
	WCHAR buf2[100];
	mbstowcs(buf2, buf1, -1);
	SetWindowText(GetDlgItem(hDlg, IDC_EDIT3), buf2);
}