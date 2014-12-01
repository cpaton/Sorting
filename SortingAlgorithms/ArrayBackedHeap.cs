using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SortingAlgorithms
{
    [DebuggerDisplay("Heap\n{ToString()}")]
    public class ArrayBackedHeap<T> : IHeap<T> where T : IComparable<T>
    {
        private readonly T[] _heapData;
        private int _insertIndex = 1;

        public ArrayBackedHeap(int size)
        {
            _heapData = new T[size];
        }

        public bool Empty
        {
            get { return _insertIndex == 1; }
        }

        private T this[int index]
        {
            get { return _heapData[index - 1]; }
            set { _heapData[index - 1] = value; }
        }

        public void Populate(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                this[_insertIndex] = item;
                UpHeap(_insertIndex);
                _insertIndex++;
            }
        }

        public T Remove()
        {
            if (Empty)
            {
                throw new InvalidOperationException("Cannot remove an item from an empty heap");
            }

            var returnValue = this[1];
            _insertIndex--;
            Swap(1, _insertIndex);
            this[_insertIndex] = default(T);

            if (!Empty)
            {
                DownHeap(1);
            }

            return returnValue;
        }

        public override string ToString()
        {
            var currentLevel = 0;
            var currentIndex = 1;
            var heapAsString = new StringBuilder();

            while (currentIndex < _insertIndex)
            {
                var lastIndexIfCurrentLevel = (int) Math.Pow(2, currentLevel + 1) - 1;

                heapAsString.AppendFormat("{0} ", this[currentIndex]);

                if (currentIndex == lastIndexIfCurrentLevel)
                {
                    currentLevel++;
                    heapAsString.AppendLine();
                }

                currentIndex++;
            }

            return heapAsString.ToString();
        }

        private void DownHeap(int index)
        {
            var smallestChildIndex = IndexOfSmallestChild(index);
            if (smallestChildIndex == null)
            {
                return;
            }

            if (this[smallestChildIndex.Value].CompareTo(this[index]) < 0)
            {
                Swap(index, smallestChildIndex.Value);
                DownHeap(smallestChildIndex.Value);
            }
        }

        private int? IndexOfSmallestChild(int index)
        {
            var leftChildIndex = 2 * index;
            var rightChildIndex = leftChildIndex + 1;

            var leftChildExists = leftChildIndex < _insertIndex;
            var rightChildExists = rightChildIndex < _insertIndex;

            if (!leftChildExists)
            {
                return null;
            }

            if (!rightChildExists)
            {
                return leftChildIndex;
            }

            var indexOfSmallest = this[leftChildIndex].CompareTo(this[rightChildIndex]) <= 0 ? leftChildIndex : rightChildIndex;
            return indexOfSmallest;
        }

        private int? ParentIndex(int index)
        {
            if (index == 1)
            {
                return null;
            }

            int parentIndex;
            if (index % 2 == 0)
            {
                parentIndex = index / 2;
            }
            else
            {
                parentIndex = (index - 1) / 2;
            }
            return parentIndex;
        }

        private void Swap(int left, int right)
        {
            var temp = this[left];
            this[left] = this[right];
            this[right] = temp;
        }

        private void UpHeap(int insertedIndex)
        {
            var item = this[insertedIndex];
            var parentIndex = ParentIndex(insertedIndex);

            while (parentIndex != null && item.CompareTo(this[parentIndex.Value]) < 0)
            {
                Swap(insertedIndex, parentIndex.Value);
                insertedIndex = parentIndex.Value;
                parentIndex = ParentIndex(parentIndex.Value);
            }
        }
    }
}