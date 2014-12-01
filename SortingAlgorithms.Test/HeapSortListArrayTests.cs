namespace SortingAlgorithms.Test
{
    public class HeapSortListArrayTests : SortingTestBase
    {
        protected override ISorter<T> CreateSorter<T>()
        {
            return new HeapSort<T>(_ => new ListBackedHeap<T>());
        }
    }
}