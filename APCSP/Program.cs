using System;

namespace APCSP
{
    internal class Program
    {
        struct Player
        {
            string looksLike;
            int atk;
            int hp;
            int criticalRate;

            public Player(string looksLike, int atk, int hp, int criticalRate)
            {
                this.looksLike = looksLike;
                this.atk = atk;
                this.hp = hp;
                this.criticalRate = criticalRate;
            }
        }

        static void StartPage()
        {
            RedBlocks();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(71, 5);
            Console.WriteLine("KunKun Fight");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(71, 13);
            Console.WriteLine("Start  Game");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(72, 17);
            Console.WriteLine("Quit Game");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(157, 37);
            Console.Write("Press W & S to");
            Console.SetCursorPosition(156, 38);
            Console.Write("change selection");
            Console.SetCursorPosition(159, 41);
            Console.Write("Press J to");
            Console.SetCursorPosition(156, 42);
            Console.Write("choose selection");
        }

        static void RedBlocks()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 90; i++)
            {
                Console.Write("■" + " ");
            }

            Console.SetCursorPosition(0, 35);
            for (int i = 0; i < 90; i++)
            {
                Console.Write("■" + " ");
            }

            Console.SetCursorPosition(0, 46);
            for (int i = 0; i < 90; i++)
            {
                Console.Write("■" + " ");
            }

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 46; i++)
            {
                Console.WriteLine("■");
            }

            Console.SetCursorPosition(150, 0);
            for (int i = 0; i < 46; i++)
            {
                Console.Write("■");
                Console.SetCursorPosition(150, Console.CursorTop + 1);
            }

            Console.SetCursorPosition(178, 0);
            for (int i = 0; i < 46; i++)
            {
                Console.Write("■");
                Console.SetCursorPosition(178, Console.CursorTop + 1);
            }
        }

        static bool StartGameOrNot()
        {
            bool start = true;
            bool endLoop = false;
            while (!endLoop)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'w':
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(72, 17);
                        Console.WriteLine("Quit Game");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(71, 13);
                        Console.WriteLine("Start  Game");
                        start = true;
                        break;

                    case 's':
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(71, 13);
                        Console.WriteLine("Start  Game");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(72, 17);
                        Console.WriteLine("Quit Game");
                        start = false;
                        break;
                    case 'j':
                        endLoop = true;
                        return start;
                }
            }
            return start;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(200, 100);
            Console.SetBufferSize(210, 110);
            byte recentStage = 0;

            Console.CursorVisible = false;
            Console.WriteLine("Full screen, then press any key to start.");
            Console.ReadKey();
            Console.Clear();

            StartPage();
            while (true)
            {
                switch (recentStage)
                {
                    case 0: //start page
                        switch (StartGameOrNot())
                        {
                            case true:
                                recentStage = 1;
                                break;
                            case false:
                                recentStage = 3;
                                break;
                        }
                        break;
                    case 1: //in game
                        break;
                    case 2: //end game page
                        break;
                    case 3: //end game
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                        break;
                }
            }

        }
    }
}
