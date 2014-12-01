using System;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public interface IHeap<T> where T : IComparable<T>
    {
        bool Empty { get; }
        void Populate(IEnumerable<T> items);
        T Remove();
    }
}