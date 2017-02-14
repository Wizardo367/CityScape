using System;

// Video used: https://www.youtube.com/watch?v=3Dw5d7PlcTM&t=580s

public class Heap<T> where T : IHeapItem<T>
{
    public T[] Items { get; private set; }
    private int _currentItemCount;

    public Heap(int maxHeapSize)
    {
        Items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = _currentItemCount;
        Items[_currentItemCount] = item;
        SortUp(item);
        _currentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = Items[0];
        _currentItemCount--;
        Items[0] = Items[_currentItemCount];
        Items[0].HeapIndex = 0;
        SortDown(Items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get { return _currentItemCount; }
    }

    public bool Contains(T item)
    {
        return Equals(Items[item.HeapIndex], item);
    }

    public void SortDown(T item)
    {
        // Sorts item downwars within tree

        while (true)
        {
            int heapIndexTwo = item.HeapIndex * 2;
            int childIndexLeft = heapIndexTwo + 1;
            int childIndexRight = heapIndexTwo + 2;

            if (childIndexLeft < _currentItemCount)
            {
                int swapIndex = childIndexLeft;

                if (childIndexRight < _currentItemCount)
                    if (Items[childIndexLeft].CompareTo(Items[childIndexRight]) < 0)
                        swapIndex = childIndexRight;

                if (item.CompareTo(Items[swapIndex]) < 0)
                    Swap(item, Items[swapIndex]);
                else
                    return;
            }
            else
                return;
        }
    }

    public void SortUp(T item)
    {
        // Sorts item upwards within tree
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = Items[parentIndex];

            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;
        }
    }

    private void Swap(T itemA, T itemB)
    {
        Items[itemA.HeapIndex] = itemB;
        Items[itemB.HeapIndex] = itemA;

        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get; set;
    }
}