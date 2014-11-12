using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    public class CurseInfo : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string shortName;
        public string ShortName
        {
            get { return shortName; }
            set
            {
                shortName = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ShortName"));
            }
        }

        private decimal data;
        public decimal Data
        {
            get { return data; }
            set
            {
                data = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
