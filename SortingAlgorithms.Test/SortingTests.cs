using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SortingAlgorithms.Test
{
    public abstract class SortingTestBase
    {
        private void CheckSort<T>(T[] toSort, T[] expectedResult) where T : IComparable<T>
        {
            var sorter = CreateSorter<T>();
            sorter.Sort(toSort);

            Assert.NotNull(toSort);
            Assert.Equal(expectedResult.Length, toSort.Length);
            Assert.Equal(expectedResult, toSort);
        }

        protected abstract ISorter<T> CreateSorter<T>() where T : IComparable<T>;
        
        [Fact]
        public void Should_sort_empty_list()
        {
            CheckSort(new int[] {}, new int[] { });
        }

        [Fact]
        public void Should_sort_list_of_1_item()
        {
            CheckSort(new int[] { 10 }, new int[] { 10 });
        }

        [Fact]
        public void Should_sort_items_given_in_reverse_order()
        {
            for (int inputSize = 1; inputSize <= 1000; inputSize++)
            {
                //Console.WriteLine("********* Sort Begin *******");

                var itemsInOrder = new int[inputSize];
                var itemsInReverseOrder = new int[inputSize];

                for (int i = 1; i <= inputSize; i++)
                {
                    itemsInOrder[i - 1] = i;
                    itemsInReverseOrder[itemsInReverseOrder.Length - i] = i;
                }

                CheckSort(itemsInReverseOrder, itemsInOrder);

                //Console.WriteLine();
                //Console.WriteLine("********* Sort End *******");
                //Console.WriteLine();
            }
            
        }

        [Fact]
        public void Should_sort_items_given_in_random_order()
        {
            for (int inputSize = 1; inputSize <= 1000; inputSize++)
            {
                //Console.WriteLine("********* Sort Begin *******");

                var itemsInOrder = new int[inputSize];
                var itemsInRandomOrder = new int[inputSize];
                var indexesToChooseFrom = new List<int>(inputSize);

                for (int i = 1; i <= inputSize; i++)
                {
                    itemsInOrder[i - 1] = i;
                    indexesToChooseFrom.Add(i - 1);
                }
                var random = new Random();
                for (int i = 1; i <= inputSize; i++)
                {
                    var randomIndex = random.Next(0, indexesToChooseFrom.Count - 1);
                    var insertIndex = indexesToChooseFrom[randomIndex];
                    indexesToChooseFrom.RemoveAt(randomIndex);
                    itemsInRandomOrder[insertIndex] = itemsInOrder[i-1];
                }

                CheckSort(itemsInRandomOrder, itemsInOrder);

                //Console.WriteLine();
                //Console.WriteLine("********* Sort End *******");
                //Console.WriteLine();
            }
        }

    }
}
