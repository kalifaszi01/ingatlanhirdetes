using System;
using System.Linq;
using System.Collections.Generic;

namespace RealEstate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ads = Ad.LoadFromCsv("realestates.csv");

            // 6. feladat – Földszinti ingatlanok átlagos alapterülete
            var groundFloorAds = ads.Where(a => a.Floors == 0);

            double averageArea = groundFloorAds.Average(a => a.Area);

            Console.WriteLine($"Földszinti ingatlanok átlagos alapterülete: {averageArea:F2} m2");

            // 8. feladat – Mesevár óvoda koordinátái
            double ovodaLat = 47.4164220114023;
            double ovodaLon = 19.066342425796986;

            var closest = ads
                .Where(a => a.FreeOfCharge)
                .OrderBy(a => a.DistanceTo(ovodaLat, ovodaLon))
                .First();

            Console.WriteLine("\nA Mesevár óvodához legközelebbi tehermentes ingatlan:");
            Console.WriteLine(closest);
        }
    }
}