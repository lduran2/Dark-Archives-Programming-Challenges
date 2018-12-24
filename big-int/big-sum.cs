/*
 * ./big-int/big-sum.cs
 * Adds two integers of arbitrary length and base.
 * by: Leomar Durán <https://github.com/lduran2>
 * for: https://github.com/lduran2/Dark-Archives-Programming-Challenges/
 * time: 2018-12-24 t17:00
 */
using System;

namespace io.github.lduran2.math
{
	public static class BigSum
	{
		public const string ALPHANUMERIC_DIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const int MIN_RADIX = 2;
		public readonly static int MAX_RADIX = ALPHANUMERIC_DIGITS.Length;

		public const uint BYTE_CARRY = (((uint)Byte.MaxValue) + 1);

		public static void Main(String[] argv)
		{
			byte[] result_bytes, m_bytes, p_bytes;
			int n_result_bytes, n_m_bytes, n_p_bytes;

			(n_m_bytes, m_bytes) = (4, new Byte[]{0xFF, 0xFF, 0xFF, 0xFF});
			(n_p_bytes, p_bytes) = (n_m_bytes, m_bytes);
			// (n_m_bytes, m_bytes) = (4, new Byte[]{0x12, 0x34, 0x56, 0xFF});
			// (n_p_bytes, p_bytes) = (3, new Byte[]{0xEE, 0xCB, 0xA9});
			// (n_m_bytes, m_bytes) = (1, new Byte[]{255});
			// (n_p_bytes, p_bytes) = (1, new Byte[]{4});

			(n_result_bytes, result_bytes) = BigSum.Add((n_m_bytes, m_bytes), (n_p_bytes, p_bytes));

			Console.WriteLine("  {0}", BigSum.toString(n_m_bytes, m_bytes));
			Console.WriteLine("+ {0}", BigSum.toString(n_p_bytes, p_bytes));
			Console.WriteLine("= {0}", BigSum.toString(n_result_bytes, result_bytes));
		}

		public static (int radix, string digits) Numeration(int radix)
		{
			if ((radix < 2) || (radix > 36))
			{
				throw new ArgumentException(String.Format("Radix must be in [2, 36]: {0}", radix));
			}
			return (radix, ALPHANUMERIC_DIGITS.Substring(0, radix));
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

		public static (int, byte[]) Parse(string n, (int radix, string digits) numeration)
		{
			return (0, new byte[0]);
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
				BigSum.AddBytes(out result[k], (uint)(min[k] + max[k]), ref carry);
			}
			for (int k = min_len; (k < max_len); ++k)
			{
				BigSum.AddBytes(out result[k], (uint)max[k], ref carry);
			}
			result[max_len] = carry;
			return (result_len, result);
		}

		public static void AddBytes(out byte result, uint sum, ref byte carry)
		{
			sum += carry;
			carry = 0;
			if (sum >= BigSum.BYTE_CARRY)
			{
				sum -= BigSum.BYTE_CARRY;
				carry += 1;
			}
			result = ((byte)sum);
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
			return null;
		}

		private static string toString(int n_bytes, byte[] bytes)
		{
			return String.Format("[{0}]", String.Join(", ", bytes));
		}

	}
}
