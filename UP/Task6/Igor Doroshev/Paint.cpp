// Paint.cpp: определяет точку входа для приложения.
//
#pragma comment(lib,"ComCtl32.Lib")
#include "stdafx.h"
#include "Paint.h"

#define MAX_LOADSTRING 100

// Глобальные переменные:
static OPENFILENAME ofn;
static WCHAR szFileName[100];
static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0All Files (*.*)\0*.*\0\0";
static int x, y, parts[4], curWidth = 2, curType = PS_SOLID, success;
static HWND status;
static wstring text;
static COLORREF curColor = RGB(0, 0, 0), dColors[1];

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
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_PAINT);
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

struct line
{
	int x1, y1, x2, y2;
	int width;
	int type;
	COLORREF color;

};

static vector<line> lines;

void init(HWND hWnd, CHOOSECOLOR & cc)
{
	dColors[0] = RGB(228, 228, 228);
	cc.Flags = CC_RGBINIT | CC_FULLOPEN;
	cc.hInstance = NULL;
	cc.hwndOwner = hWnd;
	cc.lCustData = 0L;
	cc.lpCustColors = dColors;
	cc.lpfnHook = NULL;
	cc.lpTemplateName = (LPWSTR)NULL;
	cc.lStructSize = sizeof(cc);
	cc.rgbResult = RGB(0, 0, 0);
}

void InitStatus(HWND hWnd)
{
	status = CreateWindowEx(0, STATUSCLASSNAME, L"", WS_CHILD | WS_VISIBLE | SBARS_SIZEGRIP, 0, 0, 0, 0, hWnd, (HMENU)1000, hInst, NULL);
	RECT rect;
	GetWindowRect(hWnd, &rect);
	x = rect.right - rect.left;
	parts[0] = x / 4;
	parts[1] = x / 4 * 2;
	parts[2] = x / 4 * 3;
	parts[3] = x;
	SendMessage(status, SB_SETPARTS, 3, (LPARAM)&parts);
	text = L"Width: " + to_wstring(curWidth);
	SendMessage(status, SB_SETTEXT, 0, (LONG)text.c_str());
	text = L"Color: RED(" + to_wstring(GetRValue(curColor)) + L"), GREEN(" +
		to_wstring(GetGValue(curColor)) + L"), BLUE(" + to_wstring(GetBValue(curColor)) + L")";
	SendMessage(status, SB_SETTEXT, 1, (LONG)text.c_str());
	text = L"Style: SOLID";
	SendMessage(status, SB_SETTEXT, 2, (LONG)text.c_str());
}

void open(HWND hWnd)
{
	wcscpy(szFileName, L"");
	success = GetOpenFileName(&ofn);
	if (success)
	{
		//SendDlgItemMessage(hWnd, IDC_RESULTS, LB_RESETCONTENT, 0, 0);
		ifstream in(ofn.lpstrFile);
		lines.clear();
		line data;
		while (!in.eof())
		{
			in >> data.x1 >> data.y1 >> data.x2 >> data.y2 >> data.width >> data.type;
			int r, g, b;
			in >> r >> g >> b;
			data.color = RGB(r, g, b);
			lines.push_back(data);
		}
	}
}

void saveas(HWND hWnd)
{
	wcscpy(szFileName, L"");
	success = GetOpenFileName(&ofn);
	if (success)
	{
		ofstream out(ofn.lpstrFile);
		for (int i = 0; i < lines.size(); i++)
		{
			int r, g, b;
			r = GetRValue(lines[i].color);
			g = GetGValue(lines[i].color);
			b = GetBValue(lines[i].color);
			out << lines[i].x1 << " " << lines[i].y1 << " " << lines[i].x2 << " " << lines[i].y2 << " "
				<< lines[i].width << " " << lines[i].type << " " << r << " " << g << " " << b << endl;
		}
	}
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	CHOOSECOLOR cc;
	init(hWnd, cc);
    switch (message)
    {
	case WM_CREATE:
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;
		ofn.lpstrFilter = szFilter;
		ofn.lpstrFile = szFileName;
		ofn.nMaxFile = sizeof(szFileName);
		ofn.lpstrDefExt = L"txt";
		InitStatus(hWnd);
		break;
	case WM_SIZE:
		x = LOWORD(lParam);
		y = HIWORD(lParam);
		SetWindowPos(status, NULL, 0, 0, x, y, SWP_NOZORDER);
		parts[0] = x / 4;
		parts[1] = x / 4 * 2;
		parts[2] = x / 4 * 3;
		parts[3] = x;
		SendMessage(status, SB_SETPARTS, 3, (LPARAM)&parts);
		break;
	case WM_LBUTTONDOWN:
		line rline;
		rline.x1 = LOWORD(lParam);
		rline.y1 = HIWORD(lParam);
		rline.width = curWidth;
		rline.color = curColor;
		rline.type = curType;
		lines.push_back(rline);
		break;
	case WM_MOUSEMOVE:
		if (wParam == MK_LBUTTON)
		{
			lines[lines.size() - 1].x2 = LOWORD(lParam);
			lines[lines.size() - 1].y2 = HIWORD(lParam);
			InvalidateRect(hWnd, NULL, TRUE);
		}
		break;
	case WM_LBUTTONUP:
		lines[lines.size() - 1].x2 = LOWORD(lParam);
		lines[lines.size() - 1].y2 = HIWORD(lParam);
		InvalidateRect(hWnd, NULL, TRUE);
		break;
    case WM_COMMAND:
        {
			HMENU menu = GetMenu(hWnd);
            int wmId = LOWORD(wParam);
            // Разобрать выбор в меню:
            switch (wmId)
            {
			case ID_OPEN:
				open(hWnd);
				InvalidateRect(hWnd, NULL, TRUE);
				break;
			case ID_SAVEAS:
				saveas(hWnd);
				break;
			case ID_COLOR:
				if (ChooseColor(&cc))
				{
					curColor = (COLORREF)cc.rgbResult;
				}
				break;
			case ID_W1:
				curWidth = 1;
				break;
			case ID_W2:
				curWidth = 2;
				break;
			case ID_W4:
				curWidth = 4;
				break;
			case ID_W6:
				curWidth = 6;
				break;
			case ID_W8:
				curWidth = 8;
				break;
			case ID_W14:
				curWidth = 14;
				break;
			case ID_SOLID:
				curType = PS_SOLID;
				break;
			case ID_DASH:
				curType = PS_DASH;
				break;
			case ID_DOT:
				curType = PS_DOT;
				break;
			case ID_DASHDOT:
				curType = PS_DASHDOT;
				break;
			case ID_DASHDOTDOT:
				curType = PS_DASHDOTDOT;
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
			text = L"Width: " + to_wstring(curWidth);
			SendMessage(status, SB_SETTEXT, 0, (LONG)text.c_str());
			text = L"Color: RED(" + to_wstring(GetRValue(curColor)) + L"), GREEN(" +
				to_wstring(GetGValue(curColor)) + L"), BLUE(" + to_wstring(GetBValue(curColor)) + L")";
			SendMessage(status, SB_SETTEXT, 1, (LONG)text.c_str());
			text = L"Style: ";
			switch (curType)
			{
			case PS_SOLID:
				text = text + L"SOLID";
				break;
			case PS_DOT:
				text = text + L"DOT";
				break;
			case PS_DASH:
				text = text + L"DASH";
				break;
			case PS_DASHDOT:
				text = text + L"DASHDOT";
				break;
			case PS_DASHDOTDOT:
				text = text + L"DASHDOTDOT";
				break;
			}
			SendMessage(status, SB_SETTEXT, 2, (LONG)text.c_str());
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
			HPEN pen;
			LOGBRUSH logbrush;
			logbrush.lbStyle = BS_SOLID;
			for (int i = 0; i < lines.size(); i++)
			{
				logbrush.lbColor = lines[i].color;
				pen = ExtCreatePen(PS_GEOMETRIC | lines[i].type, lines[i].width, &logbrush, 0, NULL);
				SelectObject(hdc, pen);
				MoveToEx(hdc, lines[i].x1, lines[i].y1, NULL);
				LineTo(hdc, lines[i].x2, lines[i].y2);
				DeleteObject(pen);
			}
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
