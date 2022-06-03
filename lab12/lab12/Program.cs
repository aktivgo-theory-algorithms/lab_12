using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab12
{
    internal static class Program
    {
        private static List<int> s;
        private static int n, t;
        private static float e, b;
        
        public static void Main()
        {
            Console.WriteLine("enter s: ");
            string[] sString = Console.ReadLine()?.Split();

            s = new List<int>();
            foreach (var element in sString)
            {
                s.Add(int.Parse(element));
            }

            n = s.Count;
            
            Console.WriteLine("enter t: ");
            t = int.Parse(Console.ReadLine());
            
            Console.WriteLine("enter e: ");
            e = float.Parse(Console.ReadLine());
            b = e / n;

            Console.WriteLine("full alg:");
            Alg(s, t);
            
            Console.WriteLine("approximate alg:");
            Alg2(s, t, e);
        }

        private static void Alg(List<int> S, int t)
        {
            Stopwatch w = new Stopwatch();
            w.Start();
            
            List<int> L = new List<int> { 0 };

            foreach (var s in S)
            {
                List<int> T = new List<int>();
                T.AddRange(L);
                for (int k = 0; k < T.Count; k++)
                {
                    T[k] += s;
                }
                List<int> U = T.Concat(L).Distinct().ToList();

                U = U.OrderBy(elem => elem).ToList();
                L.Clear();
                
                foreach (var u in U)
                {
                    if (u > t)
                    {
                        break;
                    }
                    L.Add(u);
                }
            }
            
            w.Stop();
            
            Console.WriteLine(w.ElapsedMilliseconds + " ms");
            
            PrintList(L);
        }
        
        private static void Alg2(List<int> S, int t, float e)
        {
            Stopwatch w = new Stopwatch();
            w.Start();
            
            List<int> L = new List<int> { 0 };
            
            foreach (var s in S)
            {
                List<int> T = new List<int>();
                T.AddRange(L);
                for (int k = 0; k < T.Count; k++)
                {
                    T[k] += s;
                }
                List<int> U = T.Concat(L).Distinct().ToList();

                U = U.OrderBy(elem => elem).ToList();
                L.Clear();

                int y = U.Min();
                L.Add(y);

                foreach (var u in U)
                {
                    if (y + e * t / S.Count < u && u <= t)
                    {
                        y = u;
                        L.Add(u);
                    }
                }
            }
            
            w.Stop();
            
            Console.WriteLine(w.ElapsedMilliseconds + " ms");
            
            PrintList(L);
        }
        
        private static void PrintList(List<int> list)
        {
            var s = "";
            foreach (var t in list)
                s += t + " ";
            Console.WriteLine(s);
        }
    }
}