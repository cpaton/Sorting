namespace SortingAlgorithms.Test
{
    public class HeapSortArrayTests : SortingTestBase
    {
        protected override ISorter<T> CreateSorter<T>()
        {
            return new HeapSort<T>(x => new ArrayBackedHeap<T>(x));
        }
    }
}