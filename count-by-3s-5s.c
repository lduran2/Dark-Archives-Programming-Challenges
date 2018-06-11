/*
 * count-by-3s-5s.c
 * Counts by 3s to 1000.
 * Then counts by 3s and 5s to 1000 but doesn't display duplicates.
 * by: https://github.com/lduran2
 * time: 2018-06-08 t18:03
 */
#include <stdio.h>

int main(int argc, char** argv) {
	void countBy(const size_t len, const int increments [], int max);

	const size_t LEN = 2; /* number of increments */
	const int increments [] = {3, 5}; /* the increments */
	const int MAX = 1000; /* maximum value to which to count*/

	/* Count by 3s demo. */
	printf("Counts by %ds to %d.\n", increments[0], MAX);
	countBy(1, increments, MAX);
	printf("\n\n");

	/* Count by 3s and 5s demo. */
	printf("Counts by %ds and %ds to %d but doesn't display duplicates.\n",
		increments[0], increments[1], MAX
	);
	countBy(LEN, increments, MAX);
	printf("\n");

	return 0;
} /* end int main(int, char**) */

/**
 * Count by the specified @increments up to $max, without duplicates.
 * I.e.: Given I := @increments, display all
 * values x : k in [0, $len], x in N (x | I_k)
 * E.g.: Count by 3s and 5s to 1000.
 * @param:
 *   const size_t len -- number of increments
 *   void increments [] -- amounts by which to increment the count
 *   int max -- the maximum amount up to which to count
 */
void countBy(const size_t len, const int increments [], int max)
/*
	// This is a fixed-increment version.

	int k = 0; // increases by 3
	int l = 0; // increases by 5
	int min = 0; // the minimum between k and l, start at 0

	// print the minimal value while it is at most max
	while (min <= MAX) {
		printf("%d\t", min);

		// find whether k or l is the minimum
		// increase its value by 3 or 5 respectively
		if (k == min) {
			k += 3;
		}
		if (l == min) {
			l += 5;
		}

		// update the minimum
		if (k <= l) {
			min = k;
		}
		else {
			min = l;
		}
	}
*/
{
	void ifill(int, const size_t, int []);
	void incIf(int, const size_t, int [], const int []);
	size_t iimin(const size_t, int []);

	int ks [len]; /* the array of counters */
	/* clear the ks array */
	ifill(0, len, ks);
	int min = 0; /* the minimal k, start at 0 */

	/* print the minimal value while it is at most max */
	while (min <= max) {
		printf("%d\t", min);
		/* increase any value equal to the minimum by its corresponding
		   increment */
		incIf(min, len, ks, increments);
		/* update the minimum */
		min = ks[iimin(len, ks)];
	} /* while (min <= max) */
} /* void countBy(const size_t, const int [], int) */


/**
 * Fill integer array @arr using the given $filler element
 * @params:
 *   int filler -- the filler element
 *   const size_t len -- size of array to fill
 *   int arr [] -- the array to fill
 * !!! mutates all values in arr
 */
void ifill(int filler, const size_t len, int arr []) {
	register size_t k; /* array index */
	for (k = len; (0 < k--); ) {
		arr[k] = filler;
	} /* end for (; (0 < k--); ) */
} /* end void ifill(int, const size_t, int []) */

/**
 * Find the index of the minimal value in integer array @arr
 * @params:
 *   const size_t len -- size of the array
 *   int arr [] -- the integer array to search
 * @return: the index of the minimal value in array @arr
 */
size_t iimin(const size_t len, int arr []) {
	size_t candidate = (len - 1); /* candidate index */
	register size_t k; /* array index */

	for (k = candidate; (0 < k--); ) {
		/* if lower value found, update the candidate */
		if (arr[k] < arr[candidate]) {
			candidate = k;
		} /* end if (arr[k] < arr[candidate]) */
	} /* end for (; (0 < k--); ) */

	/* return the final candidate */
	return candidate;
} /* end size_t iimin(const size_t, int []) */

/**
 * Increase all values in @arr equal to the $key by the
 * corresponding @increments
 * @params:
 *   int key -- for which to search
 *   const size_t len -- size of the array
 *   int arr [] -- the array to update
 *   const int increments [] -- the values by which to increment the array values
 * !!! mutates all values in @arr equal to the $key
 */
void incIf(int key, const size_t len, int arr [], const int increments []) {
	register size_t k;

	for (k = len; (0 < k--); ) {
		if (key == arr[k]) {
			arr[k] += increments[k];
		} /* end if (key == arr[k]) */
	} /* end for (; (0 < k--); ) */
} /* end void incIf(int, const size_t, int [], const int []) */
