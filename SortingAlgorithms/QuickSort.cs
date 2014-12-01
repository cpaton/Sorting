using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    public class QuickSort<T> : ISorter<T> where T : IComparable<T>
    {
        private readonly Random _random = new Random();

        public void Sort(T[] itemsToSort)
        {
            DoQuickSort(itemsToSort, 0, itemsToSort.Length - 1, "");
        }

        private void DoQuickSort(T[] itemsToSort, int start, int end, string indent)
        {
            //Console.WriteLine("{0}Sort called: Input=[{1}], Start={2}, End={3}", indent, string.Join(",", itemsToSort), start, end);

            if (start >= end)
            {
                return;
            }

            var segmentSize = end - start;
            if (segmentSize == 1)
            {
                if (itemsToSort[start].CompareTo(itemsToSort[end]) > 0)
                {
                    Swap(itemsToSort, start, end);
                }

                return;
            }

            var pivotIndex = start + _random.Next(0, segmentSize);
            var pivotValue = itemsToSort[pivotIndex];

            //Console.WriteLine("{0}Pivot Value Selected: {1}", indent, pivotValue);

            for (int currentIndex = start; currentIndex <= end; currentIndex++)
            {
                var comparisonToPivotValue = itemsToSort[currentIndex].CompareTo(pivotValue);

                if ((comparisonToPivotValue > 0 && currentIndex < pivotIndex))
                {
                    Swap(itemsToSort, currentIndex, pivotIndex);
                    pivotIndex = currentIndex;
                }
                if (comparisonToPivotValue < 0 && currentIndex > pivotIndex)
                {
                    if (pivotIndex + 1 == currentIndex)
                    {
                        Swap(itemsToSort, currentIndex, pivotIndex);
                        pivotIndex = currentIndex;
                    }
                    else
                    {
                        Swap(itemsToSort, pivotIndex, pivotIndex + 1);
                        Swap(itemsToSort, pivotIndex, currentIndex);
                        pivotIndex += 1;
                    }
                }
            }

            var newIndent = indent += "\t";
            DoQuickSort(itemsToSort, start, pivotIndex - 1, newIndent);
            DoQuickSort(itemsToSort, pivotIndex + 1, end, newIndent);
        }

        private void Swap(T[] items, int firstIndex, int secondIndex)
        {
            var temp = items[firstIndex];
            items[firstIndex] = items[secondIndex];
            items[secondIndex] = temp;
        }
    }


}
