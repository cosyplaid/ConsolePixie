using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePixie.Image
{
    public class RGBStruct
    {
        private int _r = 0;
        private int _g = 0;
        private int _b = 0;

        public int R 
        { 
            get { return _r; }
            set { _r = FixedInt(value); }
        }

        public int G
        {
            get { return _g; }
            set { _g = FixedInt(value); }
        }

        public int B
        {
            get { return _b; }
            set { _b = FixedInt(value); }
        }

        private int FixedInt(int a)
        {
            if (a < 0)
                return 0;
            else if (a > 255)
                return 255;
            else
                return a;
        }

        public RGBStruct(int r = 255, int g = 255, int b = 255)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
