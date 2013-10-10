// StatisticDlg.cpp: файл реализации
//

#include "stdafx.h"
#include "ATS.h"
#include "StatisticDlg.h"
#include "afxdialogex.h"


// диалоговое окно CStatisticDlg

IMPLEMENT_DYNAMIC(CStatisticDlg, CDialogEx)

CStatisticDlg::CStatisticDlg(Station *station, CWnd* pParent /*=NULL*/)
	: CDialogEx(CStatisticDlg::IDD, pParent), station(station)
{

}

BOOL CStatisticDlg::OnInitDialog()
{
	auto v = CDialogEx::OnInitDialog();

	std::vector<int> counts(station->GetPhones().size(), 0);
	auto phones = station->GetPhones();
	for (auto c : station->GetCalls())
	{
		int phoneIndex = std::find(phones.begin(), phones.end(), c->GetContainer()) - phones.begin();
		counts.at(phoneIndex)++;
	}

	for (int i = 0; i < counts.size(); i++)
	{
		std::wostringstream str;
		str << phones.at(i)->GetFamily() << ": " << phones.at(i)->GetNumber() << " - " << counts.at(i);
		m_list.AddString(str.str().c_str());
	}

	return v;
}

CStatisticDlg::~CStatisticDlg()
{
}

void CStatisticDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_list);
}


BEGIN_MESSAGE_MAP(CStatisticDlg, CDialogEx)
END_MESSAGE_MAP()


// обработчики сообщений CStatisticDlg
