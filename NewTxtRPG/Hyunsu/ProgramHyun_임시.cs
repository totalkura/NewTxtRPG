using NewTxtRPG.Hyunsu;
using System;

/*class ProgramHyunsu
{
    static void Main()
    {
        Player player = new Player();
        DungeonEventManager dungeon = new DungeonEventManager();

        backtovil(player, dungeon);
    }

    private static void backtovil(Player player, DungeonEventManager dungeon)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파게티르타 마을로 돌아왔습니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine(" ");
            Console.WriteLine("1. 상태창 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 상점 이용하기");
            Console.WriteLine("4. 휴식하기");
            Console.WriteLine("5. 던전 입장");
            Console.WriteLine("6. 게임 저장");
            Console.WriteLine("7. 게임 불러오기");
            Console.WriteLine("8. 게임 종료");
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            Console.Write(">> ");

            string choice = Console.ReadLine();
            int cho = int.Parse(choice);

            if (cho == 5)
            {
                // 예시: 던전 입장 시 이벤트 실행
                dungeon.TriggerRandomEvent(player);
            }
            else break;
        }
    }
}*/