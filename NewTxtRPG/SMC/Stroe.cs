using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace TxtRPG
{
    internal class Stroe
    {
        int select = 0;
        string set="";  

        public void store_view()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 상점 >");
                Console.ResetColor();

                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {Player.Instance.gold} G\n");
                Console.WriteLine("[ 아이템 목록 ]\n");

                foreach (Item item in Item_List.item_list)
                {
                    if (!item.sell && item.def > 0)
                        Console.WriteLine($"- {item.name} | 방어력 +{item.def} | {item.ex} | {item.gold} G");
                    else if (!item.sell && item.att > 0)
                        Console.WriteLine($"- {item.name} | 공격력 +{item.att} | {item.ex} | {item.gold} G");
                    else if (item.sell && item.def > 0)
                    {
                        Console.Write($"- {item.name} | 방어력 +{item.def} | {item.ex} | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("구매완료");
                    }
                    else if (item.sell && item.att > 0)
                    {
                        Console.Write($"- {item.name} | 공격력 +{item.att} | {item.ex} | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("구매완료");
                    }
                    Console.ResetColor();
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기\n");

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();

                select = OutRangeError.num_return(set, select);

                if (select == 0) break;

                switch (select)
                {
                    case 1:
                        Console.Clear();
                        store_buy();
                        break;

                    case 2:
                        Console.Clear();
                        store_sell();
                        break;

                    default:
                        OutRangeError.check();
                        continue;
                }
            }

            


        }
        private void store_buy()
        {
            
            while (true)
            {
                Console.Clear();

                int counts = 1;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 상점 - 아이템 구매 >");
                Console.ResetColor();

                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {Player.Instance.gold} G\n");
                Console.WriteLine("[ 아이템 목록 ]\n");

                foreach (Item item in Item_List.item_list)
                {
                    if (!item.sell && item.def > 0)
                        Console.WriteLine($"({counts}) {item.name} | 방어력 +{item.def} | {item.ex} | {item.gold} G");
                    else if (!item.sell && item.att > 0)
                        Console.WriteLine($"({counts}) {item.name} | 공격력 +{item.att} | {item.ex} | {item.gold} G");
                    else if (item.sell && item.def > 0)
                    {
                        Console.Write($"({counts}) {item.name} | 방어력 +{item.def} | {item.ex} | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("구매 완료");
                    }
                    else if (item.sell && item.att > 0)
                    {
                        Console.Write($"({counts}) {item.name} | 공격력 +{item.att} | {item.ex} | ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("구매 완료");
                    }
                    Console.ResetColor();
                    counts++;
                }

                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();


                select = int.TryParse(set, out select) ? int.Parse(set) : -1;

                if (select == 0) break;
             

                switch (select)
                {
                    case 1: Buyitem(0); break;
                    case 2: Buyitem(1); break;
                    case 3: Buyitem(2); break;
                    case 4: Buyitem(3); break;
                    case 5: Buyitem(4); break;
                    case 6: Buyitem(5); break;
                    default:
                        OutRangeError.check();
                        continue;
                }
                
            }
        }

        private void store_sell()
        {

            while (true)
            {
                Console.Clear();

                int counts = 1;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 상점 - 아이템 판매 >");
                Console.ResetColor();

                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {Player.Instance.gold} G\n");
                Console.WriteLine("[ 아이템 목록 ]\n");

                foreach (Item item in Item_List.inventory)
                {
                    if (item.def > 0)
                        Console.WriteLine($"({counts}) {item.name} | 방어력 +{item.def} | {item.ex} | {item.gold} G");
                    else if (item.att > 0)
                        Console.WriteLine($"({counts}) {item.name} | 공격력 +{item.att} | {item.ex} | {item.gold} G");
                    
                    Console.ResetColor();
                    counts++;
                }

                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();

                select = int.TryParse(set, out select) ? int.Parse(set) : -1;

                if (select == 0) break;

                switch (select)
                {
                    case 1: Sellitem(0); break;
                    case 2: Sellitem(1); break;
                    case 3: Sellitem(2); break;
                    case 4: Sellitem(3); break;
                    case 5: Sellitem(4); break;
                    case 6: Sellitem(5); break;
                    case 7: Sellitem(6); break;
                    case 8: Sellitem(7); break;
                    case 9: Sellitem(8); break;
                    default:
                        OutRangeError.check();
                        continue;
                }

            }
        }

        private void Buyitem(int a)
        {
            //내 돈이 아이템 돈보다 같거나 클때, 아이템이 안팔렸는지
            if ((Player.Instance.gold >= Item_List.item_list[a].gold) && !Item_List.item_list[a].sell)
            {
                Player.Instance.gold -= Item_List.item_list[a].gold;
                Item_List.item_list[a].sell = true;

                Item_List.inventory.Add(Item_List.item_list[a]);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("구매를 완료했습니다");
            }
            else if (Item_List.item_list[a].sell)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("이미 구매된 아이템입니다");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gold가 부족합니다");
            }

            Console.ResetColor();
            Thread.Sleep(750);
        }

        private void Sellitem(int a)
        {
            //a가 인벤토리 수 보다 크거나 같을때
            if (a >= Item_List.inventory.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("아무것도 없습니다");
            }
            //인벤토리a번째가 팔렸는지 확인
            else if (Item_List.inventory[a].sell)
            {
                Player.Instance.gold += (int)((float)Item_List.inventory[a].gold * 0.85f);
                Item_List.item_list[a].sell = false;

                //인벤토리의 a번째 이름과 아이템리스트 이름을 비교해서 상점 판매를 원래대로 되돌림
                Item_List.item_list.Find(item => item.name == Item_List.inventory[a].name).sell = false;

                //인벤토리 a번째가 공격력이 0이아니고 장착중일때
                if (Item_List.inventory[a].att != 0 && Item_List.inventory[a].equip)
                {
                    Player.Instance.att_we = false;
                    Player.Instance.add_att -= Item_List.inventory[a].att;
                    Player.Instance.att -= Item_List.inventory[a].att;
                    Item_List.inventory[a].equip = false;
                }
                //인벤토리 a번째가 방어력이 0이아니고 장착중일때
                else if (Item_List.inventory[a].def != 0 && Item_List.inventory[a].equip)
                {
                    Player.Instance.def_def = false;
                    Player.Instance.add_def -= Item_List.inventory[a].def;
                    Player.Instance.def -= Item_List.inventory[a].def;
                    Item_List.inventory[a].equip = false;
                }
                Item_List.inventory.Remove(Item_List.inventory[a]);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("판매를 완료했습니다");
            }

            Console.ResetColor();
            Thread.Sleep(750);
        }
    }
}
