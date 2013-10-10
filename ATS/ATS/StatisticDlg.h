#pragma once
#include "stdafx.h"
#include "Station.h"
#include "afxwin.h"

// диалоговое окно CStatisticDlg

class CStatisticDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CStatisticDlg)
	Station *station;
public:
	CStatisticDlg(Station *station, CWnd* pParent = NULL);   // стандартный конструктор
	virtual ~CStatisticDlg();
	virtual BOOL OnInitDialog() override;

// Данные диалогового окна
	enum { IDD = IDD_DIALOG4 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // поддержка DDX/DDV

	DECLARE_MESSAGE_MAP()
public:
	CListBox m_list;
};
