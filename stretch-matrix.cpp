/*
 * stretch-matrix.cpp
 * Doubles an input 2D array by 2x preserving/filling in values
 * respectively (nearest neighbor)
 * by: https://github.com/lduran2
 * time: 2018-06-18 t00:11
 */
#include <cstddef> /* for size_t type */
#include <iostream> /* for output stream and stdout */
using namespace std;

/**
 * Matrix functions.
 */
template <typename T> T mtxget(size_t const, T*, size_t, size_t);
template <typename T> ostream& mtxprt(size_t const, size_t const, T*,
                                      ostream&                       );
template <typename T> void mtxstc(size_t const, size_t const, T*,
                                  size_t const, size_t const, T* target);

/**
 * Pointer functions.
 */
template <typename T> ostream& ptrprt(T*, T*, ostream&);
template <typename T> void ptrstc(T*, T*, size_t const, T*);
template <typename T> void ptrskp(T*, T*, size_t const, T*);

/**
 * Main function.
 */
int main(int argc, char** argv) {
	/* original matrix size and elements */
	int const nI_ROWS = 2;
	int const nI_COLS = 6;
	int i [nI_ROWS * nI_COLS] =
		{ 3, 5, 1, 4, 9, 3,
		  5, 9, 4, 1, 7, 7 };

	/* stretched matrix size and elements */
	int const STRETCH_FACTOR = 2;
	int const nJ_ROWS = (nI_ROWS * STRETCH_FACTOR);
	int const nJ_COLS = (nI_COLS * STRETCH_FACTOR);
	int j [nJ_ROWS * nJ_COLS];

	/* stretch the matrix */
	mtxstc(nI_ROWS, nI_COLS, i, STRETCH_FACTOR, STRETCH_FACTOR, j);

	/* print results */
	cout << "Original matrix:\n";
	mtxprt(nI_ROWS, nI_COLS, i, cout) << '\n';
	cout << "Stretched matrix:\n";
	mtxprt(nJ_ROWS, nJ_COLS, j, cout) << '\n';
} /* end int main(int, char**) */

/**
 * Print an $nRows × $nCols matrix of type <T> @matrix_array to output
 * stream %outs
 */
template <typename T>
ostream& mtxprt(size_t const nRows, size_t const nCols,
                T* matrix_array, ostream& outs   ) {
	register T* pFrom; /* pointer from beginning of the current row */
	register T* pTo; /* pointer to the end of the current row, exclusive */
	register size_t k; /* row counter */

	pFrom = matrix_array;
	pTo = (matrix_array + nCols);

	outs << "{ ";
	ptrprt(pFrom, pTo, outs);
	for (k = 1; k < nRows; ++k) {
		/* update pointers */
		pFrom = pTo;
		pTo += nCols; 
		outs << ",\n  ";
		/* print current row */
		ptrprt(pFrom, pTo, outs);
	} /* end for (; k < nRows;) */
	outs << " }";
	return outs;
} /* end ostream& mtxprt(size_t const, size_t const, T*, ostream&) */

/**
 * Calculates the cell index from $iRow, $iCol and $nCols.
 * @return: the element at row $iRow and $iCol from the <T> matrix
 * @matrix_array with $nCols columns
 */
template <typename T>
T mtxget(size_t const nCols, T* matrix_array, size_t iRow, size_t iCol)
{
	return matrix_array[(iRow * nCols) + iCol];
} /* end T mtxget(size_t const, T*, size_t, size_t) */

/**
 * Stretch a $(nSource_rows × nSource_cols) matrix of <T>-type
 * elements, @source, using the given $row_stretch and $col_stretch
 * factors, into the destination matrix @target
 */
template <typename T>
void mtxstc(size_t const nSource_rows, size_t const nSource_cols,
            T* source, size_t const row_stretch,
            size_t const col_stretch, T* target                  ) {
	size_t const nTARGET_COLS
		= (nSource_cols * col_stretch); /* row-length of target matrix */
	register T* pSource_from; /* pointer to beginning of current row in source */
	register T* pSource_to; /* pointer to beginning of next row in source */
	register T* pTarget; /* pointer to beginning of current row in target */
	register size_t k; /* counter for different rows */
	register size_t l; /* counter for repeat rows */

	pTarget = target;
	pSource_from = source;
	pSource_to = (source + nSource_cols);

	for (k = 0; k < nSource_rows; ++k) {
		for (l = 0; l < row_stretch; ++l) {
			/* stretch the current row */
			ptrstc(pSource_from, pSource_to, col_stretch, pTarget);
			/* update target pointer */
			pTarget += nTARGET_COLS;
		} /* end for (; l < row_stretch; ) */
		/* update source pointers */
		pSource_from = pSource_to;
		pSource_to += nSource_cols;
	} /* end for (; k < nSource_rows; ) */
} /* end void mtxstc(size_t const, size_t const, T*, size_t const,
                     size_t const, T* target)                      */

/**
 * Print a <T> array from the @from pointer to the @to pointer to
 * output stream %outs
 */
template <typename T>
ostream& ptrprt(T* from, T* to, ostream& outs) {
	register T* p; /* pointer to current element */
	outs << "{ ";
	outs << *from;
	for (p = (from + 1); p < to; ++p) {
		outs << ", " << *p;
	} /* end for (; p < to; ) */
	outs << " }";
	return outs;
}

/**
 * Stretch a <T>-element array from *source_from to *source_to, using
 * the given $stretch factor, into the destination array @target
 */
template <typename T>
void ptrstc(T* source_from, T* source_to, size_t const stretch, T* target) {
	register T* pTarget; /* pointer to current element in the target array */
	T* const pSTRETCH = (target + stretch);
	for (pTarget = target; pTarget < pSTRETCH; ++pTarget) {
		ptrskp(source_from, source_to, stretch, pTarget);
	} /* end for (; pTarget < pSTRETCH; ++pTarget) */
} /* end void ptrstc(T*, T*, size_t const, T*) */

/**
 * Copy a <T>-element array from *source_from to *source_to, skipping
 * $skip spaces into the destination array @target
 */
template <typename T>
void ptrskp(T* source_from, T* source_to, size_t const skip, T* target) {
	register T* p; /* pointer to current element in the source array */
	for (p = source_from; p < source_to; ++p) {
		*target = *p;
		target += skip;
	} /* end for (; p < source_to; ) */
} /* end void ptrskp(T*, T*, size_t const, T*) */
