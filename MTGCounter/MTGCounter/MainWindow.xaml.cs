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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainController controller;

		private void InitController()
		{
			controller = new MainController();
			controller.Counters.CollectionChanged += Counters_CollectionChanged;
			UpdateCounterSize();
		}

		void Counters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			UpdateCounterSize();
		}



		private void UpdateCounterSize()
		{
			foreach (var counterController in controller.Counters)
			{
				counterController.Height = rowWithData.ActualHeight / 
					Math.Round(Math.Sqrt(controller.Counters.Count));
				counterController.Width = colWithData.ActualWidth / 
					Math.Ceiling(Math.Sqrt(controller.Counters.Count));
			}
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitController();
			DataContext = controller;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			InitController();
			DataContext = controller;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			controller.Counters.Add(new PlayerCounterController());
		}
	}
}
