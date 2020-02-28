using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RaresAStar
{
    public class Grid
    {
        public (int, int) Size { get; private set; }
        public int GridSizeX => Size.Item1; //Columns
        public int GridSizeY => Size.Item2; //Lines

        public List<(int, int)> Obstacles { get; private set; }
        public List<Node> Nodes { get; private set; }
        private readonly Node[,] grid;

        public Grid((int, int) size)
        {
            Size = size;
            Obstacles = new List<(int, int)>();
            Nodes = new List<Node>();
            grid = new Node[GridSizeX, GridSizeY];
        }

        public Node GetNode((int, int) pos)
        {
            return grid[pos.Item1, pos.Item2];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    (int, int) check = (node.position.Item1 + x, node.position.Item2 + y);

                    if (check.Item1 >= 0 && check.Item1 < GridSizeX && check.Item2 >= 0 && check.Item2 < GridSizeY)
                    {
                        int posX = check.Item1;
                        int posY = check.Item2;
                        if (!(grid[posX, posY] is Node))
                            grid[posX, posY] = new Node(true, check);
                        neighbours.Add(GetNode(check));
                    }
                }
            }
            return neighbours;
        }

        public List<Node> path;

        public void CreateGrid()
        {
            foreach (var obstacle in Obstacles)
            {
                int x = obstacle.Item1;
                int y = obstacle.Item2;
                if (GetNode((x, y)) is Node)
                    continue;
                grid[x, y] = new Node(false, obstacle);
            }
            foreach (var node in Nodes)
            {
                int x = node.position.Item1;
                int y = node.position.Item2;
                if (GetNode((x, y)) is Node)
                    continue;
                grid[x, y] = node;
            }
        }
    }
}
