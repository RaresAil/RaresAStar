using System;
namespace RaresAStar
{
    public static class Tools
    {
        public static void Log(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            string[] data = name.Split('§');

            bool start = name[0] != '§';
            foreach (string line in data)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                if (!start)
                {
                    Console.ForegroundColor = line[0] switch
                    {
                        '4' => ConsoleColor.DarkRed,
                        'c' => ConsoleColor.Red,
                        '6' => ConsoleColor.DarkYellow,
                        'e' => ConsoleColor.Yellow,
                        '2' => ConsoleColor.DarkGreen,
                        'a' => ConsoleColor.Green,
                        'b' => ConsoleColor.Cyan,
                        '3' => ConsoleColor.DarkCyan,
                        '1' => ConsoleColor.DarkBlue,
                        '9' => ConsoleColor.Blue,
                        'd' => ConsoleColor.Magenta,
                        '5' => ConsoleColor.DarkMagenta,
                        'f' => ConsoleColor.White,
                        '7' => ConsoleColor.Gray,
                        '8' => ConsoleColor.DarkGray,
                        '0' => ConsoleColor.Black,
                        _ => default
                    };
                    Console.Write(line.Substring(1));
                }
                else
                {
                    Console.Write(line);
                }
            }
            Console.WriteLine();
        }
    }
}
