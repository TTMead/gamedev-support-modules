using System;
using System.Collections;
using MatSup;

class Program {
    static void Main(string[] args) {
            
        // Create a new 3x3 Matrix
        Mat2D A = new Mat2D(3, 3) {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };
        // Use PrintMatrix to print the value of a matrix to console
        A.PrintMatrix();
        // Can specify name of matrix
        A.PrintMatrix("A");
        


        // Select a range of columns and rows from A
        Mat2D B = A[0..1, 0..1];
        B.PrintMatrix("B");



        // Transpose a Matrix
        (~B).PrintMatrix("Transpose of B");



        // Create a Matrix of ones
        Mat2D C = Mat2D.Ones(2, 2);
        C.PrintMatrix("C");


        
        // Scalar Multiplication
        (2*C).PrintMatrix("2C");



        // Element-Wise Addition
        Mat2D D = ~B + (2*C);
        D.PrintMatrix("Transpose of B + 2C");

        // Matrix Multiplication
        Mat2D E = ~B * (2*C);
        E.PrintMatrix("Transpose of B multiplied by 2C");

        // Element-Wise Multiplication
        Mat2D F = ~B % (2*C);
        F.PrintMatrix("Transpose of B element-wise muliplied by 2C");

        
    }
}
