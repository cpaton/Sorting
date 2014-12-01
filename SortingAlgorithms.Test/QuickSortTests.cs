namespace SortingAlgorithms.Test
{
    public class QuickSortTests : SortingTestBase
    {
        protected override ISorter<T> CreateSorter<T>() 
        {
            return new QuickSort<T>();
        }
    }
}