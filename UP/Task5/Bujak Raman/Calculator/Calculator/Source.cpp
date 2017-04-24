#define _CRT_SECURE_NO_WARNINGS
#include "resource.h"
#include <windows.h>
#include <string>
#include <fstream>
using namespace std;
int K = 0;
HWND hWnd;
LRESULT CALLBACK DlgProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1),
		0, (DLGPROC)(DlgProc));
	return 0;
}

LRESULT CALLBACK DlgProc(HWND hWndDlg, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	static char str1[10], str2[10];
	INT x, y;
	static OPENFILENAME ofn;
	HMENU hmenu;
	string szText;
	BOOL success;
	static char szFile[200];
	double d;
	static string str;
	static int f = 0;
	static int k = 0;
	HINSTANCE hinst = GetModuleHandle(NULL);
	switch (Msg)
	{
	case WM_INITDIALOG:
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;
		ofn.lpstrFile = szFile;
		ofn.nMaxFile = sizeof(szFile);
		hmenu = LoadMenu(hinst,MAKEINTRESOURCE(IDR_MENU1));
		SetMenu(hWndDlg, hmenu);
		return FALSE;
	case WM_COMMAND:
		switch (wParam)
		{
		case ID_Open:
			strcpy(szFile, "");
			success = GetOpenFileName(&ofn);
			if (success)
			{
				SendDlgItemMessage(hWndDlg, IDC_LIST, LB_RESETCONTENT, 0, 0);
				HWND ListBox = GetDlgItem(hWndDlg, IDC_LIST);
				int y = 0;
				ifstream myfile(ofn.lpstrFile);
				while (myfile.good())
				{
					string s;
					getline(myfile, s);
					SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)s.c_str());
				}
				myfile.close();
				MessageBox(hWnd, ofn.lpstrFile, "Считываем из файла:", MB_OK);
			}
			else
				MessageBox(hWnd, "Отказ от выбора или ошибка.", "Ошибка", MB_ICONWARNING);
			break;
		case ID_SAVE:
			strcpy(szFile, "");
			success = GetOpenFileName(&ofn);
			if (success)
			{
				ofstream rewrite(ofn.lpstrFile);
				rewrite.write("", 0);
				HWND ListBox = GetDlgItem(hWndDlg, IDC_LIST);
				int y = 0;
				while (y != K)
				{
					SendMessage(ListBox, LB_GETTEXT, y, (LPARAM)str1);
					rewrite << str1<<endl;
					y++;

				}
				rewrite.close();
				MessageBox(hWnd, ofn.lpstrFile, "Сохранение в файл:", MB_OK);
			}
			else
				MessageBox(hWnd, "Отказ от выбора или ошибка.", "Ошибка", MB_ICONWARNING);
			break;
		case ID_CLOSE:
			EndDialog(hWndDlg, 0);
			return TRUE;
			break;
		case IDC_PLUS:
		{
			k = 1;
			break;
		}
		case IDC_MIN:
		{
			k = 2;
			break;
		}
		case IDC_UMN:
		{
			k = 3;
			
			break;
		}
		case IDC_DEL:
		{
			k = 4;
			break;
		}
		case IDC_CHECK:
		{
			f++;
			break;
		}
		case IDC_BUTTON:
		{
			K++;
			SendDlgItemMessage(hWndDlg, IDC_RES, LB_RESETCONTENT, 0, 0);
			GetDlgItemText(hWndDlg, IDC_EDIT1, str1, 10);
			GetDlgItemText(hWndDlg, IDC_EDIT2, str2, 10);
			int f = 0;
			for (int i = 0; i < strlen(str1); i++)
			{
				if (!isdigit(str1[i]))
				{
					f = 1;
					break;
				}
			}
			if (f == 1 || strlen(str1) == 0)
			{
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)"ERROR!Данные введены неверно!");
				break;
			}
			for (int i = 0; i < strlen(str2); i++)
			{
				if (!isdigit(str2[i]))
				{
					f = 1;
					break;
				}
			}
			if (f == 1 || strlen(str2) == 0)
			{
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)"ERROR!Данные введены неверно!");
				break;
			}
			x = atoi(str1);
			y = atoi(str2);
			if (k == 1)
			{
				
				x = x + y;
				str = to_string(x);
				string szText = (string)str1 + '+' + (string)str2 + '=';
				szText += str;
				SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
			}
			if (k == 2)
			{
				x = x - y;
				str = to_string(x);
				string szText = (string)str1 + '-' + (string)str2 + '=';
				szText += str;
				SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
			}
			if (k == 3)
			{
				x = x * y;
				str = to_string(x);
				string szText = (string)str1 + '*' + (string)str2 + '=';
				szText += str;
				SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
				SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
			}
			if (k == 4)
			{
				
				string szText = (string)str1 + '/' + (string)str2 + '=';
				if (y == 0)
				{
					SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)"ERROR!Деление на ноль!");
					break;
				}
				if (f%2== 1)
				{

					x /= y;
					str = to_string(x);
					szText += str;
					SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
					SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
				}
				if (f%2 == 0)
				{
					d = (double)x / (double)y;
					str = to_string(d);
					szText += str;
					SendDlgItemMessage(hWndDlg, IDC_LIST, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
					SendDlgItemMessage(hWndDlg, IDC_RES, LB_ADDSTRING, 0, (LPARAM)szText.c_str());
				}
			}
			break;
		}
		case WM_DESTROY:
			EndDialog(hWndDlg, 0);
			return TRUE;
			break;
		}
	}
	return FALSE;
}