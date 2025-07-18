﻿using NewTxtRPG.Entitys;
using System;
using System.Collections.Generic;

namespace NewTxtRPG.Scene
{
    public class DungeonEvent
    {
        public string Description;
        public string Image;
        public string Option1;
        public string Option2;
        public Action Option1Result;
        public Action Option2Result;

        public DungeonEvent(string description, string image, string option1, string option2, Action option1Result, Action option2Result)
        {
            Description = description;
            Option1 = option1;
            Option2 = option2;
            Image = image;
            Option1Result = option1Result;
            Option2Result = option2Result;
        }
    }

    public class DungeonEventManager
    {
        private List<DungeonEvent> events = new List<DungeonEvent>();
        private Random rand = new Random();

        public DungeonEventManager()
        {
            events.Add(new DungeonEvent(
                "앞에 맑은 샘물이 보입니다. 당신은 참을 수 없는 갈증을 느끼고 있습니다. 샘물을 마시겠습니까?",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⠀⡀⠀⠀⢀\r\n⠀⠀⠁⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀⠀⠀⠈⠀\r\n⡀⠀⠄⠀⠐⠀⠀⠄⠀⠠⠀⠄⠀⡰⠀⠀⠀⠄⠀⠄⠀⠠⠀⠀⠂⠀⠀⠄⠂⠄⠠⡀⠠⠀⠠⠀⠀⠄⠀⠀\r\n⠀⠀⠀⠀⡀⠄⠀⠀⡀⠀⡀⠠⡑⢔⢅⢕⢔⡐⡔⣐⢔⢌⠜⢌⢪⢢⢱⢸⢠⠅⡄⢀⠀⠀⠀⠀⠀⡀⠠⠀\r\n⠀⠂⠁⠀⠀⠀⠀⢂⠂⢅⠬⡘⡜⡊⢇⢋⢊⠪⡩⠪⡑⢕⠹⡱⡱⡱⡱⢕⡕⡕⡕⡕⡔⡄⣈⠀⠀⠀⠀⠀\r\n⠀⠀⠀⢀⠀⠠⠀⠄⠅⢅⢢⡱⡨⡘⠔⡅⢕⢱⠨⡪⡘⡌⡪⢢⠢⡃⡎⡪⡨⡣⢍⠝⡜⡪⡢⡃⡇⡲⡠⠐\r\n⠀⠀⠁⡀⠄⡂⢅⢵⢙⠪⡑⡡⢢⢑⠕⢜⢌⠆⡇⡣⡱⡑⡕⡕⡕⡕⢕⢕⢱⠸⡐⡅⡇⡇⡣⡱⡘⠐⠁⠀\r\n⠀⡀⢂⠂⢅⠂⢕⢁⠢⢡⠪⠨⢢⠣⡑⡱⡐⢕⠱⡘⡌⢎⢪⠸⡨⡊⡆⡇⡣⡣⢣⠱⡘⢔⢑⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠁⠂⠁⠂⠑⠌⠢⡪⡪⡣⡪⡐⢔⢌⢢⢑⢅⢪⢘⢔⠱⡑⢜⠔⣑⢑⠌⢆⠃⠑⠁⠁⠁⠁⠀⢀⠈\r\n⠀⠀⠄⠀⢀⠀⠀⡀⠀⠁⠂⠁⠊⠐⠈⠀⠂⠢⠑⠌⠢⠑⠐⠑⠈⠀⠁⠂⠁⠁⠁⠀⠀⠀⠐⠀⠀⠄⠀⠀\r\n⠐⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠄⠀⠄⠐⠀⠀⠀⠀⠀⡀⠀⠀⢀⠀⠐⠀⠀⠀⡀⠀⠠⠀⠂⠀⢀⠀⠀⢀⠀\r\n⠀⡀⠐⠈⠀⠀⠁⠀⠈⠀⠀⠀⠀⠀⠀⠀⡀⠈⠀⠀⠀⠀⠈⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠀\r\n⡀⠀⠀⠀⠀⡀⠄⠐⠀⠀⢀⠀⠄⠂⠀⠀⠀⠀⠠⠐⠀⠁⠀⠀⡀⠄⠈⠀⠀⠀⠄⠂⠁⠀⠈⠀⠀⠀⠀⠀\r\n",
                "1. 마신다",
                "2. 마시지 않는다",
                () =>
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("샘물을 마시고 체력이 회복되었습니다! HP +10");
                        Player.CurrentHP += 10;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("샘물이 썩어 있었습니다... 식중독! HP -5");
                        Player.CurrentHP -= 5;
                    }
                },
                () =>
                {
                    Console.WriteLine("당신은 샘물을 마시지 않았습니다. 아무 일도 일어나지 않았습니다.");
                }
            ));

            events.Add(new DungeonEvent(
                "낡은 상자가 눈앞에 놓여 있습니다. 열어보시겠습니까?",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⢀⠀⠠⠀⠀⠄⠀⠀⠄⠀⢀⠠⠀⠀⠠⠀⠀⠠⠀⠀⠠⠀⠀⠄⠀⠀⠄⠀⠠⠀\r\n⠀⠀⠀⡀⠠⣠⠄⢄⡤⣤⢤⢤⢤⢤⢤⢤⡤⡤⡤⣌⡤⣤⠠⠠⣄⠀⠄⠀⠠⠀\r\n⠀⠂⠁⠀⡺⡵⡡⡱⣹⢕⢯⣫⣫⡫⣏⢯⡺⣝⢽⢕⢯⡳⣑⠱⣝⢧⠀⠀⢀⠀\r\n⡀⠀⠀⠀⢅⡭⡦⣥⢵⢭⢥⣕⣨⡬⡮⣥⢭⣌⢡⢭⡥⣭⢴⢥⣥⢑⠀⠀⠀⠀\r\n⠀⠀⠈⠀⠢⣻⢽⡓⠯⢟⠎⠗⠡⠀⠅⠄⠉⢌⢙⢕⠫⠯⢯⣻⢼⠐⠀⠀⠈⠀\r\n⠀⠂⠁⠀⢌⢋⢅⢄⠑⡐⡡⡂⢅⠌⠨⢈⠌⡂⢂⠢⡐⠄⢡⢨⠫⢈⠀⠀⠈⠀\r\n⢀⠀⠄⠀⡽⣵⢂⠢⣯⣳⣳⢽⢵⠨⣨⢆⠂⣯⢠⣻⢮⡇⢐⠰⡽⣕⠀⠀⠈⠀\r\n⠀⠀⠀⠀⡽⡾⡐⠨⣞⡾⣺⢽⣫⠂⠼⡃⠅⣟⠠⡷⣻⠆⠠⠨⣯⡳⠀⠀⠁⢀\r\n⠀⠀⠁⠀⡝⣞⠌⠜⣎⢯⡺⣕⢗⡅⠅⢌⢢⣫⢳⣨⣡⡲⡡⠱⡕⡗⠀⠀⠄⠀\r\n⠀⠐⠀⠀⣝⢮⡊⢎⣮⡳⣝⣎⣗⣝⡽⣕⣗⡵⣫⢮⡲⣝⠜⡌⡯⡮⠀⠠⠀⠐\r\n⠀⠀⠄⠀⢯⢯⡊⡢⣗⡯⣗⡷⣳⣳⢯⣗⣗⡯⡯⣗⡿⡽⡑⡌⣯⢯⠀⠀⠀⠠\r\n⠀⢀⠀⠀⠉⠋⠂⠑⠉⠋⠃⠋⠋⠊⠓⠑⠃⠋⠋⠓⠙⠙⠈⠊⠊⠃⠀⠀⠄⠀\r\n⠂⠀⠀⡀⠐⠀⠀⡀⠄⠀⠀⢀⠀⠀⡀⠀⠀⠠⠀⠀⠀⠀⠀⠠⠀⠀⠐⠀⠀⠄\r\n⠀⠀⠠⠀⠀⡀⠀⠀⠀⠀⠈⠀⠀⠀⡀⠀⠁⠀⠀⢀⠈⠀⠐⠀⠀⠀⠄⠀⠄⠀\r\n",
                "1. 연다",
                "2. 그냥 간다",
                () =>
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("상자 안에서 300G를 발견했습니다! Gold +300");
                        Player.Gold += 300;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("미믹의 공격을 받았습니다! HP -10");
                        Player.CurrentHP -= 10;
                    }
                },
                () =>
                {
                    Console.WriteLine("상자를 무시하고 지나갔습니다.");
                }
            ));

            events.Add(new DungeonEvent(
                "허름한 집을 발견했습니다. 주인이 “잠깐 쉬었다 가시겠어요?” 라고 묻습니다.",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⠀⠀⠀⠠⡐⡄⢔⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⢀⢤⡺⡞⠪⢻⢦⡀⠀⡂⡪⢐⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⡠⣴⠯⠫⢨⠠⢑⢐⠉⢟⣵⡬⡐⡐⡁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⢀⣔⣞⠏⡃⠌⢌⡴⣎⢖⣆⠅⢅⠆⡝⠔⡐⣱⢶⢤⠤⡤⠤⡴⢰⠰⢄⠀⠀\r\n⠀⠫⢱⢐⢐⠨⠨⢐⣻⢪⢟⣾⢐⠥⢑⢌⠢⡂⢗⡿⡽⣞⡿⣩⣹⣍⣯⣹⡄⠀\r\n⠀⠀⢕⢐⣸⡨⠨⢐⠽⠽⠝⠾⡀⠪⡐⡸⣬⡢⣹⣟⣿⣽⢿⣽⢷⣻⣾⣽⡽⠀\r\n⠀⠀⢊⢫⢚⢚⢓⢒⡘⡐⡕⡑⡘⡌⡒⡸⣳⠣⡘⢔⢒⢊⠪⡒⡑⡕⡂⡂⠀⠀\r\n⠀⠀⢸⠐⣄⣑⡐⡵⣻⢽⢙⣅⣊⠄⢇⢊⡯⡇⢜⢕⢕⣵⡵⣵⣕⢕⢕⠄⠀⠀\r\n⠀⠀⠜⢌⠥⣧⣽⠨⠡⢂⡧⠦⣺⠈⢜⠰⡘⢅⢱⠱⡽⣺⢽⣳⢽⡎⣗⠄⠀⠀\r\n⠀⠀⡱⣑⢕⣳⣻⠠⠑⢄⣇⣕⣸⡨⢘⢌⠌⠆⡪⢕⢯⣻⣽⣺⣳⢏⢇⠂⠀⠀\r\n⠀⠀⡆⡦⢧⢪⢜⡬⡪⣔⢵⢰⠼⣔⢵⢜⠄⡑⡜⣎⢮⢳⢻⢺⢳⢕⢕⡄⠀⠀\r\n⠀⠀⠑⠝⠊⠓⠑⠕⠙⠘⠘⠑⠙⠘⠘⠚⠈⠒⠘⠊⠪⠊⠇⠓⠱⠑⠣⠃⠀⠀\r\n",
                "1. 휴식을 취한다",
                "2. 그냥 간다",
                () =>
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("푹 쉬고 나니 개운합니다! HP +10");
                        Player.CurrentHP += 10;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("쉬는 동안 소지품을 도둑맞았습니다... HP +10, Gold -100");
                        Player.CurrentHP += 10;
                        Player.Gold = Math.Max(0, Player.Gold - 100);
                    }
                },
                () =>
                {
                    Console.WriteLine("당신은 휴식을 거부하고 길을 떠납니다.");
                }
            ));

            events.Add(new DungeonEvent(
                "도박꾼이 다가와 동전 던지기 도박을 제안합니다. 이기면 300G, 지면 300G를 잃습니다.",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⣠⢴⢪⠫⡋⠏⢏⡓⡲⡠⡀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⢀⣞⢽⢘⠨⢈⠂⠕⠡⡪⡘⡌⢎⠢⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⡮⣺⡘⠄⢌⢐⢈⠈⠌⠪⢸⠨⠢⠡⠡⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⢐⢝⢼⢌⠪⢐⢀⢂⠄⠡⡈⠄⡡⠁⠅⠅⡂⠀⠀⠀⠀⠀\r\n⠀⠀⠀⡜⢎⡗⢅⠕⡡⡂⡅⡐⠁⠄⠅⡂⠅⢕⠡⠂⠀⠀⠀⠀⠀\r\n⠀⠀⠐⠸⡸⡚⡦⣕⢬⢜⡼⣜⡬⡨⣂⡢⣕⢎⢎⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠅⢪⠸⡨⢻⠐⠡⠣⠣⣻⠓⡷⡹⡍⢏⠍⡆⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠂⠕⠌⠔⠩⡙⢍⠍⠅⠄⢓⠕⠎⢖⠉⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⢔⢅⡑⢅⢓⢌⢄⢙⠜⢔⢑⢌⢇⠂⠀⠀⠀⠀⠀⠀⠀\r\n⠀⢄⠦⡑⢅⢇⢎⡢⡢⢂⠅⡌⡊⢜⠨⡊⠜⠀⠀⠀⠀⠀⠀⠀⠀\r\n⢌⠆⢇⢣⠱⣩⢳⣕⢕⢅⢊⠄⠕⡅⣇⢯⠂⢒⠔⠄⠀⠀⠀⠀⠀\r\n⡣⡣⡕⡕⡕⡜⠘⠜⠷⣕⡖⠍⠃⠄⠠⢀⢐⠠⠐⠠⡁⠢⢀⠀⠀\r\n⠱⡱⡑⡕⢕⢕⠁⢅⢑⢌⣅⠈⡈⠀⠅⠠⠐⠠⠁⡂⠌⢌⢐⠌⢰\r\n⡨⢢⢊⠜⡢⢱⢨⢀⢢⢮⢳⠐⢈⠀⠅⡂⠌⡠⠡⢢⢑⢐⠔⡡⠘\r\n⡣⡣⡑⡅⠌⡪⡺⡪⢛⠈⠄⠨⠠⠨⠐⠠⢁⢐⠨⡐⢔⠐⢌⠤⣕\r\n⢬⢪⡒⡆⡂⡪⡸⠨⡢⠨⢀⠁⠅⢌⢐⢡⠒⠔⠩⡨⡢⣪⠪⡊⡮\r\n⢏⠮⡎⡎⡂⢪⢘⢌⢆⢕⠠⠊⠌⠢⡁⡂⡂⡅⠵⣝⢝⢮⠪⡱⡱\r\n⢝⢼⢸⢸⡸⠰⡑⡕⡌⡆⡣⡡⢡⠱⢌⢆⠣⣊⣞⢮⡫⣎⢌⠆⣯\r\n",
                "1. 도박을 한다",
                "2. 거절한다",
                () =>
                {
                    if (Player.Gold < 300)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("판돈도 없으면서 도박을 하겠다고 한 거야?! 꺼져!");
                        return;
                    }

                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("앞면! 당신이 승리했습니다! Gold +300");
                        Player.Gold += 300;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("뒷면! 당신은 패배했습니다. Gold -300");
                        Player.Gold -= 300;
                    }
                },
                () =>
                {
                    Console.WriteLine("도박을 거절했습니다. 현명한 선택일지도 모르죠.");
                }
            ));

            events.Add(new DungeonEvent(
                "죽은 모험가의 유해가 있습니다. 조사하시겠습니까?",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢀⢀⠤⢀⠄⢀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⢀⡠⢂⢪⢨⠢⢂⠊⠀⠁⠐⠐⢈⠂⢄⢀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⣔⠱⡘⣜⡕⠌⠈⠀⠀⠀⠀⠀⠀⠀⡂⠀⠀⠈⠄⠄⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⣜⠪⡊⣪⣓⢖⡡⠂⠀⠀⡀⠀⡀⠀⠀⢂⠠⠀⠀⠄⠡⡃⡀⠀⠀\r\n⠀⠀⠀⠀⣴⣧⡣⣻⡲⡙⠑⠄⡁⡢⡑⠔⠁⠀⠀⠐⠀⠀⠀⠀⠀⠁⠐⠠⠀⠀\r\n⠀⠀⠀⣸⣯⣿⣿⢿⣽⡿⣶⣤⣀⠐⢅⡁⢀⠄⠢⠁⠀⠀⠀⠀⠀⠀⠈⠈⠄⠀\r\n⠀⠀⠀⢸⣿⢛⠁⠈⡞⣿⣿⣿⣿⣷⡐⡜⡌⠥⠩⠈⡂⠀⠂⢄⠀⠀⠀⠡⠁⠀\r\n⠀⠀⠀⡾⡯⣾⡄⢁⡒⣕⢾⡮⣿⣿⠆⠉⣧⣫⢊⣐⠄⡂⠀⠀⠁⠀⡀⠒⠀⠀\r\n⠀⠀⢰⣝⣯⣿⣷⠨⡽⣔⣓⠻⠛⢋⢠⠀⣿⣻⣼⣴⡬⣲⠑⡀⡀⠢⣐⠁⠀⠀\r\n⠀⠀⠀⢰⡷⡕⡟⠂⢘⢽⣾⣿⣿⣹⣠⣀⣠⣠⣤⣔⢶⣶⣖⣜⢬⢑⠅⠀⠀⠀\r\n⠀⠀⠀⣿⣝⡒⢄⠀⠌⠖⣻⠭⣪⡏⣥⠓⡁⡻⢝⣵⡷⣻⢣⣒⡖⠃⠀⠀⠀⠀\r\n⠀⠀⠐⣿⣎⡟⡯⢹⢊⢏⢐⣹⣡⢁⠎⠀⠠⠑⠉⠈⠻⠞⠊⠈⠀⠀⠀⠀⠀⠀\r\n⠀⠀⢀⣿⣟⣏⠷⠸⡘⢃⢊⠬⠪⠁⠀⢀⠎⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⣞⣧⡹⠐⠑⠃⠀⢈⢀⠡⠤⠄⠮⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠉⠉⠚⠚⠈⠉⠈⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n",
                "1. 조사한다",
                "2. 그냥 간다",
                () =>
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("유해 근처에서 200G를 발견했습니다! Gold +200");
                        Player.Gold += 200;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("거미떼의 습격을 받았습니다! HP -5");
                    }
                },
                () =>
                {
                    Console.WriteLine("유해를 무시하고 지나갔습니다.");
                }
            ));

            events.Add(new DungeonEvent(
                "벽에 마법 문자가 새겨져 있습니다. 손을 댈까요?",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠠⠠⠐⡐⠐⠐⠐⢐⠠⠠⡀⠀⠀⠄⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠄⠌⠄⠂⠀⠀⠀⡀⠡⠀⠀⠀⠀⠂⡑⠠⡀⠀⠠⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠄⠅⠁⠀⠄⠀⠁⠠⠐⠀⠀⢈⠠⠐⠈⠀⠀⠀⠠⠡⠀⠀⢀⠀⠀\r\n⠀⠀⠀⠀⠌⡐⢀⢀⠀⡂⢀⢀⢐⠠⢈⠐⡀⡀⠄⡀⢀⢁⢀⢀⢀⠑⠄⠀⢀⠀\r\n⠀⠀⠀⠜⠀⠐⠀⠀⠀⠄⠄⠂⠀⠀⠀⠀⠀⠀⠀⢂⠠⢀⠀⠀⡀⠀⠌⠂⠀⠀\r\n⠀⠁⠠⠡⠀⠠⠈⠄⠁⠄⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠀⢀⠂⠠⠀⠠⠑⠀⠁\r\n⠠⠀⠈⠄⠡⢀⠀⠀⢂⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠡⠀⠀⡀⠄⠁⠨⠀⠀\r\n⠀⠀⠨⠐⠀⠀⠀⢂⠡⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠅⠨⠀⠀⠀⠀⢌⠀⠐\r\n⠀⠁⠀⠡⢀⠀⠐⠀⠐⠀⠐⠠⠀⠀⠀⠀⠀⡀⠄⠊⠀⡀⠀⠐⠀⠀⠨⡀⠀⠄\r\n⠀⠀⠄⠈⢄⠐⠠⠀⠨⠀⠀⠡⠀⠡⡀⢂⠁⠀⠄⠄⠄⠄⠠⠀⠌⠠⡁⠀⡀⠀\r\n⠀⠀⠀⠄⠀⠌⡠⠀⢈⠀⠠⠀⠡⠁⠀⠀⠈⠐⢀⠀⠀⡀⠀⠠⢐⠁⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠠⠀⠐⠈⢄⠐⠀⠀⠀⠐⠀⠀⠄⠁⠀⠀⠂⠄⠄⠑⠀⠀⠈⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠄⠀⠈⠂⠅⡐⠄⢌⢐⠠⢐⠠⠊⠈⠈⠀⠄⠈⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n",
                "1. 손을 댄다",
                "2. 무시한다",
                () =>
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("마법의 힘이 깃듭니다! 마나 +10");
                        Player.CurrentMP += 10;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("주문이 튕겨 나갑니다! HP -10");
                        Player.CurrentHP -= 10;
                    }
                },
                () =>
                {
                    Console.WriteLine("무언가 불길해 보여 손대지 않았습니다.");
                }
            ));

            events.Add(new DungeonEvent(
                "당신의 앞에 맛있어 보이는 버섯이 보입니다. 당신은 버섯 전문가가 아니지만 아마도 식용 버섯인 것 같습니다.",
                "\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠠⢀⢀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠐⠈⢄⠕⢅⠕⡐⢅⠪⡐⢄⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠠⠈⠠⠀⢅⠢⡑⢅⠪⡈⢂⠑⢈⠢⢑⢀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠠⠈⡠⠢⡡⡑⠅⢕⠨⠢⡑⠄⠂⡈⠄⠐⡈⡐⠅⠄⠀⠀⠀⠀⠀\r\n⠀⠀⠀⢀⠐⡠⢊⠔⡑⠔⠌⡊⡢⡑⡑⢌⠪⡐⡀⢂⠁⠄⢌⢊⠢⡁⠄⠀⠀⠀\r\n⠀⠀⠐⡀⠔⢌⠢⡑⡈⡐⢐⠀⡂⢕⢕⢕⢕⢕⢜⢔⢥⢱⢁⢪⠨⡂⠅⠡⡀⠀\r\n⠀⡈⢤⢢⢱⢱⢱⡡⣠⣢⠦⠖⠶⠺⠚⠞⠞⠞⠮⢞⡼⣜⣜⢜⢲⠨⠐⠠⢐⠀\r\n⠀⢂⢕⢕⢕⣵⡳⠋⠁⠀⠀⠁⢀⠐⠈⠀⠀⠈⠀⡀⠀⠂⠉⢯⢮⡣⡅⡡⠐⠀\r\n⠀⠀⠊⢎⡚⣞⠂⠀⠠⢸⢽⡂⠀⡀⠀⠠⠈⠀⢰⢽⡢⠀⠄⠈⡯⡪⡪⠂⠁⠀\r\n⠀⠀⠀⠀⠁⠃⠀⠕⠁⠀⠁⠀⡀⠣⠚⠑⠜⠁⠀⠈⠀⠨⠊⠀⠈⡊⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠨⠀⠀⠠⠀⠁⠀⠄⠀⠀⡀⠄⠀⠄⠂⠈⠀⢀⠠⠈⠀⡂⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠢⠈⠀⠀⠄⠂⠀⠀⠁⠀⠀⠀⡀⠀⠠⠐⠀⠀⢀⠐⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠈⠐⠠⠄⡀⣀⢁⡀⡐⡀⡁⡀⡀⠄⠄⠄⠂⠁⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀\r\n",
                "1. 손을 댄다",
                "2. 무시한다",
                () =>
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("메이플 시럽 맛이 나는 버섯입니다! HP +10 MP +5");
                        Player.CurrentHP += 10;
                        Player.CurrentMP += 5;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("속이 울렁거리고 메스꺼움을 느낍니다.독버섯인 것 같습니다... HP -10");
                        Player.CurrentHP -= 10;
                    }
                },
                () =>
                {
                    Console.WriteLine("당신은 버섯 대신 가방 안에 있는 쌀을 먹기로 결정합니다.");
                }
            ));
        }

        public void TriggerRandomEvent()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (events.Count == 0)
            {
                Console.WriteLine("아무 일도 일어나지 않았습니다. 다른 곳으로 이동해야 할 것 같습니다.");
                Console.ReadLine();
                return;
            }

            int index = rand.Next(events.Count);
            DungeonEvent e = events[index];
            events.RemoveAt(index);

            Console.Clear();
            Console.WriteLine($"[이벤트 발생]\n{e.Description}\n");
            Console.WriteLine($"\n{e.Image}\n");
            Console.WriteLine(e.Option1);
            Console.WriteLine(e.Option2);

            while (true)
            {
                Console.Write("\n>> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    e.Option1Result();
                    break;
                }
                else if (choice == "2")
                {
                    e.Option2Result();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
            Console.WriteLine("\n엔터를 누르면 계속...");
            Console.ReadLine();
        }
    }
}
