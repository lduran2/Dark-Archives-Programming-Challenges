using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
					// public const long TIME = 25_955; // moderate 10s of seconds

					public static void Main(string[] args)
					{
						long[,] timings;
						int array_size;

						timings = new long[N_WARMUPS, N_LOOPS];

						/* parse the arguments */
						/* if no size is given, default to 100 */
						if (args.Length == 0) args = new string[1]{"100"};
						/* if another bad value is given, stop */
						if (!Int32.TryParse(args[0], out array_size))
						{
							return;
						}

						/* first do a warmup round, then the actual round */
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

								/* cleanup before each round */
								GC.Collect();
								randoms = new Random();
								array = new int[array_size];
								counts = new int[n_counts];

								/* sort $N_LOOPS times */
								for (int i_loop = N_LOOPS; (i_loop-- > 0); )
								{
									/* prepare random array */
									FillRandom(array_size, array, randoms);
									/* begin timing */
									start = Stopwatch.GetTimestamp();
									/* cleanup counts array */
									Fill(0, n_counts, counts, 0);
									/* sort the random array */
									CountSort(array_size, array, n_counts, counts);
									/* end timing */
									finish = Stopwatch.GetTimestamp();
									timings[i_round, i_loop] = (finish - start);
								}

								/* print the last element of the last array to make sure the
								   VM sorts every array completely */
								Console.Error.Write("{0} ", array[array_size-1]);
							}
						}
						Console.Error.WriteLine();

						/* print the summaries */
						for (int i_round = 1; (i_round <= N_REPEATS); i_round++)
						{
							/* mean related */
							double ari_mean;
							double ari_time_sum = 0;
							int left;
							int right;
							bool isInlier = true;
							double har_mean;
							double har_time_sum = 0;
							/* minimum and maximum */
							double min;
							double max;
							long min_ticks;
							long max_ticks;

							/* quartile related */
							double inlier_min;
							double inlier_max;
							double Q1;
							double Q2;
							double Q3;
							long[] round_timings = new long[N_LOOPS];
							double IQR;
							double Q1_ticks;
							double Q2_ticks;
							double Q3_ticks;
							int iQ1;
							int iQ2;

							/* copy the timings before starting */
							Buffer.BlockCopy(timings, i_round * N_LOOPS * Marshal.SizeOf<long>(), round_timings, 0, N_LOOPS * Marshal.SizeOf<long>());
							/* sort the timings to help find the min, max and quartiles */
							Array.Sort(round_timings);

							/* find the harmonic mean */
							for (int i_loop = N_LOOPS; (i_loop-- > 0); )
							{
								har_time_sum += 1.0/((double)round_timings[i_loop]);
							}
							har_mean = (N_LOOPS / har_time_sum);

							/* find the minimum and maximum */
							min_ticks = round_timings[0];
							max_ticks = round_timings[N_LOOPS - 1];

							/* calculate indices for the mean and first quartile */
							iQ2 = ((int)Math.Ceiling(((double)N_LOOPS)/2.0));
							iQ1 = ((int)Math.Ceiling(((double)iQ2)/2.0));

							/* calculate the actual quartiles */
							Q1_ticks = (round_timings[iQ1] + round_timings[((int)Math.Floor(iQ2/2.0)) + 1])/2.0;
							Q2_ticks = (round_timings[iQ2] + round_timings[((int)Math.Floor(N_LOOPS/2.0)) + 1])/2.0;
							Q3_ticks = (round_timings[iQ1 + iQ2] + round_timings[(((int)Math.Floor(iQ2/2.0)) + 1)] + iQ2)/2.0;

							/* find the range for outliers, what's not in [inlier_min, inlier_max] */
							IQR = (Q3_ticks - Q1_ticks);
							inlier_min = (Q1_ticks - 1.5 * IQR);
							inlier_max = (Q3_ticks + 1.5 * IQR);

							/* find the edge to the lesser outliers, adding all timings until then */
							for (left = (iQ2 + 1); (left-- > 0) && isInlier; )
							{
								if (round_timings[left] < inlier_min)
								{
									isInlier = false;
								}
								else {
									ari_time_sum += round_timings[left];
								}
							}

							/* find the edge to the greater outliers, likewise */
							isInlier = true;
							for (right = (iQ2 + 1); (right < N_LOOPS) && isInlier; ++right)
							{
								if (round_timings[right] > inlier_max)
								{
									isInlier = false;
								}
								else {
									ari_time_sum += round_timings[right];
								}
							}

							/* replace all outliers with the harmonic mean */
							ari_time_sum += (N_LOOPS - (right - left + 1)) * har_mean;
							/* finish calculating the arithmetic mean */
							ari_mean = ((((ari_time_sum / N_LOOPS) * 1.0e+6)) / Stopwatch.Frequency);

							/* finish converting the quartiles */
							min = ((min_ticks * 1.0e+6) / Stopwatch.Frequency);
							Q1 = ((Q1_ticks * 1.0e+6) / Stopwatch.Frequency);
							Q2 = ((Q2_ticks * 1.0e+6) / Stopwatch.Frequency);
							Q3 = ((Q3_ticks * 1.0e+6) / Stopwatch.Frequency);
							max = ((max_ticks * 1.0e+6) / Stopwatch.Frequency);

							/* print the results */
							Console.WriteLine("Round {0}", i_round);
							// Console.WriteLine("[{0}]", string.Join(", ", round_timings));
							Console.WriteLine("min : {0}", min);
							Console.WriteLine("Q1  : {0}", Q1);
							Console.WriteLine("Q2  : {0}", Q2);
							Console.WriteLine("mean: {0}", ari_mean);
							Console.WriteLine("Q3  : {0}", Q3);
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
