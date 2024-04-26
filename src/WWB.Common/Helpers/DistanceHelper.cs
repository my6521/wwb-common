namespace WWB.Common.Helpers
{
    public class DistanceHelper
    {
        private const double EARTH_RADIUS = 6371000;

        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        public static double CalculateDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lng2 - lng1);

            double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * Math.Pow(Math.Sin(dLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EARTH_RADIUS * c;
        }

        private static double ToRadians(double degrees) => (degrees * Math.PI) / 180;
    }
}