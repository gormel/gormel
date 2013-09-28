using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace MTGCounter
{
	public class MainController : BaseController
	{
		public ObservableCollection<PlayerCounterController> Counters { get; set; }


		public MainController()
		{
			Counters = new ObservableCollection<PlayerCounterController>();
			Counters.Add(new PlayerCounterController());
			Counters.Add(new PlayerCounterController());
		}
	}
}
