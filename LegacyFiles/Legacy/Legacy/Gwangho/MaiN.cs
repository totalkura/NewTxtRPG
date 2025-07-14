using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Gwangho
{
    internal class MaiN
    {
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("스파게티르타 마을에 오신 걸 환영합니다.");
            Console.WriteLine("캐릭터의 이름을 입력해 주십시오.");
            string name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"캐릭터의 이름은 {name} 입니다. 맞습니까?");
            Console.WriteLine("1. 응 맞아.");
            Console.WriteLine("2. 아닌데?");
            string choice1 = Console.ReadLine();
            if (int.TryParse(choice1, out int cho1))
            {
                // cho1에는 변환된 정수값이 들어 있음
                if (cho1 == 1)
                {
                    Console.WriteLine($"알겠습니다. 캐릭터의 이름은{name} 입니다.");
                }
                else if (cho1 == 2)
                {
                    Main();
                }
                else
                {
                    Console.WriteLine("1이나 2만 입력해 주세요.");
                    Thread.Sleep(1500);
                    Main();
                }
            }
            else
            {
                Console.WriteLine("숫자로 입력해 주세요.");
                Main();
            }
        }
        

    }
}
