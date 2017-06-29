using System;
using System.Collections.Generic;

namespace AStarBG69YL
{
    public class AStarAlgorithm
    {

        Node firstStart;
        Node secondSeeker;
        Node firstEnd;
        Node secondEnd;
        Node current;
        List<Node> pathSeeker1 = new List<Node>();
        List<Node> pathSeeker2 = new List<Node>();
        int IDAThreshold = 3;
        bool useIDA = true;
        bool useManhattan = true;
        bool useDiagnonals = true;
        bool multiSeeker = false;


        public void RunAstar(bool generateRandom, bool _useIDA, bool _useManhattan, bool _useDiagnonals,bool _multiSeeker, int sX, int sY, int eX, int eY, int s2X, int s2Y)
        {
            useIDA = _useIDA;
            useManhattan = _useManhattan;
            useDiagnonals = _useDiagnonals;
            multiSeeker = _multiSeeker;
            firstStart = new Node(sX, sY, 'S');
            secondSeeker = new Node(s2X, s2Y, 'S', true);
            firstEnd = new Node(eX, eY, 'T');
            secondEnd = new Node(eX, eY, 'T', true);

            Level level1 = new Level(generateRandom, useDiagnonals, false); //TODO: add levelsize in through setter /// get max size from getting - magic numbers atm
            Level level2 = new Level(level1.proceeduralMap, useDiagnonals, true); //Copy map incase its a proceedural one
            level1.AddStartEndNodes(firstStart, firstEnd);

            if(multiSeeker)
            level2.AddStartEndNodes(secondSeeker, secondEnd);

            DrawLevel(level1);

            if (multiSeeker)
                DrawLevel(level2);

            Calculate(level1, pathSeeker1, firstStart, firstEnd);
            if (multiSeeker)
                Calculate(level2, pathSeeker2, secondSeeker, secondEnd);


            Console.ReadKey();
        }

        public void Calculate(Level level, List<Node> path, Node start, Node end)
        {
            List<Node> openSet = new List<Node>();
            List<Node> closedSet = new List<Node>();

            openSet.Add(start);

            while (openSet.Count > 0)
            {
                int lowestIndex = 0;
                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].fScore < openSet[lowestIndex].fScore)
                    {
                        lowestIndex = i;
                    }
                }

                current = openSet[lowestIndex];

                if (current == end)
                {
                    //Locate path back
                    if (start.gridSymbol != '+') 
                        FindPath(current, path);
                    else
                        return;
                }
                else
                {
                    openSet.Remove(current);
                    closedSet.Add(current);

                    List<Node> neighbours = current.neighbours;
                    for (int i = 0; i < neighbours.Count; i++)
                    {
                        Node neighbour = neighbours[i];
                        if (!closedSet.Contains(neighbour) && !neighbour.isObstacle)
                        {
                            int tentative_gCost = current.gCost + 1;
                            IDAThreshold++;

                            bool newPath = false;
                            if (openSet.Contains(neighbour))
                            {
                                if (tentative_gCost < neighbour.gCost)
                                {
                                    neighbour.gCost = tentative_gCost;
                                    newPath = true;
                                }
                            }
                            else
                            {
                                neighbour.gCost = tentative_gCost;
                                newPath = true;
                                openSet.Add(neighbour);
                            }

                            if (newPath)
                            {
                                neighbour.heuristic = GetHeuristicScore(neighbour, end, useManhattan);
                                neighbour.parent = current;

                                if (useIDA)
                                {
                                    if (neighbour.fScore < IDAThreshold)
                                        openSet.Add(neighbour);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < closedSet.Count; i++)
                    closedSet[i].gridSymbol = '.';

                for (int i = 0; i < openSet.Count; i++)
                    openSet[i].gridSymbol = '*';

                for (int i = 0; i < path.Count; i++)
                    path[i].gridSymbol = '+';

                DrawLevel(level);


                //level.UpdateLevel(openSet, closedSet);

            }

            if(openSet.Count <= 0)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  No Solution - Press any key to quit");
                Console.ReadKey();
            }

        }

        public static int GetHeuristicScore(Node current, Node target, bool useManhattan)
        {
            if(useManhattan)
                return Math.Abs(target.x - current.x) + Math.Abs(target.y - current.y); // Manhattan distance
            else
                return Convert.ToInt32(Math.Sqrt(Math.Pow((target.x - current.x), 2) + Math.Pow((target.y - current.y), 2))); // Euclidean distance
        }

        public void FindPath(Node current, List<Node> path)
        {
                Node temp = current;
                path.Add(temp);
                while (temp.parent != null)
                {
                    path.Add(temp.parent);
                    temp = temp.parent;
                }

        }

        public void DrawLevel(Level level)
        {
            for (int x = 0; x < level.XSize; x++)
                for (int y = 0; y < level.YSize; y++)
                    level.proceeduralMap[x, y].Draw();
        }

    }
}