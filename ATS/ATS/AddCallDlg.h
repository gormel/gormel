#pragma once
#include "stdafx.h"
#include "afxwin.h"
#include "afxdtctl.h"

#include "Station.h"


// диалоговое окно CAddCallDlg

class CAddCallDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CAddCallDlg)
	Station *station;
	Call *call;
	bool CheckData();
	bool editMode;
public:
	CAddCallDlg(Station *station, CWnd* pParent = NULL);   // стандартный конструктор
	CAddCallDlg(Station *statio, Call *call, CWnd* pParent = NULL); 
	virtual ~CAddCallDlg();
	virtual BOOL OnInitDialog() override;
// Данные диалогового окна
	enum { IDD = IDD_DIALOG2 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // поддержка DDX/DDV

	DECLARE_MESSAGE_MAP()
public:
	CComboBox m_phone;
	CDateTimeCtrl n_date;
	CEdit m_time;
	CEdit m_cost;
	afx_msg void OnBnClickedOk();
};
