namespace Test;

public class ListNode<T>
{
    public readonly T Value;

    public readonly ListNode<T>? Next;

    #region Constructors

    public ListNode(T value)
    {
        Value = value;
    }

    private ListNode(T value, ListNode<T> next, ListNode<T> toAppend, int depth)
    {
        depth--;

        Value = value;

        if (depth > 0)
        {
            Next = new ListNode<T>(next.Value, next.Next, toAppend, depth);
            return;
        }
        else if (depth <= 0)
        {
            if (toAppend.Next != null)
                Next = new ListNode<T>(toAppend.Value, toAppend.Next);
            else
                Next = new ListNode<T>(toAppend.Value);
        }
    }

    private ListNode(T value, ListNode<T> next)
    {
        Value = value;

        if (next != null)
            Next = new ListNode<T>(next.Value, next.Next);
    }
    

    private ListNode(ListNode<T> current, ListNode<T> next, int position, int count, T value)
    {
        if (position == count)
            Value = value;
        else
            Value = current.Value;

        count++;

        if (next == null)
            return;
        else
            Next = new ListNode<T>(current.Next, next.Next, position, count, value);
    }

    private ListNode(T value, ListNode<T> next, ListNode<T> toChange, int count, int position)
    {
        count++;

        Value = value;

        if (next == null)
            return;

        if (count + 1 == position)
        {
            if (toChange.Next != null)
                Next = new ListNode<T>(toChange.Value, toChange.Next);
            else
                Next = new ListNode<T>(toChange.Value);

            return;
        }

        Next = new ListNode<T>(next.Value, next.Next, toChange, count, position);
    }

    #endregion

    public ListNode<T> AppendNode(ListNode<T> last) => new ListNode<T>(Value, Next, last, CalculateDepth());

    public ListNode<T> AppendValue(T value) => new ListNode<T>(Value, Next, new ListNode<T>(value), CalculateDepth());

    public ListNode<T> ChangeNode(int position, ListNode<T> node)
    {
        if (position <= 0)
            throw new Exception("Linked list positions begins from 1");
        
        if (position == 1)
            return new ListNode<T>(node.Value, node.Next);

        return new ListNode<T>(Value, Next, node, 0, position);
    }

    public ListNode<T> ChangeValue(int position, T value)
    {
        if (position <= 0)
            throw new Exception("Linked list positions begins from 1");
        
        return new ListNode<T>(this, Next, position, 1, value);
    }

    public int CalculateDepth()
    {
        int count = 1;

        var node = Next;

        while (node != null)
        {
            node = node.Next;
            count++;
        }

        return count;
    }
}