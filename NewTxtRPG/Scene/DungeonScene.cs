using NewTxtRPG.Entitys;
using NewTxtRPG.etc;
using NewTxtRPG.Structs;
using System.Threading;


namespace NewTxtRPG.Scene
{
    internal class DungeonScene
    {
        private static readonly Random rand = new Random();
        //ColorWriter colorSet;
        public List<Monsters> FloorMonster { get; set; }

        int floor;
        int gold;
        int exp;
        public DungeonScene()
        {
            Monsters.MonsterList();
            FloorMonster = new List<Monsters>();
        }

        public void Battle(string difficult)
        {
            gold = 0;
            exp = 0;
            Console.Clear();
            FloorMonster.Clear();

            int monsterSet = rand.Next(1, 5);

            switch (difficult)
            {
                case "1":
                    for (int i = 0; i < monsterSet; i++)
                    {
                        int a = rand.Next(0, 2);
                        FloorMonster.Add(Monsters.Monster[a].copy());
                    }
                    break;
                case "2":
                    for (int i = 0; i < monsterSet; i++)
                    {
                        int a = rand.Next(0, 3);
                        FloorMonster.Add(Monsters.Monster[a].copy());
                    }
                    break;
                case "3":
                    for (int i = 0; i < monsterSet; i++)
                    {
                        int a = rand.Next(0, 4);
                        FloorMonster.Add(Monsters.Monster[a].copy());
                    }
                    break;
            }

            foreach (Monsters monster in FloorMonster) monster.CurrentSpeed = 1;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("적과 조우했습니다 !!"); Console.WriteLine("적과 조우했습니다 !!");
                        Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("적과 조우했습니다 !!"); Console.WriteLine("적과 조우했습니다 !!");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("적과 조우했습니다 !!"); Console.WriteLine("적과 조우했습니다 !!");
            Console.ResetColor();

            Thread.Sleep(1500);

            while (true)
            {
                Console.Clear();
                int checkCursorPosition = 6;

                Player.CurrentSpeed += Player.Stat.Speed;

                RenderConsole.WriteLine("┌────────────────────────────────────────────────────────────┐", ConsoleColor.DarkGray);
                foreach (Monsters monster in FloorMonster)
                {
                    string texts = $"[ {monster.Name,2} ]";
                    string speedCheck = "";

                    monster.CurrentSpeed += monster.DeathCheck ? 0 : monster.Stat.Speed;

                    if (monster.CurrentSpeed >= 10)
                    {
                        int a = (int)monster.CurrentSpeed / 10;
                        for (int i = 0; i < 10; i++)
                            speedCheck += i < a ? "■" : "□";
                    }

                    Console.Write("│");
                    Console.ForegroundColor = monster.DeathCheck ? ConsoleColor.DarkGray : ConsoleColor.Green;
                    Console.Write($"{texts}");
                    Console.ResetColor();
                    RenderConsole.Write(" HP", ConsoleColor.Red);
                    Console.Write($" : {monster.CurrentHP,4} / {monster.Stat.MaxHP,4} speed : ");
                    RenderConsole.WriteLine($"{speedCheck}", ConsoleColor.Green);
                    
                }


                RenderConsole.WriteLine("└────────────────────────────────────────────────────────────┘", ConsoleColor.DarkGray);
                foreach (Monsters monster in FloorMonster)
                {
                    if (monster.CurrentSpeed >= 100)
                    {
                        ActionMonster(monster);
                        checkCursorPosition += 4;
                        Thread.Sleep(1500);
                        RenderConsole.WriteLine("──────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
                    }
                }

                RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                RenderConsole.Write(" HP", ConsoleColor.Red);
                Console.WriteLine($" : {Player.CurrentHP} / {Player.Stat.MaxHP}\n");

                //플레이어 사망
                if (Player.CurrentHP <= 0)
                {
                    Lose();
                    break;
                }



                if (Player.CurrentSpeed >= 100)
                {
                    while (true)
                    {
                        Console.SetCursorPosition(0, checkCursorPosition);

                        RenderConsole.WriteLine("──────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
                        RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                        Console.WriteLine($" 턴입니다 행동을 선택해 주세요");
                        Console.WriteLine("                                              ");
                        Console.WriteLine("1. 공격");
                        Console.WriteLine("2. 스킬");
                        Console.WriteLine("3. 아이템");
                        RenderConsole.WriteLine("──────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
                        Console.Write(">>");
                        string playerSelect = Console.ReadLine();
                        int rands;

                        switch (playerSelect)
                        {
                            case "1":
                                Console.SetCursorPosition(0, checkCursorPosition + 7);
                                Console.WriteLine("                                              ");

                                do
                                {
                                    rands = rand.Next(0, FloorMonster.Count);
                                }
                                while (FloorMonster[rands].DeathCheck);

                                ActionPlayerAtt(FloorMonster[rands]);
                                break;
                            case "2":
                                Console.WriteLine("아직 미구현입니다");
                                //break;
                                continue;
                            case "3":
                                Console.WriteLine("아직 미구현입니다");
                                //break;
                                continue;
                            default:
                                continue;
                        }

                        break;
                    }
                }

                //몬스터 사망
                int monsterDeathCheck = FloorMonster.Count;
                foreach (Monsters monsters in FloorMonster) monsterDeathCheck -= monsters.DeathCheck ? 1 : 0;
                if (monsterDeathCheck == 0)
                {
                    Win();
                    break;
                }

                Thread.Sleep(700);

            }
        }

        public void ActionMonster(Monsters monster)
        {
            if (monster.CurrentHP > 0)
            {
                //치명타 및 회피 부분 추가시 수정 예정
                float monsterAtt = rand.Next(0, 2) == 0 ?
                    monster.Stat.Attack - monster.Stat.Attack / 10 :
                    monster.Stat.Attack + monster.Stat.Attack / 10;

                monster.CurrentSpeed -= 100;
                Player.CurrentHP -= (int)Math.Ceiling(monsterAtt);

                RenderConsole.Write($"{monster.Name}", ConsoleColor.Green);
                Console.Write("이(가) ");
                RenderConsole.Write("공격", ConsoleColor.Red);
                Console.WriteLine(" 합니다! ");
                RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                Console.Write("은(는) ");
                RenderConsole.Write($"{Math.Ceiling(monsterAtt)}", ConsoleColor.DarkRed);
                Console.Write("만큼 ");
                RenderConsole.Write("데미지", ConsoleColor.Red);
                Console.WriteLine("를 입었습니다.\n");

                Thread.Sleep(700);
            }

        }

        public void ActionPlayerAtt(Monsters monster)
        {

            if (Player.CurrentHP > 0 && Player.CurrentSpeed >= 100)
            {
                //치명타 및 회피 추가시 수정 예정
                //치명타 배수 200? 150?
                /*
                float playerCriAtt = rand.Next(0, 100) < 크리율 ? 
                    player.Stat.Attack * 1.5f  : 
                    player.Stat.Attack;
                */
                Player.CurrentSpeed -= 100;
                monster.CurrentHP -= Player.Stat.Attack;

                RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                Console.Write("이(가) ");
                RenderConsole.Write("공격", ConsoleColor.Red);
                Console.WriteLine(" 합니다! ");

                RenderConsole.Write($"{monster.Name}", ConsoleColor.Green);
                Console.Write("은(는) ");
                RenderConsole.Write($"{Player.Stat.Attack}", ConsoleColor.DarkRed);
                Console.Write("만큼 ");
                RenderConsole.Write("데미지", ConsoleColor.Red);
                Console.WriteLine("를 입었습니다.\n");
                RenderConsole.WriteLine("──────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
                Thread.Sleep(700);
            }

            if (monster.CurrentHP <= 0)
            {
                RenderConsole.Write($"\n{monster.Name}", ConsoleColor.Green);
                Console.Write("은(는) ");
                Console.WriteLine("기력이 다했다...");
                monster.CurrentHP = monster.CurrentHP < 0 ? 0 : monster.CurrentHP;
                monster.DeathCheck = true;
                gold += monster.Gold;
                exp += monster.Exp;
            }


            Thread.Sleep(1500);
        }

        public void Lose()
        {
            Console.Clear();
            Console.WriteLine("< 패 배 >");
            Console.WriteLine("적이 너무 강해서 후퇴했습니다..");
            Console.WriteLine("가지고 있던 돈을 절반 잃어버렸습니다..\n");

            RenderConsole.WriteLine($"{Player.CurrentHP} => 10 \n", ConsoleColor.Red);
            RenderConsole.WriteLine($"\n{Player.Gold} - {Player.Gold/2} ", ConsoleColor.Yellow);
            Player.CurrentHP = 10;
            Player.Gold = Player.Gold / 2;

            Thread.Sleep(4000);
        }
        public void Win()
        {
            Console.Clear();
            Console.WriteLine("< 승 리 >");
            Console.WriteLine("적을 전부 처치 하였습니다!\n");

            Console.WriteLine("보상으로 경험치와 골드를 획득하였습니다");

            RenderConsole.WriteLine($"\n{Player.Gold} + {gold} ", ConsoleColor.Yellow);
            RenderConsole.WriteLine($"{Player.Exp} + {exp} ", ConsoleColor.Cyan);

            Player.LevelUp();
            Thread.Sleep(4000);
        }
    }
}
