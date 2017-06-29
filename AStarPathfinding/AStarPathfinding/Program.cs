using System;

namespace AStarBG69YL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetWindowSize(50, 25);
            //Console.SetBufferSize(50, 25);
            Console.CursorVisible = false;
            Console.Title = "A* Pathfinding - BG69YL";

            bool detailedSetup;

            bool isRandom = true;
            bool IDA = true;
            bool manhattan = true;
            bool diagonals = true;
            bool multiSeeker = false;

            int sX, sY, eX, eY, s2X = 0, s2Y = 0;

            Console.WriteLine("Do you want a detailed setup? Enter 'Y' or 'N': ");
            detailedSetup = Evaluate(Console.ReadKey().KeyChar);

            if (detailedSetup)
            {
                Console.Clear();
                Console.WriteLine("Do you want a random level? Enter 'Y' or 'N': ");
                isRandom = Evaluate(Console.ReadKey().KeyChar);

                Console.Clear();
                Console.WriteLine("Do you want to use IDA*? Enter 'Y' or 'N': ");
                IDA = Evaluate(Console.ReadKey().KeyChar);

                Console.Clear();
                Console.WriteLine("Do you want to use Manhattan distance? (If no Elucidian distance will be used) Enter 'Y' or 'N': ");
                manhattan = Evaluate(Console.ReadKey().KeyChar);

                Console.Clear();
                Console.WriteLine("Do you want to allow diagonal movement? Enter 'Y' or 'N': ");
                diagonals = Evaluate(Console.ReadKey().KeyChar);

                Console.Clear();
                Console.Write("Enter a number between 1 - 50 for the X pos for the starting node: ");
                sX = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entered: " + sX);
                Console.Write("Enter a number between 1 - 25 for the Y pos for the starting node: ");
                sY = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entered: " + sY);
                Console.Clear();

                Console.Clear();
                Console.WriteLine("Do you want a second Seeker? Enter 'Y' or 'N': ");
                multiSeeker = Evaluate(Console.ReadKey().KeyChar);
                Console.Clear();

                if (multiSeeker)
                {
                    Console.Write("Enter a number between 1 - 50 for the X pos for the second seeker node: ");
                    s2X = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Entered: " + s2X);
                    Console.Write("Enter a number between 1 - 25 for the Y pos for the second seeker node: ");
                    s2Y = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Entered: " + s2Y);
                    Console.Clear();
                }

                Console.Write("Enter a number between 1 - 50 for the X pos for the end node: ");
                eX = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entered: " + eX);
                Console.Write("Enter a number between 1 - 25 for the Y pos for the end node: ");
                eY = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entered: " + eY);
                Console.WriteLine();
                Console.WriteLine("Press enter to run simulation");
                Console.Read();
                Console.Clear();

                sX--;
                sY--;
                eX--;
                eY--;
                s2X--;
                s2Y--;
            }
            else
            {
                sX = 0;
                sY = 0;
                eX = 49;
                eY = 24;

                Console.Clear();
                Console.WriteLine("Using quick setup: Randomly generated level, IDA*, manhattan distance and diagonal movement enabled");
                Console.WriteLine("Press enter to run simulation");
                Console.Read();
                Console.Clear();
            }

            AStarAlgorithm algorithm = new AStarAlgorithm();
            algorithm.RunAstar(isRandom, IDA, manhattan, diagonals,multiSeeker, sX,sY,eX,eY,s2X,s2Y);


            Console.ReadLine();
        }

        static bool Evaluate(char value)
        {
            if (value == 'N')
                return false;
            else if (value == 'Y')
                return true;
            else if (value == 'n')
                return false;
            else if (value == 'y')
                return true;
            else
                return false;
        }
    }
}
