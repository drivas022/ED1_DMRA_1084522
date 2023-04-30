using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Diego_Rivas
{
    internal class BuildingTree
    {
        private readonly List<Customer> _heap;

        public MinHeap()
        {
            _heap = new List<Customer>();
        }

        private static int Parent(int i) => (i - 1) / 2;
        private static int Left(int i) => 2 * i + 1;
        private static int Right(int i) => 2 * i + 2;

        private void Swap(int i, int j)
        {
            Customer temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        private void MinHeapify(int i)
        {
            int left = Left(i);
            int right = Right(i);
            int smallest = i;

            if (left < _heap.Count && _heap[left].Budget > _heap[smallest].Budget)
                smallest = left;

            if (right < _heap.Count && _heap[right].Budget > _heap[smallest].Budget)
                smallest = right;

            if (smallest != i)
            {
                Swap(i, smallest);
                MinHeapify(smallest);
            }
        }

        public void Insert(Customer customer)
        {
            _heap.Add(customer);
            int i = _heap.Count - 1;

            while (i > 0 && _heap[i].Budget > _heap[Parent(i)].Budget)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
        }

        public Customer ExtractMax()
        {
            if (_heap.Count == 0)
                return null;

            Customer max = _heap[0];
            _heap[0] = _heap[^1];
            _heap.RemoveAt(_heap.Count - 1);

            MinHeapify(0);

            return max;
        }

    public record Customer(long Dpi, int Budget, DateTime Date);
}
}
