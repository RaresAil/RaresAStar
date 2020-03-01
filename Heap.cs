using System;
namespace RaresAStar
{
    public class Heap<T> where T : IHeapItem<T>
    {
        public T[] items;
        int currentItemsCount = 0;

        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
        }

        public void Add(T item)
        {
            item.HeapIndex = currentItemsCount;
            items[currentItemsCount] = item;
            SortUp(item);
            currentItemsCount++;
        }

        public T RemoveFirst()
        {
            T first = items[0];
            currentItemsCount--;
            items[0] = items[currentItemsCount];
            items[0].HeapIndex = 0;
            SortDown(first);
            return first;
        }

        public void UpdateItem(T item)
        {
            SortUp(item);
            SortDown(item);
        }

        public int Count => items.Length;

        public long LongCount => items.LongLength;

        public bool Contains(T item)
        {
            if (item == null)
                return false;
            if (!(items[item.HeapIndex] is T check))
                return false;
            return Equals(check, item);
        }

        private void SortUp(T child)
        {
            int parentIndex = (child.HeapIndex - 1) / 2;
            while (true)
            {
                T parent = items[parentIndex];
                if (child.CompareTo(parent) > 0)
                {
                    Swap(child, parent);
                }
                else
                {
                    break;
                }
                parentIndex = (child.HeapIndex - 1) / 2;
            }
        }

        private void SortDown(T parent)
        {
            while (true)
            {
                int leftChildIndex = (parent.HeapIndex * 2) + 1;
                int rightChildIndex = (parent.HeapIndex * 2) + 2;

                if (leftChildIndex < currentItemsCount)
                {
                    int swapIndex = leftChildIndex;

                    if (rightChildIndex < currentItemsCount)
                    {
                        if (items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0)
                        {
                            swapIndex = rightChildIndex;
                        }
                    }

                    T child = items[swapIndex];
                    if (parent.CompareTo(child) < 0)
                    {
                        Swap(parent, child);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void Swap(T a, T b)
        {
            items[a.HeapIndex] = b;
            items[b.HeapIndex] = a;
            int itemAIndex = a.HeapIndex;
            a.HeapIndex = b.HeapIndex;
            b.HeapIndex = itemAIndex;
        }
    }

    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex
        {
            get;
            set;
        }

    }
}