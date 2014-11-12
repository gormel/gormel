using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Course
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListModel model = new ListModel();
        public MainWindow()
        {
            InitializeComponent();
            InitUserComponents();
        }

        private void InitUserComponents()
        {
            list1.DataContext = model;
        }

        private void ValueUpdater(object sender, EventArgs e)
        {
        }
    }


}
