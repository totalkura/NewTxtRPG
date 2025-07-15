using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Legacy.Gwangho
{
    internal class Game
    {
        public static void Backtovil()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파게티르타 마을로 들어왔습니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine(" ");
                Console.WriteLine("1. 상태창 보기");
                Console.WriteLine("2. 인벤토리 보기");
                Console.WriteLine("3. 상점 이용하기");
                Console.WriteLine("4. 휴식하기");
                Console.WriteLine("5. 던전 입장");
                Console.WriteLine("6. 게임 저장");
                Console.WriteLine("7. 게임 종료");
                Console.WriteLine(" ");
                Console.Write("원하시는 행동을 입력해 주세요: ");

                string choice = Console.ReadLine();
                if (!int.TryParse(choice, out int cho))
                {
                    Console.WriteLine("숫자로 입력해 주세요.");
                    Thread.Sleep(1500);
                    continue;
                }

                switch (cho)
                {
                    case 1:
                        Status();
                        break;
                    case 2:
                        Inventory();
                        break;
                    case 3:
                        StoreShopping();
                        break;
                    case 4:
                        Rest();
                        break;
                    case 5:
                        EnterDungeon();
                        break;
                    case 6:
                        SaveGame();
                        break;
                    case 7:
                        Console.WriteLine("게임을 종료합니다.");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("그건 올바른 행동이 아닙니다.");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

    }
}

