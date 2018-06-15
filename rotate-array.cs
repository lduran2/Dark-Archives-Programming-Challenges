/*
 * rotate-array.cs
 * Takes a 2D array of size 2x10 and rotate it clockwise to a 10x2 2D array.
 * by: https://github.com/lduran2
 * date: 2018-06-14 t17:51
 */
using System; /* for Console */
using System.IO; /* for TextWriter */
using DarkArchives.Lduran2.Matrixes; /* For Matrix classes */

/* Test class */
class RotateArrayTest {
	static void Main(string[] args) {
		int [,] arr = /* the matrix's backing array */
			{ { 3, 5, 1, 4, 9, 3, 2, 3, 8, 4},
			  { 5, 9, 4, 1, 7, 7, 2, 1, 9, 0} };

		Matrix<int> [] matrixes = new Matrix<int> [5]; /* array of matrices
		                                                  in different
		                                                  states of rotation */
		int k; /* the matrix counter */

		matrixes[0] = new ArrMatrix<int>(arr); /* the original matrix */

		/* print the original matrix */
		Console.Write("Original:\n");
		new MatrixWriteable<int>(matrixes[0]).WriteUsing(Console.Out);

		/* for each proceding rotation */
		for (k = 1; k < matrixes.Length; ++k) {
			/* rotate the matrix */
			matrixes[k] = new RotatedMatrix<int>(matrixes[k - 1]);
			/* print number of degrees rotated */
			Console.Write("\nRotated {0} degrees:\n", (90*k));
			/* print out the array */
			new MatrixWriteable<int>(matrixes[k]).WriteUsing(Console.Out);
		} /* end for (; k < matrixes.Length; ) */

		Console.Write('\n');
	} /* end static void Main(string[]) */
} /* end class RotateArray */

/**
 * Programming challenges
 */
namespace DarkArchives {
	/**
	 * My solutions
	 */
	namespace Lduran2 {
		/**
		 * Classes dealing with matrices.
		 */
		namespace Matrixes {
			/**
			 * A bi-dimensional array
			 */
			interface Matrix<T> {
				/**
				 * @return: the element at position ($iRow, $iCol).
				 * @param:
				 *   int iRow, int iCol -- matrix coordinates
				 */
				T Get(int iRow, int iCol);

				/**
				 * @return: the number of rows in the matrix
				 */
				int nRows();

				/**
				 * @return: the number of columns in the matrix
				 */
				int nCols();
			} /* end interface Matrix<T> */

			/**
			 * An object that can be written using a System.IO.TextWriter
			 */
			interface Writeable {
				/**
				 * Writes this object using the specified text writer $outs
				 * @param:
				 *   TextWriter outs -- text writer to write this object
				 */
				void WriteUsing(TextWriter outs);
			} /* end interface Writeable */

			/**
			 * A matrix wrapping a backing array
			 */
 			sealed class ArrMatrix<T> : Matrix<T> {
				/** the backing array */
				T[,] arr;

				/**
				 * Creates a matrix wrapping a backing array @anArr
				 * @param:
				 *   T[,] anArr -- an array to wrap
				 */
				public ArrMatrix(T[,] anArr) {
					this.arr = anArr;
				} /* end ArrMatrix(T[,]) */

				/**
				 * Gets the specified element from the backing array
				 */
				public T Get(int iRow, int iCol) {
					return this.arr[iRow,iCol];
				} /* end T Get(int, int) */

				/**
				 * The number of rows is the backing array's 1st dimension
				 */
				public int nRows() {
					return this.arr.GetLength(0);
				} /* end int nRows() */

				/**
				 * The number of rows is the backing array's 2nd dimension
				 */
				public int nCols() {
					return this.arr.GetLength(1);
				} /* end int nCols() */
			} /* end class ArrMatrix<T> */

			/**
			 * A matrix that has been rotated
			 */
			sealed class RotatedMatrix<T> : Matrix<T> {
				Matrix<T> parent; /** the original matrix */

				/**
				 * Rotates a matrix
				 * @param:
				 *   Matrix<T> aMatrix -- a matrix to rotate
				 */
				public RotatedMatrix(Matrix<T> aMatrix) {
					this.parent = aMatrix;
				} /* end RotatedMatrix(Matrix<T>) */

				/**
				 * Calculates the specified element from the original matrix
				 */
				public T Get(int iRow, int iCol) {
					/* a 90 degree rotate is reversing the */
					return this.parent.Get((this.parent.nRows() - iCol - 1), iRow);
				} /* end T Get(int, int) */

				/**
				 * Corresponds to the number of columns of the original array
				 */
				public int nRows() {
					return this.parent.nCols();
				} /* end int nRows() */

				/**
				 * Corresponds to the number of rows of the original array
				 */
				public int nCols() {
					return this.parent.nRows();
				} /* end int nCols() */
			} /* end class RotatedMatrix<T> */

			/**
			 * A writable for a backing matrix
			 */
			sealed class MatrixWriteable<T> : Writeable {
				Matrix<T> matrix; /* the backing matrix */

				/**
				 * Creates a writable from a backing matrix
				 * @param:
				 *   Matrix<T> aMatrix -- a matrix to rotate
				 */
				public MatrixWriteable(Matrix<T> aMatrix) {
					this.matrix = aMatrix;
				} /* end MatrixWriteable(Matrix<T>) */

				/**
				 * Writes the backing matrix using the specified text writer $outs
				 */
				public void WriteUsing(TextWriter outs) {
					int k; /* index of the row */
					outs.Write("{ ");
					this.WriteRowUsing(outs, 0);
					for (k = 1; k < matrix.nRows(); ++k) {
						outs.Write(",\n  ");
						this.WriteRowUsing(outs, k);
					} /* for (; k < matrix.nRows(); ) */
					outs.Write(" }");
				} /* end void WriteUsing(TextWriter) */

				/**
				 * Writes a row of the backing matrix using the specified text
				 * writer $outs
				 * @param int iRow -- the index of the row to write
				 */
				void WriteRowUsing(TextWriter outs, int iRow) {
					int k; /* index of the column */
					outs.Write("{");
					outs.Write(matrix.Get(iRow, 0));
					for (k = 1; k < matrix.nCols(); ++k) {
						outs.Write(", ");
						outs.Write(matrix.Get(iRow, k));
					} /* end for (; k < matrix.nCols(); ) */
					outs.Write("}");
				} /* end void WriteRowUsing(TextWriter, int) */
			} /* end class MatrixWriteable<T> */

		} /* end namespace Matrixes */
	} /* end namespace Lduran2 */
} /* end namespace DarkArchives */
