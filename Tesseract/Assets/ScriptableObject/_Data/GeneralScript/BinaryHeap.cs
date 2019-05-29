using System;
using System.Collections.Generic;
using Script.GlobalsScript;
using UnityEngine;

public static class BinaryHeap
{
    public static void CreateMinHeap(List<IHeapNode> key)
    {
        int l = key.Count;
        for (int i = l/2 - 1; i > - 1; i--)
        {
            SiftMinDown(key, i, l);
        }
    }

    public static void CreateMaxHeap(List<IHeapNode> key)
    {
        int l = key.Count;
        for (int i = l/2 - 1; i > - 1; i--)
        {
            SiftMaxDown(key, i, l);
        }
    }
    
    public static void SiftMinDown(List<IHeapNode> heap, int index, int n)
    {
        int indexSon = 2 * index + 1;
        while (indexSon < n)
        {
            if (indexSon + 1 < n && heap[indexSon].Comparable() > heap[indexSon + 1].Comparable())
            {
                indexSon++;
            }

            if (heap[indexSon].Comparable() >= heap[index].Comparable()) return;
            
            IHeapNode save = heap[index];
            heap[index] = heap[indexSon];
            heap[indexSon] = save;
            index = indexSon;
            indexSon = indexSon * 2 + 1;
        }
    }
    
    public static void SiftMaxDown(List<IHeapNode> heap, int index, int n)
    {
        int indexSon = 2 * index + 1;
        while (indexSon < n)
        {
            if (indexSon + 1 < n && heap[indexSon].Comparable() < heap[indexSon + 1].Comparable())
            {
                indexSon++;
            }

            if (heap[indexSon].Comparable() <= heap[index].Comparable()) return;
            
            IHeapNode save = heap[index];
            heap[index] = heap[indexSon];
            heap[indexSon] = save;
            index = indexSon;
            indexSon = indexSon * 2 + 1;
        }
    }

    public static void SiftMinUp(List<IHeapNode> heap, int lastIndex)
    {
        int node = lastIndex;
        int parent = node / 2;
        while (node != 1 && heap[node].Comparable() < heap[parent].Comparable())
        {
            (heap[node], heap[parent]) = (heap[parent], heap[node]);
            node = parent;
            parent = node / 2;
        }
    }
    
    public static void SiftMaxUp(List<IHeapNode> heap, int lastIndex)
    {
        int node = lastIndex;
        int parent = node / 2;
        while (node != 1 && heap[node].Comparable() > heap[parent].Comparable())
        {
            (heap[node], heap[parent]) = (heap[parent], heap[node]);
            node = parent;
            parent = node / 2;
        }
    }
    
    public static void MinHeapSort(List<IHeapNode> key)
    {
        int l = key.Count;
        for (int i = l - 1; i > -1; i--)
        {
            IHeapNode save = key[i];
            key[i] = key[0];
            key[0] = save;
            SiftMaxDown(key, 0, i);
        }
    }
    
    public static void MaxHeapSort(List<IHeapNode> key)
    {
        int l = key.Count;
        for (int i = l - 1; i > -1; i--)
        {
            IHeapNode save = key[i];
            key[i] = key[0];
            key[0] = save;
            SiftMinDown(key, 0, i);
        }
    }

    public static IHeapNode MinPop(List<IHeapNode> key)
    {
        int l = key.Count - 1;
        IHeapNode min = key[0];
        (key[0], key[l]) = (key[l], key[0]);
        key.Remove(min);
        SiftMinDown(key, 0, l);
        return min;
    }
    
    public static IHeapNode MaxPop(List<IHeapNode> key)
    {
        int l = key.Count - 1;
        IHeapNode max = key[0];
        (key[0], key[l]) = (key[l], key[0]);
        key.Remove(max);
        SiftMaxDown(key, 0, l);
        return max;
    }

    public static void MinPush(List<IHeapNode> key,IHeapNode item)
    {
        key.Add(item);
        SiftMinUp(key, key.Count - 1);
    }
    
    public static void MaxPush(List<IHeapNode> key,IHeapNode item)
    {
        key.Add(item);
        SiftMaxUp(key, key.Count - 1);
    }
}