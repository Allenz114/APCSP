﻿using System;
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
            string looksLike;
            public int atk;
            public int hp;
            int recentHp;
            int criticalRate;
            int xPosition;
            int yPosition;

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

            // void MonsterCheckRepeatPosition(int xPosition, int yPosition) //check monster.position is equal player's position when random generating
            // {
            //     if (xPosition == 74 && yPosition == 18 /*||*/)
            //     {

            //     }
            // }

            public void Show()
            {
                Console.SetCursorPosition(this.xPosition, this.yPosition);
                Console.Write(this.looksLike);
            }
        }

        static void Move(int[] importantXPositions, int[] importantYPositions)
        {
            while (true)
            {
                Console.SetCursorPosition(kunKun.xPosition, kunKun.yPosition);
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'w':
                        kunKun.yPosition += CheckRepeatPosition(kunKun.xPosition, kunKun.yPosition+1) ? 1 : 0;
                        break;
                    case 's':
                        kunKun.yPosition -= CheckRepeatPosition(kunKun.xPosition, kunKun.yPosition-1) ? 1 : 0;
                        break;
                    case 'a':
                        kunKun.xPosition -= CheckRepeatPosition(kunKun.xPosition-2, kunKun.yPosition) ? 2 : 0;
                        break;
                    case 'd':
                        kunKun.xPosition += CheckRepeatPosition(kunKun.xPosition+2, kunKun.yPosition) ? 2 : 0;
                        break;
                    case 'j': //attack
                        IsValidAttack(kunKun.xPosition, kunKun.yPosition, importantXPositions, importantYPositions);
                        break;
                    case '1': //caiDan
                        break;

                }
            }
        }

        static bool IsValidAttack(int xPosition, int yPosition, int[] xPositions, int[] yPositions) //!!!!!!!!!!!!!!!!!Change the array positions to monster.xPosition
        {
            for (int i = 0; i < xPosition.Length; i++)
            {
                if (xPositions[i] == xPosition+2 || xPositions[i] == xPosition-2 || yPositions[i] == yPosition+1 || yPositions[i] == yPosition-2)
                {
                    return true;
                }
            }
            return false;
        }

        static bool CheckRepeatPosition(int xPosition, int yPosition, int[] importantXPositions, int[] importantYPositions)
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

        static void CreateObjects()
        {
            Player kunKun = new Player("答", 10, 100, 10, 74, 18);
            Player monster = new Player("▲", r.Next(kunKun.atk - (int) (kunKun.atk * 0.1), kunKun.atk + (int) (kunKun.atk * 0.1)), r.Next(kunKun.hp - (int) (kunKun.hp * 0.1), kunKun.hp + (int) (kunKun.hp * 0.1)), 10, r.Next(2, 177), r.Next(1, 46));
            Player boss = new Player("WuJiaoXing", 114, 5141, 91, 98, 10); //hen, hen, hen, aaaaaaaaaaaaaaaaa
            Console.SetCursorPosition()
        }

        static void StartPageGuide()
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
            int[] importantXPositions = {148, 2, 74}; //1st question, 2nd save, 3rd
            int[] importantYPositions = {34, 34, 2};
            Console.SetWindowSize(200, 100);
            Console.SetBufferSize(210, 110);
            byte recentStage = 0;
            Random r = new Random();
            Console.CursorVisible = false;

            Console.WriteLine("Full screen, then press any key to start.");
            Console.ReadKey();
            Console.Clear();

            StartPageGuide();
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
                                recentStage = 4;
                                break;
                        }
                        break;
                    case 1: //in game
                        Console.Clear();
                        RedBlocks();
                        StartGameGuide();
                        CreateObjects();
                        
                        kunKun.Show();
                        int a = Console.ReadKey(true).KeyChar;
                        //Sound ji = new Sound();
                        //ji.CallJi();

                        Move(importantXPositions, importantYPositions);

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
