// ReportDlg.cpp: файл реализации
//

#include "stdafx.h"
#include "ATS.h"
#include "ReportDlg.h"
#include "afxdialogex.h"


// диалоговое окно CReportDlg

IMPLEMENT_DYNAMIC(CReportDlg, CDialogEx)

CReportDlg::CReportDlg(Station *station, CWnd* pParent /*=NULL*/)
	: CDialogEx(CReportDlg::IDD, pParent), station(station)
{

}

CReportDlg::~CReportDlg()
{
}

void CReportDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, m_text);
}

BOOL CReportDlg::OnInitDialog()
{
	auto v = CDialogEx::OnInitDialog();

	std::wostringstream str;
	for (auto c : station->GetCalls())
	{
		std::vector<std::wstring> callInfo = c->GetInfoStrings();
		str << "Category(" << callInfo.at(0) << ") Number(" << callInfo.at(1) << ") Family(" << callInfo.at(2) 
			<< ") Date(" << callInfo.at(3) << ") Time(" << callInfo.at(4) << ") Cost(" << callInfo.at(5) << ")" << "\r\n";
	}
	m_text.SetWindowTextW(CString(str.str().c_str()));

	return v;
}

BEGIN_MESSAGE_MAP(CReportDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &CReportDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// обработчики сообщений CReportDlg


void CReportDlg::OnBnClickedOk()
{
	CDialogEx::OnOK();
}
