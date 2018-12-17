/*
 * sorting-ints.c
 * Sorts an array of 100 random integers.
 * by: Leomar Durán <https://github.com/lduran2>
 * for: https://github.com/lduran2/Dark-Archives-Programming-Challenges/blob/master/sorting-ints.cpp
 * time: 2018-12-17 t13:57
 */
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

/**
 * Formats an integer preceded by a separator and writes it to @str.
 */
int uisfmt(char* str, void** ptr, char* separator);

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
 * Fills an array with random unsigned integers in [1, $len].
 */
void aurnfl(size_t len, unsigned int* arr);

void csort(size_t len, unsigned int* els) {
	unsigned int max = 0;
	int* counts;

	/* find the maximum */
	for (register int i_el = 0; i_el < len; ++i_el) {
		if (els[i_el] > max)
		{
			max = els[i_el];
		}
	}
	if ((max + 1) == 0)
	{
		return;
	}
	++max;

	/* make an array with $max elements */
	counts = (unsigned int*)malloc(max * sizeof(unsigned int));
	/* fill with 0s */
	for (register int i_count = 0; i_count < max; ++i_count)
	{
		counts[i_count] = 0;
	}

	// printf("\n%d\n", max);

	/* count each number in the original array */
	for (register int i_el = 0; i_el < len; ++i_el)
	{
		// printf("\n%d,%d\n", i_el, els[i_el]);
		++counts[els[i_el]];
	}

	/* sort the zeros */
	for (int i_el = 0; i_el < counts[0]; ++i_el)
	{
		els[i_el] = 0;
	}

	/* while accumulating counts */
	for (register int i_count = 1; i_count < max; ++i_count)
	{
		counts[i_count] += counts[i_count - 1];
		/* sort the elements */
		for (int i_el = counts[i_count - 1]; i_el < counts[i_count]; ++i_el)
		{
			els[i_el] = i_count;
		}
	}
}

/**
 * Main program.
 */
int main(int argc, char** argv)
{
	const size_t LEN = 20;
	unsigned int arr [20];
	char* unsorted = (char*)malloc(LEN*5 * sizeof(int));
	char* sorted = (char*)malloc(LEN*5 * sizeof(int));

	aurnfl(LEN, arr);
	arrtos(unsorted, LEN, (void**)arr, sizeof(*arr), uisfmt, ", ");

	csort(LEN, arr);

	arrtos(sorted, LEN, (void**)arr, sizeof(*arr), uisfmt, ", ");

	printf("unsort: %s\n", unsorted);
	printf("sorted: %s\n", sorted);

	return 0;
} /* end &main(int argc, char** argv) */


/******************************************************************//**
 * Formats an integer preceded by a separator and writes it to @str.
 */
int uisfmt(char* str, void** ptr, char* separator)
{
	return sprintf(str, "%s%u", separator, *((unsigned int*)ptr));
} /* end &uisfmt(char* str, void** ptr, char* separator) */


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
	/* if array is empty, stop here */
	if (len <= 0)
	{
		return str;
	} /* end if (len <= 0) */

	/* write first element */
	printed = format(str, arr, "");
	/* if an error occurs, stop here */
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
		/* if an error occurs, stop here */
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
 * Fills an array with random numbers in [1, $len].
 */
void aurnfl(size_t len, unsigned int* arr)
{
	for (register int k = 0; (k < len); ++k)
	{
		arr[k] = (rand() % len + 1);
	}
} /* end &arndfl(size_t len, int* arr) */

