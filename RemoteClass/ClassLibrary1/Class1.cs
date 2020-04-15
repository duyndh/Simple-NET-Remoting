using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteClass
{
    public class RemoteClass : MarshalByRefObject
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
