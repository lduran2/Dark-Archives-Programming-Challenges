/*
 * ./big-int/big-sum.cs
 * Adds two integers of arbitrary length and base.
 * by: Leomar Durán <https://github.com/lduran2>
 * for: https://github.com/lduran2/Dark-Archives-Programming-Challenges/
 * started: 2018-12-24 t12:46
 * time:    2018-12-30 t21:46
 */
using System;

namespace io.github.lduran2.math
{
	public static class BigSum
	{
		public const string ALPHANUMERIC_DIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const int MIN_RADIX = 2;
		public readonly static int MAX_RADIX = ALPHANUMERIC_DIGITS.Length;

		public const uint BYTES_RADIX = ((uint)(Byte.MaxValue + 1));

		// public static void Main(String[] argv)
		// {
			// BigSum.debug(argv);
		// }

		public static void Main(String[] argv)
		{
			string num0 = argv[0];
			int radix0 = Int32.Parse(argv[1]);
			string num1 = argv[2];
			int radix1 = Int32.Parse(argv[3]);
			int result_radix = Int32.Parse(argv[4]);
			Console.WriteLine("{0}", BigSum.Add(num0, BigSum.Numeration(radix0), num1, BigSum.Numeration(radix1), BigSum.Numeration(result_radix)));
		}

		private static void debug(String[] argv)
		{
			byte[] result_bytes, m_bytes, p_bytes;
			int n_result_bytes, n_m_bytes, n_p_bytes;

			(n_m_bytes, m_bytes) = (1, new Byte[]{255});
			(n_p_bytes, p_bytes) = (1, new Byte[]{4});
			(n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));
			Console.WriteLine("  {0}", BigSum.toString((n_m_bytes, m_bytes)));
			Console.WriteLine("+ {0}", BigSum.toString((n_p_bytes, p_bytes)));
			Console.WriteLine("= {0}", BigSum.toString((n_result_bytes, result_bytes)));
			Console.WriteLine();

			(n_m_bytes, m_bytes) = (4, new Byte[]{0xFF, 0xFF, 0xFF, 0xFF});
			(n_p_bytes, p_bytes) = (n_m_bytes, m_bytes);
			(n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));
			Console.WriteLine("  {0}", BigSum.toString((n_m_bytes, m_bytes)));
			Console.WriteLine("+ {0}", BigSum.toString((n_p_bytes, p_bytes)));
			Console.WriteLine("= {0}", BigSum.toString((n_result_bytes, result_bytes)));
			Console.WriteLine();

			(n_m_bytes, m_bytes) = (4, new Byte[]{0x12, 0x34, 0x56, 0xFF});
			(n_p_bytes, p_bytes) = (3, new Byte[]{0xED, 0xCB, 0xA9});
			(n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));
			Console.WriteLine("  {0}", BigSum.toString((n_m_bytes, m_bytes)));
			Console.WriteLine("+ {0}", BigSum.toString((n_p_bytes, p_bytes)));
			Console.WriteLine("= {0}", BigSum.toString((n_result_bytes, result_bytes)));
			Console.WriteLine();

			++p_bytes[0];
			(n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));
			Console.WriteLine("  {0}", BigSum.toString((n_m_bytes, m_bytes)));
			Console.WriteLine("+ {0}", BigSum.toString((n_p_bytes, p_bytes)));
			Console.WriteLine("= {0}", BigSum.toString((n_result_bytes, result_bytes)));
			Console.WriteLine();

			/* Reminder: The byte array is little-endian. */
			Console.WriteLine("{0}", BigSum.toString((3, new byte[]{0x87, 0xD6, 0x12})));
			Console.WriteLine("{0}", BigSum.ToString((3, new byte[]{0x87, 0xD6, 0x12}), BigSum.Numeration(10)));
			Console.WriteLine("{0}", BigSum.ToString((3, new byte[]{0x87, 0xD6, 0x12}), BigSum.Numeration(16)));
			Console.WriteLine();

			Console.WriteLine("{0}", BigSum.ToString((4, new byte[]{0x00, 0x00, 0x00, 0x10}), BigSum.Numeration(16)));
			Console.WriteLine();

			Console.WriteLine("{0}", BigSum.toString(BigSum.Parse("123456", BigSum.Numeration(17))));
			Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("123456", BigSum.Numeration(17)), BigSum.Numeration(17)));
			Console.WriteLine();

			Console.WriteLine("{0}", BigSum.ToString((1, new byte[]{1}), BigSum.Numeration(2)));
			Console.WriteLine("{0}", BigSum.ToString((1, new byte[]{0}), BigSum.Numeration(2)));
			Console.WriteLine("{0}", BigSum.ToString((0, new byte[]{ }), BigSum.Numeration(2)));
			Console.WriteLine();

			Console.WriteLine("{0}", BigSum.Add("0", BigSum.Numeration(2), "0", BigSum.Numeration(2), BigSum.Numeration(2)));
			Console.WriteLine();

			Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("123456", BigSum.Numeration(17)), BigSum.Numeration(10)));
			Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("123456", BigSum.Numeration(18)), BigSum.Numeration(10)));
			Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("3721293", BigSum.Numeration(10)), BigSum.Numeration(35)));
			Console.WriteLine("{0}", BigSum.Add("123456", BigSum.Numeration(17), "123456", BigSum.Numeration(18), BigSum.Numeration(35)));
		}

		private static string toString((int len, byte[] arr) bytes)
		{
			int len;
			byte[] arr;
			(len, arr) = bytes;
			return String.Format("[{0}]", String.Join(", ", arr));
		}

		public static (int radix, string digits) Numeration(int radix)
		{
			if ((radix < 2) || (radix > 36))
			{
				throw new ArgumentException(String.Format("Radix must be in [2, 36]: {0}", radix));
			}
			return (radix, ALPHANUMERIC_DIGITS);
		}

		public static string Add
		(
			string n, (int radix, string digits) n_numeration,
			string m, (int radix, string digits) m_numeration,
				(int radix, string digits) result_numeration
		)
		{
			return BigSum.ToString
			(
				BigSum.Add
				(
					BigSum.Parse(n, n_numeration),
					BigSum.Parse(m, m_numeration)
				),
				result_numeration
			);
		}

		public static (int, byte[]) Parse(string converthand, (int radix, string digits) numeration)
		{
			int radix;
			string digits;
			int len = converthand.Length;
			int rem_mask = (int)(BYTES_RADIX - 1);

			int n_bytes = (((int)(len * Math.Log(17, 256))) + 1);
			// Console.WriteLine(n_bytes);
			int i_byte = 0;
			byte[] bytes = new byte[n_bytes];

			int off = 0;
			int[] temp = new int[len];

			(radix, digits) = numeration;

			/* first, evaluate and copy chars into a temporary array */
			for (int k = 0; (k < len); ++k)
			{
				temp[k] = digits.IndexOf(converthand[k]);
			}

			/* afterward, divide each value in the temp array by 256 */
			/* each remainder is prepended a digit before division */
			/* the last remainder is the next least significant digit */
			/* step up the offset for all leading zeros */
			while (off < len)
			{
				bool found_nonzero = false;
				int remainder = 0;
				for (int k = off; (k < len); ++k)
				{
					int digit_value = ((remainder * radix) + temp[k]);
					int quotient = (digit_value >> 8);
					remainder = (digit_value & rem_mask);
					temp[k] = quotient;
					if (quotient != 0)
					{
						found_nonzero = true;
					}
					else if (!found_nonzero)
					{
						++off;
					}
				}
				bytes[i_byte++] = ((byte)remainder);
				// Console.WriteLine(bytes[i_byte - 1]);
			}
			// Console.WriteLine(i_byte);
			return (i_byte, bytes);
		}

		public static (int len, byte[] bytes)
			Add((int len, byte[] bytes) n, (int len, byte[] bytes) m)
		{
			byte[] result;
			int result_len;
			byte[] min, max;
			int min_len, max_len;
			byte carry = 0;

			((min_len, min), (max_len, max)) = BigSum.Sort(n, m);
			result_len = (max_len + 1);
			result = new byte[result_len];

			for (int k = 0; (k < min_len); ++k)
			{
				BigSum.AddBytes(out result[k], ((uint)(min[k] + max[k])), ref carry);
			}
			for (int k = min_len; (k < max_len); ++k)
			{
				BigSum.AddBytes(out result[k], ((uint)max[k]), ref carry);
			}
			result[max_len] = carry;
			return (result_len, result);
		}

		public static void AddBytes(out byte result, uint sum, ref byte carry)
		{
			byte new_carry = carry;
			sum += new_carry;
			new_carry = 0;
			if (BigSum.BYTES_RADIX <= sum)
			{
				sum -= BigSum.BYTES_RADIX;
				++new_carry;
			}
			(result, carry) = (((byte)sum), new_carry);
		}

		public static ((int len, byte[] bytes) min, (int len, byte[] bytes) max)
			Sort((int len, byte[] bytes) n, (int len, byte[] bytes) m)
		{
			byte[] min_bytes, max_bytes;
			int min_len, max_len;

			if (n.Item1 <= m.Item1)
			{
				((min_len, min_bytes), (max_len, max_bytes)) = (n, m);
			}
			else
			{
				((min_len, min_bytes), (max_len, max_bytes)) = (m, n);
			}
			return ((min_len, min_bytes), (max_len, max_bytes));
		}

		public static string ToString((int len, byte[] bytes) n, (int radix, string digits) numeration)
		{
			char[] chars;
			int i_char;
			int chars_len;
			string digits;
			int radix;
			byte[] bytes;
			int len;

			(len, bytes) = n;
			(radix, digits) = numeration;

			chars_len = (((int)(len * Math.Log(BigSum.BYTES_RADIX, radix))) + 1);
			chars = new char[chars_len];
			i_char = (chars_len - 1);
			// Console.Error.WriteLine(chars_len);

			while (true)
			{
				int remainder;
				for (int k = len; ((k-- > 0) && (0 == bytes[k])); )
				{
					--len;
				}
				if (len <= 0)
				{
					break;
				}
				/*
				 *	Example:
				 *
				 *	   1  234  567
				 *	--------------
				 *	  12   D6   87
				 *	--------------
				 *	  18, 214, 135
				 *	/ 10
				 *	=  1, 226,  64 R 7
				 *	=  0,  48,  57 R 6
				 *	=  0,   4, 210 R 5
				 *	=  0,   0, 123 R 4
				 *	=  0,   0,  12 R 3
				 *	=  0,   0,   1 R 2
				 *	=  0,   0,   0 R 1
				 *	-----------------------------------
				 *	  18, 214, 135[256] = 1,234,567[10]
				 *	-----------------------------------------
				 *	  1 R 8 =                        18 /10
				 *	        = ((((   0 % 10)*256) +  18)/10)
				 *	226 R 2 = ((((  18 % 10)*256) + 214)/10)
				 *	 64 R 7 = ((((2262 % 10)*256) + 135)/10)
				 *	  7     =    ( 647 % 10)
				 */
				remainder = 0;
				for (int k = len; (k-- > 0); )
				{
					int dividend = ((remainder * 256) + bytes[k]);
					int quotient = (dividend / radix);
					remainder = (dividend - (quotient * radix));
					bytes[k] = ((byte)quotient);
				}
				// Console.Error.WriteLine("{0} {1} {2}", new String(chars, (i_char + 1), (chars_len - 1 - i_char)), remainder, len);
				chars[i_char] = digits[remainder];
				--i_char;
			}
			while (i_char < 0)
			{
				++i_char;
			}
			while (true)
			{
				if (chars_len <= i_char)
				{
					return digits.Substring(0, 1);
				}
				else if (0 != chars[i_char])
				{
					break;
				}
				++i_char;
			}

			return new String(chars, i_char, (chars_len - i_char));
		}
	}
}
