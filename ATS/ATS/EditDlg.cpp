// EditDlg.cpp: файл реализации
//

#include "stdafx.h"
#include "ATS.h"
#include "EditDlg.h"
#include "afxdialogex.h"


// диалоговое окно EditDlg

IMPLEMENT_DYNAMIC(EditDlg, CDialogEx)

EditDlg::EditDlg(Station *station, CWnd* pParent)
	: CDialogEx(EditDlg::IDD, pParent), station(station)
{
}

EditDlg::~EditDlg()
{
}

void EditDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_callsList);
	DDX_Control(pDX, IDC_EDIT1, m_search);
}

void EditDlg::UpdateCallsList()
{
	m_callsList.ResetContent();

	std::vector<Call *> calls;
	CString searchText;
	m_search.GetWindowTextW(searchText);
	if (searchText.IsEmpty())
	{
		calls = station->GetCalls();
	}
	else
	{
		calls = station->FindByKeyword(std::wstring(searchText));
	}

	for (auto c : calls)
	{
		std::vector<std::wstring> callInfo = c->GetInfoStrings();
		std::wstringstream string;
		string << "Category(" << callInfo.at(0) << ") Number(" << callInfo.at(1) << ") Family(" << callInfo.at(2) 
			<< ") Date(" << callInfo.at(3) << ") Time(" << callInfo.at(4) << ") Cost(" << callInfo.at(5) << ")";
		m_callsList.AddString(string.str().c_str());
	}
}

BOOL EditDlg::OnInitDialog()
{
	auto v = CDialogEx::OnInitDialog();
	UpdateCallsList();
	return v;
}


BEGIN_MESSAGE_MAP(EditDlg, CDialogEx)
	ON_BN_CLICKED(IDC_BUTTON1, &EditDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON4, &EditDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON2, &EditDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &EditDlg::OnBnClickedButton3)
	ON_EN_CHANGE(IDC_EDIT1, &EditDlg::OnEnChangeEdit1)
END_MESSAGE_MAP()


// обработчики сообщений EditDlg


void EditDlg::OnBnClickedButton1()
{
	CAddCallDlg dlg(station, this);
	dlg.DoModal();
	UpdateCallsList();
}


void EditDlg::OnBnClickedButton4()
{
	CDialogEx::OnOK();
}


void EditDlg::OnBnClickedButton2()
{
	int sel = m_callsList.GetCurSel();
	if (sel < 0)
		return;
	station->RemoveCall(sel);
	UpdateCallsList();
}


void EditDlg::OnBnClickedButton3()
{
	int sel = m_callsList.GetCurSel();
	if (sel < 0)
		return;
	Call *call = station->GetCalls().at(sel);
	CAddCallDlg dlg(station, call, this);
	dlg.DoModal();
	UpdateCallsList();
}


void EditDlg::OnEnChangeEdit1()
{
	UpdateCallsList();
}
