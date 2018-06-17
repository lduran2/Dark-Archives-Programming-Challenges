/*
 * resize-with-filler.cpp
 * Write a program that resizes a 2D array and fills the new space with a user specified value.
 * by: https://github.com/lduran2
 * time: 2018-06-17 t14:07
 */
#include <cstddef>
#include <iostream>
#include <cmath>
using namespace std;

template <typename T> T mtxget(const size_t, T*, size_t, size_t);
template <typename T> ostream& mtxprt(const size_t, const size_t, T*, ostream&);
template <typename T> T* mtxcpy(const size_t, const size_t, T*, const size_t, T*);
template <typename T> T* mtxfil(const size_t, const size_t, const size_t, T*, T const);
template <typename T> void mtxcpf(const size_t, const size_t, T*, const size_t, const size_t, T*, T const);

template <typename T> ostream& ptrprt(T*, T*, ostream&);
template <typename T> void ptrcpy(T*, T*, T*);
template <typename T> void ptrfil(T*, T*, T);

int main(int argc, char** argv) {
	/* the original matrix */
	const size_t N_I_ROWS = 2;
	const size_t N_I_COLS = 3;
	int i [6] =
		{ 3, 5, 1,
		  5, 9, 4 };

	/* the expanded matrix by doubling the area */
	const size_t N_J_ROWS = ((size_t) round(N_I_ROWS * sqrt(2)));
	const size_t N_J_COLS = ((size_t) round(N_I_COLS * sqrt(2)));
	int j [N_J_ROWS * N_J_COLS];

	/* the filler for the new array */
	int FILLER = 0;

	/* copy the matrix with filler 0 */
	mtxcpf(N_I_ROWS, N_I_COLS, i, N_J_ROWS, N_J_COLS, j, FILLER);

	/* print the original and the results */
	cout << "Original array:\n";
	mtxprt(N_I_ROWS, N_I_COLS, i, cout) << "\n\n";
	cout << "Expanded array:\n";
	mtxprt(N_J_ROWS, N_J_COLS, j, cout) << '\n';
} /* end int main(int, char**) */

/**
 * Print an $nRows × $nCols matrix of type <T> @matrix_array to output
 * stream %outs
 */
template <typename T>
ostream& mtxprt(const size_t nRows, const size_t nCols,
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
} /* end ostream& mtxprt(const size_t, const size_t, T*, ostream&) */

/**
 * Calculates the cell index from $iRow, $iCol and $nCols.
 * @return: the element at row $iRow and $iCol from the <T> matrix
 * @matrix_array with $nCols columns
 */
template <typename T>
T mtxget(const size_t nCols, T* matrix_array, size_t iRow, size_t iCol)
{
	return matrix_array[(iRow * nCols) + iCol];
} /* end T mtxget(const size_t, T*, size_t, size_t) */

/**
 * Copy the matrix from @source to @target, filling unfilled cells with
 * <T>filler
 * @params:
 *   <typename T> -- type of elements in @source and @target
 *   const size_t nSource_rows -- the number of rows in matrix @source
 *   const size_t nSource_cols -- the number of columns in matrix @source
 *   T* source -- the matrix from which to copy
 *   const size_t nTarget_rows -- the number of rows in matrix @target
 *   const size_t nTarget_cols -- the number of columns in matrix @target
 *   T* target -- the matrix to which to copy
 *   T filler -- the filler element
 */
template <typename T>
void mtxcpf(const size_t nSource_rows, const size_t nSource_cols,
            T* source, const size_t nTarget_rows,
            const size_t nTarget_cols, T* target, T filler ) {
	T* matrix_after_copy;
	/* copy the matrix $source to $target */
	matrix_after_copy = mtxcpy(nSource_rows, nSource_cols, source, nTarget_cols, target);
	/* fill the part to the right in $target and remainder */
	mtxfil(nSource_rows, (nTarget_cols - nSource_cols), nSource_cols, (target + nSource_cols), filler);
	/* fill the part below in $target */
	mtxfil((nTarget_rows - nSource_rows), nTarget_cols, 0, matrix_after_copy, filler);
} /* end void mtxcpf(const size_t, const size_t, T*,
                     const size_t, const size_t, T*, T) */

/**
 * Copy the matrix from @source to @target
 * @params:
 *   <typename T> -- type of elements in @source and @target
 *   const size_t nSource_rows -- the number of rows in matrix @source
 *   const size_t nSource_cols -- the number of columns in matrix @source
 *   T* source -- the matrix from which to copy
 *   const size_t nTarget_cols -- the number of columns in matrix @target
 *   T* target -- the matrix to which to copy
 * @return: the following row
 */
template <typename T>
T* mtxcpy(const size_t nSource_rows, const size_t nSource_cols,
          T* source, const size_t nTarget_cols, T* target) {
	register T* pSource_from; /* pointer from beginning of row in @source */
	register T* pSource_to; /* pointer to end of row in @source */
	register T* pTarget;  /* pointer from beginning of row in @target */
	register size_t k; /* row counter */

	pSource_from = source;
	pSource_to = (source + nSource_cols);
	pTarget = target;

	for (k = 0; k < nSource_rows; ++k) {
		/* copy row */
		ptrcpy(pSource_from, pSource_to, pTarget);
		/* update pointers */
		pSource_from = pSource_to;
		pSource_to += nSource_cols;
		pTarget += nTarget_cols;
	} /* end for (; k < nSource_rows; ) */

	return pTarget;
} /* end T* mtxcpy(const size_t, const size_t, T*, const size_t, T*) */

/**
 * Fill the specified area in matrix @matrix_array with the specified
 * <T>filler
 * @params:
 *   <typename T> -- type of elements in @matrix_array
 *   const size_t nRows -- the number of rows to fill
 *   const size_t nCols -- the number of columns to fill
 *   const size_t row_gap -- the gap between rows in the matrix
 *   T* matrix -- the matrix to which to copy
 *   T filler -- the filler element
 * @return: the following row
 */
template <typename T>
T* mtxfil(const size_t nRows, const size_t nCols, const size_t row_gap,
          T* matrix_array, T filler                              ) {
	register T* pFrom; /* pointer from beginning of the current row */
	register T* pTo; /* pointer to the end of the current row, exclusive */
	register size_t k; /* row counter */

	pFrom = matrix_array;
	pTo = (pFrom + nCols);

	for (k = 0; k < nRows; ++k) {
		/* fill row */
 		ptrfil(pFrom, pTo, filler);
		/* update the pointers */
		pFrom = (pTo += row_gap);
		pTo += nCols;
	} /* end for (; k < nRows; ) */

	return pFrom;
} /* T* mtxfil(const size_t, const size_t, const size_t, T*, T) */

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
 * Copy a <T> array in the range from the @source_from pointer to the
 * @source_to pointer to the destination array @target
 */
template <typename T>
void ptrcpy(T* source_from, T* source_to, T* target) {
	register T* p = source_from; /* pointer to current element in the
                                        source array                      */
	while (p < source_to) {
		*target++ = *p++;
	} /* end while (p < source_to) */
} /* end void ptrcpy(T*, T*, T*) */

/**
 * Fill a <T> array from the @from pointer to the @to pointer with the
 * filler <T>filler
 */
template <typename T>
void ptrfil(T* from, T* to, T filler) {
	register T* p; /* pointer to current element */
	for (p = from; p < to; ++p) {
		*p = filler;
	} /* end for (; p < to; ) */
} /* end void ptrfil(T*, T*, T) */
