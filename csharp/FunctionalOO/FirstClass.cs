using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalOO
{
    public delegate double PerformSomeCalculation(double x, double y, bool condition);

    public class FirstClass
    {
        public static string UseADelegate(PerformSomeCalculation calc, double[] values)
        {
            return calc(values[0], values[1], values[2] > 0.0).ToString();
        }
    }
}
