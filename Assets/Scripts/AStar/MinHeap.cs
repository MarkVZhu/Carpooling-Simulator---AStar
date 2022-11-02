using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// min-heap
/// </summary>
/// <typeparam name="T"></typeparam>
public class MinHeap<T> where T : IComparer
{
    public List<T> data;
    public MinHeap(int capacity)
    {
        data = new List<T>(capacity);
    }
    public MinHeap()
    {
        data = new List<T>();
    }

    /// <summary>
    /// Heapify operation 
    /// </summary>
    /// <param name="arr"></param>
    public MinHeap(T[] arr)
    {
        this.data = new List<T>(arr);
        for (var i = parent(size() - 1); i >= 0; i--)
        {
            SiftDown(i);
        }

    }
    public int size()
    {
        return data.Count;
    }
    public bool isEmpty()
    {
        return data.Count == 0;
    }
 
    private int parent(int index)
    {
        if (index <= 0)
        {
            Debug.LogError(" index-0 don't hace parnt");
        }
        return (index - 1) / 2;
    }
    private int leftChild(int parent)
    {
        return parent * 2 + 1;
    }
    private int rightChild(int parent)
    {
        return parent * 2 + 2;
    }
    public void Add(T data)
    {
        this.data.Add(data);

        SiftUp(this.data.Count - 1);

    }

    public void SiftUp(int k)
    {
        // K cannot be out of bounds and the current node is greater than the parent node
        while (k > 0 && data[0].Compare(data[parent(k)], data[k]) > 0)
        {
            Swap(k, parent(k));
            k = parent(k);
        }
    }
    public void Swap(int i, int j)
    {
        if (i < 0 || i >= size() || j < 0 || j >= size())
        {
            Debug.LogError("  Index is illegal.");
        }
        var temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }
    public T findMin()
    {
        if (isEmpty())
        {
            Debug.LogError(" can dot findMax whenheap is empty");
        }
        return data[0];
    }
    public T ExtractMin()
    {
        T temp = findMin();
        Swap(0, data.Count - 1);
        data.RemoveAt(size() - 1);
        SiftDown(0);
        return temp;
    }
    /// <summary>
    /// Refactoring heap
    /// </summary>
    /// <param name="k"></param>
    private void SiftDown(int k)
    {
        //The index of the left child cannot exceed the left index if one exists  
        while (leftChild(k) < size())
        {
            int j = leftChild(k);
            //There is a child on the right and the child on the right is smaller than the child on the left
            if (j + 1 < size() && data[0].Compare(data[j], data[j + 1]) > 0)
            {
                j = rightChild(k);
            }
            //data[j] is the minimum value in the left Right Child
            if (data[0].Compare(data[k], data[j]) <= 0) break;

            Swap(k, j);
            k = j;

        }
    }

    // Remove the smallest element and place a new element
    public T replace(T _data)
    {
        T temp = findMin();
        data[0] = _data;
        SiftDown(0);
        return temp;
    }

    public bool Contains(T node)
    {
        return data.Contains(node);
    }

    public void Clear()
    {
        data.Clear();
    }

}