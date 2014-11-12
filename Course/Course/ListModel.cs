using Course.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    public class ListModel
    {
        public ObservableCollection<CurseInfo> Items { get; set; }
        public ListModel()
        {
            Items = new ObservableCollection<CurseInfo>();
            var client = new DailyInfoSoapClient();
            var curse = client.GetCursOnDate(DateTime.Now);
            foreach (var t in curse.Tables)
            {
                var table = (DataTable)t;
                foreach (var r in table.Rows)
                {
                    var row = (DataRow)r;
                    Items.Add(new CurseInfo() { Name = (string)row.ItemArray[0], ShortName = (string)row.ItemArray[4], Data = (decimal)row.ItemArray[2] / (decimal)row.ItemArray[1] });
                }
            }
        }
    }
}
