using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatiiNumereMari
{
    class BigNumber
    {
        public List<int> cifre = new List<int>();
        public int semn;
        public static BigNumber ZERO = new BigNumber(0);
        public static BigNumber ONE = new BigNumber(1);
        public BigNumber(List<int> nr)
        {
            cifre.AddRange(nr);
            semn = 1;
        }
        public BigNumber(List<int> nr, int sign)
        {
            cifre.AddRange(nr);
            semn = sign;
        }
        public BigNumber(string s)
        {
            
            if (s[0] == '-') 
            { 
                semn = -1;
                for (int i = s.Length - 1 ; i > 0; i--)
                {
                    cifre.Add(s[i] - '0');
                }
            }
            else
            {  
                semn = 1;
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    cifre.Add(s[i] - '0');
                }
            }
            
        }
        public BigNumber()
        {
            semn = 1;
        }
        public BigNumber(int s)
        {
            semn = 1;
            if (s == 0) cifre.Add(0);
            while (s > 0)
            {
                cifre.Add(s % 10);
                s /= 10;
            }
        }
        public BigNumber(int s, int sign)
        {
            semn = sign;
            while (s >= 0)
            {
                cifre.Add(s % 10);
                s /= 10;
            }
        }
        public override string ToString()
        {
            string s = "";
            if (semn < 0) s += '-';
            for (int i = cifre.Count - 1; i >= 0; i--)
            {
                s += cifre[i];
            }
            return s;
        }

        public static bool operator !=(BigNumber a, BigNumber b)
        {
            /*if (a.semn == b.semn && a.cifre.Count == b.cifre.Count)
            for (int i = 0; i < a.cifre.Count; i++)
            {
                if (a.cifre[i] == b.cifre[i]) return false;
            }
            return true;*/
            return !(a == b);

        }
        public static bool operator ==(BigNumber a, BigNumber b)
        {
            if (a.semn != b.semn) return false;
            if (a.cifre.Count != b.cifre.Count) return false;
            for (int i = 0; i < a.cifre.Count; i++)
            {
                if (a.cifre[i] != b.cifre[i]) return false;
            }
            return true;

        }
        public static bool operator <(BigNumber a, BigNumber b)
        {
            if (a == b) return false;
            if (a.semn > b.semn) return false;
            if (a.semn < b.semn) return true;
            if (a.cifre.Count > b.cifre.Count) return false;
            if (a.cifre.Count < b.cifre.Count) return true;
            for (int i = a.cifre.Count-1; i >=0  ; i--)
            {
                if (a.cifre[i] < b.cifre[i])
                    if (a.semn > 0) return true;
                    else
                        return false;
                if (a.cifre[i] > b.cifre[i])
                    if (a.semn > 0) return false;
                    else
                        return true;
            }
            return true;

        }
        public static bool operator >(BigNumber a, BigNumber b)
        {
            return !(a < b || a == b);
        }
        public static bool operator >=(BigNumber a, BigNumber b)
        {
            return (a > b || a == b);
        }
        public static bool operator <=(BigNumber a, BigNumber b)
        {
            return (a < b || a == b);
        }
        private static List<int> ToList(string s)
        {
            List<int> t = new List<int>();   
            if(s[0]!='-')
            for (int i = s.Length-1; i >= 0; i--)
            {
                int c = s[i] -'0';
                t.Add(c);
            }
            else
                for (int i = s.Length-1; i > 0; i--)
                {
                    int c = s[i] - '0';
                    t.Add(c);
                }
            return t;
        }
        public static string SaveString(BigNumber b)
        {
            string nr = "";
            if (b.semn < 0)
            {
                nr += '-';
                for (int i = b.cifre.Count - 1; i >= 0; i--)
                {
                    nr += b.cifre[i];
                }
            }
            else
                for (int i = b.cifre.Count - 1; i >= 0; i--)
                {
                    nr += b.cifre[i];
                }
            return nr;
        }
        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            BigNumber suma = new BigNumber();
            int index = 0;
            if (a.semn != b.semn)
            {
                if (a.cifre == b.cifre) return ZERO;
                if (a.semn < 0)
                {
                    string before_a = SaveString(a);
                    string before_b = SaveString(b);
                    a.semn = 1;
                    suma = b - a;
                    b.cifre = ToList(before_b);
                    a.cifre = ToList(before_a);
                    a.semn = -1;
                    return suma;
                }
                else if (b.semn < 0)
                {
                    string before_a = SaveString(a);
                    string before_b = SaveString(b);
                    b.semn = 1;
                    suma = a - b;
                    b.cifre = ToList(before_b);
                    a.cifre = ToList(before_a);
                    b.semn = -1;
                    return suma;
                }
            }
            while (index < a.cifre.Count && index < b.cifre.Count)
            {
                int nr = a.cifre[index] + b.cifre[index];
                suma.cifre.Add(nr);
                index++;
            }
            while (index < a.cifre.Count)
            {
                int nr = a.cifre[index];
                suma.cifre.Add(nr);
                index++;
            }
            while (index < b.cifre.Count)
            {
                int nr = b.cifre[index];
                suma.cifre.Add(nr);
                index++;
            }
            index = 0;
            while (index < suma.cifre.Count)
            {
                int rest = suma.cifre[index] / 10;
                suma.cifre[index] %= 10;
                if (rest > 0)
                {
                    if (index + 1 >= suma.cifre.Count)
                    {
                        suma.cifre.Add(0);
                    }
                    suma.cifre[index + 1] += rest;
                }
                index++;
            }
            suma.semn = a.semn;
            return suma;
        }
        public static BigNumber operator -(BigNumber a, BigNumber b) 
        {
            BigNumber suma = new BigNumber();
            if (a == b) return ZERO;
            int index = 0;
            if (a < b) 
            {
                suma = b - a;
                suma.semn = -1;
                return suma;
            }
            if(a.semn < 0 && b.semn > 0)
            {
                a.semn = 1;
                suma = a+b;
                suma.semn = -1;
                a.semn = -1;
                return suma;
            }
            if (a.semn > 0 && b.semn < 0)
            {
                b.semn = 1;
                if(a<b) suma.semn = -1;
                else
                    suma.semn = 1;
                suma = a + b;
                b.semn = -1;
                return suma;
            }
            if (b.semn < 0 && a.semn > 0)
            {
                b.semn = 1;
                suma = a + b;
                suma.semn = -1;
                b.semn = -1;
                return suma;
            }
            if (b.semn < 0 && a.semn < 0 )
            {
                if (b > a) suma.semn = 1;
                else
                    suma.semn = -1;
                b.semn = 1;
                a.semn = 1;
                suma = b - a;
                b.semn = -1;
                a.semn = -1;
                return suma;
            }
            
            while (index < a.cifre.Count && index < b.cifre.Count)
            {
                int nr;
                if(a.cifre[index] < b.cifre[index])
                {
                    nr = a.cifre[index] + 10 - b.cifre[index];
                    int j = index+1;
                    while(j < a.cifre.Count && a.cifre[j]==0 )
                    {
                        j++;
                    }
                    if (j != a.cifre.Count)
                    {

                            int i = j - 1;
                            while(i>=0 && a.cifre[i]==0)
                            {
                                a.cifre[i] = 9;
                                i--;
                            }
                            a.cifre[j]--;

                    }
                    
                }
                else
                {
                    nr = a.cifre[index] - b.cifre[index];
                }
                suma.cifre.Add(nr);
                index++;
            }
            while (index < a.cifre.Count)
            {
                int nr = a.cifre[index];
                if(index + 1 < a.cifre.Count)
                suma.cifre.Add(nr);
                else
                    if(a.cifre[index]!=0) suma.cifre.Add(nr);
                index++;
            }
            while (index < b.cifre.Count)
            {
                int nr = b.cifre[index];
                if (index + 1 < b.cifre.Count)
                    suma.cifre.Add(nr);
                else
                    if (b.cifre[index] != 0) suma.cifre.Add(nr);
                index++;
            }
            //a= before;
            return suma;
        }
        public static BigNumber operator *(BigNumber a, BigNumber b)
        {
            BigNumber produs = new BigNumber();
            for (int i = 0; i < b.cifre.Count; i++)
            {
                for (int j = 0; j < a.cifre.Count; j++)
                {
                    int Produsul = b.cifre[i] * a.cifre[j];
                    if (i + j >= produs.cifre.Count) produs.cifre.Add(0);
                    produs.cifre[i + j] += Produsul;
                }
            }
            int index = 0;
            while (index < produs.cifre.Count)
            {
                int rest = produs.cifre[index] / 10;
                produs.cifre[index] %= 10;
                if (index + 1 >= produs.cifre.Count && rest > 0)
                {
                    produs.cifre.Add(0);
                }
                if (rest > 0)
                {
                    produs.cifre[index + 1] += rest;
                }
                index++;
            }
            return produs;
        }
        public static BigNumber operator /(BigNumber a, BigNumber b)
        {   
            if (b == ZERO) throw new DivideByZeroException();
            if (a == ZERO) return ZERO;
            if (b == ONE) return a;
            if (a == b) return ONE;

            BigNumber r = new BigNumber();
            BigNumber q = new BigNumber();
            int b_semn = b.semn;
            int a_semn = a.semn;
            b.semn = 1; 
            r = a;
            q = ZERO;
            while(r>=b)
            {
                q = q + ONE;
                r = r - b;
            }
            a.semn = a_semn;
            b.semn = b_semn;
            q.semn = a.semn * b.semn;

            return q;
        }
        public BigNumber Power(int a)
        {
            BigNumber nr = ONE;
            for (int i = 1; i <= a; i ++)
            {
                nr *= this;
            }
            return nr;
        }
    }
}
