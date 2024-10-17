using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Calculator.Output
{
    public class ConsoleOutput : IOutput
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
