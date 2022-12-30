using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatiiNumereMari
{
    class Program
    {
        static void Main(string[] args)
        {
            BigNumber nr1 = new BigNumber("654328156418646");
            BigNumber nr2 = new BigNumber("24561131564");
            Console.WriteLine(nr1);
            Console.WriteLine(nr2);
            Console.WriteLine($"nr1 + nr2 : {nr1 + nr2}");
            Console.WriteLine($"nr1 - nr2 : {nr1 - nr2}");
            Console.WriteLine($"nr1 > nr2 : {nr1 > nr2}");
            Console.WriteLine($"nr1 < nr2 : {nr1 < nr2}");
            Console.WriteLine($"nr1 == nr2 : {nr1 == nr2}");
            Console.WriteLine($"nr1 != nr2 : {nr1 != nr2}");
            Console.WriteLine($"nr1^2 : {nr1.Power(2)}");
            Console.WriteLine($"nr1*nr2 : {nr1 * nr2}");
            Console.WriteLine($"nr1/nr2 : {nr1 / nr2}");
            Console.ReadKey();
        }
    }
}
