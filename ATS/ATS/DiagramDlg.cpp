// DiagramDlg.cpp : implementation file
//

#include "stdafx.h"
#include "ATS.h"
#include "DiagramDlg.h"
#include "afxdialogex.h"


// CDiagramDlg dialog

IMPLEMENT_DYNAMIC(CDiagramDlg, CDialogEx)

CDiagramDlg::CDiagramDlg(Station *station, CWnd* pParent /*=NULL*/)
	: CDialogEx(CDiagramDlg::IDD, pParent), station(station)
{
}

CDiagramDlg::~CDiagramDlg()
{
}

void CDiagramDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

void CDiagramDlg::DrawTomb(int x, int y, int width, int height)
{
	MoveToEx(dc, x, y, nullptr);
	LineTo(dc, x, y - height);
	LineTo(dc, x + width, y - height);
	LineTo(dc, x + width, y);
}

void CDiagramDlg::DrawTomb(int offset, int width, double percent)
{
	CRect rect;
	this->GetClientRect(&rect);
	
	int height = rect.Height() - captHeight;
	DrawTomb(offset, height, width, (int)(height * percent * 0.9));
}

BEGIN_MESSAGE_MAP(CDiagramDlg, CDialogEx)
	ON_WM_PAINT()
END_MESSAGE_MAP()


// CDiagramDlg message handlers
void CDiagramDlg::OnPaint()
{
	CDialogEx::OnPaint();
	COLORREF color = RGB(0, 0, 0);
	pen = CreatePen(PS_SOLID, 2, color);
	dc = ::GetDC(this->GetSafeHwnd());
	SelectObject(dc, pen);
	auto phones = station->GetPhonesByCaegory(1);
	CRect wndRct;
	GetClientRect(&wndRct);
	int width = wndRct.Width();
	int wid = width / (phones.size() * 2);
	int offset = wid * 2;
	auto calls = station->GetCalls();
	int max = -1;
	for (auto p : phones)
	{
		int now = std::count_if(calls.begin(), calls.end(), [&](Call *c){ return c->GetContainer() == p; });
		if (now > max)
			max = now;
	}

	int index = 0;
	for (auto p : phones)
	{
		int now = std::count_if(calls.begin(), calls.end(), [&](Call *c){ return c->GetContainer() == p; });
		int tombOffset = index++ * offset + offset / 4;
		DrawTomb(tombOffset, wid, (double)now / max);
		auto rect = new CRect(CPoint(tombOffset - wid / 4, wndRct.Height() - captHeight + 2), CSize(wid + wid / 2, captHeight));
		SetBkMode(dc, TRANSPARENT);
		std::wostringstream str;
		str << p->GetNumber() << std::endl << " (" << now << ")";
		DrawTextW(dc, str.str().c_str(), -1, rect, DT_CENTER);

	}
}

