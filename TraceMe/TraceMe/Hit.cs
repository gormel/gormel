using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public class Hit
    {
        public Color Color { get; set; }
        public double Reflection { get; set; }
        public Vector3 Normal { get; set; }
        public double Distance { get; set; }
    }
}
