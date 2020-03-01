using System;
using System.Collections.Generic;

namespace RaresAStar
{
    public static class GridExtension
    {
        public static void DrawGrid(Grid grid, List<Node> openSet = null, HashSet<Node> closedSet = null)
        {
            DrawGrid(grid, openSet.Contains, closedSet);
        }

        public static void DrawGrid(Grid grid, Heap<Node> openSet = null, HashSet<Node> closedSet = null)
        {
            DrawGrid(grid, openSet.Contains, closedSet);
        }

        private static void DrawGrid(Grid grid, Func<Node, bool> isOpen = null, HashSet<Node> closedSet = null)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;

            for (int y = 0; y < grid.SizeY; y++)
            {
                for (int x = 0; x < grid.SizeX; x++)
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
                        if (Program.SHOW_TYPE == ShowType.Path || Program.SHOW_TYPE == ShowType.All)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            nodeString = "+";
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            nodeString = "*";
                        }
                    }
                    else if (closedSet != null && closedSet.Contains(node))
                    {
                        if (Program.SHOW_TYPE == ShowType.OpenAndClosedSets || Program.SHOW_TYPE == ShowType.All)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            nodeString = "*";
                        }
                        else
                        {
                            nodeString = " ";
                        }
                    }
                    else if (isOpen != null && isOpen(node))
                    {
                        if (Program.SHOW_TYPE == ShowType.OpenAndClosedSets || Program.SHOW_TYPE == ShowType.All)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            nodeString = "?";
                        }
                        else
                        {
                            nodeString = " ";
                        }
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

    public enum ShowType
    {
        All,
        Path,
        OpenAndClosedSets
    }
}
