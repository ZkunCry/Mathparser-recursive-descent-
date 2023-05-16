using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    /*Исходная грамматика
        
       S->E
       E->E+T | E-T | T
       T->T*M | T/M | M
       M->(E) | C | F(E)
       F->sin | cos | sqrt
     */
namespace MathparserCalc
{
    class MathParser
    {

        private string source = "5*(cos(60)+cos(60))";
        private int symbol, indexsrc = 0;
        private void GetSymbol()
        {
            if (indexsrc + 1 <= source.Length)
            {
                symbol = source[indexsrc];
                indexsrc++;
            }
            else
                symbol = '\0';

        }
        public void StartAnalize()
        {
            GetSymbol();
            double result = MethodE();
            Console.WriteLine($"Result operation {source} = {result}");
        }
        private double MethodE()
        {
            double x = MethodT();
            while (symbol == '+' || symbol == '-')
            {
                char p = (char)symbol;
                GetSymbol();
                if (p == '+')
                    x += MethodT();
                else
                    x -= MethodT();
            }
            return x;
        }
        private double MethodT()
        {
            double x = MethodM();
            while (symbol == '*' || symbol == '/')
            {
                char p = (char)symbol;
                GetSymbol();
                if (p == '*')
                    x *= MethodM();
                else
                    x /= MethodM();
            }
            return x;
        }
        private double MethodM()
        {
            double x = 0;
            if (symbol == '(')
            {
                GetSymbol();
                x = MethodE();
                GetSymbol();
            }
            else
            {
                if (symbol == '-')
                {
                    GetSymbol();
                    x = -MethodM();
                }
                else if (symbol >= '0' && symbol <= '9')
                    x = MethodC();
                else if (symbol =='c' || symbol == 's')
                {
                   string namefunc = "";

                    
                    while (symbol != '(')
                    {
                        namefunc += (char)symbol;
                        GetSymbol();
                       
                    }
                       
                    switch(namefunc)
                    {
                        case "cos":
                            x = Math.Cos(MethodM());
                            break;
                        case "sin":
                            x = Math.Sin(MethodM());
                            break;
                        case "sqrt":
                            x = Math.Sqrt(MethodM());
                            break;
                    }
                }
            }
            return x;
        }
        private double MethodC()
        {
            string x = "";

            while (symbol >= '0' && symbol <= '9')
            {
                x += (char)symbol;
                GetSymbol();
                if (symbol == ',')
                {
                    x += ',';
                    GetSymbol();
                }

            }
            return double.Parse(x);
        }

    }
}
