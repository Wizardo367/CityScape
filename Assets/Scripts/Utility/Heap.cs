using System;

// Video used: https://www.youtube.com/watch?v=3Dw5d7PlcTM&t=580s

/// <summary>
/// Heap data structure.
/// </summary>
/// <typeparam name="T">Data type.</typeparam>
public class Heap<T> where T : IHeapItem<T>
{
	/// <summary>
	/// Heap elements.
	/// </summary>
	/// <value>
	/// The elements.
	/// </value>
	public T[] Items { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Heap{T}"/> class.
	/// </summary>
	/// <param name="maxHeapSize">Maximum size of the heap.</param>
	public Heap(int maxHeapSize)
    {
        Items = new T[maxHeapSize];
    }

	/// <summary>
	/// Adds the specified item.
	/// </summary>
	/// <param name="item">The item.</param>
	public void Add(T item)
    {
        item.HeapIndex = Count;
        Items[Count] = item;
        SortUp(item);
        Count++;
    }

	/// <summary>
	/// Removes the first item.
	/// </summary>
	/// <returns>The first item.</returns>
	public T RemoveFirst()
    {
        T firstItem = Items[0];
        Count--;
        Items[0] = Items[Count];
        Items[0].HeapIndex = 0;
        SortDown(Items[0]);
        return firstItem;
    }

	/// <summary>
	/// Updates the item.
	/// </summary>
	/// <param name="item">The item.</param>
	public void UpdateItem(T item)
    {
        SortUp(item);
    }

	/// <summary>
	/// Gets the count.
	/// </summary>
	/// <value>
	/// The count.
	/// </value>
	public int Count { get; private set; }

	/// <summary>
	/// Determines whether the heap contains the specified item.
	/// </summary>
	/// <param name="item">The item.</param>
	/// <returns>
	///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
	/// </returns>
	public bool Contains(T item)
    {
        return Equals(Items[item.HeapIndex], item);
    }

	/// <summary>
	/// Sorts an item downwards within the tree.
	/// </summary>
	/// <param name="item">The item.</param>
	public void SortDown(T item)
    {
        // Sorts item downwards within the tree

        while (true)
        {
            int heapIndexTwo = item.HeapIndex * 2;
            int childIndexLeft = heapIndexTwo + 1;
            int childIndexRight = heapIndexTwo + 2;

            if (childIndexLeft < Count)
            {
                int swapIndex = childIndexLeft;

                if (childIndexRight < Count)
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

	/// <summary>
	/// Sorts an item upwards within the tree.
	/// </summary>
	/// <param name="item">The item.</param>
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

	/// <summary>
	/// Swaps the specified items.
	/// </summary>
	/// <param name="itemA">Item a.</param>
	/// <param name="itemB">Item b.</param>
	private void Swap(T itemA, T itemB)
    {
        Items[itemA.HeapIndex] = itemB;
        Items[itemB.HeapIndex] = itemA;

        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

/// <summary>
/// Defines a heap item.
/// </summary>
/// <typeparam name="T">Data type.</typeparam>
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get; set;
    }
}