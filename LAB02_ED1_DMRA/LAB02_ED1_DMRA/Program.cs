using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private static void Quicksort(double[] arr, string[] ID, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, ID, left, right);
                Quicksort(arr, ID, left, pivotIndex - 1);
                Quicksort(arr, ID, pivotIndex + 1, right);
            }
        }
        /*private static void BubbleSort(double[] arr, string[] ID)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Intercambiar elementos en arr
                        double temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;

                        // Intercambiar elementos en ID
                        string tempID = ID[j];
                        ID[j] = ID[j + 1];
                        ID[j + 1] = tempID;
                    }
                }
            }
        }*/
        private static int Partition(double[] arr, string[] ID, int left, int right)
        {
            double pivotValue = arr[right];
            int pivotIndex = left - 1;
            for (int i = left; i < right; i++)
            {
                if (arr[i] <= pivotValue)
                {
                    pivotIndex++;
                    Swap(arr, ID, pivotIndex, i);
                }
            }
            Swap(arr, ID, pivotIndex + 1, right);
            return pivotIndex + 1;
        }

        private static void Swap(double[] arr, string[] ID, int i, int j)
        {
            double tempD = arr[i];
            string tempS = ID[i];
            arr[i] = arr[j];
            ID[i] = ID[j];
            arr[j] = tempD;
            ID[j] = tempS;
        }

        private static void PrintResults(string[] ID, int contRes)
        {
            var finalResult = new StringBuilder("[");
            for (int i = 0; i < contRes; i++)
            {
                finalResult.Append($"\"{ID[i]}\"");
                if (i < contRes - 1)
                {
                    finalResult.Append(",");
                }
            }
            finalResult.Append("]");

            Console.WriteLine(finalResult);
        }
    }
}
