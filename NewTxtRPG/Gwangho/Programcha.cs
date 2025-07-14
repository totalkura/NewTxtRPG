using System;
using System.Collections.Generic;

class Program
 {
 
    static int level = 1;
    static float atk = 10;
    static int def = 5;
    static int hp = 100;
    static int gold = 1500;
    static string nickname = "Giga_Chad";
    static string job = "전사";

    static int exp = 0;
    static int maxexp = 100;

    static Random rand = new Random();

    internal class Item
    {
        public string Name
        {
            get;
            set;
        }
        public string itemPower
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }

        public Item(string name, string power, string desc, int price)
        {
            Name = name;
            itemPower = power;
            Description = desc;
            Price = price;
        }


    }

    static List<Item> inventory = new List<Item>();
    static List<Item> storeItems = new List<Item>();

    static Item equippedWeapon = null;
    static Item equippedArmor = null;



    static void Main()
    {
        InitializeStore();

        Console.Clear();
        Console.WriteLine("스파게티르타 마을에 오신 걸 환영합니다.");
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

        string choice = Console.ReadLine();
        int cho = int.Parse(choice);


        switch (cho)
        {
            case 1:
                status();
                break;
            case 2:
                Inventory();
                break;
            case 3:
                storeshopping();
                break;
            case 4:
                rest();
                break;
            case 5:
                enterdungeon();
                break;
            case 6:
                savegame();
                backtovil();
                break;
            case 7:
                loadgame();
                backtovil();
                break;
            case 8:
                Environment.Exit(0);
                Console.WriteLine("게임을 종료합니다.");
                Thread.Sleep(2000);
                break;
            default:
                Console.WriteLine("그건 올바른 행동이 아닙니다.");
                Thread.Sleep(1500);
                backtovil();
                break;

        }

    }

    static void status()
    {
        Console.Clear();
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine(" ");

        float totalAtk = atk;
        int totalDef = def;

        float atkBonus = 0;
        int defBonus = 0;

        if (equippedWeapon != null && equippedWeapon.itemPower.Contains("공격력"))
        {
            string[] parts = equippedWeapon.itemPower.Split('+');
            atkBonus = float.Parse(parts[1]);
            totalAtk += atkBonus;
        }

        if (equippedArmor != null && equippedArmor.itemPower.Contains("방어력"))
        {
            string[] parts = equippedArmor.itemPower.Split('+');
            defBonus = int.Parse(parts[1]);
            totalDef += defBonus;
        }

        Console.WriteLine($"Lv. {level} | {exp} / {maxexp}");
        Console.WriteLine($"{nickname} ({job})");
        Console.WriteLine($"공격력 : {totalAtk}" + (atkBonus > 0 ? $" (+{atkBonus})" : ""));
        Console.WriteLine($"방어력 : {totalDef}" + (defBonus > 0 ? $" (+{defBonus})" : ""));
        Console.WriteLine($"체  력 : {hp}");
        Console.WriteLine($"Gold   : {gold} G");
        Console.WriteLine(" ");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 이름 변경");
        Console.WriteLine(" ");
        Console.WriteLine("원하시는 행동을 선택해 주세요.");

        string choice = Console.ReadLine();
        int cho = int.Parse(choice);

        switch (cho)
        {
            case 0:
                backtovil();
                break;
            case 1:
                changename();
                break;
            default:
                Console.WriteLine("그건 올바른 행동이 아닙니다.");
                Thread.Sleep(1500);
                status();
                break;
        }

    }

    static void backtovil()
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

        string choice = Console.ReadLine();
        int cho = int.Parse(choice);


        switch (cho)
        {
            case 1:
                status();
                break;
            case 2:
                Inventory();
                break;
            case 3:
                storeshopping();
                break;
            case 4:
                rest();
                break;
            case 5:
                enterdungeon();
                break;
            case 6:
                savegame();
                backtovil();
                break;
            case 7:
                loadgame();
                backtovil();
                break;
            case 8:
                Console.WriteLine("게임을 종료합니다.");
                Thread.Sleep(2000);
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("그건 올바른 행동이 아닙니다.");
                Thread.Sleep(1500);
                backtovil();
                break;

        }
    }

    static void changename()
    {
        Console.Clear();
        Console.WriteLine("어떤 이름으로 변경하시겠습니까?");
        nickname = Console.ReadLine();
        Console.WriteLine($"당신의 이름은 {nickname} 입니다. 맞습니까?");
        Console.WriteLine("1. 네");
        Console.WriteLine("2. 아니오");

        string choice = Console.ReadLine();
        int cho = int.Parse(choice);

        switch (cho)
        {
            case 1:
                Console.WriteLine($"알겠습니다. 이제 당신의 이름은 {nickname} 입니다.");
                Thread.Sleep(1500);
                status();
                break;
            case 2:
                changename();
                break;
            default:
                Console.WriteLine("그건 올바른 행동이 아닙니다.");
                Thread.Sleep(1500);
                changename();
                break;
        }
    }

    static void Inventory()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine(" ");
        Console.WriteLine("[아이템 목록]");

        if (inventory.Count == 0)
        {
            Console.WriteLine(" ");
            Console.WriteLine("비어 있습니다.");
            Console.WriteLine(" ");
            Console.WriteLine("0. 나가기");
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력해 주세요.");

            string choice = Console.ReadLine();
            int cho = int.Parse(choice);

            switch (cho)
            {
                case 0:
                    backtovil();
                    break;
                default:
                    Console.WriteLine("그건 올바른 행동이 아닙니다.");
                    Thread.Sleep(1500);
                    Inventory();
                    break;

            }
        }
        else
        {
            Console.WriteLine(" ");
            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                string equippedText = (item == equippedWeapon || item == equippedArmor) ? "[E]" : "";
                Console.WriteLine($"{i + 1}. {equippedText}{item.Name} | {item.itemPower} | {item.Description}");
            }

            
            Console.WriteLine(" ");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력해 주세요.");

            string choice = Console.ReadLine();
            int cho = int.Parse(choice);

            switch (cho)
            {
                case 1:
                    equip();
                    break;
                case 0:
                    backtovil();
                    break;
                default:
                    Console.WriteLine("그건 올바른 행동이 아닙니다.");
                    Thread.Sleep(1500);
                    Inventory();
                    break;
            }
        }
    }

    static void InitializeStore()
    {
        storeItems.Add(new Item("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", 1000));
        storeItems.Add(new Item("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2100));
        storeItems.Add(new Item("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
        storeItems.Add(new Item("무희의 옷", "방어력 +2", "고물가 시대, 원가 절감을 통한 극한의 가성비를 추구한 갑?옷입니다.", 300));
        storeItems.Add(new Item("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", 600));
        storeItems.Add(new Item("청동 도끼", "공격력 +5", "어디선가 사용됐던것 같은 도끼입니다.", 1500));
        storeItems.Add(new Item("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2200));
        storeItems.Add(new Item("스파게토 채찍", "공격력 +1", "알고 계셨나요? 스파게티는 사실 복수형 단어이고, 단수형 단어로는 스파게토(Spaghetto)라고 합니다.", 250));
    }


    static void storeshopping()
        {
        Console.Clear();
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine(" ");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine("Gold: " + gold + " G");
        Console.WriteLine(" ");
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < storeItems.Count; i++)
        {
            var item = storeItems[i];

            bool alreadyOwned = inventory.Exists(invItem => invItem.Name == item.Name);

            if (alreadyOwned)
            {
                Console.WriteLine($"{i + 2}. {item.Name} | {item.itemPower} | {item.Description} | 구매 완료");
            }
            else
            {
                Console.WriteLine($"{i + 2}. {item.Name} | {item.itemPower} | {item.Description} | {item.Price} G");
            }
        }

        Console.WriteLine();
        Console.WriteLine("1.아이템 판매");
        Console.WriteLine("0. 나가기");
        Console.Write("구매할 아이템 번호를 입력하세요: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice))
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            storeshopping();
            return;
        }

        if (choice == 0)
        {
            backtovil();
            return;
        }
        
        if (choice == 1)
        {
            sell();
            return;
        }

        int itemIndex = choice - 2;

        if (choice < 0 || choice > storeItems.Count +1)
        {
            Console.WriteLine("존재하지 않는 아이템입니다.");
            Thread.Sleep(1500);
            storeshopping();
            return;
        }

        var selectedItem = storeItems[itemIndex];

        if (gold < selectedItem.Price)
        {
            Console.WriteLine("Gold가 부족합니다!");
            Thread.Sleep(1500);
            storeshopping();
            return;
        }

        if (inventory.Exists(invItem => invItem.Name == selectedItem.Name))
        {
            Console.WriteLine("이미 구매한 아이템입니다.");
            Thread.Sleep(1500);
            storeshopping();
            return;
        }

        gold -= selectedItem.Price;
        AddItemToInventory(selectedItem);
        Console.WriteLine($"{selectedItem.Name} 구매를 완료했습니다.");
        Thread.Sleep(1500);
        storeshopping();
    }

        static void AddItemToInventory(Item item)
        {
            inventory.Add(item);
            Console.WriteLine($"{item.Name} 아이템을 인벤토리에 추가했습니다!");
        }

        static void equip()
        {
        Console.Clear();
        Console.WriteLine("장착할 아이템을 선택하세요.");
        Console.WriteLine("장비 가능한 무기 또는 방어구만 선택하세요.");
        Console.WriteLine();

        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {inventory[i].Name} | {inventory[i].itemPower}");
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.Write("선택: ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice))
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            equip();
            return;
        }

        if (choice == 0)
        {
            Inventory();
            return;
        }

        if (choice < 1 || choice > inventory.Count)
        {
            Console.WriteLine("해당 번호의 아이템은 존재하지 않습니다.");
            Thread.Sleep(1500);
            equip();
            return;
        }

        var selected = inventory[choice - 1];

        if (selected.itemPower.Contains("공격력"))
        {
            if (equippedWeapon == selected)
            {
                equippedWeapon = null;
                Console.WriteLine($"{selected.Name} 무기 장착을 해제했습니다.");
            }
            else
            {
                equippedWeapon = selected;
                Console.WriteLine($"{selected.Name}을(를) 무기로 장착했습니다.");
            }
        }
        else if (selected.itemPower.Contains("방어력"))
        {
            if (equippedArmor == selected)
            {
                equippedArmor = null;
                Console.WriteLine($"{selected.Name} 방어구 장착을 해제했습니다.");
            }
            else
            {
                equippedArmor = selected;
                Console.WriteLine($"{selected.Name}을(를) 방어구로 장착했습니다.");
            }
        }
        else
        {
            Console.WriteLine("이 아이템은 장비할 수 없습니다.");
        }

        Thread.Sleep(1500);
        Inventory();
    }

    static void rest()
    {
        Console.Clear();
        Console.WriteLine($"500G를 내면 휴식 할 수 있습니다. (보유 골드 : {gold}G)");
        Console.WriteLine("1. 휴식하기");
        Console.WriteLine("0. 나가기");
        Console.WriteLine(" ");
        Console.WriteLine("원하시는 행동을 입력해 주세요.");
        string choice = Console.ReadLine();
        int cho = int.Parse(choice);

        switch (cho)
        {
            case 1:
                if (gold < 500)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Thread.Sleep(1500);
                    backtovil();
                    return;
                }

                if (hp < 100)
                {
                    int recovered = 100 - hp;
                    hp = 100;
                    gold -= 500;
                    Console.WriteLine("휴식을 완료했습니다.");
                }
                else
                {
                    Console.WriteLine("이미 체력이 최대입니다. 휴식할 필요가 없습니다.");
                }

                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
                backtovil();
                break;
            case 0:
                backtovil();
                break;
            default:
                Console.WriteLine("그건 올바른 행동이 아닙니다.");
                Thread.Sleep(1500);
                backtovil();
                break;
        }

    }

    static void sell()
    {
        Console.Clear();
        Console.WriteLine("판매할 아이템을 선택하세요:");
        if (inventory.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어 있습니다.");
            Thread.Sleep(1500);
            storeshopping();
            return;
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            var item = inventory[i];
            Console.WriteLine($"{i + 1}. {item.Name} | {item.itemPower} | {item.Description} | 판매가: {(item.Price * 85) / 100} G");
        }

        Console.WriteLine("0. 나가기");
        Console.Write("판매할 아이템 번호 입력: ");
        string input = Console.ReadLine();
        int choice;
        if (!int.TryParse(input, out choice) || choice < 0 || choice > inventory.Count)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            sell();
            return;
        }

        if (choice == 0)
        {
            storeshopping();
            return;
        }

        var selectedItem = inventory[choice - 1];

        if (selectedItem == equippedWeapon)
            equippedWeapon = null;
        if (selectedItem == equippedArmor)
            equippedArmor = null;

        inventory.Remove(selectedItem);
        gold += (selectedItem.Price * 85) / 100;
        Console.WriteLine($"{selectedItem.Name}을(를) {(selectedItem.Price * 85) / 100}G에 판매했습니다!");
        Thread.Sleep(1500);
        storeshopping();
    }

    static void enterdungeon()
    {
        Console.Clear();
        Console.WriteLine("던전입장");
        Console.WriteLine("입장할 던전의 난이도를 선택하세요:");
        Console.WriteLine("1. 쉬움 (Easy) | 방어력 5 이상 권장");
        Console.WriteLine("2. 보통 (Normal) | 방어력 11 이상 권장");
        Console.WriteLine("3. 어려움 (Hard) | 방어력 17 이상 권장");
        Console.WriteLine("0. 나가기");

        string choice = Console.ReadLine();
        int cho = int.Parse(choice);

        switch (cho)
        {
            case 1:
                dungeonbattle("쉬움", 5, 1000, 50);
                break;
            case 2:
                dungeonbattle("보통", 10, 1700, 120);
                break;
            case 3:
                dungeonbattle("어려움", 20, 2500, 270);
                break;
            case 0:
                backtovil();
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1500);
                backtovil();
                break;

        }
    }

    static void dungeonbattle(string difficulty, int recommendDEF, int rewardgold, int rewardexp)
    {
        Console.Clear();
        Console.WriteLine($"{difficulty} 난이도의 던전에 입장했습니다!");
        Thread.Sleep(1000);

        float totalATK = atk;
        int totalDEF = def;
        int beforehp = hp;
        int beforegold = gold;

        if (equippedWeapon != null && equippedWeapon.itemPower.Contains("공격력"))
            totalATK += int.Parse(equippedWeapon.itemPower.Split('+')[1]);

        if (equippedArmor != null && equippedArmor.itemPower.Contains("방어력"))
            totalDEF += int.Parse(equippedArmor.itemPower.Split('+')[1]);

        if (totalDEF < recommendDEF)
        {
            int fortune = rand.Next(0, 100);
            if (fortune == 40 || fortune < 40)
            {
                Console.WriteLine("당신은 전투에서 패배했습니다...");
                int beforedhp = hp;                
                hp -= hp / 2;
                Console.WriteLine($"체력이 반감되었습니다. {beforedhp} => {hp}");
                Thread.Sleep(2000);
                backtovil();
                return;

            }            
        }

        int rewardATK = Convert.ToInt32(totalATK);

        int damageTaken = rand.Next(20, 36) - (totalDEF - recommendDEF);
        hp -= damageTaken;

        if (0 >= hp)
        {
            Console.WriteLine("당신은 죽었습니다....");
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        rewardgold += rewardgold * (rand.Next(rewardATK, rewardATK * 2) / 100);

        gold += rewardgold;

        Console.WriteLine($"전투에서 승리했습니다! {rewardgold} G를 획득했습니다!");
        Console.WriteLine($"축하합니다! {difficulty} 난이도의 던전을 클리어 했습니다.");
        Console.WriteLine($"남은 체력: {beforehp} => {hp}");
        Console.WriteLine($"보유한 골드 : {beforegold} => {gold}");
        int beforexp = exp;
        exp += rewardexp;
        Console.WriteLine($"현재 경험치 : {beforexp} / {maxexp} => {exp} / {maxexp}");
        if (exp >= maxexp)
        {
            int beforelevel = level;
            level++;
            atk += 0.5f;
            def += 1;
            exp -= maxexp;
            maxexp += (int)(maxexp * 0.8f);
            Console.WriteLine($"레벨 업! Lv. {beforelevel} => Lv. {level}");
        }
        Console.WriteLine("계속하려면 아무 키나 누르세요...");
        Console.ReadKey();
        backtovil();
    }

    static void savegame()
    {
        using (StreamWriter writer = new StreamWriter("save.txt"))
        {
            writer.WriteLine(level);
            writer.WriteLine(atk);
            writer.WriteLine(def);
            writer.WriteLine(hp);
            writer.WriteLine(gold);
            writer.WriteLine(exp);
            writer.WriteLine(maxexp);
            writer.WriteLine(nickname);
            writer.WriteLine(job);

            writer.WriteLine(equippedWeapon != null ? equippedWeapon.Name : "null");
            writer.WriteLine(equippedArmor != null ? equippedArmor.Name : "null");

            writer.WriteLine(inventory.Count);
            foreach (var item in inventory)
            {
                writer.WriteLine(item.Name);
            }
        }

        Console.WriteLine("게임이 저장되었습니다.");
        Thread.Sleep(1000);
    }

    static void loadgame()
    {
        if (!File.Exists("save.txt"))
        {
            Console.WriteLine("저장된 게임이 없습니다.");
            Thread.Sleep(1000);
            return;
        }

        using (StreamReader reader = new StreamReader("save.txt"))
        {
            level = int.Parse(reader.ReadLine());
            atk = float.Parse(reader.ReadLine());
            def = int.Parse(reader.ReadLine());
            hp = int.Parse(reader.ReadLine());
            gold = int.Parse(reader.ReadLine());
            exp = int.Parse(reader.ReadLine());
            maxexp = int.Parse(reader.ReadLine());
            nickname = reader.ReadLine();
            job = reader.ReadLine();

            string weaponName = reader.ReadLine();
            string armorName = reader.ReadLine();

            int invCount = int.Parse(reader.ReadLine());
            inventory.Clear();
            for (int i = 0; i < invCount; i++)
            {
                string itemName = reader.ReadLine();
                var item = storeItems.Find(x => x.Name == itemName);
                if (item != null) inventory.Add(item);
            }

            equippedWeapon = inventory.Find(x => x.Name == weaponName);
             equippedArmor = inventory.Find(x => x.Name == armorName);
         }
 
         Console.WriteLine("게임을 불러왔습니다.");
         Thread.Sleep(1000);
     }
 }