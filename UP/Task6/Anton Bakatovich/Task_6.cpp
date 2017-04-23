#include "stdafx.h"
#include "Task_6.h"
#pragma comment(lib,"ComCtl32.Lib")
#define MAX_LOADSTRING 100

HINSTANCE hInst;                                
WCHAR szTitle[MAX_LOADSTRING];                 
WCHAR szWindowClass[MAX_LOADSTRING]; 
COLORREF color = RGB(0, 0, 0);
DWORD dColors[3];
static int widht = 2;
static int style = PS_SOLID;
static HWND hwndSb;

void changeWidht(HWND& hwndSb, int widht);
void changeStyle(HWND& hwndSb, int style);
void changeColor(HWND& hwndSb, COLORREF color);

struct Point
{
	Point(int x, int y, int widht, int style, COLORREF color)
	{
		this->x = x;
		this->y = y;
		this->widht = widht;
		this->style = style;
		this->color = color;
		logbrush.lbStyle = PS_SOLID;
		logbrush.lbColor = color;
		logbrush.lbHatch = NULL;
	}
	LOGBRUSH logbrush;
	int x;
	int y;
	int widht;
	int style;
	COLORREF color;
};


ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    FntSet(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_TASK_6, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_TASK_6));

    MSG msg;

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

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_TASK_6));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_TASK_6);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance;

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
	static HDC hdc;
	static HPEN hPen;
	static LOGBRUSH logbrush;
	static int xCenter;
	static int yCenter;
	static int xClient;
	static int yClient;
	static int x = 0;
	static int y = 0;
	static bool click = false;

	static vector<Point> curve;
	static vector<vector<Point> > curves;
	vector<Point>::iterator it;

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
	int parts[3];

    switch (message)
    {
	case WM_CREATE:
	{
		hdc = GetDC( hWnd );
		hwndSb = CreateStatusWindow(WS_CHILD | WS_VISIBLE | SBARS_SIZEGRIP, L"", hWnd, 4000);
		int factor = LOWORD(lParam) / 100;
		parts[0] = factor / 3 ;
		parts[1] = factor / 1.5;
		parts[2] = factor;
		SendMessage(hwndSb, SB_SETPARTS, 3, (LPARAM)&parts);
		changeWidht(hwndSb, widht);
		changeStyle(hwndSb, style);
		changeColor(hwndSb, color);
		break;
	}
	case WM_LBUTTONDOWN:
		logbrush.lbStyle = PS_SOLID;
		logbrush.lbColor = color;
		logbrush.lbHatch = NULL;
		hPen = ExtCreatePen(PS_GEOMETRIC | style, widht, &logbrush, NULL, NULL);
		SelectObject(hdc, hPen);
		click = true;
		x = LOWORD( lParam );
		y = HIWORD( lParam );
		curve.push_back(Point(x, y, widht, style, color));
		break;
	case WM_LBUTTONUP:
		if ( click )
		{
			MoveToEx( hdc, x, y, NULL );
			LineTo( hdc, LOWORD( lParam ), HIWORD( lParam ) );
			click = false;
			curves.push_back(curve);
			curve.clear();
		}
		break;
	case WM_MOUSEMOVE:
		if ( click )
		{
			MoveToEx( hdc, x, y, NULL );
			x = LOWORD( lParam );
			y = HIWORD( lParam );
			LineTo( hdc, x, y );
			curve.push_back( Point(x, y, widht, style, color) );
		}
		break;
	case WM_SIZE:
		SetWindowPos(hwndSb, NULL, 0, 0, LOWORD(lParam), HIWORD(lParam), SWP_NOZORDER);
		xCenter = ( xClient = LOWORD( lParam ) ) / 2;
		yCenter = ( yClient = HIWORD( lParam ) ) / 2;
		parts[0] = xCenter / 4;
		parts[1] = xCenter / 2;
		parts[2] = xCenter;
		SendMessage(hwndSb, SB_SETPARTS, 3, (LPARAM)&parts);
		break;
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
			switch (wmId)
			{
			case ID_SETTINGS:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_SET), hWnd, FntSet);
				break;
			case IDM_ABOUT:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
				break;
			case IDM_COLOUR:
				if (ChooseColor(&cc))
				{
					color = (COLORREF)cc.rgbResult;
					changeColor(hwndSb, color);
				}
				break;
			case ID_SAVE:
			{
				static WCHAR szFileName[100];
				static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
					L"All files (*.*)\0*.*\0\0";
				OPENFILENAME ofn = { 0 };
				ofn.lStructSize = sizeof(OPENFILENAME);
				ofn.hwndOwner = hWnd;
				ofn.lpstrFilter = szFilter;
				ofn.lpstrFile = szFileName;
				ofn.nMaxFile = sizeof(szFileName);
				ofn.lpstrDefExt = L"txt";
				GetSaveFileName(&ofn);

				wofstream out(ofn.lpstrFile);
				for (int i = 0; i < curves.size(); ++i)
				{
					for (it = curves[i].begin(); it != curves[i].end(); ++it)
					{
						out << it->x << " " << it->y << " " << it->widht << " " << it->style << " " << it->color << "\r\n";
					}
				}
				out.close();
				break;
			}
			case ID_OPEN:
			{
				static WCHAR szFileName[100];
				static WCHAR szFilter[] = L"Text Files (*.txt)\0*.txt\0"\
					L"All files (*.*)\0*.*\0\0";
				OPENFILENAME ofn = { 0 };
				ofn.lStructSize = sizeof(OPENFILENAME);
				ofn.hwndOwner = hWnd;
				ofn.lpstrFilter = szFilter;
				ofn.lpstrFile = szFileName;
				ofn.nMaxFile = sizeof(szFileName);
				ofn.lpstrDefExt = L"txt";
				GetSaveFileName(&ofn);

				ifstream in(ofn.lpstrFile);
				RECT rect;
				curve.clear();
				curves.clear();
				GetWindowRect(hWnd, &rect);
				RedrawWindow(hWnd, &rect, NULL, RDW_INVALIDATE | RDW_ERASE);

				while (!in.eof())
				{
					int x;
					int y;
					int widht;
					int style;
					int color;
					in >> x >> y >> widht >> style >> color;
					curve.push_back(Point(x, y, widht, style, color));
				}
				curves.push_back(curve);
				InvalidateRect(hWnd, NULL, TRUE);
				in.close();
				break;
			}
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
			for (int i = 0; i < curves.size(); ++i)
			{
				it = curves[i].begin();
				hPen = ExtCreatePen(PS_GEOMETRIC | it -> style, it -> widht , &(it -> logbrush), NULL, NULL);
				MoveToEx(hdc, it -> x, it -> y, NULL);
				for (it + 1; it != curves[i].end(); ++it)
				{
					SelectObject(hdc, hPen);
					LineTo(hdc, it -> x, it -> y);
					DeleteObject(hPen);
				}
			}
            EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
		DeleteObject(hPen);
		ReleaseDC(hWnd, hdc);
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

INT_PTR CALLBACK FntSet(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{	
		case ID_SIZE_1:
			widht = 1;
			changeWidht(hwndSb, widht);
			break;
		case IDC_SIZE_2:
			widht = 2;
			changeWidht(hwndSb, widht);
			break;
		case IDC_SIZE_3:
			widht = 3;
			changeWidht(hwndSb, widht);
			break;
		case IDC_SIZE_4:
			widht = 4;
			changeWidht(hwndSb, widht);
			break;
		case IDC_SIZE_5:
			widht = 5;
			changeWidht(hwndSb, widht);
			break;
		case ID_SOLID:
			style = PS_SOLID;
			changeStyle(hwndSb, PS_SOLID);
			break;
		case ID_DASH:
			style = PS_DASH;
			changeStyle(hwndSb, PS_DASH);
			break;
		case ID_DOT:
			style = PS_DOT;
			changeStyle(hwndSb, PS_DOT);
			break;
		case ID_DASH_DOT:
			style = PS_DASHDOT;
			changeStyle(hwndSb, PS_DASHDOT);
			break;
		case ID_DASH_DOT_DOT:
			style = PS_DASHDOTDOT;
			changeStyle(hwndSb, PS_DASHDOTDOT);
			break;
		case IDOK:
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		case IDD_SET:
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		case WM_DESTROY:
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		default:
			return DefWindowProc(hDlg, message, wParam, lParam);
		}
	}
	return (INT_PTR)FALSE;
}

void changeWidht(HWND& hwndSb, int widht)
{
	wstring text;
	text = L"WIDHT: " + to_wstring(widht);
	SendMessage(hwndSb, SB_SETTEXT, 0, (LONG)text.c_str());
}

void changeStyle(HWND& hwndSb, int style)
{
	wstring text;
	text = L"STYLE: ";
	switch (style)
	{
	case PS_SOLID:      text += L"SOLID";      break;
	case PS_DASH:       text += L"DASH";       break;
	case PS_DOT:        text += L"DOT";        break;
	case PS_DASHDOT:    text += L"DASHDOT";    break;
	case PS_DASHDOTDOT: text += L"DASHDOTDOT"; break;
	}
	SendMessage(hwndSb, SB_SETTEXT, 1, (LONG)text.c_str());
}

void changeColor(HWND& hwndSb, COLORREF color)
{
	wstring text;
	text = L"Color: RED(" + to_wstring(GetRValue(color)) + L"), GREEN(" +
		to_wstring(GetGValue(color)) + L"), BLUE(" + to_wstring(GetBValue(color)) + L")";
	SendMessage(hwndSb, SB_SETTEXT, 2, (LONG)text.c_str());
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
