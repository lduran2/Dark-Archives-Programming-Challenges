/*
 * sorting-ints.c
 * Sorts an array of 100 random integers.
 * Then counts by 3s and 5s to 1000 but doesn't display duplicates.
 * by: Leomar Durán <https://github.com/lduran2>
 * for: https://github.com/lduran2/Dark-Archives-Programming-Challenges/blob/master/sorting-ints.cpp
 * time: 2018-12-17 t13:57
 */
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

/******************************************************************//**
 * Formats an integer preceded by a separator and writes it to @str.
 */
int isfmt(char* str, void** ptr, char* separator);

/******************************************************************//**
 * Formats a double preceded by a separator and writes it to @str.
 */
int dsfmt(char* str, void** ptr, char* separator);

/******************************************************************//**
 * Joins an array of the length specified by $len and with elements
 * of the size specified by $element_size as a string written to @str,
 * using the specified formating function and with the given separator.
 */
char* arrtos
(
	char* str, size_t len, void** arr, size_t element_size,
	int (*format)(char* str, void** ptr, char* separator), char* separator
);

int main(int argc, char** argv)
{
	/* length of test arrays */
	const int LEN = 6;
	/* int test */
	int test [] = {-32021.79, 2.2, 32713.13, 41.1, 5.5, 70.0};
	/* double test */
	double test2 [] = {-32021.79, 2.2, 32713.13, 41.1, 5.5, 70.0};

	/* enough room for 6 six-digit ints and 6 fifteen-digit doubles */
	char* join = (char*)(malloc(((5*2) + (6*6) + (15*6) + 1 + 100) * sizeof(char)));

	/* test ints */
	strcpy(join, "int: ");
	arrtos(join + strlen(join), LEN, (void**)test, sizeof(*test), isfmt, ",");

	/* test doubles */
	strcat(join, "\ndbl: ");
	arrtos(join + strlen(join), LEN, (void**)test2, sizeof(*test2), dsfmt, ";");

	/* print results */
	printf("Hello, world!\n%s\n%d\n", join, strlen(join));

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
 * Formats a double preceded by a separator and writes it to @str.
 */
int dsfmt(char* str, void** ptr, char* separator)
{
    int printed;
    printed = sprintf(str, "%s%f", separator, *((double*)ptr));
	return printed;
} /* end &dsfmt(char* str, void** ptr, char* separator) */

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

	printf("%s\n", str);

	return str;
} /* end &arrtos
     (
     	char* str, size_t len, void** arr, size_t element_size,
     	int (*format)(char* str, void** ptr, char* separator),
     	char* separator
     )
   */

