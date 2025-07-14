using System.ComponentModel;

namespace TxtRPG
{
    internal class Inventory
    {
        

        int select = 0;
        string set = "";
        
        public void inventory_view()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 인벤토리 >");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기\n");

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();

                select = OutRangeError.num_return(set, select);


                if (select == 0) break;

                switch (select)
                {
                    case 1:
                        equ();
                        Console.Clear();
                        break;
                    default:
                        OutRangeError.check();
                        continue;
                }
            }

            


        }

        private void equ()
        {
            while (true)
            {
                Console.Clear();
                int count = 1;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 인벤토리 - 장착 관리 >");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[ 아이템 목록 ]");

                foreach (Item item in Item_List.inventory)
                {
                    if (item.def != 0)
                    {
                        if (item.equip)
                        {
                            Console.Write($"{count}. {item.name} | 방어력 +{item.def} | {item.ex} | ");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" [ E ]");
                            Console.ResetColor();
                        }
                        else
                            Console.WriteLine($"{count}. {item.name} | 방어력 +{item.def} | {item.ex} ");
                    }
                    else if (item.att != 0)
                    {
                        if (item.equip)
                        {
                            Console.Write($"{count}. {item.name} | 공격력 +{item.att} | {item.ex} | ");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" [ E ]");
                            Console.ResetColor();
                        }
                        else
                            Console.WriteLine($"{count}. {item.name} | 공격력 +{item.att} | {item.ex} ");
                    }
                    ++count;
                }

              
                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");

                set = Console.ReadLine();

                select = OutRangeError.num_return(set, select);

                if (select == 0) break;

                switch (select)
                {
                    case 1: equip(0); break;
                    case 2: equip(1); break;
                    case 3: equip(2); break;
                    case 4: equip(3); break;
                    case 5: equip(4); break;
                    case 6: equip(5); break;
                    case 7: equip(6); break;
                    case 8: equip(7); break;
                    case 9: equip(8); break;
                    default:
                        OutRangeError.check();
                        continue;
                }
            }
        }

        private void equip(int a)
        {
            //인벤토리 비어있는거 확인
            if (Item_List.inventory.Count > a)
            {
                //인벤토리 장비 안되어있는거 확인
                if (!Item_List.inventory[a].equip )
                {   
                    //공격력 0 아닌거 확인 
                    if (Item_List.inventory[a].att != 0 )
                    {
                        //플레이어 무기 장착 확인
                        if (Player.Instance.att_we)
                        {
                            for (int i = 0; i < Item_List.inventory.Count; ++i)
                            {
                                //인벤토리 i번째 장착중인지 확인
                                if (Item_List.inventory[i].equip && Item_List.inventory[i].att != 0)
                                {
                                    //장착 false / 플레이어 공격력,추가공격력 정상화
                                    Item_List.inventory[i].equip = false;
                                    Player.Instance.att -= Item_List.inventory[i].att;
                                    Player.Instance.add_att -= Item_List.inventory[i].att;
                                }
                            }
                        }
                        //플레이어 무기 장착여부 On / 플레이어 공격력, 추가공격력 + (인벤토리 a번째꺼만큼)
                        Player.Instance.att_we = true;
                        Player.Instance.att += Item_List.inventory[a].att;
                        Player.Instance.add_att += Item_List.inventory[a].att;
                      
                    }
                    //방어력 0 아닌거 확인 
                    else if (Item_List.inventory[a].def != 0 )
                    {
                        //플레이어 방어구 장착 확인
                        if (Player.Instance.def_def)
                        {
                            for (int i = 0; i < Item_List.inventory.Count; ++i)
                            {
                                //인벤토리 i번째 장착중인지 확인
                                if (Item_List.inventory[i].equip && Item_List.inventory[i].def != 0)
                                {
                                    //장착 false / 플레이어 공격력,추가공격력 정상화
                                    Item_List.inventory[i].equip = false;
                                    Player.Instance.def -= Item_List.inventory[i].def;
                                    Player.Instance.add_def -= Item_List.inventory[i].def;
                                }
                            }
                        }
                        //플레이어 방어구 장착여부 On / 플레이어 방어력, 추가방어력 + (인벤토리 a번째꺼만큼)
                        Player.Instance.def_def = true;
                        Player.Instance.def += Item_List.inventory[a].def;
                        Player.Instance.add_def += Item_List.inventory[a].def;
                        
                    }
                    //인벤토리 a번째 창착 On
                    Item_List.inventory[a].equip = true;

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("장착이 완료되었습니다");
                }
                else if (Item_List.inventory[a].equip)
                {
                    Item_List.inventory[a].equip = false;

                    if (Item_List.inventory[a].att != 0 && Player.Instance.att_we)
                    {
                        Player.Instance.att_we = false;
                        Player.Instance.att -= Item_List.inventory[a].att;
                        Player.Instance.add_att -= Item_List.inventory[a].att;
                    }
                    else if (Item_List.inventory[a].def != 0 && Player.Instance.def_def)
                    {
                        Player.Instance.def_def = false;
                        Player.Instance.def -= Item_List.inventory[a].def;
                        Player.Instance.add_def -= Item_List.inventory[a].def;
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("장착이 해제되었습니다");
                }

                

            }
            Console.ResetColor();
            Thread.Sleep(750);

        }


    }
}
