using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace RaresAStar {
    class Program {

        static void Main(string[] args) {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args) {

            (int, int) posA = (7, 4);
            (int, int) posB = (4, 1);

            Grid grid = new Grid((20, 10));

            grid.Obstacles.Add((3, 1));
            grid.Obstacles.Add((3, 2));
            grid.Obstacles.Add((4, 2));
            grid.Obstacles.Add((5, 2));
            grid.Obstacles.Add((6, 2));
            grid.Obstacles.Add((7, 2));

            grid.Nodes.Add(new Node(true, posA) {
                name = "A"
            });
            grid.Nodes.Add(new Node(true, posB) {
                name = "B"
            });

            grid.CreateGrid();

            Pathfinding pathfinding = new Pathfinding(grid);
            GridExtension.DrawGrid(pathfinding.grid);

            await Task.Delay(1000);
            await pathfinding.FindPath(posA, posB);
        }

    }
}
