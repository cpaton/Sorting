using System;

namespace SortingAlgorithms
{
    public interface ISorter<in T> where T : IComparable<T>
    {
        void Sort(T[] itemsToSort);
    }
}