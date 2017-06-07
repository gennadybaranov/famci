#include <Windows.h>
#include <string>
#include <vector>
#include <stack>
#include <sstream>
#include "resource.h"


BOOL CALLBACK DlgProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK H(HWND, UINT, WPARAM, LPARAM);

HINSTANCE hInst;

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE, LPSTR, int){
	DialogBox(hInst, MAKEINTRESOURCE(IDD_MAIN), NULL, DlgProc);
	return 0;
}

std::vector<std::wstring> v;
std::wstring tmp;

int fmin(int a, int b, int c, int d, int e)
{
	if (a < b && a < c && a < d && a < e)
		return a;
	if (b < a && b < c && b < d && b < e)
		return b;
	if (a < b && a < c && a < d && a < e)
		return a;
	if (c < b && c < a && c < d && c < e)
		return c;
	if (d < b && d < c && d < a && d < e)
		return d;
	if (e < b && e < c && e < d && e < a)
		return e;
}

std::wstring calc(std::wstring s)
{
	std::wstring str = s;
	std::wstring tmp_str;
	std::wstringstream ss;
	double ans;

	int plus = str.find_first_of('+');
	if(plus == -1)plus = 99999;
	int minus = str.find_first_of('-');
	if(minus == -1)minus = 99999;
	int mul = str.find_first_of('*');
	if(mul == -1)mul = 99999;
	int div = str.find_first_of('/');
	if(div == -1)div = 99999;
	int e = str.find_first_of('=');
	int oper = fmin(e, plus, minus, mul, div);
	tmp_str = str.substr(0, oper);
	str.erase(0, oper);
	ss << tmp_str;
	ss >> ans;

	double d = 0;
	while(true)
	{
		tmp_str.clear();
		tmp_str = str.substr(0, 1);
		str.erase(0, 1);

		if(tmp_str == L"="){
			ans += d;
			break;
		}

		plus = str.find_first_of('+');
		if(plus == -1)plus = 99999;
		minus = str.find_first_of('-');
		if(minus == -1)minus = 99999;
		mul = str.find_first_of('*');
		if(mul == -1)mul = 99999;
		div = str.find_first_of('/');
		if(div == -1)div = 99999;
		e = str.find_first_of('=');
		oper = fmin(e, plus, minus, mul, div);

		double tmp_d;
		std::wstring str_tmp_d;
		str_tmp_d = str.substr(0, oper);
		ss.clear();
		ss << str_tmp_d;
		ss >> tmp_d;
		str.erase(0, oper);
		if(tmp_str == L"+"){
			ans += d;
			d = tmp_d;
		}
		if(tmp_str == L"-"){
			ans += d;
			d = 0;
			d -= tmp_d;
		}
		if(tmp_str == L"*"){
			d *= tmp_d;
		}
		if(tmp_str == L"/"){
			d /= tmp_d;
		}
	}
	
	ss.clear();
	tmp_str.clear();
	ss << ans;
	ss >> tmp_str;
	return tmp_str;
}

bool indf = true;

BOOL CALLBACK DlgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam){
	switch(msg)
	{
	case WM_INITDIALOG:
		{
			break;
		}
	case WM_COMMAND:
		{
			int wmId = LOWORD(wParam);
			switch(wmId)
			{
			case ID_H:
				{
					DialogBox(hInst, MAKEINTRESOURCE(IDD_H), NULL, H);
					break;
				}
			case IDC_1:
				if(indf){
					tmp += L"1";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_2:
				if(indf){
					tmp += L"2";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_3:
				if(indf){
					tmp += L"3";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_4:
				if(indf){
					tmp += L"4";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_5:
				if(indf){
					tmp += L"5";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_6:
				if(indf){
					tmp += L"6";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_7:
				if(indf){
					tmp += L"7";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_8:
				if(indf){
					tmp += L"8";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_9:
				if(indf){
					tmp += L"9";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_0:
				if(indf){
					tmp += L"0";
					SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				}
				break;
			case IDC_POINT:
				if(indf){
					if(tmp[tmp.length() - 1] != '*' && tmp[tmp.length() - 1] != '+' && tmp[tmp.length() - 1] != '-' && tmp[tmp.length() - 1] != '/'  && tmp[tmp.length() - 1] != '.'){
						tmp += L".";
						SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
					}
				}
				break;
			case IDC_PLUS:
				if(indf){
					if(tmp != L"" && tmp[tmp.length() - 1] != '*' && tmp[tmp.length() - 1] != '+' && tmp[tmp.length() - 1] != '-' && tmp[tmp.length() - 1] != '/'){
						tmp += L"+";
						SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
					}
				}
				break;
			case IDC_MINUS:
				if(indf){
					if(tmp != L"" && tmp[tmp.length() - 1] != '*' && tmp[tmp.length() - 1] != '+' && tmp[tmp.length() - 1] != '-' && tmp[tmp.length() - 1] != '/'){
						tmp += L"-";
						SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
					}
				}
				break;
			case IDC_MULT:
				if(indf){
					if(tmp != L"" && tmp[tmp.length() - 1] != '*' && tmp[tmp.length() - 1] != '+' && tmp[tmp.length() - 1] != '-' && tmp[tmp.length() - 1] != '/'){
						tmp += L"*";
						SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
					}
				}
				break;
			case IDC_DIV:
				if(indf){
					if(tmp != L"" && tmp[tmp.length() - 1] != '*' && tmp[tmp.length() - 1] != '+' && tmp[tmp.length() - 1] != '-' && tmp[tmp.length() - 1] != '/'){
						tmp += L"/";
						SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
					}
				}
				break;
			case IDC_C:
				indf = true;
				tmp = L"";
				SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
				break;
			case IDC_EQ:
				{
					if(indf){
						if(tmp != L"" && tmp[tmp.length() - 1] != '*' && tmp[tmp.length() - 1] != '+' && tmp[tmp.length() - 1] != '-' && tmp[tmp.length() - 1] != '/'){
							tmp += L"=";
							tmp += calc(tmp);
							v.push_back(tmp);
							SetDlgItemText(hWnd, IDC_EDIT, tmp.c_str());
							indf = false;
						}
					}
					break;
				}
			case IDCANCEL:
				EndDialog(hWnd, LOWORD(wParam));
				return (INT_PTR)TRUE;
			}
			break;
		}
	default:
		return FALSE;
	}
}

INT_PTR CALLBACK H(HWND hH, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_INITDIALOG:
		{
			for(std::vector<std::wstring>::iterator i = v.begin(); i != v.end(); ++i){
				SendDlgItemMessage(hH, IDC_LIST, LB_ADDSTRING, 0, (LPARAM) (*i).c_str());
			}
			return (INT_PTR)TRUE;
		}

	case WM_COMMAND:
		if(LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL){
			EndDialog(hH, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}