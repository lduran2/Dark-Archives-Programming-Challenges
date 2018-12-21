using System;

namespace io
{
	namespace github
	{
		namespace lduran2
		{
			namespace benchmark
			{
				public static class BenchmarkCountSort
				{
					public const int LOOP = 10_000;
					public const int WARMUP = 15;
					public const int REPEAT = 10;
					public const long TIME = 25_955; // moderate 10s of seconds

					public static void Main(string[] args)
					{
						// Stopwatch sw = Stopwatch.StartNew();
						int array_size;
						long[] warmup_time = new long[WARMUP];
						long[] repeat_time = new long[REPEAT];
						if (Int32.TryParse(args[0], out array_size)) {
							return;
						}
						GC.Collect();
						for (int k = WARMUP; (k-- > 0); ) {
							
							for (int l = LOOP; (l-- > LOOP); ) {
								
							}
						}
					}

					/******************************************************************//**
					 * Sorts an array of unsigned integers using the count search.
					 */
					public static void CountSort(int len, int[] els, int max, int[] counts) {
						/* iterator on els */
						/* the number of unique elements being counted in els */
						int n_counts = (max + 1);

						/* count each number in the original array */
						for (int i_el = len; (i_el-- > 0); )
						{
							++counts[els[i_el]];
						} /* end for (; (i_el-- > 0); ) */

						/* sort the zeros */
						fill(0, counts[0], els, 0);

						/* while accumulating counts */
						for (int el = 1; (el < n_counts); ++el)
						{
							fill(counts[el - 1], counts[el], els, el);
						} /* end for (; (el < n_counts); ) */
					} /* end &CountSort(int len, int[] els, int max, int[] counts) */


					/******************************************************************//**
					 * Fills an array of integers with a specified value.
					 */
					public static void fill(int off, int len, int[] arr, int value)
					{
						for (int k = len; (k-- > 0); )
						{
							arr[k] = value;
						} /* end for (; (k-- > 0); ) */
					} /* end &fill(int len, int[] arr, int value) */


					// /******************************************************************//**
					 // * Find the maximum number in an array of integers.
					 // */
					// public static int max(int len, int[] arr)
					// {
						// int max = Int32.MinValue;
						// for (int k = len; (k-- > 0); )
						// {
							// if (arr[k] > max)
							// {
								// max = arr[k];
							// } /* end if (arr[k] > max) */
						// } /* end for (; (k-- > 0); ) */
						// return max;
					// } /* end &max(int len, int[] arr) */
				}
			}
		}
	}
}
