using System;
using System.Diagnostics;

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
					public const int N_LOOPS = 10_000;
					public static readonly int[] N_TRIALS = {15, 10};
					public const int N_TRIAL_TYPES = 2;
					public static readonly int N_WARMUPS = N_TRIALS[0];
					public static readonly int N_REPEATS = N_TRIALS[1];
					public const long TIME = 25_955; // moderate 10s of seconds

					public static void Main(string[] args)
					{
						long[,] timings;
						int array_size;
						if (args.Length == 0) args = new string[1]{"100"};
						timings = new long[N_WARMUPS,N_LOOPS];
						if (!Int32.TryParse(args[0], out array_size))
						{
							return;
						}
						for (int i_trial_type = 0; (i_trial_type < N_TRIAL_TYPES); i_trial_type++)
						{
							for (int i_round = N_TRIALS[i_trial_type]; (i_round-- > 0); )
							{
								long start;
								long finish;
								int[] array;
								int[] counts;
								int n_counts = (array_size + 1);
								Random randoms;

								GC.Collect();
								randoms = new Random();
								array = new int[array_size];
								counts = new int[n_counts];

								for (int i_loop = N_LOOPS; (i_loop-- > 0); )
								{
									FillRandom(array_size, array, randoms);
									start = Stopwatch.GetTimestamp();
									Fill(0, n_counts, counts, 0);
									CountSort(array_size, array, n_counts, counts);
									finish = Stopwatch.GetTimestamp();
									timings[i_round, i_loop] = (finish - start);
								}

								Console.Error.Write("{0} ", array[array_size-1]);
							}
						}
						Console.Error.WriteLine();
						for (int i_round = N_REPEATS; (i_round-- > 0); )
						{
							double mean;
							double time_sum = 0;
							double min;
							double max;
							long min_ticks = Int64.MaxValue;
							long max_ticks = Int64.MinValue;
							for (int i_loop = N_LOOPS; (i_loop-- > 0); )
							{
								time_sum += ((double)timings[i_round, i_loop]);
								if (timings[i_round, i_loop] < min_ticks)
								{
									// Console.WriteLine(">>[min]> {0}>{1}", min_ticks, timings[i_round, i_loop]);
									min_ticks = timings[i_round, i_loop];
								}
								if (timings[i_round, i_loop] > max_ticks)
								{
									// Console.WriteLine(">>[max]> {0}<{1}", max_ticks, timings[i_round, i_loop]);
									max_ticks = timings[i_round, i_loop];
								}
							}
							mean = ((((time_sum / N_LOOPS) * 1.0e+6)) / Stopwatch.Frequency);
							min = ((min_ticks * 1.0e+6) / Stopwatch.Frequency);
							max = ((max_ticks * 1.0e+6) / Stopwatch.Frequency);
							Console.WriteLine("min : {0}", min);
							Console.WriteLine("mean: {0}", mean);
							Console.WriteLine("max : {0}", max);
							Console.WriteLine();
						}
					}

					/******************************************************************//**
					 * Sorts an array of unsigned integers using the count search.
					 */
					public static void CountSort(int len, int[] els, int n_counts, int[] counts) {
						/* the number of unique elements being counted in els */

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
							counts[el] += counts[el - 1];
							Fill(counts[el - 1], counts[el], els, el);
						} /* end for (; (el < n_counts); ) */
					} /* end &CountSort(int len, int[] els, int n_counts, int[] counts) */


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
