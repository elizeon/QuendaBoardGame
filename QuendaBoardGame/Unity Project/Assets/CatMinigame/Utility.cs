using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Utility methods used throughout game
    /// By Elizabeth Haynes
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Get max of the 2 values
        /// </summary>
        /// <param name="a">val 1</param>
        /// <param name="b">val 2</param>
        /// <returns>max of the 2 values</returns>
        public static float Max(float a, float b)
        {
            if (a >= b)
                return a;
            return b;
        }
        /// <summary>
        /// Check if value is within angle in radians
        /// </summary>
        /// <param name="a">angle 1 in radians</param>
        /// <param name="b">angle 2 in radians</param>
        /// <param name="restriction">angle difference limit in radians</param>
        /// <returns></returns>
        public static bool WithinAngle(float a, float b, float restriction)
        {
            a = Mathf.Rad2Deg *a;
            b = Mathf.Rad2Deg * b;
            if (360 - (Mathf.Abs(a - b)) % 360 < restriction || (Mathf.Abs(a - b)) % 360 < restriction)
            {
                return true;
            }
            return false;
        }

    }

