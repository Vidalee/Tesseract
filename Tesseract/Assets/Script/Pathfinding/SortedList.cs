using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Pathfinding
{
    public class SortedList<T> where T: IComparable<T>
    {
        private readonly List<T> _backing;
        
        public SortedList()
        {
            _backing = new List<T>();
        }

        public void Put(T item)
        {
            if (!_backing.Contains(item))
            {
                int iMax = _backing.Count;
                int i = 0;
                while (i < iMax && _backing[i].CompareTo(item) < 0)
                {
                    i++;
                }
                _backing.Insert(i, item);
            }
            else Update(item);
        }

        public T Take()
        {
            T firstValue = _backing[0];
            _backing.Remove(firstValue);
            return firstValue;
        }

        public int Length()
        {
            return _backing.Count;
        }

        public void Update(T item)
        {
            _backing.Remove(item);
            Put(item);
        }

        public bool IsEmpty()
        {

            try
            {
                return _backing[0] == null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return true;
            }
        }

    }
}