using System;
using System.IO.Enumeration;
using System.Runtime.InteropServices;

namespace APCSP
{
    internal class Program
    {
        //struct Sound
        //{
        //    [DllImport("winmm.dll")]
        //    public static extern long PlaySound(string fineName, long a, long b);

        //    public string fileName;
        //    public string curentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    public string fullPath;

        //    public Sound()
        //    {
        //        fileName = "";
        //        fullPath = "";
        //    }

        //    public void CallJi()
        //    {
        //        PlaySound(Ji(), 0, 0);
        //    }
        //    public string Ji()
        //    {
        //        fileName = "Ji.mp3";
        //        fullPath = Path.Combine(curentDirectory, fileName);
        //        return fullPath;
        //    }
        //}


        struct Player
        {
            public string looksLike;
            public int atk;
            public int atkedHp;
            public int hp;
            public int recentHp;
            public int criticalRate;
            public int xPosition;
            public int yPosition;
            public string weapon = "Wooden Sword";

            public Player(string looksLike, int atk, int hp, int criticalRate, int xPosition, int yPosition)
            {
                this.looksLike = looksLike;
                this.atk = atk;
                this.hp = hp;
                this.recentHp = hp;
                this.criticalRate = criticalRate;
                this.xPosition = xPosition;
                this.yPosition = yPosition;
            }

            public Player(string looksLike, int atk, int hp, int criticalRate, int xPosition)
            {
                this.looksLike = looksLike;
                this.atk = atk;
                this.hp = hp;
                this.recentHp = hp;
                this.criticalRate = criticalRate;
                this.xPosition = xPosition;
                this.yPosition = 0;
            }

            public int RandomGenerateXPosition()
            {
                Random r = new Random();
                int xPosition = r.Next(2, 149);
                while (xPosition % 2 != 0)
                {
                    xPosition = r.Next(2, 149);
                }
                return xPosition;
            }

            public int RandomGenerateYPosition(int xPosition, int[] xPositions, int[] yPositions, Player kunKun, Player monster)
            {
                Random r = new Random();
                int yPosition = r.Next(1, 34);
                bool continueLoop = true;

                while (continueLoop)
                {
                    if (xPosition == kunKun.xPosition && yPosition == kunKun.yPosition)
                    {
                        yPosition = r.Next(1, 34);
                        continue;
                    }

                    for (int i = 0; i < xPositions.Length; i++)
                    {
                        if (xPosition == xPositions[i] && yPosition == yPositions[i])
                        {
                            yPosition = r.Next(1, 34);
                            break;
                        }
                        continueLoop = false;
                    }
                }
                return yPosition;
            }

            public void Show() //print looksLike
            {
                Console.SetCursorPosition(xPosition, yPosition);
                Console.Write(looksLike);
            }
        }

        

        static bool Move(int[] importantXPositions, int[] importantYPositions, ref Player kunKun, ref Player monster, ref Player boss)
        {
            while (true)
            {
                Console.SetCursorPosition(kunKun.xPosition, kunKun.yPosition);
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'w':
                        if (CheckRepeatPosition(kunKun.xPosition, kunKun.yPosition - 1, importantXPositions, importantYPositions))
                        {
                            CoverLastPosition(kunKun.xPosition, kunKun.yPosition);
                            kunKun.yPosition -= 1;
                            kunKun.Show();
                        }
                        break;
                    case 's':
                        if (CheckRepeatPosition(kunKun.xPosition, kunKun.yPosition + 1, importantXPositions, importantYPositions))
                        {
                            CoverLastPosition(kunKun.xPosition, kunKun.yPosition);
                            kunKun.yPosition += 1;
                            kunKun.Show();
                        }
                        break;
                    case 'a':
                        if (CheckRepeatPosition(kunKun.xPosition - 2, kunKun.yPosition, importantXPositions, importantYPositions))
                        {
                            CoverLastPosition(kunKun.xPosition, kunKun.yPosition);
                            kunKun.xPosition -= 2;
                            kunKun.Show();
                        }
                        break;
                    case 'd':
                        if (CheckRepeatPosition(kunKun.xPosition + 2, kunKun.yPosition, importantXPositions, importantYPositions))
                        {
                            CoverLastPosition(kunKun.xPosition, kunKun.yPosition);
                            kunKun.xPosition += 2;
                            kunKun.Show();
                        }
                        break;
                    case 'j': //attack
                        switch (IsValidAttack(kunKun.xPosition, kunKun.yPosition, importantXPositions, importantYPositions))
                        {
                            case 0: //question
                                ClearConsole();
                                Question();
                                kunKun.weapon = "Physics Excalibur";
                                break;
                            case 1: //save
                                break;
                            case 2: //boss
                                if (BossFight(ref kunKun, ref boss))
                                {
                                    return true;
                                }
                                break;
                            case 3: //monster
                                MonsterFight(ref kunKun, ref monster);
                                break;
                            case 114514: //nothing happens
                                break;
                        }
                        break;
                    case '1': //special gift
                        if (Console.ReadKey(true).KeyChar == '1' ? Console.ReadKey(true).KeyChar == '4' ? Console.ReadKey(true).KeyChar == '5' ? Console.ReadKey(true).KeyChar == '1' ? Console.ReadKey(true).KeyChar == '4' ? true : false : false : false : false : false)
                        {
                            kunKun.weapon = "Lawyer's Letter";
                            ClearConsole();
                            Console.Write("Congradulations! You got Weapon: Lawyer's letter");
                        }
                        break;
                }
            }
        }

        static void Question() //lai san dao ti jiu xing le
        {
            Console.SetCursorPosition(2, 36);
            Console.Write("");
        }

        static bool BossFight(ref Player kunKun, ref Player boss)
        {
            ClearConsole();
            Console.Write("Start fight with BOSS!");
            Console.SetCursorPosition(2, 37);
            Console.Write("Press J to continue.");
            while (kunKun.recentHp <= 0)
            {
                if (JAttack(ref kunKun, ref boss)) //if one of their's hp <= 0
                {
                    if (boss.recentHp <= 0)
                    {

                    }
                    else
                    {

                    }
                    break;
                }
                ClearConsole();
                Console.Write("You hit boss {0} hp, boss still have {1} hp", kunKun.atkedHp, boss.recentHp);
                Console.SetCursorPosition(2, 37);
                Console.Write("Boss hit you {0} hp, you still have {1} hp", boss.atkedHp, kunKun.recentHp);
            }
            return false;
        }

        static bool MonsterFight(ref Player kunKun, ref Player monster)
        {
            ClearConsole();
            Console.Write("Start fight with monster!");
            Console.SetCursorPosition(2, 37);
            Console.Write("Press J to continue.");
            while (kunKun.recentHp <= 0)
            {
                if (JAttack(ref kunKun, ref monster)) //if one of their's hp <= 0
                {
                    if (monster.recentHp <= 0)
                    {
                        ClearConsole();
                        Console.Write("Congradulations! You won the monster!");
                        return false;
                    }
                    else
                    {
                        ClearConsole();
                        Console.Write("You lost");
                        return true;
                    }
                    break;
                }
                ClearConsole();
                Console.Write("You hit monster {0} hp, monster still have {1} hp", kunKun.atkedHp, monster.recentHp);
                Console.SetCursorPosition(2, 37);
                Console.Write("Monster hit you {0} hp, you still have {1} hp", monster.atkedHp, kunKun.recentHp);
            }
            return false;
        }

        static bool JAttack(ref Player kunKun, ref Player nonKunKun)
        {
            Random r = new Random();
            switch (Console.ReadKey(true).KeyChar)
            {
                case 'j':
                    kunKun.atkedHp = kunKun.atk + r.Next(0, 101) <= kunKun.criticalRate ? kunKun.atk : 0;
                    nonKunKun.recentHp -= kunKun.atkedHp;
                    nonKunKun.atkedHp = nonKunKun.atk + r.Next(0, 101) <= nonKunKun.criticalRate ? nonKunKun.atk : 0;
                    kunKun.recentHp -= nonKunKun.atkedHp;
                    if (nonKunKun.recentHp <= 0 || kunKun.recentHp <= 0)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }

        static void CoverLastPosition(int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(" ");
        }

        static void ClearConsole()
        {
            Console.SetCursorPosition(2, 36);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 37);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 38);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 39);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 40);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 41);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 42);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 43);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 44);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 45);
            Console.Write("                                                                                                                                                                                            ");
            Console.SetCursorPosition(2, 36);
        }

        static void ClearInstructure()
        {

        }

        static int IsValidAttack(int xPosition, int yPosition, int[] xPositions, int[] yPositions)
        {
            for (int i = 0; i < xPositions.Length; i++)
            {
                if (xPositions[i] == xPosition+2 || xPositions[i] == xPosition-2 || yPositions[i] == yPosition+1 || yPositions[i] == yPosition-2)
                {
                    return i;
                }
            }
            return 114514;
        }

        static bool CheckRepeatPosition(int xPosition, int yPosition, int[] importantXPositions, int[] importantYPositions) //for kunKun
        {
            if (xPosition > 148 || xPosition < 2 || yPosition > 34 || yPosition < 1) //don't cross red blocks
            {
                return false;
            }

            for (int i = 0; i < importantXPositions.Length; i++)
            {
                if (xPosition == importantXPositions[i] && yPosition == importantYPositions[i])
                {
                    return false;
                }
            }

            return true;
        }

        static void CreateObjects(int[] importantXPositions, int[] importantYPositions, ref Player kunKun, ref Player monster, ref Player boss)
        {
            Random r = new Random();
            kunKun = new Player("答", 10, 100, 10, 74, 18);
            monster = new Player("▲", r.Next(kunKun.atk - (int) (kunKun.atk * 0.1), kunKun.atk + (int) (kunKun.atk * 0.1)), r.Next(kunKun.hp - (int) (kunKun.hp * 0.1), kunKun.hp + (int) (kunKun.hp * 0.1)), 10, monster.RandomGenerateXPosition());
            monster.yPosition = monster.RandomGenerateYPosition(monster.xPosition, importantXPositions, importantYPositions, kunKun, monster);
            boss = new Player("首", 114, 5141, 91, 98, 10); //hen, hen, hen, aaaaaaaaaaaaaaaaa

            importantXPositions[3] = monster.xPosition; //1st question, 2nd save, 3rd BOSS, 4th monster
            importantYPositions[3] = monster.yPosition;

            Console.SetCursorPosition(148, 34);
            Console.Write("Question"); //need a sign
            Console.SetCursorPosition(2, 34);
            Console.Write("Save"); //need a sign
        }

        static void StartPageGuide()
        {
            RedBlocks();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(71, 5);
            Console.Write("KunKun Fight");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(71, 13);
            Console.Write("Start  Game");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(72, 17);
            Console.Write("Quit Game");

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

        static void StartGameGuide()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(158, 32);
            Console.Write("答 = yourself");
            Console.SetCursorPosition(159, 33);
            Console.Write("▲ = monster");
            Console.SetCursorPosition(157, 34);
            Console.Write("★ = final boss");

            Console.SetCursorPosition(156, 38);
            Console.Write("Press WASD to move");
            Console.SetCursorPosition(159, 41);
            Console.Write("Press J to");
            Console.SetCursorPosition(157, 42);
            Console.Write("interact others");
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
                Console.Write("■");
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
                        Console.Write("Quit Game");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(71, 13);
                        Console.Write("Start  Game");
                        start = true;
                        break;

                    case 's':
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(71, 13);
                        Console.Write("Start  Game");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(72, 17);
                        Console.Write("Quit Game");
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
            int[] importantXPositions = {148, 2, 98, 0}; //arrays without monster's position
            int[] importantYPositions = {34, 34, 10, 0};
            Console.SetWindowSize(200, 100);
            Console.SetBufferSize(210, 110);
            int recentStage = 0;
            Random r = new Random();
            Console.CursorVisible = false;

            Console.Write("Full screen, then press any key to start.");
            Console.ReadKey();
            Console.Clear();

            StartPageGuide();
            while (true)
            {
                switch (recentStage)
                {
                    case 0: //start page
                        recentStage = StartGameOrNot() ? 1 : 4;
                        break;
                    case 1: //in game
                        Console.Clear();
                        RedBlocks();
                        StartGameGuide();

                        Player kunKun = new Player();
                        Player monster = new Player();
                        Player boss = new Player();
                        CreateObjects(importantXPositions, importantYPositions, ref kunKun, ref monster, ref boss);
                        


                        kunKun.Show();
                        monster.Show();
                        boss.Show();
                        //Sound ji = new Sound();
                        //ji.CallJi();

                        if (Move(importantXPositions, importantYPositions, ref kunKun, ref boss, ref monster))
                        {
                            recentStage = 3;
                        }

                        break;
                    case 2: //continue game
                        break;
                    case 3: //end game page
                        break;
                    case 4: //end game
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                        break;
                }
            }

        }
    }
}
