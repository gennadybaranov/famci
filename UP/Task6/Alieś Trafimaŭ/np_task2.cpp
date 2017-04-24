#include "stdafx.h"
#include "np_task2.h"

#include <vector>
#include <string>
#include <CommCtrl.h>
#include <commdlg.h>
#include <fstream>

using namespace std;

#define MAX_LOADSTRING 100
#define ID_STATUS 200
HWND hStatus;

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

class Line {
public:
	vector<pair<int, int>> c;
	int w;
	int style;
	COLORREF color;
	Line() {
		w = 8;
		style = PS_SOLID;
		color = RGB(100, 200, 200);
	}
};


void DrawLine(HDC hdc, Line& line);

vector<Line> lines;

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	PenStyleDlgProc(HWND, UINT, WPARAM, LPARAM);
void UpdatePenStatus(HWND hStatus, Line& line);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_NP_TASK2, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_NP_TASK2));

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
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_NP_TASK2));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_NP_TASK2);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // Store instance handle in our global variable

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

void TrackMouse(HWND hwnd)
{
	TRACKMOUSEEVENT tme;
	tme.cbSize = sizeof(TRACKMOUSEEVENT);
	tme.dwFlags = TME_HOVER | TME_LEAVE; //Type of events to track & trigger.
	tme.dwHoverTime = 1; //How long the mouse has to be in the window to trigger a hover event.
	tme.hwndTrack = hwnd;
	TrackMouseEvent(&tme);
}

Line curLine;

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	static int window_width;
	static bool clicked, trackMainWindow, line_pushed, ofnSuccess;
	static int x, y;

	static CHOOSECOLOR cc;
	static	COLORREF acrCustClr[16];

	static WCHAR szFile[MAX_PATH];
	static OPENFILENAME ofn;
	static WCHAR szFilter[] = L"Text Files (*.TXT)\0*.txt\0" \
		"All Files (*.*)\0*.*\0\0";

	switch (message)
	{
		case WM_CREATE:
		{
			clicked = false;
			trackMainWindow = false;

			SetWindowText(hWnd, L"Paint. Made by Ales");

			// cc
			cc.lStructSize = sizeof(CHOOSECOLOR);
			cc.hwndOwner = hWnd;
			cc.Flags = CC_FULLOPEN | CC_RGBINIT;
			cc.rgbResult = RGB(100, 200, 200);
			cc.lpCustColors = acrCustClr;
			cc.lCustData = 0L; // equal to NULL?

			// ofn
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFilter = szFilter;
			ofn.lpstrFile = szFile;
			ofn.nMaxFile = sizeof(szFile);
			ofn.lpstrDefExt = L".txt";

			// status bar		
			hStatus = CreateWindow(STATUSCLASSNAME, NULL, WS_CHILD | WS_VISIBLE, 0, 0, 0, 0,
				hWnd, (HMENU)ID_STATUS, hInst, NULL);
			wstring st1 = L"clicked: " + to_wstring(clicked);
			SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)st1.c_str());
		}
		break;

		case WM_SIZE:
		{
			window_width = LOWORD(lParam);

			SendMessage(hStatus, WM_SIZE, 0, 0);
			int statwidths[3] = { window_width / 5, 2 * window_width / 5, -1 };
			SendMessage(hStatus, SB_SETPARTS, 3, (LPARAM)statwidths);
			UpdatePenStatus(hStatus, curLine);
		}
		break;

		case WM_LBUTTONDOWN:
		{
			clicked = true;
			line_pushed = false; // prevent double push of the same line
			curLine.c.clear();
			x = LOWORD(lParam);
			y = HIWORD(lParam);
			curLine.c.push_back(make_pair(x, y));

			wstring st1 = L"clicked: " + to_wstring(clicked);
			SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)st1.c_str());
		}
		break;

		case WM_LBUTTONUP:
		{
			if (clicked) {
				lines.push_back(curLine);
				line_pushed = true;
			}
			clicked = false;

			wstring st1 = L"clicked: " + to_wstring(clicked);
			SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)st1.c_str());

			InvalidateRect(hWnd, NULL, false);
		}
		break;

		case WM_MOUSEMOVE:
		{
			if (!trackMainWindow) {
				TrackMouse(hWnd);
				trackMainWindow = true;
			}
			x = LOWORD(lParam);
			y = HIWORD(lParam);
			wstring st2 = L"x: " + to_wstring(x) + L"; y: " + to_wstring(y);
			SendMessage(hStatus, SB_SETTEXT, 1, (LPARAM)st2.c_str());

			if (clicked) {
				curLine.c.push_back(make_pair(x, y));
				InvalidateRect(hWnd, NULL, false);
			}
		}
		break;

		case WM_MOUSELEAVE:
		{
			if (clicked && !line_pushed) {
				lines.push_back(curLine);
				line_pushed = true;
			}
			clicked = false;
			trackMainWindow = false;

			wstring st1 = L"clicked: " + to_wstring(clicked);
			SendMessage(hStatus, SB_SETTEXT, 0, (LPARAM)st1.c_str());
		}
		break;


		case WM_COMMAND:
		{
			int wmId = LOWORD(wParam);

			switch (wmId)
			{
				case ID_FILE_CHOOSECOLOR:
				{
					if (ChooseColor(&cc)) {
						curLine.color = cc.rgbResult;
					}
				}
				break;

				case ID_FILE_CHANGEPENSTYLE:
				{
					DialogBox(hInst, MAKEINTRESOURCE(IDD_PENDIALOG), hWnd, PenStyleDlgProc);
					UpdatePenStatus(hStatus, curLine);
				}
				break;

				case ID_FILE_SAVEAS:
				{
					wcscpy_s(szFile, L"");
					ofnSuccess = GetSaveFileName(&ofn);
					if (ofnSuccess) {
						ofstream fout(szFile);
							// number of lines
							// width
							// style
							// color
							// number of points
							// points: x, " ", y
							// ....
						
						fout << lines.size() << endl;
						for (int i = 0; i < lines.size(); i++) {
							fout << lines[i].w << endl;
							fout << lines[i].style << endl;
							fout << lines[i].color << endl;
							fout << lines[i].c.size() << endl;
							for (int j = 0; j < lines[i].c.size(); j++)
								fout << lines[i].c[j].first << " " << lines[i].c[j].second << endl;
						}
						fout.close();
					}
				}
				break;

				case ID_FILE_LOADFROMFILE:
				{
					wcscpy_s(szFile, L"");
					ofnSuccess = GetOpenFileName(&ofn);
					if (ofnSuccess) {
						ifstream fin(szFile);
						int n;
						fin >> n;
						for (int i = 0; i < n; i++) {
							Line cLine;
							fin >> cLine.w;
							fin >> cLine.style;
							fin >> cLine.color;
							int k;
							fin >> k;
							for (int j = 0; j < k; j++) {
								int x, y;
								fin >> x >> y;
								cLine.c.push_back(make_pair(x, y));
							}
							lines.push_back(cLine);
						}
						InvalidateRect(hWnd, NULL, true);
					}
				}
				break;

				case IDM_EXIT:
				{
					DestroyWindow(hWnd);
				}
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

			if (clicked)
				DrawLine(hdc, curLine);
			for (int i = 0; i < lines.size(); i++) {
				DrawLine(hdc, lines[i]);
			}

			EndPaint(hWnd, &ps);
		}
		break;

		case WM_DESTROY:
		{
			PostQuitMessage(0);
		}
		break;

		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

void DrawLine(HDC hdc, Line& line) {
	HPEN pen = CreatePen(PS_DOT, line.w, line.color);
	SelectObject(hdc, pen);
	MoveToEx(hdc, line.c[0].first, line.c[0].second, NULL);
	for (int j = 1; j < line.c.size(); j++) {
		int x = line.c[j].first;
		int y = line.c[j].second;
		LineTo(hdc, x, y);
	}

	DeleteObject(pen);
}

INT_PTR CALLBACK PenStyleDlgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) {

	static vector<wstring> w(12);
	static vector<wstring> ps(5);

	ps[0] = L"Solid";
	ps[1] = L"Dash";
	ps[2] = L"Dot";
	ps[3] = L"Dash-Dot";
	ps[4] = L"Dash-Dot-Dot";

	static int width;
	static int style;

	switch (msg) {
		case WM_INITDIALOG:
		{
			w[0] = L"1";
			SendMessage(GetDlgItem(hWnd, IDC_PENWIDTH), CB_ADDSTRING, 0, (LPARAM)w[0].c_str());
			for (int i = 1; i < w.size(); i++) {
				w[i] = to_wstring(i * 2);
				SendMessage(GetDlgItem(hWnd, IDC_PENWIDTH), CB_ADDSTRING, 0, (LPARAM)w[i].c_str());
			}
			SendMessage(GetDlgItem(hWnd, IDC_PENWIDTH), CB_SETCURSEL, curLine.w / 2, 0);

			for (int i = 0; i < ps.size(); i++)
				SendMessage(GetDlgItem(hWnd, IDC_PENSTYLE), CB_ADDSTRING, 0, (LPARAM)ps[i].c_str());
			SendMessage(GetDlgItem(hWnd, IDC_PENSTYLE), CB_SETCURSEL, curLine.style, 0);
		}
		break;

		case WM_COMMAND:
		{
			int wmId = LOWORD(wParam);
			switch (wmId) {
				case IDC_PENWIDTH:
				{
					if (HIWORD(wParam) == CBN_SELENDOK) {
						WCHAR selected[255];
						int ix = SendMessage(GetDlgItem(hWnd, IDC_PENWIDTH), CB_GETCURSEL, 0, 0);
						if (ix > 0) {
							SendMessage(GetDlgItem(hWnd, IDC_PENSTYLE), CB_SETCURSEL, 0, 0);
							style = PS_SOLID;
						}
						width = stoi(w[ix]);
					}
				}
				break;

				case IDC_PENSTYLE:
				{
					if (HIWORD(wParam) == CBN_SELENDOK) {
						WCHAR selected[255];
						int ix = SendMessage(GetDlgItem(hWnd, IDC_PENSTYLE), CB_GETCURSEL, 0, 0);
						SendMessage(GetDlgItem(hWnd, IDC_PENSTYLE), CB_GETLBTEXT, ix, (LPARAM)selected);
						wstring sel(selected);
						if (sel == L"Solid") {
							style = PS_SOLID;
							break;
						}
						else {
							SendMessage(GetDlgItem(hWnd, IDC_PENWIDTH), CB_SETCURSEL, 0, 0);
							width = 1;
							if (sel == L"Dash") {
								style = PS_DASH;
							}
							else if (sel == L"Dot") {
								style = PS_DOT;
							}
							else if (sel == L"Dash-Dot") {
								style = PS_DASHDOT;
							}
							else if (sel == L"Dash-Dot-Dot") {
								style = PS_DASHDOTDOT;
							}
						}
					}
				}
				break;

				case IDOK:
				{
					curLine.w = width;
					curLine.style = style;
					EndDialog(hWnd, 0);
					return true;
				}
				break;

				case IDCANCEL:
				{
					EndDialog(hWnd, 1);
					return false;
				}
				break;
			}
		}
		break;

		case WM_DESTROY:
		{
			EndDialog(hWnd, 1);
			return false;
		}
		break;

		default:
			return false;
	}
	return FALSE;
}

void UpdatePenStatus(HWND hStatus, Line& line){
	wstring st = L"width: " + to_wstring(curLine.w) + L";    style: ";
	switch (curLine.style) {
		case PS_SOLID:
			st += L"Solid";
			break;
		case PS_DOT:
			st += L"Dot";
			break;
		case PS_DASH:
			st += L"Dash";
			break;
		case PS_DASHDOT:
			st += L"Dash-Dot";
			break;
		case PS_DASHDOTDOT:
			st += L"Dash-Dot-Dot";
			break;
		default:
			st += L"Other style";
	}
	SendMessage(hStatus, SB_SETTEXT, 2, (LPARAM)st.c_str());
}