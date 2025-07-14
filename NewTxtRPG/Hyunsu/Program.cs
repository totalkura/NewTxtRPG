using System;
using System.Collections.Generic;

class TextRPG
{
    static string NickName = "";
    static int AttackPower = 10;
    static int Defense = 5;
    static int Hp = 100;
    static int Gold = 1500;

    static void Main()
    {
        SetUserName();
        Play();
    }

    static void SetUserName()
    {
        bool SetUserName = true;

        while (SetUserName)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("\n원하시는 이름을 설정해주세요.\n");
            string UserName = Console.ReadLine();
            Console.WriteLine($"\n입력하신 이름은 {UserName} 입니다.\n");

            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                NickName = UserName;
                SetUserName = false;
            }
            else if (choice == "2") continue;
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadLine();
            }
        }
    }

    static void Play()
    {
        Console.Clear();
        while (true)  // 계속 반복해서 마을 행동 가능
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("0. 종료\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            Console.Write(">> ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Status();
                Exit();
            }
            else if (choice == "2")
            {
                Inventory();
            }
            else if (choice == "3")
            {
                Shop();
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadLine();
            }
            Console.Clear();
        }
    }

    static void Status()
    {
        int totalAttackBonus = 0;
        int totalDefenseBonus = 0;

        foreach (Item item in InventoryItems)
        {
            if (item.Equipped)
            {
                totalAttackBonus += item.AttackBonus;
                totalDefenseBonus += item.DefenseBonus;
            }
        }

        int finalAttack = AttackPower + totalAttackBonus;
        int finalDefense = Defense + totalDefenseBonus;

        Console.Clear();
        Console.WriteLine("\n[상태 보기]");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
        Console.WriteLine("Lv. 01");
        Console.WriteLine($"{NickName} ( 전사 )");
        if (totalAttackBonus > 0)
        {
            Console.WriteLine($"공격력 : {finalAttack} (+{totalAttackBonus})");
        }
        else
        {
            Console.WriteLine($"공격력 : {finalAttack}");
        }

        if (totalDefenseBonus > 0)
        {
            Console.WriteLine($"방어력 : {finalDefense} (+{totalDefenseBonus})");
        }
        else
        {
            Console.WriteLine($"방어력 : {finalDefense}");
        }

        Console.WriteLine($"체력 : {Hp}");
        Console.WriteLine($"Gold : {Gold} G\n");
        Console.WriteLine("0. 나가기");
    }

    static void Exit()
    {
        while (true)
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");
            Console.Write(">> ");
            string exitchoice = Console.ReadLine();
            if (exitchoice == "0") break;
            else Console.WriteLine("\n잘못된 입력입니다.");
        }
    }

    static List<Item> InventoryItems = new List<Item>()
    {
        new Item("무쇠갑옷", "방어력 +5", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5),
        new Item("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0),
        new Item("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0)
    };
    class Item
    {
        public string Name;
        public string Stat;
        public string Text;
        public bool Equipped;
        public int AttackBonus;
        public int DefenseBonus;
        public Item(string name, string stat, string text, int attackbonus, int defensebonus)
        {
            Name = name;
            Stat = stat;
            Text = text;
            AttackBonus = attackbonus;
            DefenseBonus = defensebonus;
            Equipped = false;
        }
    }

    static void Inventory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");

            for (int i = 0; i < InventoryItems.Count; i++)
            {
                Item item = InventoryItems[i];
                string equipped = "";
                if (item.Equipped)
                {
                    equipped = "[E]";
                }
                else
                {
                    equipped = "";
                }
                Console.WriteLine($"- {i + 1} {equipped}{item.Name} | {item.Stat} | {item.Text}");
            }

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            int index;
            if (int.TryParse(input, out index))
            {
                if (index >= 1 && index <= InventoryItems.Count)
                {
                    Item selectedItem = InventoryItems[index - 1];
                    selectedItem.Equipped = !selectedItem.Equipped;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadLine();
            }
        }
    }

    class ShopItem
    {
        public string Name;
        public string Stat;
        public string Text;
        public int Price;
        public bool Purchased;
        public int AttackBonus;
        public int DefenseBonus;

        public ShopItem(string name, string stat, string text, int price, int attackbonus, int defensebonus)
        {
            Name = name;
            Stat = stat;
            Text = text;
            Price = price;
            AttackBonus = attackbonus;
            DefenseBonus = defensebonus;
            Purchased = false;
        }
    }

    static List<ShopItem> ShopItems = new List<ShopItem>()
    {
        new ShopItem("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", 1000, 0, 5),
        new ShopItem("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, 0, 9),
        new ShopItem("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 0, 15),
        new ShopItem("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", 600, 2, 0),
        new ShopItem("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", 1500, 5, 0),
        new ShopItem("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000, 7, 0)
    };
    static void Shop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine($"[보유 골드]\n{Gold} G\n");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < ShopItems.Count; i++)
            {
                ShopItem item = ShopItems[i];

                string priceText = "";
                if (item.Purchased)
                {
                    priceText = "구매완료";
                }
                else
                {
                    priceText = item.Price + " G";
                }

                Console.WriteLine($"- {i + 1} {item.Name} | {item.Stat} | {item.Text} | {priceText}");
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            int index;
            if (int.TryParse(input, out index))
            {
                if (index == 0)
                {
                    break;
                }
                else if (index >= 1 && index <= ShopItems.Count)
                {
                    ShopItem selectedItem = ShopItems[index - 1];

                    if (selectedItem.Purchased)
                    {
                        Console.WriteLine("\n이미 구매한 아이템입니다.");
                    }
                    else if (Gold >= selectedItem.Price)
                    {
                        Gold -= selectedItem.Price;
                        selectedItem.Purchased = true;

                        InventoryItems.Add(new Item(
                            selectedItem.Name,
                            selectedItem.Stat,
                            selectedItem.Text,
                            selectedItem.AttackBonus,
                            selectedItem.DefenseBonus
                        ));
                        Console.WriteLine($"\n'{selectedItem.Name}' 아이템 구매를 완료했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("\nGold가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                }
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
            }
            Console.ReadLine();
        }
    }
}
