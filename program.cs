using System;
using System.Collections;


namespace unityai {

    class Program {
        static void Main(string[] args) {
                
            // statement
            // printing Hello World!
            // Console.WriteLine("Hello World!");

            // Mat2D A = new Mat2D(3, 3) {
            //     {1, 2, 3},
            //     {4, 5, 6},
            //     {7, 8, 9}
            // };
            
            // Mat2D D = A[0..1, 0..1];

            // Mat2D B = Mat2D.Ones(2, 2);

            // Mat2D C = D + B;

            // C.PrintMatrix();

            //(double min, _, _) = C.Min();

            //Console.WriteLine(min);

            Console.WriteLine(double.PositiveInfinity.ToString());


            Mat2D map = new Mat2D(10, 10) {
                {double.NaN, double.NaN,              double.NaN,               double.NaN,             double.NaN,              double.NaN,              double.NaN,              double.NaN,              double.NaN,              double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, 0,                       double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NaN},
                {double.NaN, double.NaN,              double.NaN,               double.NaN,             double.NaN,              double.NaN,              double.NaN,              double.NaN,              double.NaN,              double.NaN},
            };

            map.PrintMatrix();

            Mat2D distanceTransform = PathFinding.DistanceTransform(map, Mat2D.ManhattenKernal);

            distanceTransform.PrintMatrix();
            
        }
    }
}