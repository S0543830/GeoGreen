using System;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    public static class Absolutwert
    {
        public static double Absltdou(this double value)
        {
            return Math.Abs(value);

        }

        public static float Absltflo(this float value)
        {
            return Math.Abs(value);
        }
    }
}