#include "stdafx.h"
#include "REVERSEPAINT.h"

#define MAX_LOADSTRING 100
#define MAXPOINTS 1000
#define ID_STATUS 200

HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    Dialog(HWND, UINT, WPARAM, LPARAM);

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
    LoadStringW(hInstance, IDC_REVERSEPAINT, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_REVERSEPAINT));

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

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_REVERSEPAINT));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_REVERSEPAINT);
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

struct Line {
	POINT points[MAXPOINTS];
	int count;
	int width;
	int style;
	COLORREF col;
};

int Width = 1;
int Style = 0;
static HWND bar;
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	static HWND hStatus; //идентификатор окна строки состояния
	static OPENFILENAME ofn;
	wchar_t fn[1024];
	static CHOOSECOLOR cr = { 0 };
	static COLORREF acrCustClr[16];
	static COLORREF ref = RGB(0, 0, 0); //this
	static HDC hdc;
	static Line oneLine;
	static vector<Line> myVector;
	static int number = 0 ;
    switch (message)
    {
	case WM_CREATE:

		cr.lStructSize = sizeof(CHOOSECOLOR);
		cr.hwndOwner = hWnd;
		cr.Flags = CC_FULLOPEN | CC_RGBINIT;
		cr.rgbResult = RGB(0, 0, 0);
		cr.lpCustColors = acrCustClr;
		cr.lCustData = 0L;
		
		RECT rect;
		GetWindowRect(hWnd, &rect);
		bar = CreateWindowEx(0,                       // no extended styles
			STATUSCLASSNAME,         // name of status bar class
			(PCTSTR)NULL,           // no text when first created
			SBARS_SIZEGRIP |         // includes a sizing grip
			WS_CHILD | WS_VISIBLE,   // creates a visible child window
			0, 0, 0, 0,              // ignores size and position
			hWnd,              // handle to parent window
			(HMENU)1,       // child window identifier
			hInst,                   // handle to application instance
			NULL);

		break;
	case WM_SIZE: {
		SendMessage(bar, WM_SIZE, 0, 0);
		InvalidateRect(hWnd, NULL, TRUE);
		break;
	}
	case WM_LBUTTONDOWN:
	{
		oneLine.count = 0;
		oneLine.style = Style;
		oneLine.width = Width;
		oneLine.col = ref;
		oneLine.points[oneLine.count].x = LOWORD(lParam);
		oneLine.points[oneLine.count].y = HIWORD(lParam);
		oneLine.count++;
		wstringstream str;
		str << "Type: " << Style << " Width: " << Width;
		SetWindowText(bar, str.str().c_str());
		break;
	}
	case WM_MOUSEMOVE:
		if (wParam && MK_LBUTTON && oneLine.count < MAXPOINTS)
		{
			oneLine.points[oneLine.count].x = LOWORD(lParam);
			oneLine.points[oneLine.count].y = HIWORD(lParam);
			oneLine.count++;
			hdc = GetDC(hWnd);
			ReleaseDC(hWnd, hdc);
			InvalidateRect(hWnd, NULL, FALSE);
		}
		break;
	case WM_LBUTTONUP:
		number++;
		myVector.push_back(oneLine);
		break;
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Разобрать выбор в меню:
            switch (wmId)
            {
			case ID_32772:
				if (ChooseColor(&cr)) 
				{
					ref = cr.rgbResult;
				}
				break;
			case ID_32773:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_DIALOG1), 0, Dialog);
				break;
			case ID_32774:
				fn[0] = L'\0';
				ofn.lStructSize = sizeof(OPENFILENAMEW);
				ofn.hwndOwner = hWnd;
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
					ofstream of(fn);
					int temp1, temp2, temp3;
					for (int i = 0; i < number; i++)
					{
						temp1 = GetRValue(myVector[i].col);
						temp2 = GetGValue(myVector[i].col);
						temp3 = GetBValue(myVector[i].col);
						of << "NEWLINE\n";
						of << myVector[i].style << " " << myVector[i].width  << " " << temp1 << " " << temp2 << " " << temp3 << "\n";
						for (int j = 0; j < myVector[i].count - 1; j++)
						{
							of << myVector[i].points[j].x << ";" << myVector[i].points[j].y << ";";
							if (j != myVector[i].count -2)
								of << "\n";
						}
						if (i != number - 1)
							of << "\n";
					}
					of.close();
				}
				break;
			case ID_32775:
				fn[0] = L'\0';
				ofn.lStructSize = sizeof(OPENFILENAMEW);
				ofn.hwndOwner = hWnd;
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
					myVector.clear();
					int tempCount, tempNumber = 0;
					int tempStyle, tempWidth, tempcol1, tempcol2, tempcol3;
					string temp;
					Line tempLine;
					COLORREF tempColor;
					ifstream in;
					in.open(fn);
					while (!in.eof()) {
						in >> temp;
						if (temp == "NEWLINE")
						{
							if (tempNumber != 0)
							{
								tempLine.count = tempCount;
								myVector.push_back(tempLine);
							}
							in >> tempStyle >> tempWidth >> tempcol1 >> tempcol2 >>  tempcol3;
							tempLine.style = tempStyle;
							tempLine.width = tempWidth;
							tempColor = RGB(tempcol1, tempcol2, tempcol3);
							tempLine.col = tempColor;
							tempCount = 0;
							tempNumber++;
						}
						else
						{
							istringstream str(temp);
							string Xtemp, Ytemp;
							getline(str, Xtemp, ';');
							getline(str, Ytemp, ';');
							tempLine.points[tempCount].x = stoi(Xtemp);
							tempLine.points[tempCount].y = stoi(Ytemp);
							tempCount++;
						}
					}
					tempLine.count = tempCount;
					myVector.push_back(tempLine);
					number = tempNumber;
					in.close();
					InvalidateRect(hWnd, NULL, TRUE);
				}
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
        }
        break;
    case WM_PAINT:
        {
			SetWindowText(hWnd, L"FURS");
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
			for (int i = 0; i < number; i++)
			{
				for (int j = 0; j < myVector[i].count -1 ; j++)
				{
					HPEN pen = CreatePen(myVector[i].style, myVector[i].width, myVector[i].col);
					SelectObject(hdc, pen);
					MoveToEx(hdc, myVector[i].points[j].x, myVector[i].points[j].y, 0);
					LineTo(hdc, myVector[i].points[j + 1].x, myVector[i].points[j + 1].y);
					DeleteObject(pen);
				}
			}
			for (int j = 0; j < oneLine.count - 1; j++)
			{
				HPEN pen = CreatePen(oneLine.style, oneLine.width, oneLine.col);
				SelectObject(hdc, pen);
				MoveToEx(hdc, oneLine.points[j].x, oneLine.points[j].y, 0);
				LineTo(hdc, oneLine.points[j + 1].x, oneLine.points[j + 1].y);
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

INT_PTR CALLBACK Dialog(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
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
		if (LOWORD(wParam) == IDC_RADIO1)
		{
			Width = 1;
			break;
		}
		if (LOWORD(wParam) == IDC_RADIO2)
		{
			Width = 2;
			break;
		}
		if (LOWORD(wParam) == IDC_RADIO3)
		{
			Width = 3;
			break;
		}
		if (LOWORD(wParam) == IDC_RADIO4)
		{
			Width = 5;
			break;
		}
		if (LOWORD(wParam) == IDC_RADIO5)
		{
			Style = PS_SOLID;
			break;
		}
		if (LOWORD(wParam) == IDC_RADIO6)
		{
			Style = PS_NULL;
			break;
		}
		if (LOWORD(wParam) == IDC_RADIO7)
		{
			Style = PS_DOT;
			break;
		}
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
