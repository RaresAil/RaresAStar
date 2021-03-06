﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace RaresAStar
{
    public class Node : IEquatable<Node>, IHeapItem<Node>
    {
        public bool walkable = false;
        public (int, int) position;
        public Node parent;

        public string name = null;

        public int gCost;
        public int hCost;
        public int FCost => gCost + hCost;

        public Node(bool _walkable, (int, int) _position)
        {
            walkable = _walkable;
            position = _position;
        }

        public bool IsIn(Node[,] list)
        {
            return list[position.Item1, position.Item2] != null;
        }

        public override int GetHashCode()
        {
            return int.Parse(
                position.Item1.ToString().PadRight(3, '0') +
                position.Item2.ToString().PadRight(3, '0')
            );
        }

        public override bool Equals(object obj)
        {
            return obj is Node node && Equals(node);
        }

        public bool Equals([AllowNull] Node other)
        {
            if (other == null)
                return false;
            return position.Item1 == other.position.Item1 && position.Item2 == other.position.Item2;
        }

        public int HeapIndex { get; set; }

        public int CompareTo([AllowNull] Node other)
        {
            if (other == null)
                return -1;
            int compare = FCost.CompareTo(other.FCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(other.hCost);
            }
            return -compare;
        }
    }
}
