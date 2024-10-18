using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.Output
{
    public class OutputFactory
    {

        //public static IOutput GetCosnoleObj() { return new ConsoleOutput(); }

        public IOutput Create() { return new ConsoleOutput(); }
    }
}
