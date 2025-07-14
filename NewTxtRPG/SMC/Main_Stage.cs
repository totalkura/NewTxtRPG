using System.Runtime.CompilerServices;

namespace TxtRPG

{
    internal class Main_Stage
    {
        private static Main_Stage instance;

        public static Inventory inventory = new Inventory();
        public static Status status = new Status();
        public static Stroe store = new Stroe();
        public static Inn inn = new Inn();
        public static Dungeon dungeon = new Dungeon();
        public static SaveLoad save = new SaveLoad();

        static void Main()
        {
            
            int select = 0;
            string set = "";


            if (File.Exists(save.filepath))
            {
                while (true)
                {
                    Console.WriteLine("저장된 데이터가 있습니다.");
                    Console.WriteLine("불러오시겠습니까?\n");

                    Console.WriteLine("1. 불러오기");
                    Console.WriteLine("2. 새로시작");

                    Console.Write("원하시는 행동을 입력해주세요\n>> ");

                    set = Console.ReadLine();

                    if (int.TryParse(set, out select))
                    {
                        select = int.Parse(set);
                    }

                    switch (select)
                    {
                        case 1:
                            save.load();
                            Console.Clear();
                            break;

                        case 2:
                            status.Player_Status();
                            Console.Clear();
                            break;
                        default:
                            OutRangeError.check();
                            continue;
                    }
                    break;
                }
            }
            else status.Player_Status(); 


            while (true)
                {
                    select = 0;
                    set = "";
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                    Console.WriteLine("1. 상태 보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine("4. 여관");
                    Console.WriteLine("5. 던전 입구");
                    Console.WriteLine("6. 저장\n");

                    if (Player.Instance.max_hp < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("* HP가 0 미만입니다 그냥 다시하세요 \n");
                        Console.ResetColor();
                    }

                    Console.Write("원하시는 행동을 입력해주세요\n>> ");

                    set = Console.ReadLine();

                    select = OutRangeError.num_return(set, select);

                    //if (select == 0) break;

                    switch (select)
                    {
                        case 1:
                            status.status_view();
                            Console.Clear();
                            break;

                        case 2:
                            inventory.inventory_view();
                            Console.Clear();
                            break;

                        case 3:
                            store.store_view();
                            Console.Clear();
                            break;

                        case 4:
                            inn.inn_view();
                            Console.Clear();
                            break;

                        case 5:
                            dungeon.dungeon_view();
                            Console.Clear();
                            break;

                        case 6:
                            save.save_view();
                            Console.Clear();
                            break;
                        default:
                            OutRangeError.check();
                            continue;
                    }

                }
            /*
            Status status = new Status();
            status.status_view();
            */
        }
    }



    public class OutRangeError
    {
        public static void check()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("잘못된 입력입니다");
            Console.ResetColor();
            Thread.Sleep(750);
            Console.Clear();
        }

        public static int num_return(string a, int b)
        {
            b = int.TryParse(a, out b) ? int.Parse(a) : -1;
            /*
            if (int.TryParse(a, out b))
            {
                b = int.Parse(a);
            }
            */
            return b;
        }

    }



}


    