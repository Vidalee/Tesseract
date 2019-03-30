using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BinaryHeap", menuName = "GeneralScript/BinaryHeap")]
public class BinaryHeap : ScriptableObject
{
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
}