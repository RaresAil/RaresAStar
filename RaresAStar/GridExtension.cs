using System;
using System.Collections.Generic;

namespace RaresAStar
{
    public static class GridExtension
    {

        public static void DrawGrid(Grid grid, List<Node> openSet = null, HashSet<Node> closedSet = null)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;

            for (int y = 0; y < grid.GridSizeY; y++)
            {
                for (int x = 0; x < grid.GridSizeX; x++)
                {

                    Node node = grid.GetNode((x, y));
                    bool walkable = node == null ? true : node.walkable;
                    string nodeString;

                    Console.Write($"[");

                    if (!walkable)
                    {
                        nodeString = "X";
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else if (grid.path != null && node != null && grid.path.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        nodeString = "+";
                    }
                    else if (closedSet != null && closedSet.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        nodeString = "*";
                    }
                    else if (openSet != null && openSet.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        nodeString = "?";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        nodeString = node == null ? " " : node.name ?? " ";
                    }

                    if (node != null && node.name != null && !string.IsNullOrEmpty(node.name.Trim()))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        nodeString = node.name;
                    }

                    Console.Write(nodeString);

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"]");

                }

                Console.WriteLine("");
            }
        }

    }
}
