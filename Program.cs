using System.Threading.Tasks;

namespace RaresAStar
{
    class Program
    {

        public readonly static bool DRAW_GRID = false;
        public readonly static bool USE_HEAP = true;
        public readonly static int DELAY_MS = 0;
        public readonly static ShowType SHOW_TYPE = ShowType.Path;

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] _)
        {
            (int, int) posA = (7, 4);
            (int, int) posB = (4, 1);

            Grid grid = new Grid((20, 10));

            grid.Obstacles.Add((3, 1));
            grid.Obstacles.Add((3, 2));
            grid.Obstacles.Add((4, 2));
            grid.Obstacles.Add((5, 2));
            grid.Obstacles.Add((6, 2));
            grid.Obstacles.Add((7, 2));

            grid.Nodes.Add(new Node(true, posA)
            {
                name = "A"
            });
            grid.Nodes.Add(new Node(true, posB)
            {
                name = "B"
            });

            grid.CreateGrid();

            Pathfinding pathfinding = new Pathfinding(grid);

            if (DRAW_GRID)
                GridExtension.DrawGrid(pathfinding.grid, null as Heap<Node>);

            await Task.Delay(1000);
            if (USE_HEAP)
                await pathfinding.FindPathHeap(posA, posB);
            else
                await pathfinding.FindPath(posA, posB);

        }

    }

}
