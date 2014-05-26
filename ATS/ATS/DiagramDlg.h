#pragma once

#include "stdafx.h"
#include "Station.h"

// CDiagramDlg dialog

class CDiagramDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CDiagramDlg)
	Station *station;
	void DrawTomb(int x, int y, int width, int height);
	void DrawTomb(int offset, int width, double percent);
	static const int captHeight = 35;
	HPEN pen;
	HDC dc;
public:
	CDiagramDlg(Station *station, CWnd* pParent = NULL);   // standard constructor
	virtual ~CDiagramDlg();
	afx_msg void OnPaint();
	
// Dialog Data
	enum { IDD = IDD_DIALOG5 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
};
