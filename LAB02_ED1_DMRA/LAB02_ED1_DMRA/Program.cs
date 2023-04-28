using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB02_ED1_DMRA
{
    internal class Program
    {
        public class InputLab
        {
            public Input1[] input1 { get; set; }
            public Input2 input2 { get; set; }
        }
        public class Input1
        {
            public Dictionary<string, bool> services { get; set; }
            public Builds builds { get; set; }
        }

        public class Input2
        {
            public double budget { get; set; }
            public string typeBuilder { get; set; }
            public string[] requiredServices { get; set; }
            public string? commercialActivity { get; set; }
            public bool? wannaPetFriendly { get; set; }
            public string? minDanger { get; set; }
        }
        public class Houses
        {
            public string zoneDangerous { get; set; }
            public string address { get; set; }
            public double price { get; set; }
            public string contactPhone { get; set; }
            public string id { get; set; }
        }

        public class Apartments
        {
            public bool isPetFriendly { get; set; }
            public string address { get; set; }
            public double price { get; set; }
            public string contactPhone { get; set; }
            public string id { get; set; }
        }

        public class Premises
        {
            public string[] commercialActivities { get; set; }
            public string address { get; set; }
            public double price { get; set; }
            public string contactPhone { get; set; }
            public string id { get; set; }
        }

        public class Builds
        {
            public Premises[]? Premises { get; set; }
            public Apartments[]? Apartments { get; set; }
            public Houses[]? Houses { get; set; }
        }
                
        static void Main(string[] args)
        {

        }
        private static int ProcessApartments(InputLab input, string[] ID, double[] prices)
        {
            int contRes = 0;
            foreach (var item in input.input1)
            {
                if (item.builds.Apartments != null)
                {
                    bool[] petFriendlyStatuses = item.builds.Apartments.Select(a => a.isPetFriendly).ToArray();
                    double[] apartmentPrices = item.builds.Apartments.Select(a => a.price).ToArray();

                    for (int i = 0; i < item.builds.Apartments.Length; i++)
                    {
                        if (petFriendlyStatuses[i] == input.input2.wannaPetFriendly && apartmentPrices[i] <= input.input2.budget)
                        {
                            ID[contRes] = item.builds.Apartments[i].id;
                            prices[contRes] = apartmentPrices[i];
                            contRes++;
                        }
                    }
                }
            }
            return contRes;
        }
        private static int ProcessHouses(InputLab input, string[] ID, double[] prices)
        {
            int contRes = 0;
            int dangerLevel = 0;

            for (int i = 0; i < input.input1.Length; i++)
            {
                if (input.input1[i].builds.Houses == null) { continue; }

                for (int j = 0; j < input.input1[i].builds.Houses.Length; j++)
                {
                    var house = input.input1[i].builds.Houses[j];

                    // Assign a number depending on the danger zone color.
                    switch (house.zoneDangerous)
                    {
                        case "Green":
                            dangerLevel = 3;
                            break;
                        case "Yellow":
                            dangerLevel = 2;
                            break;
                        case "Orange":
                            dangerLevel = 1;
                            break;
                        case "Red":
                            dangerLevel = 0;
                            break;
                    }

                    if (Convert.ToInt32(dangerLevel) <= Convert.ToInt32(input.input2.minDanger) && Convert.ToInt32(house.price) <= Convert.ToInt32(input.input2.budget))
                    {
                        ID[contRes] = house.id;
                        prices[contRes] = house.price;
                        contRes++;
                    }
                }
            }
            return contRes;
        }
        private static int ProcessPremises(InputLab input, string[] ID, double[] prices)
        {
            int contRes = 0;

            foreach (var item in input.input1)
            {
                if (item.builds.Premises != null)
                {
                    foreach (var premise in item.builds.Premises)
                    {
                        if (premise.commercialActivities.Contains(input.input2.commercialActivity) && premise.price <= input.input2.budget)
                        {
                            ID[contRes] = premise.id;
                            prices[contRes] = premise.price;
                            contRes++;
                        }
                    }
                }
            }
            return contRes;
        }
    }
}
