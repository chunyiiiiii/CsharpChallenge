using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigNumber
{
    public class BigNumber
    {

        static void Main()
        {
            BigNumber bn1 = new BigNumber("123456789123456789123456789123456789123456789");
            BigNumber bn2 = new BigNumber("987654321987654321987654321987654321987654321");
            BigNumber bn3 = bn1 + bn2;
            BigNumber bn4 = bn1 - bn2;
            BigNumber bn5 = bn1 * bn2;
            BigNumber bn6 = bn1 / bn2;
            BigNumber bn7 = bn1 % bn2;

            Console.WriteLine($"bn1 = {bn1}");
            Console.WriteLine($"bn2 = {bn2}");
            Console.WriteLine($"bn3 = {bn3}");
            Console.WriteLine($"bn4 = {bn4}");
            Console.WriteLine($"bn5 = {bn5}");
            Console.WriteLine($"bn6 = {bn6}");
            Console.WriteLine($"bn7 = {bn7}");
            Console.ReadKey();
        }

        public List<int> bits = new List<int>();
        public bool isNegative = false; 

        // bit length
        public int Length { get => bits.Count; }


        public BigNumber() { }

        public BigNumber(string s)
        {
            for (int i = 0; i < s.Length; i++)
                bits.Add(s[s.Length - 1 - i] - 48);
        }


        public void Norminalize(bool removeZeros = true)
        {
            bits.Add(0);
            for (int i = 0; i < Length - 1; i++)
            {
                bits[i + 1] += bits[i] / 10;
                bits[i] = bits[i] % 10;
            }

            if (removeZeros)
                while (bits.Count > 0 && bits[bits.Count - 1] == 0)
                    bits.RemoveAt(bits.Count - 1);
        }


        public static BigNumber operator *(BigNumber bn1, BigNumber bn2)
        {
            BigNumber result = new BigNumber();

            for (int i = 0; i <= bn1.Length + bn2.Length; i++)
                result.bits.Add(0);


            for (int i = 0; i < bn2.Length; i++)
            {
                for (int j = 0; j < bn1.Length; j++)
                {
                    result.bits[j + i] += bn1.bits[j] * bn2.bits[i];
                }
                result.Norminalize(false);
            }

            result.Norminalize();

            return result;
        }

        public static BigNumber operator +(BigNumber bn1, BigNumber bn2)
        {
            BigNumber result = new BigNumber();
            int maxLength = Math.Max(bn1.Length, bn2.Length);

            for (int i = 0; i < maxLength; i++)
            {
                int sum = (i < bn1.Length ? bn1.bits[i] : 0) + (i < bn2.Length ? bn2.bits[i] : 0);
                result.bits.Add(sum);
            }

            result.Norminalize();

            return result;
        }

        public static BigNumber operator -(BigNumber bn1, BigNumber bn2)
        {
            BigNumber result = new BigNumber();
            int maxLength = Math.Max(bn1.Length, bn2.Length);
            int borrow = 0;
            bool isNegative = false;

            // Determine if the result will be negative
            if (bn1 < bn2)
            {
                BigNumber temp = bn1;
                bn1 = bn2;
                bn2 = temp;
                isNegative = true;
            }

            for (int i = 0; i < maxLength; i++)
            {
                int diff = (i < bn1.Length ? bn1.bits[i] : 0) - (i < bn2.Length ? bn2.bits[i] : 0) - borrow;
                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                result.bits.Add(diff);
            }

            result.Norminalize();

            if (isNegative)
            {
                result = new BigNumber(result.ToString()) { isNegative = true };
            }

            return result;
        }
        public static BigNumber operator /(BigNumber bn1, BigNumber bn2)
        {
            BigNumber result = new BigNumber();
            BigNumber remainder = new BigNumber(bn1.ToString());
            BigNumber divisor = new BigNumber(bn2.ToString());

            if (divisor == new BigNumber("0"))
            {
                throw new DivideByZeroException("Division by zero error.");
            }

            while (remainder >= divisor)
            {
                int count = 0;
                while (remainder >= divisor)
                {
                    remainder -= divisor;
                    count++;
                }
                result.bits.Insert(0, count);
                remainder.Norminalize();
            }

            return result;
        }

        public static BigNumber operator %(BigNumber bn1, BigNumber bn2)
        {
            BigNumber remainder = new BigNumber(bn1.ToString());
            BigNumber divisor = new BigNumber(bn2.ToString());

            if (divisor == new BigNumber("0"))
            {
                throw new DivideByZeroException("Division by zero error.");
            }

            while (remainder >= divisor)
            {
                remainder -= divisor;
            }

            return remainder;
        }

        public static bool operator >=(BigNumber bn1, BigNumber bn2)
        {
            if (bn1.Length > bn2.Length)
            {
                return true;
            }
            else if (bn1.Length < bn2.Length)
            {
                return false;
            }
            else
            {
                for (int i = bn1.Length - 1; i >= 0; i--)
                {
                    if (bn1.bits[i] > bn2.bits[i])
                    {
                        return true;
                    }
                    else if (bn1.bits[i] < bn2.bits[i])
                    {
                        return false;
                    }
                }
                return true; // If all digits are equal, consider them equal
            }
        }

        public static bool operator <=(BigNumber bn1, BigNumber bn2)
        {
            return bn1 == bn2 || bn1 < bn2;
        }

        public static bool operator >(BigNumber bn1, BigNumber bn2)
        {
            return !(bn1 <= bn2);
        }

        public static bool operator <(BigNumber bn1, BigNumber bn2)
        {
            return !(bn1 >= bn2);
        }

        public static bool operator ==(BigNumber bn1, BigNumber bn2)
        {
            if (bn1.Length != bn2.Length)
            {
                return false;
            }

            for (int i = 0; i < bn1.Length; i++)
            {
                if (bn1.bits[i] != bn2.bits[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(BigNumber bn1, BigNumber bn2)
        {
            return !(bn1 == bn2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (isNegative)
            {
                sb.Append("-");
            }

            if (bits.Count == 0)
            {
                sb.Append("0");
            }
            else
            {
                for (int i = bits.Count - 1; i >= 0; i--)
                {
                    sb.Append(bits[i]);
                }
            }

            return sb.ToString();
        }
    }
}
