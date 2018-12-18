/*
 * sorting-ints.c
 * Sorts an array of 100 random integers.
 * by: Leomar Durán <https://github.com/lduran2>
 * for: https://github.com/lduran2/Dark-Archives-Programming-Challenges/blob/master/sorting-ints.cpp
 * time: 2018-12-17 t20:37
 */
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

/**
 * Sorts an array of unsigned integers using the count search.
 */
void csort(size_t len, unsigned int* els);

/**
 * Fills an array with random unsigned integers in [1, $len].
 */
void aurnfl(size_t len, unsigned int* arr);

/**
 * Compares two arrays of unsigned integers for equality.
 */
int uisequ(size_t len, unsigned int* a, unsigned int* b);

/**
 * Joins an array of the length specified by $len and with elements
 * of the size specified by $element_size as a string written to @str,
 * using the specified formating function and with the given separator.
 */
char* arrtos
(
	char* str, size_t len, void** arr, size_t element_size,
	int (*format)(char* str, void** ptr, char* separator), char* separator
);

/**
 * Formats an integer preceded by a separator and writes it to @str.
 */
int uisfmt(char* str, void** ptr, char* separator);

/**
 * Main program.
 */
int main(int argc, char** argv)
{
	const size_t LEN = 100;
	unsigned int arr [LEN];
	char* unsorted = (char*)malloc(LEN*5 * sizeof(int));
	char* sorted = (char*)malloc(LEN*5 * sizeof(int));

	/* 100 random numbers */
	/* https://www.random.org/integers/?num=100&min=1&max=100&col=100&base=10&format=html&rnd=new */
	/* Timestamp: 2018-12-17 21:12:49 UTC */
	const size_t N_CORRECTNESS = 100;
	unsigned int u_correctness [] = { 20, 79, 22, 94, 23, 89, 97, 8, 95, 4, 34, 2, 92, 46, 58, 70, 69, 29, 21, 6, 28, 62, 70, 19, 55, 8, 17, 16, 76, 71, 30, 84, 10, 64, 33, 96, 42, 72, 70, 24, 100, 1, 47, 3, 31, 6, 65, 79, 85, 33, 95, 36, 87, 4, 95, 99, 60, 42, 88, 59, 61, 81, 51, 69, 45, 16, 74, 54, 59, 23, 56, 76, 94, 97, 8, 46, 4, 62, 85, 44, 2, 27, 78, 12, 46, 80, 84, 76, 55, 60, 68, 83, 52, 15, 65, 64, 60, 4, 34, 55 };
	unsigned int s_correctness [] = { 1, 2, 2, 3, 4, 4, 4, 4, 6, 6, 8, 8, 8, 10, 12, 15, 16, 16, 17, 19, 20, 21, 22, 23, 23, 24, 27, 28, 29, 30, 31, 33, 33, 34, 34, 36, 42, 42, 44, 45, 46, 46, 46, 47, 51, 52, 54, 55, 55, 55, 56, 58, 59, 59, 60, 60, 60, 61, 62, 62, 64, 64, 65, 65, 68, 69, 69, 70, 70, 70, 71, 72, 74, 76, 76, 76, 78, 79, 79, 80, 81, 83, 84, 84, 85, 85, 87, 88, 89, 92, 94, 94, 95, 95, 95, 96, 97, 97, 99, 100 };

	aurnfl(LEN, arr);
	arrtos(unsorted, LEN, (void**)arr, sizeof(*arr), uisfmt, ", ");

	csort(LEN, arr);

	arrtos(sorted, LEN, (void**)arr, sizeof(*arr), uisfmt, ", ");

	csort(N_CORRECTNESS, u_correctness);

	// char* su_correctness = (char*)malloc(N_CORRECTNESS*5 * sizeof(int));
	// char* ss_correctness = (char*)malloc(N_CORRECTNESS*5 * sizeof(int));
	// arrtos(su_correctness, N_CORRECTNESS, (void**)u_correctness, sizeof(*arr), uisfmt, ", ");
	// arrtos(ss_correctness, N_CORRECTNESS, (void**)s_correctness, sizeof(*arr), uisfmt, ", ");
	// printf("unsort: %s\n", su_correctness);
	// printf("sorted: %s\n", ss_correctness);

	printf("csort is correct: %d\n", uisequ(N_CORRECTNESS, u_correctness, s_correctness));

	printf("unsort: %s\n", unsorted);
	printf("sorted: %s\n", sorted);

	return 0;
} /* end &main(int argc, char** argv) */


/******************************************************************//**
 * Sorts an array of unsigned integers using the count search.
 */
void csort(size_t len, unsigned int* els) {
	/* dependencies */
	unsigned int uismax(size_t len, unsigned int* arr);
	unsigned int* uisfil(size_t len, unsigned int* arr, int value);

	/* iteartor on els */
	unsigned int* it = els;
	/* the maximum number in @els */
	const unsigned int MAX = uismax(len, els);
	/* the numbers of times each element in @els appears in @els */
	int* counts;
	/* the number of unique elements being counted in els */
	const int N_COUNTS = (MAX + 1);
	/* overflow check : halt if the max is one less than unsigned 0 */
	if (N_COUNTS == 0) return;

	/* the maximum represents the final element in @counts */
	/* therefore, counts */
	counts = uisfil(N_COUNTS, (unsigned int*)malloc(N_COUNTS*sizeof(int)), 0);

	/* count each number in the original array */
	for (register int i_el = len; i_el--; )
	{
		++counts[els[i_el]];
	} /* end for (; i_el--; ) */

	/* sort the zeros */
	uisfil(counts[0], els, 0);

	/* while accumulating counts */
	for (int el = 1; el <= MAX; ++el)
	{
		it += counts[el - 1];
		uisfil(counts[el], it, el);
	} /* end for (; el <= MAX; ) */
} /* end &csort(size_t len, unsigned int* els) */


/******************************************************************//**
 * Find the maximum number in an array of integers.
 */
unsigned int uismax(size_t len, unsigned int* arr)
{
	unsigned int max = 0;
	for (register int k = len; k--; )
	{
		if (arr[k] > max)
		{
			max = arr[k];
		} /* end if (arr[k] > max) */
	} /* end for (; k--; ) */
	return max;
} /* end &uismax(size_t len, unsigned int* arr) */


/******************************************************************//**
 * Fills an array of random unsigned with a specified value.
 */
unsigned int* uisfil(size_t len, unsigned int* arr, int value)
{
	for (register int k = len; k--; )
	{
		arr[k] = value;
	} /* end for (; k--; ) */
	return arr;
} /* end &uisfil(size_t len, unsigned int* arr, int value) */


/******************************************************************//**
 * Fills an array with random numbers in [1, $len].
 */
void aurnfl(size_t len, unsigned int* arr)
{
	for (register int k = 0; (k < len); ++k)
	{
		arr[k] = (rand() % len + 1);
	}
} /* end &arndfl(size_t len, int* arr) */


/******************************************************************//**
 * Compares two arrays of unsigned integers for equality.
 */
int uisequ(size_t len, unsigned int* a, unsigned int* b)
{
	int are_equal = 1;
	for (int k = len; (k-- && are_equal); a++, b++)
	{
		are_equal = (*a == *b);
	} /* end for (; (k-- && are_equal); ) */
	return are_equal;
} /* end &uisequ(size_t len, unsigned int* a, unsigned int* b) */


/******************************************************************//**
 * Joins an array of the length specified by $len and with elements
 * of the size specified by $element_size as a string written to @str,
 * using the specified formating function and with the given separator.
 */
char* arrtos
(
	char* str, size_t len, void** arr, size_t element_size,
	int (*format)(char* str, void** ptr, char* separator), char* separator
)
{
	/* pointer to array bytes */
	char* ptr;
	/* number of characters printed by each call to &format */
	int printed;
	/* the current index in @str */
	int index;
	/* halt if array is empty */
	if (len <= 0) return str;

	/* write first element */
	printed = format(str, arr, "");
	/* halt if an error occurs while printing */
	if (printed < 0) return str;
	/* move to end of @str */
	index += printed;

	ptr = ((char*)arr);
	/* write the remaining elements */
	for (int k = 1; (k < len); ++k)
	{
		/* move to next element */
		ptr += element_size;
		/* convert pointer before printing */
		printed = format(str + index, (void**)ptr, separator);
		/* halt if an error occurs while printing */
		if (printed < 0) return str;
		/* move to end of @str */
		index += printed;
	} /* end for (; k < len ;) */

	return str;
} /* end
		&arrtos
		(
			char* str, size_t len, void** arr, size_t element_size,
			int (*format)(char* str, void** ptr, char* separator),
			char* separator
		)
   */


/******************************************************************//**
 * Formats an integer preceded by a separator and writes it to @str.
 */
int uisfmt(char* str, void** ptr, char* separator)
{
	return sprintf(str, "%s%u", separator, *((unsigned int*)ptr));
} /* end &uisfmt(char* str, void** ptr, char* separator) */
