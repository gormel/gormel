using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MTGCounter
{
	/// <summary>
	/// Interaction logic for PlayerCounter.xaml
	/// </summary>
	public partial class PlayerCounter : UserControl
	{
		private PlayerCounterController Controller { get { return DataContext as PlayerCounterController; } }
		public PlayerCounter()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Controller.Life++;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Controller.Life--;
		}

		private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Controller.Life--;
		}

		private void TextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			Controller.Life++;
		}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Controller.Life += 5;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Controller.Life -= 5;
        }
	}
}
