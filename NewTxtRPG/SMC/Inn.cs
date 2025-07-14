using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace TxtRPG
{
    internal class Inn
    {
        int select = 0;
        string set="";

        int check_chat = -1;

        string[] sans_normal = {
    "░░░░░░░░░░▄██████████████████▄░░░░░░░░░░░░",
    "░░░░░░░░▄██████████████████████▄░░░░░░░░░░",
    "░░░░░░░▄████████████████████████▄░░░░░░░░░",
    "░░░░░░░███▀░░░░░▀█████▀░░░░░▀▀███░░░░░░░░░",
    "░░░░░░░██░░░░▄▄░░█████░░░▄▄░░░░██░░░░░░░░░",
    "░░░░░░░░█▄▄░░░░░░██░░█▄░░░░░▄▄██░░░░░░░░░░",
    "░░░░░░░░░████▀▀▄██░░░░██▄▀▀▀███░░░░░░░░░░░",
    "░░░░░░░░███░▀█████▄▄▄▄█████▀░███░░░░░░░░░░",
    "░░░░░░░░▀██▄▀░▄▄░▄▄░▄▄░▄▄░▄░███▀░░░░░░░░░░",
    "░░░░░▄▄░░▀███▄▄▀░██░██░█▀░▄███▀░▄▄▄▄░░░░░░",
    "░░░░░████▄░░▀████▄▄▄▄▄▄▄▄████▀░▄██▀░▄░░░░░",
    "░░░▄▀░░▀▀▀▀▄░░░░░░░░░░░░░░░░░▄█▀▀░░░▀▄▄░░░",
    "░░▄▀░░░▄░░░░▀▄▄▄▄░▀████░░▄▄▄▄▀▀░░░▄░░░▀▄░░",
    "▄▀░░░░░█░░░░░█░░█░░▀▀▀▀░█▀█░░░░░░░█░░░░▀▄░",
    "█░░░░░░█░░░░░█░░█░████▄░█▄█░░░▄▄▄██░░░░░▀▄",
    "█░░░░░░█▀░░░░░█░▄░█████░▄▄█░▄▀▀░░░█░░░░░░█",
    "█░░░░░█░░░░░░░█▀░░█████░░░█░█░░░░░█░░░░░░█",
    "▀▀▄▄░░█░░░░░░░▀▄▀░█████░▀█▄▀░░░░░░█░░░░▄▀▀",
    "░░░▀█░█▄▄░░░░░░█▄▄▄▄▄▄▄▄▄█░░░░░░░▄█░▄▄▀░░░",
    "░░░░░░▀███▄▄▄▄▄▀▀▀▀░░░░░▀▀▀▀▀▀▀▀██░▀▀░░░░░",
    "░░░░░░░░░░░▄▄░░░░░░░░░░░░░▄▄░░░░▄░░░░░░░░░",
    "░░░░░░░░█░▄█▀░░░░░░░▄░░░░░░██░░░░░█░░░░░░░",
    "░░░░░░░░█░██░░░░░░░░██░░░░░███░░░░█░░░░░░░",
    "░░░░░░░█░▄█░░░░░░░░▄█░█░░░░███░░░░░█░░░░░░",
    "░░░░░░░█░██░░░░░░░░█░░█░░░░░██░░░░░█░░░░░░",
    "░░░░░░░█▄██░░░░░░░░█░░█░░░░░██▄░▄▄▄█░░░░░░",
    "░░░░░░░░░░░▄▄▄▄▄▄▄▄░░░░▄▄▄▄▄░░░▄░░░░░░░░░░",
    "░░░░░▄███▄▄▀███░░░░░░░░░░██████░▄▄██▄▄░░░░",
    "░░░░████████░▀▀░░░░░░░░░░▀▀▀▀▀▄████████░░░"
};
        string[] sans_angry = {
    "░░░░░░░░░░▄██████████████████▄░░░░░░░░░░░░",
    "░░░░░░░░▄██████████████████████▄░░░░░░░░░░",
    "░░░░░░░▄████████████████████████▄░░░░░░░░░",
    "░░░░░░░███▀░░░░░▀█████▀░▄▄▄░▀▀███░░░░░░░░░",
    "░░░░░░░██░░░░░░░░█████░██░██░░░██░░░░░░░░░",
    "░░░░░░░░█▄▄░░░░░░██░░█▄░▀▀▀░▄▄██░░░░░░░░░░",
    "░░░░░░░░░████▀▀▄██░░░░██▄▀▀▀███░░░░░░░░░░░",
    "░░░░░░░░███░▀█████▄▄▄▄█████▀░███░░░░░░░░░░",
    "░░░░░░░░▀██▄▀░▄▄░▄▄░▄▄░▄▄░▄░███▀░░░░░░░░░░",
    "░░░░░▄▄░░▀███▄▄▀░██░██░█▀░▄███▀░▄▄▄▄░░░░░░",
    "░░░░░████▄░░▀████▄▄▄▄▄▄▄▄████▀░▄██▀░▄░░░░░",
    "░░░▄▀░░▀▀▀▀▄░░░░░░░░░░░░░░░░░▄█▀▀░░░▀▄▄░░░",
    "░░▄▀░░░▄░░░░▀▄▄▄▄░▀████░░▄▄▄▄▀▀░░░▄░░░▀▄░░",
    "▄▀░░░░░█░░░░░█░░█░░▀▀▀▀░█▀█░░░░░░░█░░░░▀▄░",
    "█░░░░░░█░░░░░█░░█░████▄░█▄█░░░▄▄▄██░░░░░▀▄",
    "█░░░░░░█▀░░░░░█░▄░█████░▄▄█░▄▀▀░░░█░░░░░░█",
    "█░░░░░█░░░░░░░█▀░░█████░░░█░█░░░░░█░░░░░░█",
    "▀▀▄▄░░█░░░░░░░▀▄▀░█████░▀█▄▀░░░░░░█░░░░▄▀▀",
    "░░░▀█░█▄▄░░░░░░█▄▄▄▄▄▄▄▄▄█░░░░░░░▄█░▄▄▀░░░",
    "░░░░░░▀███▄▄▄▄▄▀▀▀▀░░░░░▀▀▀▀▀▀▀▀██░▀▀░░░░░",
    "░░░░░░░░░░░▄▄░░░░░░░░░░░░░▄▄░░░░▄░░░░░░░░░",
    "░░░░░░░░█░▄█▀░░░░░░░▄░░░░░░██░░░░░█░░░░░░░",
    "░░░░░░░░█░██░░░░░░░░██░░░░░███░░░░█░░░░░░░",
    "░░░░░░░█░▄█░░░░░░░░▄█░█░░░░███░░░░░█░░░░░░",
    "░░░░░░░█░██░░░░░░░░█░░█░░░░░██░░░░░█░░░░░░",
    "░░░░░░░█▄██░░░░░░░░█░░█░░░░░██▄░▄▄▄█░░░░░░",
    "░░░░░░░░░░░▄▄▄▄▄▄▄▄░░░░▄▄▄▄▄░░░▄░░░░░░░░░░",
    "░░░░░▄███▄▄▀███░░░░░░░░░░██████░▄▄██▄▄░░░░",
    "░░░░████████░▀▀░░░░░░░░░░▀▀▀▀▀▄████████░░░"
};
        bool color_check = false;

        public void inn_view()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.White;
            
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 여관 >");
                Console.ResetColor();

                color_check = check_chat != 3 ? false : true;
                color(color_check);

                SlowwriteLine();

                Console.WriteLine();

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {Player.Instance.gold} G");


                Console.ResetColor();

                Console.WriteLine("\n1. 숙박하기 ( -300 gold | HP 100 회복)");
                Console.WriteLine("0. 나가기\n");

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();

                select = OutRangeError.num_return(set, select);

                if (select == 0) break;

                switch (select)
                {
                    case 1:
                        if (Player.Instance.gold < 300)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Gold가 부족합니다");
                            check_chat++;
                        }
                        else if (Player.Instance.now_hp == Player.Instance.max_hp)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("이미 HP가 최대치 입니다");
                        }
                        else
                        {
                            Player.Instance.gold -= 300;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Player.Instance.now_hp += 100;
                            Player.Instance.now_hp = Player.Instance.now_hp > Player.Instance.max_hp ? Player.Instance.max_hp : Player.Instance.now_hp;
                            Console.WriteLine("HP를 100 회복하였습니다");
                            Console.WriteLine($"현재 HP : {Player.Instance.now_hp} / {Player.Instance.max_hp}");
                        }
                        Console.ResetColor();
                        Thread.Sleep(1500);
                        break;
                    default:
                        OutRangeError.check();
                        continue;
                }
            }

            


        }

        void SlowwriteLine()
        {
            Console.SetCursorPosition(85, 1);
            string text = "";
            int delay = 40;
            switch (check_chat)
            {
                case -1: text = "..."; delay -= 40; break;
                case 0: text = "정말 아름다운 날이야\n"; break;
                case 1: text = "새들은 지저귀고, 꽃들은 피어나고..\n"; break;
                case 2: text = "이런 날엔, 너 같은 돈안내는 놈들은..\n"; break;
                case 3: text = "지옥에서 불타고 있어야 하는데\n";  break;
                default: text = "..."; delay -=40;  break;
            }
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            
            Console.SetCursorPosition(0, 1);
        }

        void color(bool check)
        {
            
            int offsetX = 50;

            if (!check)
            {
                
                for (int y = 0; y < sans_normal.Length; y++)
                {
                    Console.SetCursorPosition(offsetX, y);
                    string line = sans_normal[y].Replace('░', ' ');
                    Console.Write(line);
                }
            }
            else if (check)
            {
                for (int y = 0; y < sans_angry.Length; y++)
                {
                    Console.SetCursorPosition(offsetX, y);
                    string line = sans_angry[y].Replace('░', ' ');
                    Console.Write(line);
                }

                var bluePositions = new List<(int x, int y)> {
    (24, 3), (25, 3), (26, 3),
    (23, 4), (24 , 4), (26, 4), (27 , 4),
    (24, 5), (25 , 5), (26 , 5)
};
                
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                foreach (var pos in bluePositions)
                {
                    int x = pos.x + offsetX;
                    int y = pos.y;

                    Console.SetCursorPosition(x, y);
                    char c = sans_angry[y][pos.x] == '░' ? ' ' : sans_angry[y][pos.x];
                    if (c != ' ')
                    {
                        Console.Write(c);
                    }
                }
            }
            Console.ResetColor();
            
        }
    }
}
