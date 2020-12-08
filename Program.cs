using System;

namespace lr_4
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0;
            double func = 2 * x * Math.Log10(x + 2) + Math.Pow(x, 3) - 6;

            double eps1 = Math.Pow(10, -2);
            double eps2 = Math.Pow(10, -4);
            double eps3 = Math.Pow(10, -8);

            double a = -1.99;
            double b = 2;

            Console.WriteLine("f(x) = 2*x*lg(x+2) + x^3 - 6");

            Console.WriteLine();

            Console.WriteLine("Dichotomy Method:");
            DichotomyCount(a, b, eps1);
            DichotomyCount(a, b, eps2);
            DichotomyCount(a, b, eps3);

            Console.WriteLine();

            Console.WriteLine("Golden Section Search Count:");
            GoldenSectionSearchCount(a, b, eps1);
            GoldenSectionSearchCount(a, b, eps2);
            GoldenSectionSearchCount(a, b, eps3);

            Console.WriteLine();

            Console.WriteLine("Fibonacci Method:");
            FibonacciCount(a, b, eps1);
            FibonacciCount(a, b, eps2);
            FibonacciCount(a, b, eps3);

            Console.WriteLine();

            Console.WriteLine("Parabola Method:");
            ParabolaMethod(a, b, eps1);
            ParabolaMethod(a, b, eps2);
            ParabolaMethod(a, b, eps3);

            Console.ReadKey();
        }

        static double funcCount(double x)
        {
            return 2 * x * Math.Log10(x + 2) + Math.Pow(x, 3) - 6;
        }

        static void DichotomyCount(double a_, double b_, double e_)
        {
            double a = a_;
            double b = b_;
            double e = e_;

            double sig = e / 3;

            int countIteration = 0;
            int countFunc = 0;

            while ((b - a) > e)
            {
                countIteration++;

                double x1 = (a + b - sig) / 2;
                double x2 = (a + b + sig) / 2;
                double f1 = funcCount(x1);
                double f2 = funcCount(x2);

                countFunc = countFunc + 2;

                if (f1 <= f2)
                {
                    b = x2;
                }
                else
                {
                    a = x1;
                }
            }

            double xZir = (a + b) / 2;
            double fZir = funcCount(xZir);

            countFunc++;

            Console.WriteLine($"f({xZir}) = {fZir}");
            Console.WriteLine($"with {e} there are {countIteration} iterations with {countFunc} function count");
        }

        static void GoldenSectionSearchCount(double a, double b, double e)
        {
            int countIteration = 0;
            int countFunc = 3;

            double u = a + ((3 - Math.Sqrt(5)) / 2) * (b - a);
            double v = a + b - u;
            double fu = funcCount(u);
            double fv = funcCount(v);

            double x = a;
            double fsh = funcCount(x);

            while ((b - a) > e)
            {
                countIteration++;
                if (fu <= fv)
                {
                    //a = a;
                    b = v;
                    x = u;
                    fsh = fu;
                    v = u;
                    fv = fu;

                    u = a + b - v;
                    if (u < v)
                    {
                        fu = funcCount(u);
                        countFunc++;
                    }
                }
                if (fu > fv)
                {
                    a = u;
                    //b = b;
                    x = v;
                    fsh = funcCount(v);

                    countFunc++;

                    u = v;
                    fu = fv;

                    v = a + b - u;
                    if (u < v)
                    {
                        fv = funcCount(v);
                        countFunc++;
                    }
                }
                if (u > v)
                {
                    u = a + ((3 - Math.Sqrt(5)) / 2) * (b - a);
                    v = a + b - u;
                    fu = funcCount(u);
                    fv = funcCount(v);

                    countFunc = countFunc + 2;
                }
            }

            Console.WriteLine($"f({x}) = {fsh}");
            Console.WriteLine($"with {e} there are {countIteration} iterations with {countFunc} function count");
        }

        static void FibonacciCount(double a, double b, double e)
        {
            int k = 1;
            //double n = 10;
            int countIteration = 0;
            int countFunc = 3;

            double n = (b - a) / e;  /// n - ?????

            double x = a;
            double fsh = funcCount(x);

            double u = a + FibonacciNum(n - k + 1) / FibonacciNum(n - k + 3) * (b - a);
            double v = a + b - u;

            double fu = funcCount(u);
            double fv = funcCount(v);

            while (k <= n)
            {
                countIteration++;
                k++;

                if (fu <= fv)
                {
                    //a = a;
                    b = v;
                    x = u;
                    fsh = fu;
                    v = u;
                    fv = fu;

                    u = a + b - v;
                    if (u < v)
                    {
                        fu = funcCount(u);
                        countFunc++;
                    }
                }
                if (fu > fv)
                {
                    a = u;
                    //b = b;
                    x = v;
                    fsh = funcCount(v);
                    countFunc++;
                    u = v;
                    fu = fv;

                    v = a + b - u;
                    if (u < v)
                    {
                        fv = funcCount(v);
                        countFunc++;
                    }
                }
                if (u > v)
                {
                    u = a + FibonacciNum(n - k + 1) / FibonacciNum(n - k + 3) * (b - a);
                    v = a + b - u;
                    fu = funcCount(u);
                    fv = funcCount(v);
                    countFunc = countFunc + 1;
                }
            }

            Console.WriteLine($"f({x}) = {fsh}");
            Console.WriteLine($"with {e} there are {countIteration} iterations with {countFunc} function count");
        }

        static double FibonacciNum(double n)
        {
            double f = (Math.Pow(((1 + Math.Sqrt(5)) / 2), n) - Math.Pow(((1 - Math.Sqrt(5)) / 2), n)) / Math.Sqrt(5);
            //Console.WriteLine(f);
            return f;
        }

        static void ParabolaMethod(double a, double b, double e)
        {
            int countIteration = 0;
            int countFunc = 4;

            double x0 = a;
            double x1 = a + e;
            double x2 = b;
            double x3 = a + 2 * e;

            double fx0 = funcCount(x0);
            double fx1 = funcCount(x1);
            double fx2 = funcCount(x2);
            double fx3 = funcCount(x3);

            while (fx1 >= Math.Min(fx0, fx2) && fx1 < Math.Min(fx0, fx2))  // cute three
            {
                x1 = x1 + e;
                fx1 = funcCount(x1);
                Console.WriteLine("111");
                countFunc++;
            }

            double xZir = (x0 + x1) / 2 + (fx1 - fx0) * (x2 - x0) * (x2 - x1) / (2 * ((fx1 - fx0) * (x2 - x0) - (fx2 - fx0) * (x1 - x0)));
            double fZir = funcCount(xZir);

            while (Math.Abs(x1 - xZir) > e && Math.Abs(x2 - xZir) > e)
            {
                countIteration++;
                if (xZir < x1)
                {
                    x3 = x2;
                    fx3 = fx2;
                    x2 = x1;
                    fx2 = fx1;
                    x1 = xZir;
                    fx1 = fZir;
                }
                else if (xZir > x1)
                {
                    x3 = x2;
                    fx3 = fx2;
                    x2 = xZir;
                    fx2 = fZir;
                }
                if (fx1 > fx2)
                {
                    x0 = x1;
                    fx0 = fx1;
                    x1 = x2;
                    fx1 = fx2;
                    x2 = x3;
                    fx2 = fx3;
                }

                xZir = (x0 + x1) / 2 + (fx1 - fx0) * (x2 - x0) * (x2 - x1) / (2 * ((fx1 - fx0) * (x2 - x0) - (fx2 - fx0) * (x1 - x0)));
                fZir = funcCount(xZir);
                countFunc++;
            }

            Console.WriteLine($"f({xZir}) = {fZir}");
            countFunc++;
            Console.WriteLine($"with {e} there are {countIteration} iterations with {countFunc} function count");
        }

    }
}
