using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaresAStar {
    public class Pathfinding {
        public readonly Grid grid = null;

        public Pathfinding(Grid grid) {
            this.grid = grid;
        }

        public async Task FindPath((int, int) startPos, (int, int) targetPos) {
            Node startNode = grid.GetNode(startPos);
            Node targetNode = grid.GetNode(targetPos);

            if (startNode == null)
                return;
            if (targetNode == null)
                return;

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++) {
                    if (openSet[i].FCost < currentNode.FCost ||
                        openSet[i].FCost == currentNode.FCost ||
                        openSet[i].hCost < currentNode.hCost) {

                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    RetracePath(startNode, targetNode, (openSet, closedSet));
                    break;
                }

                foreach (var neighbour in grid.GetNeighbours(currentNode)) {
                    Console.WriteLine(neighbour.walkable);
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                        continue;

                    int newMoveCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMoveCost < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newMoveCost;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }

                GridExtension.DrawGrid(grid, openSet, closedSet);
                await Task.Delay(100);
            }
        }

        private void RetracePath(Node start, Node target, (List<Node>, HashSet<Node>) sets) {
            List<Node> path = new List<Node>();
            Node currentNode = target;

            while (currentNode != start) {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            grid.path = path;
            GridExtension.DrawGrid(grid, sets.Item1, sets.Item2);
        }

        private int GetDistance(Node a, Node b) {
            int disX = Math.Abs(a.position.Item1 - b.position.Item1);
            int disY = Math.Abs(a.position.Item2 - b.position.Item2);

            if (disX > disY)
                return 14 * disY + 10 * (disX - disY);

            return 14 * disX + 10 * (disY - disX);
        }

    }
}
