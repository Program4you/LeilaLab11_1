using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IR11_1 {
    class Program {
        static long polinom(long[] p, int n, ref long x) {
            if (n == 1)
                return x + p[1];

            int j = (n + 1) / 2; // calc next power of polinom
            long b = p[j] - 1; // calc b

            long[] q = new long[j];
            long[] r = new long[j];

            // get polinoms q and r (p = (x^j + b) * q(x) + r(x))
            for (int i = 0; i < j; i++) {
                q[i] = p[i];
                r[i] = p[i + j] - b * q[i];
            }

            long x1 = x;
            long p1 = polinom(q, n / 2, ref x);
            long p2 = polinom(r, n / 2, ref x1);
            x *= x;

            return (x + b) * p1 + p2;
        }

        static void Main(string[] args) {
            string[] lines = File.ReadAllLines("input_11_1.txt");
            int count = Convert.ToInt32(lines[0]); // number of polinomes
            string[] results = new string[count]; // resulted strings for write to file

            for (int i = 0; i < count; i++) {
                string[] tmp = lines[i + 1].Split(' '); // array of values for polinomes
                int n = Convert.ToInt32(tmp[0]); // power of polinom
                long[] coefficients = new long[n + 1]; // array of coeeficients of polinom

                // convert coefficients from string
                for (int j = 0; j < n + 1; j++)
                    coefficients[j] = Convert.ToInt64(tmp[j + 1]);

                int m = Convert.ToInt32(tmp[n + 2]); // number of points

                for (int j = 0; j < m; j++) {
                    long x = long.Parse(tmp[n + 3 + j]);
                    results[i] += polinom(coefficients, n, ref x) + " ";
                }
            }

            File.WriteAllLines("output_11_1.txt", results);
        }
    }
}