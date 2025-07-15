using System;
using System.Threading;

namespace Legacy.Gwangho
{
    internal class MaiN
    {
        static void Main()
        {
            string name;
            string job;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파게티르타 마을에 오신 걸 환영합니다.");
                Console.WriteLine("캐릭터의 이름을 입력해 주십시오.");
                name = Console.ReadLine();

                Console.Clear();
                Console.WriteLine($"캐릭터의 이름은 {name} 입니다. 맞습니까?");
                Console.WriteLine("1. 응 맞아.");
                Console.WriteLine("2. 아닌데?");
                string choice1 = Console.ReadLine();

                if (int.TryParse(choice1, out int cho1))
                {
                    if (cho1 == 1)
                    {
                        Console.WriteLine($"알겠습니다. 캐릭터의 이름은 {name} 입니다.");
                        break;
                    }
                    else if (cho1 == 2)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("1이나 2만 입력해 주세요.");
                        Thread.Sleep(1500);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("숫자로 입력해 주세요.");
                    Thread.Sleep(1500);
                    continue;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("직업을 선택해 주십시오.");

            }
            // 이후 게임 진행 로직
            Console.WriteLine("게임을 시작합니다...");
        }
    }
}