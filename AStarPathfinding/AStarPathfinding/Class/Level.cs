using System;
using System.Collections.Generic;

namespace AStarBG69YL
{
    public class Level
    {
        //might make setter for changing level
        static int xSize = 50; //TODO: getting slow with higher sizes, need to optimise
        static int ySize = 25;
        static Random random = new Random();
        bool useDiagnonals;


        public static string[] defaultMap = new string[]
           {
                "x                                                 ",
                "                                                  ",
                "                      xx                          ",
                "                       xx                         ",
                "                       xx                         ",
                "                        xx                        ",
                "                 xxxxxxxxxxxxxxxxxxxxxxxxxxxxx    ",
                "                           xx                     ",
                "                             xx                   ",
                "                          xxxx                    ",
                "                              xx                  ",
                "                              xx                  ",
                "                              xx                  ",
                "                           xx x                   ",
                "                              xx                  ",
                "                               x                  ",
                "                               x                  ",
                "                               x                  ",
                "                               x                  ",
                "                               x                  ",
                "                               x                  ",
                "                               x                  ",
                "                               xxxxxxxxxxx        ",
                "                                                  ",
                "                                                  "
           };

        public Node[,] proceeduralMap = new Node[xSize, ySize];

        public int XSize
        {
            get { return xSize; }
            set { xSize = value; }
        }

        public int YSize
        {
            get { return ySize; }
            set { ySize = value; }
        }

        public Level(bool isRandom, bool _useDiagnonals, bool isSeeker2)
        {
            useDiagnonals = _useDiagnonals;
            if (isRandom)
            {
                for (int y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        char characterToPlace = ' ';
                        if (random.NextDouble() < 0.3)
                            characterToPlace = 'x';

                        if (isSeeker2)
                            proceeduralMap[x, y] = new Node(x, y, defaultMap[y].ToCharArray()[x], true);
                        else
                            proceeduralMap[x, y] = new Node(x, y, characterToPlace);
                    }
                }
            }
            else
            {
                for (int x = 0; x < XSize; x++)
                {
                    for (int y = 0; y < YSize; y++)
                    {
                        if(isSeeker2)
                            proceeduralMap[x, y] = new Node(x, y, defaultMap[y].ToCharArray()[x], true);
                        else
                        proceeduralMap[x,y] = new Node(x, y, defaultMap[y].ToCharArray()[x]);
                    }
                }
            }

        }

        public Level(Node[,] copyMap, bool _useDiagnonals, bool isSeeker2)
        {
            useDiagnonals = _useDiagnonals;
            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    if (isSeeker2)
                        proceeduralMap[x, y] = new Node(x, y, copyMap[x,y].gridSymbol, true);
                }
            }

        }

        public void UpdateLevel(List<Node> openSet, List<Node> closedSet)
        {
            foreach (Node node in openSet)
            {
                proceeduralMap[node.x, node.y] = node;
            }
            foreach (Node node in closedSet)
            {
                proceeduralMap[node.x, node.y] = node;
            }
        }

        public void AddStartEndNodes(Node start, Node end)
        {
            proceeduralMap[start.x, start.y] = start;
            proceeduralMap[end.x, end.y] = end;
            AddNeighbours();
        }

        public void AddNeighbours()
        {
            //Separate loop as cant make it in previous section due to nodes being missing during creation
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    proceeduralMap[x, y].AddNeighbours(this, useDiagnonals);
                }
            }
        }

    }
}
