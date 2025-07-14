using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace TxtRPG
{
    internal class Dungeon
    {
        Random random = new Random();

        int select = 0;
        int ran = 0;
        int stack = 0;

        string set="";
        
        
        public void dungeon_view()
        {
            
            while (true)
            {
                ran = random.Next(0,100);
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 던전입장 >");
                Console.ResetColor();

                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다\n" );

                Console.WriteLine("[ 던전 난이도 ]");

                Console.WriteLine("\n1. 쉬움 | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려움 | 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기\n");

                if (Player.Instance.now_hp < 50)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("< 체력이 낮을때 던전에 들어가는건 위험합니다>\n");
                    Console.ResetColor();

                }

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();

                select = OutRangeError.num_return(set, select);

                if (select == 0) break;

                switch (select)
                {
                    case 1:
                        if (Player.Instance.def < 5 && ran <= 40)
                        {
                            DungoeonFail(select);
                        }
                        else
                            DungoeonClear(select);
                        break;
                    case 2:
                        if ((Player.Instance.def < 11 && ran <= 40) || Player.Instance.def * 2 < 11)
                        {
                            DungoeonFail(select);
                        }
                        else
                            DungoeonClear(select);
                        break;
                    case 3:
                        if (Player.Instance.def < 17 && ran <= 40 || Player.Instance.def * 2 < 11)
                        {
                            DungoeonFail(select);
                        }
                        else
                            DungoeonClear(select);
                        break;
                    default:
                        OutRangeError.check();
                        continue;
                }
            }
        }

        private void DungoeonClear(int a)
        {
            float dam = 0;
            float add_gold = 0; 

            int golds = 0;
            int exps = 0;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("< 던전 클리어 >");
            Console.ResetColor();

            Console.WriteLine("축하합니다!!");

            if (a == 1)
            {
                exps = 10;
                golds = 1000;
                dam = 5 - Player.Instance.def;
                Console.WriteLine("쉬움 던전을 클리어 하였습니다\n");
                Player.Instance.now_hp -= random.Next(20 + (int)dam, 35 + (int)dam);
                
            }
            else if (a == 2)
            {
                exps = 15;
                golds = 1700;
                dam = 11 - Player.Instance.def;
                Console.WriteLine("일반 던전을 클리어 하였습니다\n");
                Player.Instance.now_hp -= random.Next(30 + (int)dam, 45 + (int)dam);
            }
            else if (a == 2)
            {
                exps = 25;
                golds = 2500;
                dam = 17 - Player.Instance.def;
                Console.WriteLine("어려움 던전을 클리어 하였습니다\n");
                Player.Instance.now_hp -= random.Next(50 + (int)dam, 75 + (int)dam);
            }
            
            add_gold = dam * -1;

            Console.WriteLine("< 체력이 감소하였습니다 >");
            Console.ResetColor();
            Console.WriteLine($"현재 HP : {Player.Instance.now_hp} / {Player.Instance.max_hp}");

            if (Player.Instance.now_hp <= 0)
            {
                if (stack < 2)
                {
                    Console.WriteLine($"\n클리어는 하였지만 심각한 부상으로 인해 보상을 적게 받습니다..");
                    stack++;
                    Player.Instance.now_hp = 10;
                    golds = golds / 2;
                }
                else
                {
                    Console.WriteLine($"\n무리해 버려서 최대 HP가 10 감소하고 가지고 있는 Gold 의 대부분을 잃습니다..");
                    Player.Instance.now_hp = 10;
                    Player.Instance.max_hp -= 10;
                    
                    stack = 0;

                    exps = 0;
                    golds = 0;
                    Player.Instance.gold -= (int)((float)Player.Instance.gold*0.99);
                }

            }

            

            Console.WriteLine($"던전 클리어 경험치 : {exps} exp");
            Console.WriteLine($"던전 클리어 골드 : {golds} G");
            Console.Write($"클리어 최종 골드 : {golds} + {(int)((float)golds * (float)(random.Next((int)add_gold, (int)add_gold * 2) / (float)100))} = ");

            golds = golds + (int)((float)golds * (float)(random.Next((int)add_gold, (int)add_gold * 2) / (float)100));
            Console.WriteLine($"{golds} G");

            Main_Stage.status.Player_Dungeon_Clear(golds, exps);
            Main_Stage.status.Player_Exp();

            Console.WriteLine("- 자동 저장됩니다 -");
            Main_Stage.save.save();

            Thread.Sleep(2500);

        }

        private void DungoeonFail(int a)
        {

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("< 던전 클리어 실패 >");
            Console.ResetColor();
            
            if (a == 1)
            {
                Console.WriteLine("쉬움 던전에서 퇴각 하였습니다\n");
                Player.Instance.now_hp -= Player.Instance.max_hp / 2;
            }
            else if (a == 2)
            {
                Console.WriteLine("일반 던전에서 퇴각 하였습니다\n");
                Player.Instance.now_hp -= Player.Instance.max_hp * 3 / 4;
            }
            else if (a == 3)
            {
                Console.WriteLine("어려움 던전에서 퇴각 하였습니다\n");
                Player.Instance.now_hp -= Player.Instance.max_hp * 9 / 10;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("< 최대 체력이 감소하였습니다 >");
            Player.Instance.max_hp -= 10;
            Player.Instance.now_hp = 10;
            Console.ResetColor();
            Console.WriteLine($"현재 HP : {Player.Instance.now_hp} / {Player.Instance.max_hp}\n");
            Console.WriteLine("- 자동 저장됩니다 -");
            Main_Stage.save.save();
            Thread.Sleep(2500);
        }


    }
}
