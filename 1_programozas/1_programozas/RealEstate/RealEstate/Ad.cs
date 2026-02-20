
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RealEstate
{
    internal class Ad
    {
        public int Area { get; set; }
        public Category Category { get; set; }
        public DateTime CreateAt { get; set; }
        public string Description { get; set; }
        public int Floors { get; set; }
        public bool FreeOfCharge { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string LatLong { get; set; }
        public int Rooms { get; set; }
        public Seller Seller { get; set; }

        public Ad(int area, Category category, DateTime createAt, string description,
                  int floors, bool freeOfCharge, int id, string imageUrl,
                  string latLong, int rooms, Seller seller)
        {
            Area = area;
            Category = category;
            CreateAt = createAt;
            Description = description;
            Floors = floors;
            FreeOfCharge = freeOfCharge;
            Id = id;
            ImageUrl = imageUrl;
            LatLong = latLong;
            Rooms = rooms;
            Seller = seller;
        }

        // 3. feladat – CSV betöltés
        public static List<Ad> LoadFromCsv(string fileName)
        {
            var ads = new List<Ad>();
            var lines = File.ReadAllLines(fileName).Skip(1);

            foreach (var line in lines)
            {
                var parts = line.Split(';');

                int id = int.Parse(parts[0]);
                int rooms = int.Parse(parts[1]);
                string latLong = parts[2];
                int floors = int.Parse(parts[3]);
                int area = int.Parse(parts[4]);
                string description = parts[5];
                bool freeOfCharge = parts[6] == "1";
                string imageUrl = parts[7];
                DateTime createAt = DateTime.Parse(parts[8]);
                int sellerId = int.Parse(parts[9]);
                string sellerName = parts[10];
                string sellerPhone = parts[11];
                int categoryId = int.Parse(parts[12]);
                string categoryName = parts[13];

                var category = new Category(categoryId, categoryName);
                var seller = new Seller(sellerId, sellerName, sellerPhone);

                ads.Add(new Ad(area, category, createAt, description,
                               floors, freeOfCharge, id,
                               imageUrl, latLong, rooms, seller));
            }

            return ads;
        }

        // 7. feladat – DistanceTo metódus
        public double DistanceTo(double lat, double lon)
        {
            var coords = LatLong.Split(',');
            double myLat = double.Parse(coords[0], CultureInfo.InvariantCulture);
            double myLon = double.Parse(coords[1], CultureInfo.InvariantCulture);

            return Math.Sqrt(Math.Pow(myLat - lat, 2) + Math.Pow(myLon - lon, 2));
        }

        public override string ToString()
        {
            return
                   $"Eladó neve: {Seller.Name}\n" +
                   $"Telefonszám: {Seller.Phone}\n" +
                   $"Alapterület: {Area}\n" +
                   $"Szobák száma: {Rooms}\n";
        }
    }
}
