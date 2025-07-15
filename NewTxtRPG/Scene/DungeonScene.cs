using NewTxtRPG.Entitys;


namespace NewTxtRPG.Scene
{
    internal class DungeonScene
    {
        private static readonly Random rand = new Random();

        public List<Monsters> FloorMonster { get; set; }

        int floor;

        public DungeonScene()
        {
            Monsters.MonsterList();
            FloorMonster = new List<Monsters>();
        }

        public void Battle(string difficult, Player player)
        {

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


            Console.WriteLine("적과 조우했습니다 !!");
            Thread.Sleep(1500);

            while (true)
            {
                Console.Clear();
                int checkCursorPosition = 6;

                player.CurrentSpeed += player.Stat.Speed;
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

                    if (monster.DeathCheck)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{texts}");
                    Console.ResetColor();
                    Console.WriteLine($" HP : {monster.CurrentHP,4} / {monster.Stat.MaxHP,4} speed : {speedCheck}\n");
                    checkCursorPosition++;
                }

                foreach (Monsters monster in FloorMonster)
                {
                    if (monster.CurrentSpeed >= 100)
                    {
                        ActionMonster(monster, player);
                        checkCursorPosition += 3;

                        Thread.Sleep(1500);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n{player.Name}의 HP");
                Console.ResetColor();
                Console.WriteLine($" : {player.CurrentHP} / {player.Stat.MaxHP}\n");

                if (player.CurrentSpeed >= 100)
                {
                    while (true)
                    {
                        Console.SetCursorPosition(0, checkCursorPosition);

                        Console.WriteLine($"{player.Name}의 턴입니다 행동을 선택해 주세요");
                        Console.WriteLine("                                              ");
                        Console.WriteLine("1. 공격");
                        Console.WriteLine("2. 스킬");
                        Console.WriteLine("3. 아이템");

                        string playerSelect = Console.ReadLine();
                        int rands;

                        switch (playerSelect)
                        {
                            case "1":
                                Console.SetCursorPosition(0, checkCursorPosition + 5);
                                Console.WriteLine("                                              ");

                                do
                                {
                                    rands = rand.Next(0, FloorMonster.Count);
                                }
                                while (FloorMonster[rands].DeathCheck);

                                ActionPlayerAtt(player, FloorMonster[rands]);
                                break;
                            case "2":

                                break;
                            case "3":

                                break;
                            default:
                                continue;
                        }

                        break;
                    }
                }

                Thread.Sleep(500);

            }
        }

        public void ActionMonster(Monsters monster, Player player)
        {
            if (monster.CurrentHP > 0)
            {
                //치명타 및 회피 부분 추가시 수정 예정
                float monsterAtt = rand.Next(0, 2) == 0 ?
                    monster.Stat.Attack - monster.Stat.Attack / 10 :
                    monster.Stat.Attack + monster.Stat.Attack / 10;

                monster.CurrentSpeed -= 100;
                player.CurrentHP -= (int)Math.Ceiling(monsterAtt);
                Console.WriteLine($"{monster.Name}이(가) 공격합니다! ");
                Console.WriteLine($"{player.Name}는 {Math.Ceiling(monsterAtt)}만큼 데미지를 입었습니다.\n");
            }

            monster.CurrentHP = monster.CurrentHP < 0 ? 0 : monster.CurrentHP;

        }

        public void ActionPlayerAtt(Player player, Monsters monster)
        {

            if (player.CurrentHP > 0 && player.CurrentSpeed >= 100)
            {
                //치명타 및 회피 추가시 수정 예정
                //치명타 배수 200? 150?
                /*
                float playerCriAtt = rand.Next(0, 100) < 크리율 ? 
                    player.Stat.Attack * 1.5f  : 
                    player.Stat.Attack;
                */
                player.CurrentSpeed -= 100;
                monster.CurrentHP -= player.Stat.Attack;
                Console.WriteLine($"{player.Name}이(가) 공격합니다! ");
                Console.WriteLine($"{monster.Name}은(는) {player.Stat.Attack}만큼 데미지를 입었습니다.");
                Thread.Sleep(1000);
            }

            if (monster.CurrentHP <= 0)
            {
                Console.WriteLine($"\n{monster.Name}은(는) 기력이 다했다...");
                monster.DeathCheck = true;
            }


            Thread.Sleep(1500);
        }

        public void Lose(Player player)
        {

        }
        public void Win(Player player)
        {

        }
    }
}
