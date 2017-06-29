using System;
using System.Collections.Generic;

namespace AStarBG69YL
{
    public class Node
    {
        public int x;
        public int y;
        public List<Node> neighbours = new List<Node>();

        public char gridSymbol;

        //public int fScore;
        public int fScore { get { return gCost + heuristic; } }

        public int gCost; // beginning to current end
        public int heuristic; // from end to beginning

        public Node parent;

        public bool isObstacle = false;

        int xMult = 0;

        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
            gridSymbol = ' ';
    }

        public Node(int _x, int _y, char _gridSymbol)
        {
            x = _x;
            y = _y;
            gridSymbol = _gridSymbol;
            if(gridSymbol == 'x')
                isObstacle = true;
        }

        public Node(int _x, int _y, char _gridSymbol, bool isSeeker2)
        {
            x = _x;
            y = _y;
            gridSymbol = _gridSymbol;
            if (gridSymbol == 'x')
                isObstacle = true;

            if (isSeeker2)
            {
                xMult = 52;
            }
        }

        public void Draw()
        {
            if (isObstacle) { 
                gridSymbol = 'x';
            }

            Console.SetCursorPosition(x+xMult, y);

            if (gridSymbol == ' ')
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (gridSymbol == 'x')
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            if (gridSymbol == '.')
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Red;
            }

            if (gridSymbol == '+')
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            if (gridSymbol == '*')
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Green;
            }

            if (gridSymbol == 'S' || gridSymbol == 'T')
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.Write(gridSymbol);

            //Console.ResetColor();
        }

        public void AddNeighbours(Level level, bool useDiagnonals)
        {
            if(x < level.XSize - 1)
                neighbours.Add(level.proceeduralMap[x + 1, y]);
            if (x > 0)
                neighbours.Add(level.proceeduralMap[x - 1, y]);

            if (y < level.YSize - 1)
                neighbours.Add(level.proceeduralMap[x, y + 1]);
            if (y > 0)
                neighbours.Add(level.proceeduralMap[x, y - 1]);

            //Diag
            if (useDiagnonals)
            {
                if (x > 0 && y > 0)
                    neighbours.Add(level.proceeduralMap[x - 1, y - 1]);
                if (x < level.XSize - 1 && y > 0)
                    neighbours.Add(level.proceeduralMap[x + 1, y - 1]);
                if (x > 0 && y < level.YSize - 1)
                    neighbours.Add(level.proceeduralMap[x - 1, y + 1]);
                if (x < level.XSize - 1 && y < level.YSize - 1)
                    neighbours.Add(level.proceeduralMap[x + 1, y + 1]);
            }
        }

    }
}