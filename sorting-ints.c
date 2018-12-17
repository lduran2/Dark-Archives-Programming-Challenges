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

unsigned int uismax(size_t len, unsigned int* arr)
{
	unsigned int max = 0;
	for (register int k = len; k--; ) {
		if (arr[k] > max)
		{
			max = arr[k];
		}
	}
	return max;
}

unsigned int* uisfil(size_t len, unsigned int* arr, int value)
{
	for (register int k = len; k--; )
	{
		arr[k] = value;
	}
	return arr;
}

void csort(size_t len, unsigned int* els) {
    /* iteartor on els */
    unsigned int* it = els;
	/* the maximum number in @els */
	const unsigned int max = uismax(len, els);
	/* the numbers of times each element in @els appears in @els */
	int* counts;
	/* halt if the max is one less than unsigned 0 */
	if ((max + 1) == 0) return;

	/* the maximum represents the final element in @counts */
	/* therefore, counts */
	counts = uisfil(max,
		(unsigned int*)malloc((max + 1) * sizeof(unsigned int)),
		0
	);

	/* count each number in the original array */
	for (register int i_el = 0; i_el < len; ++i_el)
	{
		// printf("\n%d,%d\n", i_el, els[i_el]);
		++counts[els[i_el]];
	}

	/* sort the zeros */
	uisfil(counts[0], els, 0);

	/* while accumulating counts */
	for (register int el = 1; el <= max; ++el)
	{
	    it += counts[el - 1];
	    uisfil(counts[el], it, el);
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
 * Fills an array with random numbers in [1, $len].
 */
void aurnfl(size_t len, unsigned int* arr)
{
	for (register int k = 0; (k < len); ++k)
	{
		arr[k] = (rand() % len + 1);
	}
} /* end &arndfl(size_t len, int* arr) */

