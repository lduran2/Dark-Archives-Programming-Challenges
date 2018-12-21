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
					public const int N_LOOPS = 10000;
					public const int N_WARMUPS = 15;
					public const int N_REPEATS = 10;
					public const long TIME = 25955; // moderate 10s of seconds

					public static void Main(string[] args)
					{
						args = new string[1]{"100"};
						// Stopwatch sw = Stopwatch.StartNew();
						long[] warmup_time = new long[N_WARMUPS];
						long[] repeat_time = new long[N_REPEATS];
						int array_size;
						if (!Int32.TryParse(args[0], out array_size))
						{
							return;
						}
						for (int i_round = N_WARMUPS; (i_round-- > 0); )
						{
							int[] array;
							int[] counts;
							Random randoms;
							GC.Collect();
							randoms = new Random();
							array = new int[array_size];
							counts = new int[array_size + 1];
							for (int i_loop = N_LOOPS; (i_loop-- > 0); )
							{
								FillRandom(array_size, array, randoms);
								CountSort(array_size, array, array_size, counts);
							}
							Console.WriteLine(String.Join(", ", array));
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
						Fill(0, counts[0], els, 0);

						/* while accumulating counts */
						for (int el = 1; (el < n_counts); ++el)
						{
							Fill(counts[el - 1], counts[el], els, el);
						} /* end for (; (el < n_counts); ) */
					} /* end &CountSort(int len, int[] els, int max, int[] counts) */


					/******************************************************************//**
					 * Fills an array with random numbers in [1, $len].
					 */
					public static void FillRandom(int len, int[] arr, Random randoms)
					{
						for (int k = len; (k-- > 0); )
						{
							arr[k] = randoms.Next(1, len);
						}
					} /* end &arndfl(size_t len, int* arr) */


					/******************************************************************//**
					 * Fills an array of integers with a specified value.
					 */
					public static void Fill(int off, int len, int[] arr, int value)
					{
						for (int k = len; (k-- > off); )
						{
							arr[k] = value;
						} /* end for (; (k-- > 0); ) */
					} /* end &fill(int len, int[] arr, int value) */


					// /******************************************************************//**
					 // * Find the maximum number in an array of integers.
					 // */
					// public static int Max(int len, int[] arr)
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
