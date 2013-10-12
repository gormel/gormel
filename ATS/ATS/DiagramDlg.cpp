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
	RECT windowRect;
	GetWindowRect(&windowRect);
	int height = windowRect.bottom - windowRect.top;
	DrawTomb(offset, height, width, (int)(height * percent));
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
	RECT wndRct;
	GetWindowRect(&wndRct);
	int width = - wndRct.left + wndRct.right;
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
		DrawTomb(index++ * offset + offset / 4, wid, (double)now / max);
	}
}

