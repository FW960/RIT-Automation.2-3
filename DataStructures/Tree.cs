namespace Test;

using System.Collections.Generic;
using System.Linq;

public class Tree
{
    public int? Value { get; private set; }
    public Tree? Parent { get; private set; }
    public Tree? LeftBranch { get; private set; }
    public Tree? RightBranch { get; private set; }

    public Tree(int value)
    {
        Value = value;
        
    }

    public void BinaryEnter(int value)
    {
        if (Value == null)
        {
            Value = value;
            return;
        }

        if (value > Value && RightBranch != null)
        {
            RightBranch.BinaryEnter(value);
        }
        else if (value > Value)
        {
            RightBranch = new Tree(value);
            RightBranch.Parent = this;
            RightBranch.BinaryEnter(value);
        }

        if (value < Value && LeftBranch != null)
        {
            LeftBranch.BinaryEnter(value);
        }
        else if (value < Value)
        {
            LeftBranch = new Tree(value);
            LeftBranch.Parent = this;
            LeftBranch.BinaryEnter(value);
        }
    }

    public List<Tree> BreadthFirstSearch()
    {
        var queue = new Queue<Tree>();
        queue.Enqueue(this);

        return BreadthFirstSearch(queue, new List<Tree>());
    }

    private List<Tree> BreadthFirstSearch(Queue<Tree> queue, List<Tree> nodes)
    {
        if (queue.Count == 0)
            return nodes;

        if (queue.First().LeftBranch != null)
            queue.Enqueue(queue.First().LeftBranch);

        if (queue.First().RightBranch != null)
            queue.Enqueue(queue.First().RightBranch);

        nodes.Add(queue.Dequeue());

        return BreadthFirstSearch(queue, nodes);
    }

    public List<Tree> DepthFirstSearch()
    {
        var stack = new Stack<Tree>();
        stack.Push(this);

        return DepthFirstSearch(stack, new List<Tree>());
    }

    private List<Tree> DepthFirstSearch(Stack<Tree> stack, List<Tree> nodes)
    {
        if (stack.First().LeftBranch != null)
        {
            stack.Push(stack.First().LeftBranch);
            DepthFirstSearch(stack, nodes);
        }

        if (stack.First().RightBranch != null)
        {
            stack.Push(stack.First().RightBranch);
            DepthFirstSearch(stack, nodes);
        }

        nodes.Add(stack.Pop());

        return nodes;
    }
}