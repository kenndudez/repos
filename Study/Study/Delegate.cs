using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{

    public delegate int BinaryOp(int x, int y);

    public delegate string MyDelegate(bool a, bool b, bool c);

    public delegate string MyOtherDelegate(out bool a, ref bool b, int c);
}