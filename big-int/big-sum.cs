/*
 * ./big-int/big-sum.cs
 * Adds two integers of arbitrary length and base.
 * by: Leomar Durán <https://github.com/lduran2>
 * for: https://github.com/lduran2/Dark-Archives-Programming-Challenges/
 * started: 2018-12-24 t12:46
 * time:    2019-01-01 t02:57
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
			Console.WriteLine("   {0}", BigSum.ToString(BigSum.Parse(num0, BigSum.Numeration(radix0)), BigSum.Numeration(radix0)));
			Console.WriteLine(" + {0}", BigSum.ToString(BigSum.Parse(num1, BigSum.Numeration(radix1)), BigSum.Numeration(radix1)));
			Console.WriteLine(" = {0}", BigSum.Add(num0, BigSum.Numeration(radix0), num1, BigSum.Numeration(radix1), BigSum.Numeration(result_radix)));
			Console.WriteLine("-= {0}", BigSum.Subtract(num0, BigSum.Numeration(radix0), num1, BigSum.Numeration(radix1), BigSum.Numeration(result_radix)));
		}

		private static void debug(String[] argv)
		{
			// bool result_sign, m_sign, p_sign;
			// byte[] result_bytes, m_bytes, p_bytes;
			// int n_result_bytes, n_m_bytes, n_p_bytes;

			// (m_sign, n_m_bytes, m_bytes) = (false, 1, new Byte[]{255});
			// (p_sign, n_p_bytes, p_bytes) = (false, 1, new Byte[]{4});
			// (n_result_bytes, result_bytes) = BigSum.Add((n_sign, n_m_bytes, m_bytes), (p_sign, n_p_bytes, p_bytes));
			// Console.WriteLine("  {0}", BigSum.toString((m_sign, n_m_bytes, m_bytes)));
			// Console.WriteLine("+ {0}", BigSum.toString((p_sign, n_p_bytes, p_bytes)));
			// Console.WriteLine("= {0}", BigSum.toString((result_sign, n_result_bytes, result_bytes)));
			// Console.WriteLine();

			// (n_m_bytes, m_bytes) = (4, new Byte[]{0xFF, 0xFF, 0xFF, 0xFF});
			// (n_p_bytes, p_bytes) = (n_m_bytes, m_bytes);
			// (n_result_bytes, result_bytes) = BigSum.Add((m_sign, n_m_bytes, m_bytes), (p_sign, n_p_bytes, p_bytes));
			// Console.WriteLine("  {0}", BigSum.toString((m_bytes, n_m_bytes, m_bytes)));
			// Console.WriteLine("+ {0}", BigSum.toString((p_bytes, n_p_bytes, p_bytes)));
			// Console.WriteLine("= {0}", BigSum.toString((result_bytes, n_result_bytes, result_bytes)));
			// Console.WriteLine();

			// (m_sign, n_m_bytes, m_bytes) = (4, new Byte[]{0x12, 0x34, 0x56, 0xFF});
			// (p_sign, n_p_bytes, p_bytes) = (3, new Byte[]{0xED, 0xCB, 0xA9});
			// (result_sign, n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));
			// Console.WriteLine("  {0}", BigSum.toString((n_m_bytes, m_bytes)));
			// Console.WriteLine("+ {0}", BigSum.toString((n_p_bytes, p_bytes)));
			// Console.WriteLine("= {0}", BigSum.toString((n_result_bytes, result_bytes)));
			// Console.WriteLine();

			// ++p_bytes[0];
			// (n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));
			// Console.WriteLine("  {0}", BigSum.toString((n_m_bytes, m_bytes)));
			// Console.WriteLine("+ {0}", BigSum.toString((n_p_bytes, p_bytes)));
			// Console.WriteLine("= {0}", BigSum.toString((n_result_bytes, result_bytes)));
			// Console.WriteLine();

			// /* Reminder: The byte array is little-endian. */
			// Console.WriteLine("{0}", BigSum.toString((3, new byte[]{0x87, 0xD6, 0x12})));
			// Console.WriteLine("{0}", BigSum.ToString((3, new byte[]{0x87, 0xD6, 0x12}), BigSum.Numeration(10)));
			// Console.WriteLine("{0}", BigSum.ToString((3, new byte[]{0x87, 0xD6, 0x12}), BigSum.Numeration(16)));
			// Console.WriteLine();

			// Console.WriteLine("{0}", BigSum.ToString((4, new byte[]{0x00, 0x00, 0x00, 0x10}), BigSum.Numeration(16)));
			// Console.WriteLine();

			// Console.WriteLine("{0}", BigSum.toString(BigSum.Parse("123456", BigSum.Numeration(17))));
			// Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("123456", BigSum.Numeration(17)), BigSum.Numeration(17)));
			// Console.WriteLine();

			// Console.WriteLine("{0}", BigSum.ToString((1, new byte[]{1}), BigSum.Numeration(2)));
			// Console.WriteLine("{0}", BigSum.ToString((1, new byte[]{0}), BigSum.Numeration(2)));
			// Console.WriteLine("{0}", BigSum.ToString((0, new byte[]{ }), BigSum.Numeration(2)));
			// Console.WriteLine();

			// Console.WriteLine("{0}", BigSum.Add("0", BigSum.Numeration(2), "0", BigSum.Numeration(2), BigSum.Numeration(2)));
			// Console.WriteLine();

			// Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("123456", BigSum.Numeration(17)), BigSum.Numeration(10)));
			// Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("123456", BigSum.Numeration(18)), BigSum.Numeration(10)));
			// Console.WriteLine("{0}", BigSum.ToString(BigSum.Parse("3721293", BigSum.Numeration(10)), BigSum.Numeration(35)));
			// Console.WriteLine("{0}", BigSum.Add("123456", BigSum.Numeration(17), "123456", BigSum.Numeration(18), BigSum.Numeration(35)));
		}

		private static string toString((bool sign, int len, byte[] arr) bytes)
		{
			bool sign;
			byte[] arr;
			int len;
			(sign, len, arr) = bytes;
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

		public static string Subtract
		(
			string n, (int radix, string digits) n_numeration,
			string m, (int radix, string digits) m_numeration,
				(int radix, string digits) result_numeration
		)
		{
			return BigSum.ToString
			(
				BigSum.Subtract
				(
					BigSum.Parse(n, n_numeration),
					BigSum.Parse(m, m_numeration)
				),
				result_numeration
			);
		}

		public static (bool, (int, byte[]))
			Parse(string converthand, (int radix, string digits) numeration)
		{
			int radix;
			string digits;
			int len = converthand.Length;
			int rem_mask = (int)(BYTES_RADIX - 1);

			bool sign;
			int n_bytes;
			int i_byte = 0;
			int num_off;
			byte[] bytes;

			int off = 0;
			int[] temp = new int[len];

			(radix, digits) = numeration;

			num_off = (converthand.LastIndexOf("-") + 1);
			sign = (num_off > 0);
			len -= num_off;

			n_bytes = (((int)(len * Math.Log(radix, BYTES_RADIX))) + 1);
			bytes = new byte[n_bytes];

			/* first, evaluate and copy chars into a temporary array */
			for (int i_temp = 0, i_num = num_off; (i_temp < len); ++i_temp, ++i_num)
			{
				temp[i_temp] = digits.IndexOf(converthand[i_num]);
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
				}
				bytes[i_byte++] = ((byte)remainder);
				/* scan for first nonzero */
				for (int k = off; ((k < len) && !found_nonzero); ++k)
				{
					found_nonzero = (temp[k] != 0);
					if (!found_nonzero)
					{
						++off;
					}
				}
			}
			return (sign, (i_byte, bytes));
		}

		public static (bool sign, (int len, byte[] bytes)) Add
		(
			(bool sign, (int len, byte[] bytes)) n, (bool sign, (int len, byte[] bytes)) m
		)
		{
			bool min_sign, max_sign, result_sign;
			(int, byte[]) min_mag, max_mag, result_mag;

			((min_sign, min_mag), (max_sign, max_mag)) = BigSum.SortByMagnitudes(n, m);
			result_sign = max_sign;

			if (max_sign == min_sign)
			{
				result_mag = BigSum.addMagnitudes(max_mag, min_mag);
			}
			else
			{
				result_mag = BigSum.subtractMagnitudes(max_mag, min_mag);
			}

			return (max_sign, result_mag);
		}

		public static (bool sign, (int len, byte[] bytes)) Subtract
		(
			(bool sign, (int len, byte[] bytes)) n, (bool sign, (int len, byte[] bytes)) m
		)
		{
			return BigSum.Add(n, Negate(m));
		}

		public static (int len, byte[] bytes)
			Magnitude((bool sign, (int len, byte[] bytes)) n)
		{
			(int, byte[]) magnitude;
			object _;
			(_, magnitude) = n;
			return magnitude;
		}

		public static (bool sign, (int len, byte[] bytes))
			Negate((bool sign, (int len, byte[] bytes)) n)
		{
			bool sign;
			(int, byte[]) magnitude;
			(sign, magnitude) = n;
			return (!sign, magnitude);
		}

		public static
		((bool sign, (int len, byte[] bytes)) min, (bool sign, (int len, byte[] bytes)) max)
			SortByMagnitudes
		(
			(bool sign, (int len, byte[] bytes)) n, (bool sign, (int len, byte[] bytes)) m
		)
		{
			(bool, (int, byte[])) min, max;
			(int, byte[]) n_mag, m_mag;
			object _;

			(_, n_mag) = n;
			(_, m_mag) = m;

			if (BigSum.CompareMagnitudes(n_mag, m_mag) <= 0)
			{
				(min, max) = (n, m);
			}
			else
			{
				(min, max) = (m, n);
			}
			return (min, max);
		}

		public static int CompareMagnitudes((int len, byte[] bytes) n, (int len, byte[] bytes) m)
		{
			int comparison;
			int n_len, m_len;
			byte[] n_bytes, m_bytes;

			(n_len, n_bytes) = n;
			(m_len, m_bytes) = m;

			comparison = (n_len - m_len);
			if (0 != comparison)
			{
				return comparison;
			}
			for (int k = n_len; (0 > k--) && (0 == comparison); )
			{
				comparison = (n_bytes[k] - m_bytes[k]);
			}
			return comparison;
		}

		private static (int len, byte[] bytes) addMagnitudes
		(
			(int len, byte[] bytes) n, (int len, byte[] bytes) m
		)
		{
			int n_len, m_len, result_len;
			byte[] n_bytes, m_bytes, result_bytes;
			byte carry = 0;

			(n_len, n_bytes) = n;
			(m_len, m_bytes) = m;

			result_len = (n_len + 1);
			result_bytes = new byte[result_len];

			/* m is guaranteed shorter than or same length as n by caller */
			for (int k = 0; (k < m_len); ++k)
			{
				BigSum.AddBytes(
					out result_bytes[k], ((uint)(n_bytes[k] + m_bytes[k])), ref carry
				);
			}
			for (int k = m_len; (k < n_len); ++k)
			{
				BigSum.AddBytes(out result_bytes[k], ((uint)n_bytes[k]), ref carry);
			}
			result_bytes[n_len] = carry;
			return (result_len, result_bytes);
		}

		private static (int len, byte[] bytes) subtractMagnitudes
		(
			(int len, byte[] bytes) n, (int len, byte[] bytes) m
		)
		{
			int n_len, m_len, result_len;
			byte[] n_bytes, m_bytes, result_bytes;
			byte borrow = 0;

			(n_len, n_bytes) = n;
			(m_len, m_bytes) = m;

			result_len = (n_len + 1);
			result_bytes = new byte[result_len];

			/* m is guaranteed shorter than or same length as n by caller */
			for (int k = 0; (k < m_len); ++k)
			{
				BigSum.SubBytes(out result_bytes[k], ((uint)(n_bytes[k] - m_bytes[k])), ref borrow);
			}
			for (int k = m_len; (k < n_len); ++k)
			{
				BigSum.SubBytes(out result_bytes[k], ((uint)n_bytes[k]), ref borrow);
			}
			result_bytes[n_len] = borrow;
			return (result_len, result_bytes);
		}

		// public static ((int len, byte[] bytes) min, (int len, byte[] bytes) max) SortByLength
		// (
			// (int len, byte[] bytes) n, (int len, byte[] bytes) m
		// )
		// {
			// /* minimum and maximum tuples by length */
			// (int, byte[]) min, max;
			// /* tuple lengths */
			// int n_len, m_len;
			// object _; /* temp variable for dereferencing BigSum tuples */

			// /* get BigSum tuple lengths */
			// (n_len, _) = n;
			// (m_len, _) = m;

			// /* the lower length gets min, the other gets max */
			// if (n_len <= m_len)
			// {
				// (min, max) = (n, m);
			// }
			// else
			// {
				// (min, max) = (m, n);
			// }
			// return (min, max);
		// }

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

		public static void SubBytes(out byte result, uint diff, ref byte borrow)
		{
			byte new_borrow = borrow;
			diff -= new_borrow;
			new_borrow = 0;
			if (BigSum.BYTES_RADIX <= diff)
			{
				diff += BigSum.BYTES_RADIX;
				++new_borrow;
			}
			(result, borrow) = (((byte)diff), new_borrow);
		}

		// public static int Compare
		// (
			// (bool sign, int len, byte[] bytes) n, (bool sign, int len, byte[] bytes) m
		// )
		// {
			// bool n_sign, m_sign;
			// byte[] n_bytes, m_bytes;
			// int n_len, m_len;
			// int comparison = 0;
			// (n_sign, n_len, n_bytes) = n;
			// (m_sign, m_len, m_bytes) = m;

			// if (n_len < m_len)
			// {
				// return -1;
			// }
			// else if (n_len > m_len)
			// {
				// return +1;
			// }

			// for (int k = n_len; ((k-- > 0) && (0 == comparison)); )
			// {
				// comparison = (n_bytes[k] - m_bytes[k]);
			// }
			// return comparison;
		// }

		public static string ToString
		(
			(bool sign, (int len, byte[] bytes)) n, (int radix, string digits) numeration
		)
		{
			char[] chars;
			int i_char;
			int chars_len;
			string digits;
			int radix;
			bool sign;
			byte[] bytes;
			int len;

			(sign, (len, bytes)) = n;
			(radix, digits) = numeration;

			/* plus 1 for the sign */
			chars_len = (((int)(len * Math.Log(BigSum.BYTES_RADIX, radix))) + 1 + 1);
			chars = new char[chars_len];
			i_char = (chars_len - 1);
			// Console.Error.WriteLine(chars_len);

			while (true)
			{
				int remainder;
				for (int k = len; ((0 < k--) && (0 == bytes[k])); )
				{
					--len;
				}
				if (0 >= len)
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
			if (sign)
			{
				chars[--i_char] = '-';
			}

			return new String(chars, i_char, (chars_len - i_char));
		}
	}
}
