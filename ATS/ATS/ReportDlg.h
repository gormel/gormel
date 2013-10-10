#pragma once

#include "stdafx.h"
#include "Station.h"
#include "afxwin.h"


// диалоговое окно CReportDlg

class CReportDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CReportDlg)
	Station *station;
public:
	CReportDlg(Station *station, CWnd* pParent = NULL);   // стандартный конструктор
	virtual ~CReportDlg();

	virtual BOOL OnInitDialog() override;

// Данные диалогового окна
	enum { IDD = IDD_DIALOG3 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // поддержка DDX/DDV

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	CEdit m_text;
};
