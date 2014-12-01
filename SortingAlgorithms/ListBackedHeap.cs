using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SortingAlgorithms
{
    public class ListBackedHeap<T> : IHeap<T> where T : IComparable<T>
    {
        private readonly List<T[]> _heapData = new List<T[]>();
        private HeapItemIndex _insertIndex = new HeapItemIndex(0, 0);

        private T this[HeapItemIndex index]
        {
            get { return _heapData[index.Level][index.Index]; }
            set { _heapData[index.Level][index.Index] = value; }
        }

        public bool Empty
        {
            get { return _insertIndex == HeapItemIndex.Top; }
        }

        public void Populate(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                EnsureSpaceInHeap();
                this[_insertIndex] = item;
                UpHeap(_insertIndex);
                _insertIndex = _insertIndex.Next();
            }
        }

        public T Remove()
        {
            if (Empty)
            {
                throw new InvalidOperationException("Cannot remove an item from an empty heap");
            }

            var returnValue = this[HeapItemIndex.Top];
            _insertIndex = _insertIndex.Previous();
            Swap(HeapItemIndex.Top, _insertIndex);
            this[_insertIndex] = default(T);

            if (!Empty)
            {
                DownHeap(HeapItemIndex.Top);
            }

            return returnValue;
        }

        private void DownHeap(HeapItemIndex index)
        {
            var smallestChildIndex = IndexOfSmallestChild(index);
            if (smallestChildIndex == null)
            {
                return;
            }

            if (this[smallestChildIndex].CompareTo(this[index]) < 0)
            {
                Swap(index, smallestChildIndex);
                DownHeap(smallestChildIndex);
            }
        }

        private void EnsureSpaceInHeap()
        {
            if (_heapData.Count <= _insertIndex.Level || _heapData[_insertIndex.Level] == null)
            {
                _heapData.Add(new T[(int) Math.Pow(2, _insertIndex.Level)]);
            }
        }

        private HeapItemIndex IndexOfSmallestChild(HeapItemIndex index)
        {
            var leftChildIndex = index.LeftChild();
            var rightChildIndex = leftChildIndex.Next();

            var leftChildExists = leftChildIndex.CompareTo(_insertIndex) < 0;
            var rightChildExists = rightChildIndex.CompareTo(_insertIndex) < 0;

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

        private HeapItemIndex ParentIndex(HeapItemIndex index)
        {
            if (index.Level == 0)
            {
                return null;
            }

            return new HeapItemIndex(index.Level - 1, index.Index / 2);
        }

        private void Swap(HeapItemIndex index, HeapItemIndex parentIndex)
        {
            var temp = this[index];
            this[index] = this[parentIndex];
            this[parentIndex] = temp;
        }

        private void UpHeap(HeapItemIndex index)
        {
            var parentIndex = ParentIndex(index);
            if (parentIndex == null)
            {
                return;
            }

            var item = this[index];
            var parentItem = this[parentIndex];

            if (item.CompareTo(parentItem) < 0)
            {
                Swap(index, parentIndex);
                UpHeap(parentIndex);
            }
        }

        [DebuggerDisplay("Level: {Level}, Index: {Index}")]
        private class HeapItemIndex : IEquatable<HeapItemIndex>, IComparable<HeapItemIndex>
        {
            public static readonly HeapItemIndex Top = new HeapItemIndex(0, 0);
            private readonly int _index;
            private readonly int _level;

            public HeapItemIndex(int level, int index)
            {
                _level = level;
                _index = index;
            }

            public int Index
            {
                get { return _index; }
            }

            public int Level
            {
                get { return _level; }
            }

            public int CompareTo(HeapItemIndex other)
            {
                if (ReferenceEquals(this, other))
                {
                    return 0;
                }

                if (ReferenceEquals(null, other))
                {
                    return -1;
                }

                var levelComparison = _level.CompareTo(other._level);
                if (levelComparison == 0)
                {
                    return _index.CompareTo(other._index);
                }
                return levelComparison;
            }

            public bool Equals(HeapItemIndex other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return _level == other._level && _index == other._index;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((HeapItemIndex) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (_level * 397) ^ _index;
                }
            }

            public HeapItemIndex LeftChild()
            {
                return new HeapItemIndex(_level + 1, 2 * _index);
            }

            public HeapItemIndex Next()
            {
                var maxIndexForThisLevel = (int) Math.Pow(2, _level) - 1;

                if (_index >= maxIndexForThisLevel)
                {
                    return new HeapItemIndex(_level + 1, 0);
                }
                return new HeapItemIndex(_level, _index + 1);
            }

            public HeapItemIndex Previous()
            {
                if (_index > 0)
                {
                    return new HeapItemIndex(_level, _index - 1);
                }
                if (_level > 0)
                {
                    return new HeapItemIndex(_level - 1, (int)Math.Pow(2, _level - 1) - 1);
                } 
                return Top;
            }

            public static bool operator ==(HeapItemIndex left, HeapItemIndex right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(HeapItemIndex left, HeapItemIndex right)
            {
                return !Equals(left, right);
            }
        }
    }
}