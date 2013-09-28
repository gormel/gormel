using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MTGCounter
{
	class PlayerCounterConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, 
			object parameter, System.Globalization.CultureInfo culture)
		{
			PlayerCounterController counter = value as PlayerCounterController;
			PlayerCounter counterView = new PlayerCounter();
			counterView.DataContext = counter;
			return counterView;
		}

		public object ConvertBack(object value, Type targetType, object parameter, 
			System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
