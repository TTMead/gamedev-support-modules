using System;
using System.Collections;

namespace unityai {

    class PathFinding {

        // Applies a distance transform to a given map
        // Use the following key to initialise:
        // Obstructions -> NaN
        // Travel Space -> +Inf
        // Target -> 0
        public static Mat2D DistanceTransform(Mat2D Map, Mat2D Kernal) {
            // Clone the matrix so changes aren't affected at the pointer
            Map = (Mat2D) Map.Clone();

            Mat2D prevMap = (Mat2D) Map.Clone();
            Mat2D newMap;

            while(true) {
                // Copy the old map
                newMap = (Mat2D) prevMap.Clone();

                // Apply kernal to every square
                for (int x = 0; x < Map.Size(0) - 2; x++) {
                    for (int y = 0; y < Map.Size(1) - 2; y++) {
                        if (Convert.ToDouble(prevMap[x+1, y+1]) < 0)
                            continue;
                        
                        Mat2D window = prevMap[x..(x+2), y..(y+2)];
                        (double newVal, _, _) = (window + Mat2D.EuclideanKernal).Min();
                        newMap[x+1, y+1] = newVal;
                    }
                }

                // Check if the new map has changed
                if (newMap == prevMap)
                    break;
                
                prevMap = (Mat2D) newMap.Clone();
            }

            
            return newMap;
        }

    }

}