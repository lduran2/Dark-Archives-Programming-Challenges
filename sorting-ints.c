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
int isfmt(char* str, void** ptr, char* separator);

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
 * Fills an array with random numbers in [1, $len].
 */
void arndfl(size_t len, int* arr);

/**
 * Main program.
 */
int main(int argc, char** argv)
{
	char* str = (char*)malloc(20*4 * sizeof(int));
	int arr [20];

	arndfl(20, arr);
	arrtos(str, 20, (void**)arr, sizeof(int), isfmt, ", ");

	printf(str);

	return 0;
} /* end &main(int argc, char** argv) */


/******************************************************************//**
 * Formats an integer preceded by a separator and writes it to @str.
 */
int isfmt(char* str, void** ptr, char* separator)
{
	return sprintf(str, "%s%d", separator, *ptr);
} /* end &isfmt(char* str, void** ptr, char* separator) */


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
void arndfl(size_t len, int* arr)
{
	for (int k = 0; (k < len); ++k)
	{
		arr[k] = (rand() % len + 1);
	}
} /* end &arndfl(size_t len, int* arr) */

