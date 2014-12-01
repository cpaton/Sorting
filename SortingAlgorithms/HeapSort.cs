using System;

namespace SortingAlgorithms
{
    public class HeapSort<T> : ISorter<T> where T : IComparable<T>
    {
        private readonly Func<int, IHeap<T>> _heapFactory;

        public HeapSort(Func<int, IHeap<T>> heapFactory)
        {
            _heapFactory = heapFactory;
        }

        public void Sort(T[] itemsToSort)
        {
            var heap = _heapFactory(itemsToSort.Length);
            heap.Populate(itemsToSort);

            var index = 0;
            while (!heap.Empty)
            {
                var smallestItem = heap.Remove();
                itemsToSort[index] = smallestItem;
                index++;
            }
        }
    }
}