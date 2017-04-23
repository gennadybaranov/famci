// Paint.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Paint.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name


typedef struct {
	vector<POINT> points;
	COLORREF color;
	int type;
	int width;
} Line;

static Line * line;
vector<Line *> pic;

BOOL fDraw = FALSE;
HWND hWnd;
HINSTANCE hInstance;

static COLORREF lineColor;
static CHOOSECOLOR cc;
static COLORREF acrCustClr[16];

int type = 0;
double width = 1;

static HWND bar;


void RedrawLines(HDC hdc) {
	for (auto& line : pic) {
		HPEN hPen = CreatePen(line->type, line->width, line->color);
		SelectObject(hdc, hPen);
		Polyline(hdc, line->points.data(), line->points.size());
		DeleteObject(hPen);
	}
}

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_PAINT, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PAINT));

	MSG msg;

	// Main message loop:
	while (GetMessage(&msg, nullptr, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PAINT));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_PAINT);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // Store instance handle in our global variable

	HWND hWnd = CreateWindowW(szWindowClass, L"Anna", WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

	if (!hWnd)
	{
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

LRESULT CALLBACK DialogProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
	case WM_INITDIALOG:
	{
		HMENU hMenu = LoadMenu(hInst, MAKEINTRESOURCE(IDD_DIALOG1));
		SetMenu(hWnd, hMenu);
		break;
	}
	case WM_COMMAND:
	{
		if (LOWORD(wParam) == IDOK) {
			if (IsDlgButtonChecked(hWnd, IDC_SOLID)) {
				type = PS_SOLID;
			}
			if (IsDlgButtonChecked(hWnd, IDC_DASH)) {
				type = PS_DASH;
			}
			if (IsDlgButtonChecked(hWnd, IDC_DOT)) {
				type = PS_DOT;
			}
			if (IsDlgButtonChecked(hWnd, IDC_02)) {
				width = 1;
			}
			if (IsDlgButtonChecked(hWnd, IDC_04)) {
				width = 2;
			}
			if (IsDlgButtonChecked(hWnd, IDC_06)) {
				width = 3;
			}
			if (IsDlgButtonChecked(hWnd, IDC_08)) {
				width = 4;
			}
			if (IsDlgButtonChecked(hWnd, IDC_1)) {
				width = 5;
			}
			EndDialog(hWnd, 0);
		}
		break;
	}
	case WM_CLOSE:
	{
		EndDialog(hWnd, 0);
		break;
	}
	}
	return 0;
}
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{

	switch (message)
	{
	case WM_CREATE: {
		cc.lStructSize = sizeof(CHOOSECOLOR);
		cc.hwndOwner = hWnd;
		cc.Flags = CC_FULLOPEN | CC_RGBINIT;
		cc.rgbResult = RGB(130, 160, 150);
		cc.lpCustColors = acrCustClr;
		cc.lCustData = 0L;

		InitCommonControls();
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
			hInstance,                   // handle to application instance
			NULL);

		break;
	}
	case WM_SIZE: {
		SendMessage(bar, WM_SIZE, 0, 0);
		break;
	}
	case WM_COMMAND: {
		switch (LOWORD(wParam)) {
		case ID_EDIT_COLOR: {
			if (ChooseColor(&cc)) {
				lineColor = cc.rgbResult;
			}
			break;
		}
		case ID_EDIT_WIDTH:
		{
			DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, reinterpret_cast<DLGPROC>(DialogProc));
			break;
		}
		case ID_FILE_SAVE:
		{
			OPENFILENAME ofn = { 0 };
			WCHAR filename[1000];
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFile = filename;
			ofn.nMaxFile = 1000;
			ofn.lpstrFile[0] = '\0';
			GetSaveFileName(&ofn);

			wofstream out(ofn.lpstrFile);
			for (auto& line : pic) {
				for (auto& point : line->points) {
					out << point.x << " " << point.y << " ";
				}
				out << "\r\n" << line->type << " " << line->width << " " << line->color << "\r\n";
			}
			out.close();
			break;
		}
		case ID_FILE_OPEN: {
			OPENFILENAME ofn = { 0 };
			WCHAR filename[1000];
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFile = filename;
			ofn.nMaxFile = 1000;
			ofn.lpstrFile[0] = '\0';
			GetOpenFileName(&ofn);

			ifstream in(ofn.lpstrFile);
			pic.clear();
			RECT rect;
			GetWindowRect(hWnd, &rect);
			RedrawWindow(hWnd, &rect, NULL, RDW_INVALIDATE | RDW_ERASE);

			while (!in.eof()) {
				string str;
				int coordinate;
				getline(in, str, '\n');
				stringstream ss(str);
				Line* line = new Line();
				while (ss >> coordinate) {
					POINT point;
					point.x = coordinate;
					ss >> point.y;
					line->points.push_back(point);
				}
				getline(in, str, '\n');
				ss = stringstream(str);
				ss >> line->type;
				ss >> line->width;
				ss >> line->color;
				pic.push_back(line);
			}
			HDC hdc = GetDC(hWnd);
			RedrawLines(hdc);
			in.close();
			break;
		}
						   break;
		}
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hWnd, &ps);
		RedrawLines(hdc);
		EndPaint(hWnd, &ps);
		break;
	}
	case WM_LBUTTONDOWN:
	{
		fDraw = TRUE;
		line = new Line();
		line->width = width;
		line->type = type;
		line->color = lineColor;
		pic.push_back(line);
		line->points.push_back({ LOWORD(lParam), HIWORD(lParam) });
		wstringstream str;
		str << "Type: " << line->type << " Width: " << line->width;
		SetWindowText(bar, str.str().c_str());
		break;
	}
	case WM_LBUTTONUP:
		if (fDraw)
		{
			HDC hdc = GetDC(hWnd);
			line->color = lineColor;
			RedrawLines(hdc);
			line = NULL;
			ReleaseDC(hWnd, hdc);
		}
		fDraw = FALSE;
		break;
	case WM_MOUSEMOVE:
		if (fDraw)
		{
			HDC hdc = GetDC(hWnd);
			line->points.push_back({ LOWORD(lParam), HIWORD(lParam) });
			RedrawLines(hdc);
			ReleaseDC(hWnd, hdc);
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
