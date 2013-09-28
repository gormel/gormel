using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace MTGCounter
{
	public abstract class BaseController : DependencyObject, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;
		protected void FirePropertyChange(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
