#include "resource.h"
#include <Windows.h>
#include <fstream>
#include <string>

using namespace std;

BOOL CALLBACK DlgProc(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK AboutDlgProc(HWND, UINT, WPARAM, LPARAM);
void SaveLog(HWND, LPCTSTR);
void LoadLog(HWND, LPCTSTR);
void Calculate(HWND hwnd);
double ReadArgument(HWND hEdit);

static OPENFILENAME ofn;
static wchar_t szFilter[] = L"Text Files (*.TXT)\0*.txt\0" \
"All files (*.*)\0*.*\0\0";
static wchar_t szFile[MAX_PATH];

int	CALLBACK WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
	LPSTR CmdLine, int CmdShow) {
	DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), 0, DlgProc, 0);
	return 0;
}

BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	BOOL success;
	switch (uMsg) {

	case WM_INITDIALOG: {
		SetWindowPos(hwnd, HWND_TOP, 600, 300, 0, 0, SWP_NOSIZE);
		SendMessage(GetDlgItem(hwnd, IDC_HISTORY), LB_SETHORIZONTALEXTENT, 1000, 0);

		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hwnd;
		ofn.lpstrFilter = szFilter;
		ofn.lpstrFile = szFile;
		ofn.nMaxFile = sizeof(szFile);
		ofn.lpstrDefExt = L"txt";
	}
	break;

	case WM_COMMAND: {
		switch (LOWORD(wParam)) {

		case ID_CALC: {
			Calculate(hwnd);
		}
		break;

		case ID_CLEAR: {
			SendMessage(GetDlgItem(hwnd, IDC_HISTORY), LB_RESETCONTENT, 0, 0);
		}
		break;

		case ID_FILE_EXIT: {
			EndDialog(hwnd, 0);
			return TRUE;
		}
		break;

		case ID_FILE_ABOUT: {
			DialogBox(NULL, MAKEINTRESOURCE(IDD_DIALOG2), hwnd, AboutDlgProc);
		}
		break;

		case ID_FILE_SAVELOG: {
			wcscpy_s(szFile, L"");
			success = GetSaveFileName(&ofn);
			if (success) {
				MessageBox(hwnd, ofn.lpstrFile, L"Log is saving to", MB_OK);
				SaveLog(hwnd, ofn.lpstrFile);
			}
			else
				MessageBox(hwnd, L"Refusal to save file or some error occured", L"Aler!", MB_ICONWARNING);
		}
		break;

		case ID_FILE_LOADLOG: {
			wcscpy_s(szFile, L"");
			success = GetOpenFileName(&ofn);
			if (success) {
				MessageBox(hwnd, ofn.lpstrFile, L"File is opening: ", MB_OK);
				LoadLog(hwnd, ofn.lpstrFile);
			}
			else
				MessageBox(hwnd, L"Refusal to load file or some error occured", L"Alert!", MB_ICONWARNING);
		}
		break;
			
		}
	}
	break;

	case WM_CLOSE: {
		EndDialog(hwnd, 0);
		return TRUE;
	}
	break;

	}

	return FALSE; // it is VERY IMPORTANT to return FALSE
}

BOOL CALLBACK AboutDlgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) {
	switch (msg) {

	case WM_COMMAND: {
		switch (LOWORD(wParam)) {
		case IDOK:
			EndDialog(hWnd, 0);
			return TRUE;
		}
		break;
	}

	case WM_CLOSE: {
		EndDialog(hWnd, 0);
		return TRUE;
	}
	break;

	}

	return FALSE;
}

void Calculate(HWND hwnd) {
	wstring out = L"";

	double a, b;
	double res;
	bool divisionByZero = false;

	a = ReadArgument(GetDlgItem(hwnd, IDC_ARG1));
	b = ReadArgument(GetDlgItem(hwnd, IDC_ARG2));

	wstring operation = L"\nOperation: ";
	if (IsDlgButtonChecked(hwnd, IDC_ADD)) {
		res = a + b;
		operation = L"+";
	}
	else if (IsDlgButtonChecked(hwnd, IDC_SUB)) {
		res = a - b;
		operation = L"-";
	}
	else if (IsDlgButtonChecked(hwnd, IDC_MUL)) {
		res = a * b;
		operation = L"*";
	}
	else if (IsDlgButtonChecked(hwnd, IDC_DIV)) {
		if (b != 0) {
			if (IsDlgButtonChecked(hwnd, IDC_INT_DIV)) {
				res = (int)(a / b);
				operation = L":";
			}
			else {
				res = a / b;
				operation = L"/";
			}
		}
		else {
			divisionByZero = true;
			MessageBox(hwnd, L"Division by 0 is forbidden", L"Alert!", MB_ICONWARNING);
		}
	}
	else {
		MessageBox(hwnd, L"Operation is not chosen", L"Alert!", MB_ICONWARNING);
		return;
	}

	if (!divisionByZero) {
		SetWindowText(GetDlgItem(hwnd, IDC_RES), to_wstring(res).c_str());
		out = to_wstring(a) + L" " + operation + L" " + to_wstring(b) + L" = " + to_wstring(res).c_str() + L"\n";
	}
	else {
		SetWindowText(GetDlgItem(hwnd, IDC_RES), L"Error!");
		out = to_wstring(a) + L" / " + to_wstring(b) + L" = " + L"Error!" + L"\n";
	}

	SendMessage(GetDlgItem(hwnd, IDC_HISTORY), LB_ADDSTRING, 0, (LPARAM)out.c_str());
}

double ReadArgument(HWND hEdit)
{
	WCHAR buf[41];
	GetWindowText(hEdit, buf, 40);
	wchar_t* rest =	NULL;
	double res = wcstod(buf, &rest);

	return res;
}

void SaveLog(HWND hDlg, LPCTSTR file) {
	wofstream fout(file);
	wstring wLog = L"";
	int num = SendMessage(GetDlgItem(hDlg, IDC_HISTORY), LB_GETCOUNT, 0, 0);
	for (int ix = 0; ix < num; ix++) {
		wchar_t buf[261];
		SendMessage(GetDlgItem(hDlg, IDC_HISTORY), LB_GETTEXT, ix, (LPARAM)buf);
		wstring tmp(buf);
		wLog += tmp;
	}

	fout << (wstring)wLog; // if not to use conversion to wstring wrong output occurs!
	fout.close();
}

void LoadLog(HWND hDlg, LPCTSTR file)
{
	ifstream fin(file);
	if (!file) {
		MessageBox(hDlg, L"Error during file opening", L"Alert!", MB_ICONWARNING);
		return;
	}
	SendMessage(GetDlgItem(hDlg, IDC_HISTORY), LB_RESETCONTENT, 0, 0);
	string curInput = "";
	while (!fin.eof()) {
		getline(fin, curInput);
		if (curInput == "")
			continue;
		curInput += '\n';
		wstring wCurInput(curInput.begin(), curInput.end());
		SendMessage(GetDlgItem(hDlg, IDC_HISTORY), LB_ADDSTRING, 0, (LPARAM)wCurInput.c_str());
	}
	fin.close();
}