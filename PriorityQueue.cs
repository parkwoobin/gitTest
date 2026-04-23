using UnityEngine;
using System.Collections.Generic;

public class PriorityQueue<TElement, TPriority>
{
    private List<(TElement Element, TPriority Priority)> queue;
    private readonly IComparer<TPriority> comparer;
    public int Count => queue.Count;

    int j = 2;

    public PriorityQueue()
    {
        queue = new List<(TElement, TPriority)>();
        comparer = Comparer<TPriority>.Default;
    }
    public PriorityQueue(IComparer<TPriority> customComparer)
    {
        queue = new List<(TElement, TPriority)>();
        comparer = customComparer ?? Comparer<TPriority>.Default;
    }


    public void Enqueue(TElement element, TPriority priority)
    {
        queue.Add((element, priority));
        HeapfyUp(queue.Count - 1);
    }

    public TElement Dequeue()
    {
        if (queue.Count == 0)
        {
            throw new System.InvalidOperationException("큐가 비어 있습니다.");
        }
        TElement result = queue[0].Element; // 나갈 인덱스

        int lastIndex = queue.Count - 1;
        queue[0] = queue[lastIndex]; // 마지막 요소를 루트로 이동
        queue.RemoveAt(lastIndex); // 마지막 요소 제거

        if (queue.Count > 0)
        {
            HeapfyDown(0); // 루트에서 힙 속성 복원
        }

        return result;
    }

    public TElement Peek()
    {
        if (queue.Count == 0)
        {
            throw new System.InvalidOperationException("큐가 비어 있습니다.");
        }
        return queue[0].Element;
    }

    public void Clear()
    {
        queue.Clear();
    }

    public List<(TElement Element, TPriority Priority)> GetQueue()  // 디버그용 큐 상태 반환
    {
        return queue;
    }

    public void HeapfyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;  // 부모 인덱스 계산

            if (comparer.Compare(queue[index].Priority, queue[parentIndex].Priority) >= 0)
            {
                break;
            }

            // Swap
            Swap(index, parentIndex);

            index = parentIndex;

        }
    }
    public void HeapfyDown(int index)
    {
        int lastIndex = queue.Count - 1;

        while (true)
        {
            // 좌 우 자식 인덱스 계산
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestIndex = index;

            if (leftChildIndex <= lastIndex && comparer.Compare(queue[leftChildIndex].Priority, queue[smallestIndex].Priority) < 0)
            {
                smallestIndex = leftChildIndex;
            }

            if (rightChildIndex <= lastIndex && comparer.Compare(queue[rightChildIndex].Priority, queue[smallestIndex].Priority) < 0)
            {
                smallestIndex = rightChildIndex;
            }

            if (smallestIndex == index)
            {
                break;
            }

            // Swap
            Swap(index, smallestIndex);

            index = smallestIndex;
        }
    }

    private void Swap(int index, int parentIndex)
    {
        var temp = queue[index];
        queue[index] = queue[parentIndex];
        queue[parentIndex] = temp;
    }
}
