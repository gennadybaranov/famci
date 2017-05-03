// PAINT.cpp: определяет точку входа для приложения.
//

#include "stdafx.h"
#include "PAINT.h"

#define MAX_LOADSTRING 100

// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK Style(HWND, UINT, WPARAM, LPARAM);
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
    LoadStringW(hInstance, IDC_PAINT, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PAINT));

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
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PAINT));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_PAINT);
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
struct Point
{
	int x;
	int y;
	int width;
	int style;
	COLORREF color;
	Point(int x, int y,int w ,int s, COLORREF c)
	{
		this->x = x;
		this->y = y;
		width = w;
		style = s;
		color = c;
	}
	void write(ofstream& f)
	{
		f << x << " " << y << " " << width << " " << style << " " << color << "\n";
	}
};
void show(HWND& hwndStatusB,COLORREF color,int width,int style)
{
	wstring	text = L"Color: (" + to_wstring(GetRValue(color)) + L";" +
		to_wstring(GetGValue(color)) + L";" + to_wstring(GetBValue(color)) + L")" +
		L" | Width: " + to_wstring(width) + L" | Style: ";
	switch (style)
	{
	case PS_SOLID:
		text += L"SOLID";
		break;
	case PS_DASH:
		text += L"DASH";
		break;
	case PS_DOT:
		text += L"DOT";
		break;
	case PS_DASHDOT:
		text += L"DASHDOT";
		break;
	case PS_DASHDOTDOT:
		text += L"DASHDOTDOT";
		break;
	}
	SendMessage(hwndStatusB, SB_SETTEXT, 0, (LONG)text.c_str());
}
static vector<vector<Point>> vlines;
static vector<Point> vline;
static HPEN hPen;
HWND hwndStatusB = NULL;
static COLORREF color = RGB(0, 0, 0);
static int style = PS_SOLID;
static int width = 1;
DWORD dColors[3];
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	static int xClient, yClient;
	HDC hdc;
	hdc = GetDC(hWnd);
	POINT point;
	static int xM, yM;
	static int xOld, yOld;
	RECT window;
	static bool toPaint = false;
	CHOOSECOLOR cc;
	cc.Flags = CC_RGBINIT | CC_FULLOPEN;
	cc.hInstance = NULL;
	cc.hwndOwner = hWnd;
	cc.lCustData = 0L;
	cc.lpCustColors = dColors;
	cc.lpfnHook = NULL;
	cc.lpTemplateName = (LPCWSTR)NULL;
	cc.lStructSize = sizeof(cc);
	cc.rgbResult = RGB(0, 0, 255);
	static bool success;
	static OPENFILENAME ofn;
	static WCHAR szFileName[100];
	static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
		L"All files (*.*)\0*.*\0\0";
	ofn.lStructSize = sizeof(OPENFILENAME);
	ofn.hwndOwner = hWnd;
	ofn.lpstrFilter = szFilter;
	ofn.lpstrFile = szFileName;
	ofn.nMaxFile = sizeof(szFileName);
	ofn.lpstrDefExt = L"txt";
	wstring text;
	int divide[3];
	switch (message)
    {
	case WM_SIZE:
		xClient = LOWORD(lParam);
		yClient = HIWORD(lParam);
		divide[0] = xClient / 8;
		divide[1] = xClient / 4;
		divide[2] = xClient/2;
		GetWindowRect(hWnd, &window);
		break;
	case WM_CREATE:
		hwndStatusB = CreateStatusWindow(WS_CHILD | WS_VISIBLE | SBARS_SIZEGRIP, L"", hWnd, 4000);
		show(hwndStatusB, color, width, style);
		break;
	case WM_LBUTTONDOWN:
		hdc = GetDC(hWnd);
		xM = LOWORD(lParam);
		yM = HIWORD(lParam);
		toPaint = true;
		hPen = CreatePen(style, width, color);
		vline.push_back(Point(xM,yM,width,style,color));
		break;
	case WM_LBUTTONUP:
		if (toPaint)
		{
			toPaint = false;
			xM = LOWORD(lParam);
			yM = HIWORD(lParam);
			vline.push_back(Point(xM, yM, width, style, color));
			vlines.push_back(vline);
			vline.clear();
			DeleteObject(hPen);
		}
		break;
	case WM_MOUSEMOVE:

		if (toPaint)
		{
			SelectObject(hdc, hPen);
			MoveToEx(hdc, xM, yM, NULL);
			xM = LOWORD(lParam);
			yM = HIWORD(lParam);
			LineTo(hdc, xM, yM);
			vline.push_back(Point(xM,yM,width,style,color));
		}
		break;
    case WM_PAINT:
        {
            hdc = BeginPaint(hWnd, &ps);
				for (int i = 0; i < vlines.size(); ++i)
				{
					hPen = CreatePen(vlines[i][0].style, vlines[i][0].width, vlines[i][0].color);
					SelectObject(hdc, hPen);
					MoveToEx(hdc, vlines[i][0].x, vlines[i][0].y, NULL);
					for (int j = 1; j < vlines[i].size(); ++j)
					{
						LineTo(hdc, vlines[i][j].x, vlines[i][j].y);
					}
					DeleteObject(hPen);
				}
				EndPaint(hWnd, &ps);
        }
        break;
	case WM_COMMAND:
	{
		int wmId = LOWORD(wParam);
		switch (wmId)
		{
		case ID_OPEN:
			wcscpy_s(szFileName, L"");
			success = GetOpenFileName(&ofn);
			if (success)
			{
				ifstream f(ofn.lpstrFile);
				vlines.clear();
				InvalidateRect(NULL, &window, true);
				int x, y, width,style;
				COLORREF color;
				string tmp;
				while (!f.eof())
				{
					getline(f, tmp);
					if (tmp != "END")
					{
						istringstream ist(tmp);
						ist >> x >> y >> width >> style >> color;
						vline.push_back(Point(x, y, width, style, color));
					}
					else
					{
						vlines.push_back(vline);
						vline.clear();
					}
				}
				f.close();
				MessageBox(hWnd, ofn.lpstrFile, L"File is opening...", MB_OK);
			}
			else
			{
				MessageBox(hWnd, L"File Open Error", L"Error", MB_ICONWARNING);
			}
			break;
		case ID_SAVE:
			wcscpy_s(szFileName, L"");
			success = GetSaveFileName(&ofn);
			if (success)
			{
				MessageBox(hWnd, ofn.lpstrFile, L"File is saving...", MB_OK);
				ofstream f(ofn.lpstrFile);
				for (int i = 0; i < vlines.size(); ++i)
				{
					for (int j = 0; j < vlines[i].size(); ++j)
					{
						vlines[i][j].write(f);
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
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
			case ID_COLOR:
				if (ChooseColor(&cc))
				{
					color = (COLORREF)cc.rgbResult;
					show(hwndStatusB, color, width, style);
				}
				break;
			case ID_STYLE:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_STYLE), hWnd, Style);
				break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
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
INT_PTR CALLBACK Style(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_W1:
			width = 1;
			show(hwndStatusB, color, width, style);
			break;
		case IDC_W2:
			width = 2;
			show(hwndStatusB, color, width, style);
			break;
		case IDC_W3:
			width = 4;
			show(hwndStatusB, color, width, style);

			break;
		case IDC_W4:
			width = 8;
			show(hwndStatusB, color, width, style);

			break;
		case IDC_W5:
			width = 10;
			show(hwndStatusB, color, width, style);

			break;
		case IDC_S1:
			style = PS_SOLID;
			show(hwndStatusB, color, width, style);

			break;
		case IDC_S2:
			style = PS_DASH;
			show(hwndStatusB, color, width, style);
			break;
		case IDC_S3:
			style = PS_DOT;
			show(hwndStatusB, color, width, style);

			break;
		case IDC_S4:
			style = PS_DASHDOT;
			show(hwndStatusB, color, width, style);

			break;
		case IDC_S5:
			style = PS_DASHDOTDOT;
			show(hwndStatusB, color, width, style);

			break;
		case IDOK:
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		case SCANCEL:
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		default:
			return DefWindowProc(hDlg, message, wParam, lParam);
		}
	}
	return (INT_PTR)FALSE;
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
