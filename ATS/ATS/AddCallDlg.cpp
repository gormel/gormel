// AddCallDlg.cpp: файл реализации
//

#include "stdafx.h"
#include "ATS.h"
#include "AddCallDlg.h"
#include "afxdialogex.h"


// диалоговое окно CAddCallDlg

IMPLEMENT_DYNAMIC(CAddCallDlg, CDialogEx)

CAddCallDlg::CAddCallDlg(Station *station, CWnd* pParent /*=NULL*/)
	: CDialogEx(CAddCallDlg::IDD, pParent), station(station), editMode(false), call(0)
{

}
CAddCallDlg::CAddCallDlg(Station *station, Call *call, CWnd* pParent)
	: CDialogEx(CAddCallDlg::IDD, pParent), station(station), editMode(true), call(call)
{
}

CAddCallDlg::~CAddCallDlg()
{
}

void CAddCallDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO1, m_phone);
	DDX_Control(pDX, IDC_DATETIMEPICKER1, n_date);
	DDX_Control(pDX, IDC_EDIT1, m_time);
	DDX_Control(pDX, IDC_EDIT2, m_cost);
}

bool CAddCallDlg::CheckData()
{
	return true;
}

BOOL CAddCallDlg::OnInitDialog()
{
	auto v = CDialogEx::OnInitDialog();
	int i = 0;
	for (auto p : station->GetPhones())
	{
		i++;
		std::wostringstream str;
		str << "Class(" << p->GetCategory() << ") Number(" << p->GetNumber()
			<< ") Family(" << p->GetFamily() << ")";
		m_phone.AddString(str.str().c_str());
	}

	m_time.SetWindowTextW(L"0");
	m_cost.SetWindowTextW(L"0");

	if (i > 0)
		m_phone.SetCurSel(0);

	if (editMode)
	{
		auto phones = station->GetPhones();
		int phoneIndex = std::find(phones.begin(), phones.end(), call->GetContainer()) - phones.begin();
		CTime datetime = call->GetDate();
		std::wstring time = std::to_wstring(call->GetTime());
		std::wstring cost = std::to_wstring(call->GetCost());

		m_phone.SetCurSel(phoneIndex);
		n_date.SetTime(&datetime);
		m_time.SetWindowTextW(time.c_str());
		m_cost.SetWindowTextW(cost.c_str());
	}
	return v;
}


BEGIN_MESSAGE_MAP(CAddCallDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &CAddCallDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// обработчики сообщений CAddCallDlg


void CAddCallDlg::OnBnClickedOk()
{
	if (!CheckData())
		return;

	int index = m_phone.GetCurSel();

	SYSTEMTIME _datetime;
	n_date.GetTime(&_datetime);
	CTime datetime(_datetime);

	CString str;
	m_time.GetWindowTextW(str);
	int time = std::stoi(std::wstring(str));

	CString costCstr;
	m_cost.GetWindowTextW(costCstr);
	double cost = std::stod(std::wstring(costCstr));

	if (!editMode)
	{
		Call *call = new Call();
		call->SetTime(time);
		call->SetCost(cost);
		call->SetDate(datetime);
		station->AddCall(station->GetPhones().at(index), call);
	}

	if (editMode)
	{
		call->SetTime(time);
		call->SetCost(cost);
		call->SetDate(datetime);
	}

	CDialogEx::OnOK();
}
