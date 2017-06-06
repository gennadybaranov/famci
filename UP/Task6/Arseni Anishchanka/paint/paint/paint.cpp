// paint.cpp: определяет точку входа для приложения.
//

#include "stdafx.h"
#include "paint.h"

#define MAX_LOADSTRING 100
using namespace std;
// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна
//начальные данные о линии
int width = 1;
int style = IDC_SOLID;
COLORREF color = RGB(0, 0, 0);
int tempstyle = 0, tempwidth = 0;
//начальные данные о линии
struct Point;
struct Curves;
vector<Point> curve;
vector<Curves> curves;
vector<Point>::iterator it;
static CHOOSECOLOR cc;
static COLORREF acrCustClr[16];
int i;
static char szFilter[] = "Text Files (*.TXT)\0*.txt\0 ";
static OPENFILENAME ofn;
static char szFile[MAX_PATH];

struct Point
{
	int x, y;
	Point(int x0=0, int y0=0) : x(x0), y(y0) {}
};

int penst(int num)
{
	switch (num)
	{
	case IDC_DASH:
		return PS_DASH;
		break;
	case IDC_DOT:
		return PS_DOT;
		break;
	case IDC_DASHDOT:
		return PS_DASHDOT;
		break;
	case IDC_DASHDOTDOT:
		return PS_DASHDOTDOT;
		break;
	default:
		return PS_SOLID;
		break;
	}
}

int penstrev(int num)
{
	switch (num)
	{
	case PS_DASH:
		return IDC_DASH;
		break;
	case PS_DOT:
		return IDC_DOT;
		break;
	case PS_DASHDOT:
		return IDC_DASHDOT;
		break;
	case PS_DASHDOTDOT:
		return IDC_DASHDOTDOT;
		break;
	default:
		return IDC_SOLID;
		break;
	}
}

struct Curves
{
	vector<Point> curve;
	COLORREF color;
	int style;
	int width;
	Curves(vector<Point> curve0, COLORREF color0, int style0, int width0)
	{
		curve = curve0;
		color = color0;
		switch (style0)
		{
		case IDC_SOLID:
			style = PS_SOLID;
			if (width0 > 0)
				width = width0;
			else
				width = 1;
			break;
		case IDC_DASH:
			style = PS_DASH;
			width = 1;
			break;
		case IDC_DOT:
			style = PS_DOT;
			width = 1;
			break;
		case IDC_DASHDOT:
			style = PS_DASHDOT;
			width = 1;
			break;
		case IDC_DASHDOTDOT:
			style = PS_DASHDOTDOT;
			width = 1;
			break;
		default:
			style = PS_SOLID;
			if (width0 > 0)
				width = width0;
			else
				width = 1;
			break;
		}
	}
};

void filesave(ofstream &f, vector<Curves> v)
{
	for (int i = 0; i < v.size(); i++)
	{
		int R, G, B;
		int n = v[i].curve.size();
		R = GetRValue(v[i].color); G = GetGValue(v[i].color); B = GetBValue(v[i].color);
		f << R << endl << G << endl << B << endl << v[i].style << endl << v[i].width << endl << n << endl;
		for (int j = 0; j < n; j++)
		{
			if (j == n - 1)
				f << v[i].curve[j].x << endl << v[i].curve[j].y;
			else
				f << v[i].curve[j].x << endl << v[i].curve[j].y << endl;
		}
		if (i != v.size() - 1)
			f << endl;
	}
}

void readfile(ifstream &f, vector<Curves> &v)
{
	string temp;
	int width0, style0, size0, R, G, B;
	COLORREF color0;
	vector<Point> curve0;
	Point A;
	while (f)
	{
		f >> temp; R = stoi(temp);
		f >> temp; G = stoi(temp);
		f >> temp; B = stoi(temp);
		color0 = RGB(R, G, B);
		f >> temp; style0 = penstrev(stoi(temp));
		f >> temp; width0 = stoi(temp);
		f >> temp; size0 = stoi(temp);
		for (int i = 0; i < size0; i++)
		{
			f >> temp;
			A.x = stoi(temp);
			f >> temp;
			A.y = stoi(temp);
			curve0.push_back(A);
		}
		Curves Ob(curve0, color0, style0, width0);
		v.push_back(Ob);
		curve0.clear();
	}
	v.erase(v.end() - 1);
}

// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK StyleWidth(HWND, UINT, WPARAM, LPARAM);

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
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	static HDC hDC;
	HDC hdc;
	PAINTSTRUCT ps;
	static bool down = false;
	int x, y;
	HPEN pen = 0;
	switch (message)
    {
	case WM_CREATE:
		hDC = GetDC(hWnd);
		cc.lStructSize = sizeof(CHOOSECOLOR);
		cc.hwndOwner = hWnd;
		cc.Flags = CC_FULLOPEN | CC_RGBINIT;
		cc.rgbResult = RGB(255, 255, 255);
		cc.lpCustColors = acrCustClr;
		cc.lCustData = 0L;
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;
		ofn.lpstrFilter = szFilter;
		ofn.lpstrFile = szFile;
		ofn.nMaxFile = sizeof(szFile);
		ofn.lpstrDefExt = "txt";
		break;
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
			case ID_STWID:
				DialogBox(0, MAKEINTRESOURCE(IDD_WIDST), hWnd, StyleWidth);
				break;
			case ID_COLOR:
				if (ChooseColor(&cc))
					color = cc.rgbResult;
				break;
			case ID_CLEAR:
				curves.clear();
				InvalidateRect(hWnd, 0, true);
				break;
			case ID_SAVE:
			{
				strcpy(szFile, "");
				bool success = GetSaveFileName(&ofn);
				if (success)
				{
					MessageBox(hWnd, ofn.lpstrFile, "Сохраняется файл:", MB_OK);
					ofstream f(ofn.lpstrFile);
					filesave(f, curves);
				}
				else
					MessageBox(hWnd, "Get Open File Name", "Отказ от выбора или ошибка", MB_ICONWARNING);
			} break;
			case ID_OPEN:
			{
				strcpy(szFile, "");
				bool success = GetOpenFileName(&ofn);
				if (success)
				{
					MessageBox(hWnd, ofn.lpstrFile, "Открывается файл:", MB_OK);
					ifstream f(ofn.lpstrFile);
					readfile(f, curves);
					InvalidateRect(hWnd, 0, TRUE);
				}
				else
					MessageBox(hWnd, "Get Open File Name", "Отказ от выбора или ошибка", MB_ICONWARNING);
			} break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
	case WM_LBUTTONDOWN:
	{
		down = true;
		x = LOWORD(lParam);
		y = HIWORD(lParam);
		MoveToEx(hDC, x, y, 0);
		curve.push_back(Point(x, y));
		pen = CreatePen(penst(style), width, color);
		SelectObject(hDC, pen);
	} break;
	case WM_LBUTTONUP:
	{
		down = false;
		Curves temp(curve, color, style, width);
		curves.push_back(temp);
		curve.clear();
		DeleteObject(pen);
	} break;
	case WM_MOUSEMOVE:
	{
		if (down)
		{
			x = LOWORD(lParam);
			y = HIWORD(lParam);
			LineTo(hDC, x, y);
			curve.push_back(Point(x, y));
		}
	} break;
	case WM_PAINT:
	{
		hdc = BeginPaint(hWnd, &ps);
		for (i = 0; i < curves.size(); ++i)
		{
			it = curves[i].curve.begin();
			HPEN pen = CreatePen(curves[i].style, curves[i].width, curves[i].color);
			SelectObject(hdc, pen);
			MoveToEx(hdc, it->x, it->y, 0);
			for (it + 1; it != curves[i].curve.end(); ++it)
				LineTo(hdc, it->x, it->y);
			DeleteObject(pen);
		}
		EndPaint(hWnd, &ps);
	} break;
    case WM_DESTROY:
		DeleteObject(SelectObject(hDC, GetStockObject(BLACK_PEN)));
		ReleaseDC(hWnd, hDC);
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

INT_PTR CALLBACK StyleWidth(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	HWND edit = GetDlgItem(hDlg, IDC_WIDTH);
	switch (message)
	{
	case WM_INITDIALOG:
		CheckRadioButton(hDlg, IDC_SOLID, IDC_DASHDOTDOT, style);
		if (style!=IDC_SOLID) Edit_SetReadOnly(edit, true);
		SetDlgItemText(hDlg, IDC_WIDTH, to_string(width).c_str());
		tempstyle = style; tempwidth = width;
		return (INT_PTR)TRUE;
		break;
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_SOLID:
			style = IDC_SOLID;
			Edit_SetReadOnly(edit, false);
			SetDlgItemText(hDlg, IDC_WIDTH, to_string(tempwidth).c_str());
			break;
		case IDC_DASH:
			style = IDC_DASH;
			Edit_SetReadOnly(edit, true);
			SetDlgItemText(hDlg, IDC_WIDTH, "1");
			width = 1;
			break;
		case IDC_DOT:
			style = IDC_DOT;
			Edit_SetReadOnly(edit, true);
			SetDlgItemText(hDlg, IDC_WIDTH, "1");
			width = 1;
			break;
		case IDC_DASHDOT:
			style = IDC_DASHDOT;
			Edit_SetReadOnly(edit, true);
			SetDlgItemText(hDlg, IDC_WIDTH, "1");
			width = 1;
			break;
		case IDC_DASHDOTDOT:
			style = IDC_DASHDOTDOT;
			Edit_SetReadOnly(edit, true);
			SetDlgItemText(hDlg, IDC_WIDTH, "1");
			width = 1;
			break;
		case IDOK:
		{
			char s[60];
			GetDlgItemText(hDlg, IDC_WIDTH, s, 19);
			width = atoi(s);
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		} break;
		case IDCANCEL:
		{
			style = tempstyle;
			width = tempwidth;
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		} break;
		}
		break;
	}
	
	return (INT_PTR)FALSE;
}