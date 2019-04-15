﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BinaryHeap", menuName = "GeneralScript/BinaryHeap")]
public class BinaryHeap : ScriptableObject
{
    /* Code de Wiwi
    public void CreateMinHeap(IHeapNode[] key)
    {
        int l = key.Length;
        for (int i = l/2 - 1; i > - 1; i--)
        {
            SiftMinDown(key, i, l);
        }
    }

    public void CreateMaxHeap(IHeapNode[] key)
    {
        int l = key.Length;
        for (int i = l/2 - 1; i > - 1; i--)
        {
            SiftMaxDown(key, i, l);
        }
    }
    
    public void SiftMinDown(IHeapNode[] heap, int index, int n)
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
    
    public void SiftMaxDown(IHeapNode[] heap, int index, int n)
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

    public void MinHeapSort(IHeapNode[] key)
    {
        int l = key.Length;
        for (int i = l - 1; i > -1; i--)
        {
            IHeapNode save = key[i];
            key[i] = key[0];
            key[0] = save;
            SiftMaxDown(key, 0, i);
        }
    }
    
    public void MaxHeapSort(IHeapNode[] key)
    {
        int l = key.Length;
        for (int i = l - 1; i > -1; i--)
        {
            IHeapNode save = key[i];
            key[i] = key[0];
            key[0] = save;
            SiftMinDown(key, 0, i);
        }
    }
    */
    
    public void CreateMinHeap(List<IHeapNode> key)
    {
        int l = key.Count;
        for (int i = l/2 - 1; i > - 1; i--)
        {
            SiftMinDown(key, i, l);
        }
    }

    public void CreateMaxHeap(List<IHeapNode> key)
    {
        int l = key.Count;
        for (int i = l/2 - 1; i > - 1; i--)
        {
            SiftMaxDown(key, i, l);
        }
    }
    
    public void SiftMinDown(List<IHeapNode> heap, int index, int n)
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
    
    public void SiftMaxDown(List<IHeapNode> heap, int index, int n)
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

    public void SiftMinUp(List<IHeapNode> heap, int lastIndex)
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
    
    public void SiftMaxUp(List<IHeapNode> heap, int lastIndex)
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
    
    public void MinHeapSort(List<IHeapNode> key)
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
    
    public void MaxHeapSort(List<IHeapNode> key)
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

    public IHeapNode MinPop(List<IHeapNode> key)
    {
        int l = key.Count;
        IHeapNode min = key[0];
        (key[0], key[l - 1]) = (key[l - 1], null);
        SiftMinDown(key, 0, l);
        return min;
    }
    
    public IHeapNode MaxPop(List<IHeapNode> key)
    {
        int l = key.Count;
        IHeapNode max = key[0];
        (key[0], key[l - 1]) = (key[l - 1], null);
        SiftMaxDown(key, 0, l);
        return max;
    }

    public void MinPush(List<IHeapNode> key,IHeapNode item)
    {
        key.Add(item);
        SiftMinUp(key, key.Count - 1);
    }
    
    public void MaxPush(List<IHeapNode> key,IHeapNode item)
    {
        key.Add(item);
        SiftMaxUp(key, key.Count - 1);
    }
}