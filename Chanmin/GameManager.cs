using STDungeon.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STDungeon
{
    internal class GameManager
    {
        public Player Player { get; private set; }

        private int beginningGold = 300; // 게임 시작 시 플레이어가 보유한 골드

        // 게임 초기화
        public void InitializeGame()
        {
            Console.Write("플레이어 이름을 입력하세요: ");
            string name = Console.ReadLine();

            RenderConsole.WriteLine("직업을 선택하세요:");
            RenderConsole.WriteLine("1. 전사");
            RenderConsole.WriteLine("   - 공격력: 10, 방어력: 5, 체력: 100, 마나: 30");
            RenderConsole.WriteLine("   - 설명: 튼튼한 방어와 무난한 공격력을 가진 근접 전투 전문가");
            RenderConsole.WriteLine("   - 초기 장비: 가죽 튜닉");
            RenderConsole.WriteLine("2. 도적");
            RenderConsole.WriteLine("   - 공격력: 14, 방어력: 3, 체력: 100, 마나: 40");
            RenderConsole.WriteLine("   - 설명: 빠른 공격과 높은 기동성을 가진 치명적인 암살자");
            RenderConsole.WriteLine("   - 초기 장비: 초보자의 검");

            IJob selectedJob = null;
            ItemInfo? initialItem = null;
            while (selectedJob == null)
            {
                Console.Write("번호를 입력하세요 (1 또는 2): ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    selectedJob = new WarriorJob();
                    initialItem = Items.ItemList.FirstOrDefault(x => x.Name == "가죽 튜닉");
                }
                else if (input == "2")
                {
                    selectedJob = new ThiefJob();
                    initialItem = Items.ItemList.FirstOrDefault(x => x.Name == "초보자의 검");
                }
                else
                {
                    RenderConsole.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                }
            }

            Player = new Player(name, beginningGold, selectedJob);
            if (initialItem.HasValue)
            {
                Player.Inventory.AddItem(initialItem.Value);
                // 바로 장착
                if (initialItem.Value.AttackBonus > 0)
                    Player.ItemAttackBonus += initialItem.Value.AttackBonus;
                if (initialItem.Value.DefenseBonus > 0)
                    Player.ItemDefenseBonus += initialItem.Value.DefenseBonus;
                // 장착 상태로 표시
                var equipField = typeof(Inventory).GetField("equippedItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var equippedSet = equipField?.GetValue(Player.Inventory) as HashSet<string>;
                equippedSet?.Add(initialItem.Value.Name);
            }
            RenderConsole.WriteLine($"{name}님, {(selectedJob is WarriorJob ? "전사" : "도적")}로 게임을 시작합니다!");

            Thread.Sleep(1000); // 1초 대기
            Console.Clear();    // 로그 클리어
        }

        // 게임 시작
        public void StartGame()
        {
            RenderConsole.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            RenderConsole.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            while (true)
            {
                RenderConsole.WriteLine("\x1b[1m\x1b[38;5;208m마을\x1b[0m");
                RenderConsole.WriteEmptyLine();
                RenderConsole.WriteLine("무엇을 하시겠습니까?");
                RenderConsole.WriteLine("1. 상태보기");
                RenderConsole.WriteLine("2. 인벤토리");
                RenderConsole.WriteLine("3. 상점");
                RenderConsole.WriteLine("4. 휴식 (100G, 체력 회복)");
                RenderConsole.WriteLine("0. 게임 종료");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                Console.Clear(); // 선택 후 콘솔 전체 지우기

                switch (input)
                {
                    case "1":
                        ShowPlayerStatus();
                        break;
                    case "2":
                        ShowInventory();
                        break;
                    case "3":
                        ShowShop();
                        break;
                    case "4":
                        Rest();
                        break;
                    case "0":
                        EndGame();
                        return;
                    default:
                        RenderConsole.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }
        }

        // 상점 출력 및 구매/판매 기능
        public void ShowShop()
        {
            Shop shop = new Shop();

            while (true)
            {
                Console.Clear();
                shop.ShowShopItems(Player);
                RenderConsole.WriteLine($"보유 골드: {Player.Gold}");
                RenderConsole.WriteLine("상점에서 할 행동을 선택하세요.");
                RenderConsole.WriteLine("1. 구매하기");
                RenderConsole.WriteLine("2. 판매하기");
                RenderConsole.WriteLine("0. 상점 나가기");
                Console.Write("선택: ");
                string actionInput = Console.ReadLine();

                if (actionInput == "0")
                    break;

                if (actionInput == "1")
                {
                    RenderConsole.WriteLine("구매할 아이템 번호를 입력하세요. (0 입력 시 취소)");
                    Console.Write("선택: ");
                    string input = Console.ReadLine();

                    if (input == "0")
                        continue;

                    int itemIndex;
                    if (int.TryParse(input, out itemIndex))
                    {
                        itemIndex -= 1; // 배열 인덱스 보정
                        shop.BuyItem(Player, itemIndex);
                    }
                    else
                    {
                        RenderConsole.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                    }
                }
                else if (actionInput == "2")
                {
                    shop.SellItem(Player);
                }
                else
                {
                    RenderConsole.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                }

                RenderConsole.WriteLine("계속하려면 Enter를 누르세요...");
                Console.ReadLine();
            }
            Console.Clear();
        }




        // 플레이어 상태 출력
        public void ShowPlayerStatus()
        {
            RenderConsole.WriteLine($"이름: {Player.Name}");
            RenderConsole.WriteLine($"체력: {Player.CurrentHP} / {Player.Stat.MaxHP}");
            RenderConsole.WriteLine($"마나: {Player.CurrentMP} / {Player.Stat.MaxMP}");

            // 아이템 추가 공격력/방어력이 있을 때만 괄호로 표시
            string attackText = Player.ItemAttackBonus > 0
                ? $" (+{Player.ItemAttackBonus})"
                : "";
            string defenseText = Player.ItemDefenseBonus > 0
                ? $" (+{Player.ItemDefenseBonus})"
                : "";

            RenderConsole.WriteLine($"공격력: {Player.Stat.Attack + Player.ItemAttackBonus}{attackText}");
            RenderConsole.WriteLine($"방어력: {Player.Stat.Defense + Player.ItemDefenseBonus}{defenseText}");
            RenderConsole.WriteLine($"골드: {Player.Gold}");

            if (Player != null && Player.Job != null)
            {
                RenderConsole.WriteLine("스킬 목록:");
                RenderConsole.WriteLine($"  1. {Player.Job.Skill1.Name} - {Player.Job.Skill1.Effect} (배수: {Player.Job.Skill1.Multiplier}, 마나 소모: {Player.Job.Skill1.ManaCost})");
                RenderConsole.WriteLine($"  2. {Player.Job.Skill2.Name} - {Player.Job.Skill2.Effect} (배수: {Player.Job.Skill2.Multiplier}, 마나 소모: {Player.Job.Skill2.ManaCost})");
            }

            RenderConsole.WriteLine("계속하려면 Enter를 누르세요...");
            Console.ReadLine();
            Console.Clear();
        }


        // 인벤토리 출력 및 장착/해제
        public void ShowInventory()
        {
            Player.Inventory.ShowAndEquip(Player);
        }

        // 휴식 기능: 100G를 내면 체력을 최대치로 회복 (확인 절차 추가)
        public void Rest()
        {
            if (Player.Gold < 100)
            {
                RenderConsole.WriteLine("골드가 부족하여 휴식할 수 없습니다.");
                return;
            }

            RenderConsole.WriteLine($"휴식에는 100골드가 필요합니다. 휴식하시겠습니까? (보유 골드 : {Player.Gold} + )");
            RenderConsole.WriteLine("1. 휴식하기");
            RenderConsole.WriteLine("0. 나가기");
            Console.Write("선택: ");
            string input = Console.ReadLine();
            if (input?.Trim().ToUpper() == "1")
            {
                Player.Gold -= 100;
                Player.CurrentHP = Player.Stat.MaxHP;
                RenderConsole.WriteLine("휴식을 취했습니다! 체력이 모두 회복되었습니다.");
            }
            else
            {
                RenderConsole.WriteLine("마을로 돌아갑니다.");
            }
            Console.Clear();
        }


        // 전투 시작 (예시)
        public void StartBattle()
        {
            RenderConsole.WriteLine("전투가 시작됩니다! (전투 로직은 추후 구현)");
        }

        // 아이템 사용 (예시)
        public void UseItem()
        {
            RenderConsole.WriteLine("사용할 아이템 번호를 입력하세요:");
            var items = Player.Inventory.GetItems();
            for (int i = 0; i < items.Count; i++)
            {
                RenderConsole.WriteLine($"{i + 1}. {items[i].Name}");
            }
            // 실제 사용 로직은 추후 구현
        }

        // 게임 종료
        public void EndGame()
        {
            RenderConsole.WriteLine("게임을 종료합니다.");
        }

        // 저장 기능 (예시)
        public void SaveGame()
        {
            RenderConsole.WriteLine("게임 저장 기능은 미구현 입니다.");
        }

        // 불러오기 기능 (예시)
        public void LoadGame()
        {
            RenderConsole.WriteLine("게임 불러오기 기능은 미구현 입니다.");
        }
    }
}
