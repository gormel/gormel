using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMe
{
    public class Lay
    {
        public Vector3 Point { get; set; }
        public Vector3 Direction { get; set; }

        public Lay(Vector3 point, Vector3 direction)
        {
            Point = point;
            Direction = direction;
        }

        public Lay(Vector3 direction)
            : this(Vector3.Zero, direction)
        {
        }

    }
}
