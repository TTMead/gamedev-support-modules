using System;
using System.Collections;

namespace unityai {
    class Mat2D : IEnumerable{

        // Data reference
        private Object[,] m;

        // Constructor
        public Mat2D(int rows, int cols)
        {
            m = new Object[rows, cols];
        }

        // Getter / Setter for Values
        public Object this[int rows, int cols]
        {
            get => m[rows, cols];
            set => m[rows, cols] = value;
        }

        // Getter for a range of values
        public Mat2D this[Range rows, Range cols]
        {
            get {
                if (!(rows.End.Value < m.GetLength(0) && rows.Start.Value >= 0 && cols.End.Value < m.GetLength(1) && cols.Start.Value >= 0))
                    throw new IndexOutOfRangeException("Range out of bounds of this matrix");

                int newRows = 1 + rows.End.Value - rows.Start.Value;
                int newCols = 1 + cols.End.Value - cols.Start.Value;
                Mat2D A = new Mat2D(newRows, newCols);

                for (int i = 0; i < newRows; i++) {
                    for (int j = 0; j < newCols; j++) {
                        A[i, j] = m[rows.Start.Value + i, cols.Start.Value + j];
                    }
                }

                return A;
            }
        }

        // IEnumerable functions
        public IEnumerator GetEnumerator()
        {
            yield return m;
        }

        private int currentRow = 0;
        public void Add(params Object[] a)
        {
            for (int c = 0; c < a.Length; c++)
            {
                m[currentRow, c] = a[c];
            }
            currentRow++;
        }

        
        public Mat2D Clone() {
            Mat2D A = new Mat2D (Size(0), Size(1));

            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    A[i, j] = this[i, j];
                }
            }

            return A;
        }


        public override bool Equals(object O) {
            Mat2D A = this;
            Mat2D B = (Mat2D) O;

            if (A.Size(0) != B.Size(0) || A.Size(1) != B.Size(1))
                return false;
            
            
            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    if (!Object.Equals(A[i, j], B[i, j]))
                        return false;
                }
            }

            return true;
        }


        public override int GetHashCode() {
            return m.GetHashCode();
        }



        #region matrix utilities


        // Returns the size of the matrix as a tuple
        public (int, int) Size() {
            return (m.GetLength(0), m.GetLength(1));
        }

        // Returns the size of the matrix along a given dimension
        public int Size(int dimension) {
            return m.GetLength(dimension);
        }

        // Formats and prints the matrix to the System Console
        public void PrintMatrix() {
            string text = "";
            for (int i = 0; i < m.GetLength(0); i++) {
                text = text + "| ";
                for (int j = 0; j < m.GetLength(1); j++) {
                    Object a = this[i, j];
                    if ((a is int || a is float || a is double)) {
                        double A = Convert.ToDouble(a);
                        if (A == double.PositiveInfinity) {
                            text = text + "Inf   " + " | ";
                        } else {
                            text = text + Math.Round(A, 4).ToString().PadRight(6) + " | ";
                        }
                    } else {
                        text = text + this[i, j].ToString().PadRight(6) + " | ";
                    }
                    
                }
                text = text + "\n";
            }
            Console.WriteLine(text);
        }


        // Returns the minimum value and location of this value within the matrix as a tuple.
        public (double, int, int) Min() {
            int x = 0;
            int y = 0;

            double min = double.PositiveInfinity;

            // Iterate through array to find maximum and minimum element in array.
            //Inside loop for each array element check for maximum and minimum.
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                { 
                    Object v = m[i, j];

                    if (!(v is int || v is float || v is double))
                        throw new ArithmeticException("Cannot find min of non-numeric or non-scalar values");

                    double newVal = Convert.ToDouble(v);

                    if (newVal >= 0) {
                        //Assign current array element to min if if (arr[i,j] < min)
                        if (newVal < min)
                        {
                            min = newVal;
                            x = i-1;
                            y = j-1;
                        }
                    }
                }

            }

            return (min, x, y);
        }


        #endregion



        #region predefined matrices

        // ======== Kernals ========

        public static Mat2D ManhattenKernal = new Mat2D(3, 3) {
            {Double.PositiveInfinity, 1, Double.PositiveInfinity},
            {1,                       0, 1                      },
            {Double.PositiveInfinity, 1, Double.PositiveInfinity}
        };

        public static Mat2D EuclideanKernal = new Mat2D(3, 3) {
            {Math.Pow(2, 0.5), 1, Math.Pow(2, 0.5)},
            {1,                0, 1               },
            {Math.Pow(2, 0.5), 1, Math.Pow(2, 0.5)}
        };



        // Returns a matrix of a given size, filled with zeros
        public static Mat2D Zeros(int rows, int cols) {
            Mat2D A = new Mat2D (rows, cols);

            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    A[i, j] = 0;
                }
            }

            return A;
        }

        // Returns a matrix of a given size, filled with ones
        public static Mat2D Ones(int rows, int cols) {
            Mat2D A = new Mat2D (rows, cols);

            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    A[i, j] = 1;
                }
            }

            return A;
        }


        #endregion



        #region operations


        // Equality
        public static bool operator ==(Mat2D A, Mat2D B) => A.Equals(B);

        // Inequality
        public static bool operator !=(Mat2D A, Mat2D B) => !A.Equals(B);

        // Element-wise addition
        public static Mat2D operator +(Mat2D A, Mat2D B)
        {
            if (A.Size(0) != B.Size(0) || A.Size(1) != B.Size(1))
                throw new ArithmeticException("Cannot add matrices of different sizes: (" + A.Size(0).ToString() + ", " + A.Size(1).ToString() + ") + (" + B.Size(0).ToString() + ", " + B.Size(1).ToString() + ")");
            
            Mat2D C = Mat2D.Zeros(A.Size(0), A.Size(1));
            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    Object a = A[i, j];
                    Object b = B[i, j];

                    if ((a is int || a is float || a is double) && (b is int || b is float || b is double)) {
                        C[i, j] = Convert.ToDouble(a) + Convert.ToDouble(b);
                    } else if ((a is string || a is char) && (b is string || b is char)) {
                        C[i, j] = Convert.ToString(a) + Convert.ToString(b);
                    } else {
                        throw new ArithmeticException("Cannot add matrix elements with these types: (" + a.GetType() + ", " + b.GetType() + ")");
                    }
                    
                }
            }
            return C;
        }

        // Element-wise multiplication
        public static Mat2D operator %(Mat2D A, Mat2D B)
        {
            if (A.Size(0) != B.Size(0) || A.Size(1) != B.Size(1))
                throw new ArithmeticException("Cannot perform element-wise multiplication using matrices of different sizes: (" + A.Size(0).ToString() + ", " + A.Size(1).ToString() + ") + (" + B.Size(0).ToString() + ", " + B.Size(1).ToString() + ")");
            
            // Predefine the resultant matrix
            Mat2D C = Mat2D.Zeros(A.Size(0), A.Size(1));

            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    Object a = A[i, j];
                    Object b = B[i, j];

                    if ((a is int || a is float || a is double) && (b is int || b is float || b is double)) {
                        C[i, j] = Convert.ToDouble(a) * Convert.ToDouble(b);
                    } else {
                        throw new ArithmeticException("Cannot add matrix elements with these types: (" + a.GetType() + ", " + b.GetType() + ")");
                    }
                    
                }
            }
            
            return C;
        }

        // Scalar multiplication
        public static Mat2D operator *(Mat2D A, double b)
        {
            // Predefine the resultant matrix
            Mat2D C = A.Clone();

            for (int i = 0; i < A.Size(0); i++) {
                for (int j = 0; j < A.Size(1); j++) {
                    Object a = A[i, j];

                    if ((a is int || a is float || a is double)) {
                        C[i, j] = Convert.ToDouble(a) * b;
                    } else {
                        throw new ArithmeticException("Matrix element being multiplied is non-numerical, type:" + a.GetType());
                    }
                    
                }
            }
            
            return C;
        }

        // Commutativity of Scalar Multiplication
        public static Mat2D operator *(double a, Mat2D B)
        {
            return B * a;
        }


        #endregion

    }

}


