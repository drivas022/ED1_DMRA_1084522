using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Diego_Rivas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Custom = @"C:\Users\driva\OneDrive - Universidad Rafael Landivar\Escritorio\Lab03-Diego-Rivas\Lab03-Diego-Rivas\input_customer_example_lab_3.jsonl";
            string Auctions = @"C:\Users\driva\OneDrive - Universidad Rafael Landivar\Escritorio\Lab03-Diego-Rivas\Lab03-Diego-Rivas\input_auctions_example_lab_3.jsonl";

            List<PropertyData> Bettors = new List<PropertyData>();
            List<Client> Clientes = new List<Client>();

            using (StreamReader sr = new StreamReader(Custom))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Client cliente = JsonSerializer.Deserialize<Client>(line);
                    Clientes.Add(cliente);
                }
            }

            using (StreamReader sr = new StreamReader(Auctions))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    PropertyData cliente = JsonSerializer.Deserialize<PropertyData>(line);
                    Bettors.Add(cliente);
                }
            }
             public class Node
             {
            public Client Value;
            public Node Left;
            public Node Right;
            public int Height;

            public Node(Client value)
            {
                Value = value;
                Height = 1;
            }
            }

        public Node Root;

        private int Height(Node node)
        {
            return node != null ? node.Height : 0;
        }

        private int BalanceFactor(Node node)
        {
            return Height(node.Right) - Height(node.Left);
        }

        private void UpdateHeight(Node node)
        {
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        private Node RotateLeft(Node node)
        {
            Node rightNode = node.Right;
            node.Right = rightNode.Left;
            rightNode.Left = node;
            UpdateHeight(node);
            UpdateHeight(rightNode);
            return rightNode;
        }

        private Node RotateRight(Node node)
        {
            Node leftNode = node.Left;
            node.Left = leftNode.Right;
            leftNode.Right = node;
            UpdateHeight(node);
            UpdateHeight(leftNode);
            return leftNode;
        }

        private Node Balance(Node node)
        {
            UpdateHeight(node);

            if (BalanceFactor(node) == 2)
            {
                if (BalanceFactor(node.Right) < 0)
                {
                    node.Right = RotateRight(node.Right);
                }
                return RotateLeft(node);
            }

            if (BalanceFactor(node) == -2)
            {
                if (BalanceFactor(node.Left) > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                return RotateRight(node);
            }

            return node;
        }

        private Node Insert(Node node, Client value)
        {
            if (node == null) return new Node(value);

            if (value.DPI < node.Value.DPI)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value.DPI > node.Value.DPI)
            {
                node.Right = Insert(node.Right, value);
            }

            return Balance(node);
        }
        public void Insert(Client value)
        {
            Root = Insert(Root, value);
        }
        

        public Client Find(long dpi)
        {
            Node currentNode = Root;

            while (currentNode != null)
            {
                if (dpi < currentNode.Value.DPI)
                {
                    currentNode = currentNode.Left;
                }
                else if (dpi > currentNode.Value.DPI)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    return currentNode.Value;
                }
            }
            return null;
        }
    }
    }

    public class Client
    {
    public long DPI { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Job { get; set; }
    public string PlaceJob { get; set; }
    public int Salary { get; set; }
    }

    public class PropertyData
    {
        [JsonProperty("property")]
        public string Property { get; set; }

        [JsonProperty("customers")]
        public List<Customer> Customers { get; set; }

        [JsonProperty("rejection")]
        public int Rejection { get; set; }

        public class Customer
        {
            [JsonProperty("dpi")]
            public long Dpi { get; set; }

        [JsonProperty("budget")]
        public int Budget { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
        }
    }


