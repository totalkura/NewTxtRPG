using System;
using NewTxtRPG.Entitys;
using NewTxtRPG.etc;

namespace NewTxtRPG.Scene
{
    internal class CasinoScene
    {
        private static readonly Random random = new();

        public void StartCasino()
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("=== 슬롯머신 ===", ConsoleColor.Magenta);
                RenderConsole.WriteLine("1부터 6까지의 숫자가 3칸의 슬롯에 랜덤하게 나오게됩니다.", ConsoleColor.DarkYellow);
                RenderConsole.WriteLineWithSpacing("같은 숫자가 2개일 시 2배, 같은 숫자가 3개라면 5배를 받게됩니다!", ConsoleColor.DarkYellow);

                RenderConsole.WriteLine($"현재 골드: {Player.Gold}");
                RenderConsole.WriteLine("베팅 금액을 입력하세요 (0 입력 시 나가기): ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                if (!int.TryParse(input, out int bet) || bet <= 0)
                {
                    RenderConsole.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                    Console.ReadLine();
                    continue;
                }

                if (Player.Gold < bet)
                {
                    RenderConsole.WriteLine("골드가 부족합니다.");
                    Console.ReadLine();
                    continue;
                }

                Player.Gold -= bet;

                // 슬롯머신 결과 생성 (1~6)
                int[] slots = new int[3];
                for (int i = 0; i < 3; i++)
                    slots[i] = random.Next(1, 7);

                // 슬롯머신 결과 출력 (한 칸씩 딜레이)
                Console.Write("결과: ");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"[ {slots[i]} ] ");
                    Thread.Sleep(800);
                }
                Console.WriteLine();

                int reward = 0;
                if (slots[0] == slots[1] && slots[1] == slots[2])
                {
                    reward = bet * 5;
                    RenderConsole.WriteLine("★★★ JACKPOT! 5배 획득! ★★★");
                }
                else if (slots[0] == slots[1] || slots[1] == slots[2] || slots[0] == slots[2])
                {
                    reward = bet * 2;
                    RenderConsole.WriteLine("★ 2개 일치! 2배 획득! ★");
                }
                else
                {
                    RenderConsole.WriteLine("꽝! 골드를 잃었습니다.");
                }

                if (reward > 0)
                {
                    Player.Gold += reward;
                    Console.WriteLine($"획득 골드: {reward} (현재 골드: {Player.Gold})");
                }
                else
                {
                    Console.WriteLine($"현재 골드: {Player.Gold}");
                }

                Console.WriteLine("계속하려면 Enter를 누르세요...");
                Console.ReadLine();
            }
            Console.Clear();
        }
    }
}
