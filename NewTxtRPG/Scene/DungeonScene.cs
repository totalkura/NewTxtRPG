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

        //몬스터로 얻는 골드 축적
        int gold; 
        //몬스터로 얻는 경험치 축적
        int exp;


        public DungeonScene()
        {
            Monsters.MonsterList();
            FloorMonster = new List<Monsters>();
        }

        public void StageSet(string difficult)
        {
            
        }

        public void Battle(string difficult)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

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

            RenderConsole.WriteEmptyLine();
            RenderConsole.WriteEmptyLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("적과 조우했습니다 !!".PadLeft(20)); Console.WriteLine("적과 조우했습니다 !!".PadLeft(20));
                        Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("적과 조우했습니다 !!".PadLeft(20)); Console.WriteLine("적과 조우했습니다 !!".PadLeft(20));
                        Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("적과 조우했습니다 !!".PadLeft(20)); Console.WriteLine("적과 조우했습니다 !!".PadLeft(20));
            Console.ResetColor();

            Thread.Sleep(2000);

            while (true)
            {
                
                int checkCursorPosition = 6;

                Player.CurrentSpeed += Player.Stat.Speed;

                draw();

                foreach (Monsters monster in FloorMonster)
                {
                    if (monster.CurrentSpeed >= 100)
                    {
                        ActionMonster(monster);
                        //checkCursorPosition += 6;
                        Thread.Sleep(1000);
                   }
                }

                draw();
                   
                
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
                        Console.SetCursorPosition(0, checkCursorPosition + 2);


                        RenderConsole.WriteLineWithSpacing("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
                        RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                        RenderConsole.WriteLine($" 차례 입니다 행동을 선택해 주세요");

                        RenderConsole.WriteLine("                                              ");
                        RenderConsole.WriteLine("1. 공격");
                        RenderConsole.WriteLine("2. 스킬");
                        RenderConsole.WriteLine("3. 아이템");

                        RenderConsole.WriteLineWithSpacing("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);

                        RenderConsole.Write(">>");
                        string playerSelect = Console.ReadLine();
                        int rands;

                        Console.SetCursorPosition(0, checkCursorPosition + 12);
                        RenderConsole.WriteLine("                                              ");

                        switch (playerSelect)
                        {
                            case "1":
                                do
                                {
                                    rands = rand.Next(0, FloorMonster.Count);
                                }
                                while (FloorMonster[rands].DeathCheck);

                                ActionPlayerAtt(FloorMonster[rands]);
                                break;
                            case "2":
                                ActionPlayerSkill();
                                break;
                            case "3":
                                Player.Inventory.ShowConsumablesItem();
                                break;
                            default:
                                continue;
                        }
                        Player.CurrentSpeed -= 100;
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

                Thread.Sleep(400);

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
                RenderConsole.Write("이(가) ");
                RenderConsole.Write("공격", ConsoleColor.Red);
                RenderConsole.WriteLine(" 합니다! ");
                RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                RenderConsole.Write("은(는) ");
                RenderConsole.Write($"{Math.Ceiling(monsterAtt)}", ConsoleColor.DarkRed);
                RenderConsole.Write("만큼 ");
                RenderConsole.Write("데미지", ConsoleColor.Red);
                RenderConsole.WriteLine("를 입었습니다.");
                RenderConsole.WriteLine("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
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
                monster.CurrentHP -= Player.Stat.Attack;

                RenderConsole.Write($"{Player.Name}", ConsoleColor.Blue);
                RenderConsole.Write("이(가) ");
                RenderConsole.Write("공격", ConsoleColor.Red);
                RenderConsole.WriteLine(" 합니다! ");

                RenderConsole.Write($"{monster.Name}", ConsoleColor.Green);
                RenderConsole.Write("은(는) ");
                RenderConsole.Write($"{Player.Stat.Attack}", ConsoleColor.DarkRed);
                RenderConsole.Write("만큼 ");
                RenderConsole.Write("데미지", ConsoleColor.Red);
                RenderConsole.WriteLineWithSpacing("를 입었습니다.");

                RenderConsole.WriteLineWithSpacing("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);

                Thread.Sleep(700);
            }

            if (monster.CurrentHP <= 0)
            {
                RenderConsole.Write($"\n{monster.Name}", ConsoleColor.Green);
                RenderConsole.Write("은(는) ");
                RenderConsole.WriteLine("기력이 다했다...");
                monster.CurrentHP = monster.CurrentHP < 0 ? 0 : monster.CurrentHP;
                monster.DeathCheck = true;
                gold += monster.Gold;
                exp += monster.Exp;
            }

            Thread.Sleep(2000);
        }

        public void ActionPlayerSkill()
        {

            RenderConsole.WriteLineWithSpacing("< 사용할 스킬을 선택하여 주세요 >");

            RenderConsole.Write($"1. ");
            RenderConsole.Write($"{Player.Job.Skill1.Name}", ConsoleColor.Cyan);
            RenderConsole.Write(" - ");
            RenderConsole.WriteLine($"{Player.Job.Skill1.Effect}",ConsoleColor.Gray);
            RenderConsole.Write("[ ".PadLeft(3));
            RenderConsole.Write("MP",ConsoleColor.Blue);
            RenderConsole.Write($" {Player.Job.Skill1.ManaCost} ] [ ");
            RenderConsole.Write("공격력",ConsoleColor.Red);
            RenderConsole.WriteLineWithSpacing($" x {Player.Job.Skill1.Multiplier} ]");

            RenderConsole.Write($"2. ");
            RenderConsole.Write($"{Player.Job.Skill2.Name}", ConsoleColor.Cyan);
            RenderConsole.Write(" - ");
            RenderConsole.WriteLine($"{Player.Job.Skill2.Effect}", ConsoleColor.Gray);
            RenderConsole.Write("[ ".PadLeft(3));
            RenderConsole.Write("MP", ConsoleColor.Blue);
            RenderConsole.Write($" {Player.Job.Skill2.ManaCost} ] [ ");
            RenderConsole.Write("공격력", ConsoleColor.Red);
            RenderConsole.WriteLineWithSpacing($" x {Player.Job.Skill2.Multiplier} ]");
            
            RenderConsole.Write(">>");

            string skillSelect = Console.ReadLine();
            RenderConsole.WriteLineWithSpacing("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);

            switch (skillSelect)
            {
                case "1":
                    Player.Job.UseSkill(1,FloorMonster,gold,exp);
                    break;
                case "2":
                    Player.Job.UseSkill(2, FloorMonster, gold, exp);
                    break;
                case "3":
                    Player.Job.UseSkill(3, FloorMonster, gold, exp);
                    break;
            }

            foreach (var monster in FloorMonster)
            {
                if (monster.CurrentHP <= 0 && !monster.DeathCheck)
                {
                    gold += monster.Gold;
                    exp += monster.Exp;
                    monster.DeathCheck = true; // 중복 보상 방지
                }
            }

            Thread.Sleep(2500);
        }


        public void Lose()
        {
            Console.Clear();
            RenderConsole.WriteLine("< 패 배 >");
            RenderConsole.WriteLine("적이 너무 강해서 후퇴했습니다..");
            RenderConsole.WriteLineWithSpacing("가지고 있던 돈을 절반 잃어버렸습니다..");


            RenderConsole.WriteLineWithSpacing($" HP => 10 \n", ConsoleColor.Red);
            RenderConsole.WriteLineWithSpacing($"\n GOLD : {Player.Gold} => {Player.Gold/2} ", ConsoleColor.Yellow);

            Player.CurrentHP = 10;
            Player.Gold = Player.Gold / 2;

            Thread.Sleep(4000);
        }
        public void Win()
        {
            Player.DungeonCleared++; // 던전 클리어 횟수 증가
            Console.Clear();
            RenderConsole.WriteLine("< 승 리 >");
            RenderConsole.WriteLineWithSpacing("적을 전부 처치 하였습니다!");

            RenderConsole.WriteLine("보상으로 경험치와 골드를 획득하였습니다");

            int dropChance = rand.Next(0, 100); // 0~99

            int beforeExp = Player.Exp;
            int beforeGold = Player.Gold;

            if (dropChance < 30) // 30% 확률
            {
                var droppablePotions = Items.ItemList
                .Where(p => p.ItemType == ItemType.Consumables)
                .ToList();

                int index = rand.Next(droppablePotions.Count);

                ItemInfo dropped = droppablePotions[index];
                Player.Inventory.AddItem(dropped);

                Console.WriteLine($"당신은 '{dropped.Name}' 아이템을 획득했습니다!");
            }
            else
            {
                RenderConsole.WriteLineWithSpacing("아이템을 획득하지 못했습니다.");
            }


            Player.Gold += gold;
            Player.Exp += exp;

            RenderConsole.WriteLineWithSpacing($"\n GOLD : {beforeGold} => {Player.Gold} ", ConsoleColor.Yellow);
            RenderConsole.WriteLineWithSpacing($" EXP : {beforeExp} => {Player.Exp} ", ConsoleColor.Cyan);

            Player.LevelUp();
            Thread.Sleep(4000);
        }

        public void draw()
        {
            Console.Clear();
            RenderConsole.WriteLine("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
            foreach (Monsters monster in FloorMonster)
            {
                string speedCheck = "";

                monster.CurrentSpeed += monster.DeathCheck ? 0 : monster.Stat.Speed;

                if (monster.CurrentSpeed >= 5)
                {
                    int a = (int)monster.CurrentSpeed / 5;
                    for (int i = 0; i < 20; i++)
                        speedCheck += i < a ? "█" : "░";
                }
                
                RenderConsole.Write(" [");
                Console.ForegroundColor = monster.DeathCheck ? ConsoleColor.DarkGray : ConsoleColor.Green;
                Console.Write($"{monster.Name}".PadLeft(6));
                Console.ResetColor();
                RenderConsole.Write(" ]");
                RenderConsole.Write(" HP", ConsoleColor.Red);
                RenderConsole.Write($" : {monster.CurrentHP} / {monster.Stat.MaxHP}".PadRight(18));
                RenderConsole.Write(" speed : ".PadLeft(8));
                RenderConsole.WriteLine($"{speedCheck.PadRight(20)}", ConsoleColor.Green);

            }
            string speedCheckP = "";
            if (Player.CurrentSpeed >= 5)
            {
                int a = (int)Player.CurrentSpeed / 5;
                for (int i = 0; i < 20; i++)
                    speedCheckP += i < a ? "█" : "░";
            }

            RenderConsole.Write("\n [");
            RenderConsole.Write($"{Player.Name}".PadLeft(6), ConsoleColor.Blue);
            RenderConsole.Write(" ]");
            RenderConsole.Write(" HP", ConsoleColor.Red);
            RenderConsole.Write($" : {Player.CurrentHP} / {Player.Stat.MaxHP}".PadRight(18));
            RenderConsole.Write(" speed : ".PadLeft(8));
            RenderConsole.WriteLine($"{speedCheckP.PadRight(20)}", ConsoleColor.DarkBlue);
            RenderConsole.Write(" MP".PadLeft(13), ConsoleColor.DarkBlue);
            RenderConsole.WriteLine($" : {Player.CurrentMP} / {Player.Stat.MaxMP}".PadRight(18));
            RenderConsole.WriteLine("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);
                
        }


    }
}
