using System;
using System.Collections;
using System.Collections.Generic;

namespace PriorityQueue
{

   public class PriorityQueue<T> : IEnumerable<T> where T : IComparable
    {
        private readonly List<T> _elements;

        public PriorityQueue()
        {

            _elements = new List<T>
            {
                default(T)
            };

        }


        public  int Count => _elements.Count - 1;

        public  T Peek()
        {
            if (Count == 0)
            {
                return default(T);
            }

            Console.WriteLine(_elements[1]);
            return _elements[1];
        }

        public void PrintElements()
        {
            foreach (var value in _elements)
            {
                 Console.WriteLine(value);
            }
        }

        public  void Enqueue(T value)
        {

            _elements.Add(value);


            Swim(_elements.Count - 1);
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                return default(T);
            }

                T minValue = _elements[1];


                if (_elements.Count > 2)
                {
                    T lastValue = _elements[_elements.Count - 1];

                    _elements.RemoveAt(_elements.Count - 1);
                    _elements[1] = lastValue;


                    Sink(1);
                }
                else
                {

                    _elements.RemoveAt(1);
                }

                return minValue;
            }


        private  void Swim(int startIndex)
        {
            int start = startIndex;

            while (IsParentBigger(start))
            {

                T parentValue = _elements[start / 2];
                T childValue = _elements[start];


                _elements[start / 2] = childValue;
                _elements[start] = parentValue;

                start /= 2;
            }
        }


        private  void Sink(int startIndex)
        {
            int start = startIndex;


            while (IsLeftChildSmaller(start) || IsRightChildSmaller(start))
            {
                int child = CompareChild(start);

                if (child == -1)
                {

                    T parentValue = _elements[start];
                    T leftChildValue = _elements[2 * start];

                    _elements[start] = leftChildValue;
                    _elements[2 * start] = parentValue;

                    start = 2 * start;
                }
                else if (child == 1)
                {
                    swap(start);
                }
            }
        }

        private void swap(int startIndex)
        {
            int start = startIndex;
            T parentValue = _elements[start];
            T rightChildValue = _elements[2 * start + 1];

            _elements[start] = rightChildValue;
            _elements[2 * start + 1] = parentValue;

            start = 2 * start + 1;

        }

        private  bool IsParentBigger(int child)
        {
            if (child == 1)
            {
                return false;
            }

            return _elements[child / 2].CompareTo(_elements[child]) > 0;

        }


        private  bool IsLeftChildSmaller(int parent)
        {
            if (2 * parent >= _elements.Count)
            {
                return false;
            }

            return _elements[2 * parent].CompareTo(_elements[parent]) < 0;

        }


        private  bool IsRightChildSmaller(int parent)
        {
            if (2 * parent + 1 >= _elements.Count)
            {
                return false;
            }
                return _elements[2 * parent + 1].CompareTo(_elements[parent]) < 0;

        }


        private int CompareChild(int parent)
        {
            bool leftChildSmaller = IsLeftChildSmaller(parent);
            bool rightChildSmaller = IsRightChildSmaller(parent);

            if (leftChildSmaller || rightChildSmaller)
            {
                if (leftChildSmaller && rightChildSmaller)
                {

                    int leftChild = 2 * parent;
                    int rightChild = 2 * parent + 1;

                    T leftValue = _elements[leftChild];
                    T rightValue =_elements[rightChild];


                    if (leftValue.CompareTo(rightValue) <= 0)
                    {
                        return -1;
                    }

                    return 1;
                }
                if (leftChildSmaller)
                {
                    return -1;
                }
                return 1;
            }

            return 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T value in _elements)
            {

                if (value == null)
                {
                    break;
                }


                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
