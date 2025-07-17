using NewTxtRPG.Entitys;
using NewTxtRPG.etc;

namespace NewTxtRPG.Scene
{
    internal class VillageScene
    {
        public void StartVillageScene()
        {
            while (true)
            {
                Console.Clear(); // 선택 후 콘솔 전체 지우기
                RenderConsole.WriteLineWithSpacing("\x1b[1m\x1b[38;5;208m마을\x1b[0m");
                RenderConsole.WriteLineWithSpacing("무엇을 하시겠습니까?");
                RenderConsole.WriteLineWithSpacing("1. 인벤토리");
                RenderConsole.WriteLineWithSpacing("2. 여관 이용하기");
                RenderConsole.WriteLineWithSpacing("3. 상점 이용하기");
                RenderConsole.WriteLineWithSpacing("0. 마을 나가기");

                Console.Write("선택: ");
                string input = Console.ReadLine();

                Console.Clear(); // 선택 후 콘솔 전체 지우기

                switch (input)
                {
                    case "1":
                        UseInventory();
                        break;
                    case "2":
                        UseInn();
                        break;
                    case "3":
                        UseShop();
                        break;
                    case "0":
                        QuitVillage();
                        return;
                    default:
                        RenderConsole.WriteLineWithSpacing("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }
        }
        private void UseInventory()
        {
            Player.Inventory.ShowAndEquip();
        }

        private void UseInn()
        {
            if (Player.Gold < 100)
            {
                RenderConsole.WriteLine("골드가 부족하여 휴식할 수 없습니다.");
                Console.ReadLine();
                return;
            }
            while (true)
            {
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
                    RenderConsole.WriteLine("마을로 돌아갑니다.");
                    break;
                }
                else if (input?.Trim().ToUpper() == "0")
                {
                    RenderConsole.WriteLine("휴식을 취하지 않습니다.");
                    RenderConsole.WriteLine("마을로 돌아갑니다.");
                    break;
                }
                else if (input?.Trim().ToUpper() == "18")
                {
                    RenderConsole.WriteLine("여관 주인 : 왜 욕하세요? 고소할게요.", ConsoleColor.Yellow);
                    RenderConsole.WriteLine("합의금으로 100G를 줬다..", ConsoleColor.DarkRed);
                    Player.Gold -= 100;
                    break;
                }
                Console.Clear();
            }
            Thread.Sleep(2000); // 대기
        }
        private void UseShop()
        {
            while (true)
            {
                Console.Clear(); // 선택 후 콘솔 전체 지우기
                RenderConsole.WriteLineWithSpacing("이용할 상점을 선택 해 주십시오.");
                RenderConsole.WriteLineWithSpacing("1. 장비 상점");
                RenderConsole.WriteLineWithSpacing("2. 포션 상점");
                RenderConsole.WriteLineWithSpacing("0. 상점 나가기");
                RenderConsole.Write("선택: ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    RenderConsole.WriteLineWithSpacing("상점을 나갑니다.");
                    break;
                }
                else if (input == "1")
                {
                    EquipmentShop shopScene = new EquipmentShop();
                    shopScene.ShowShopItems();
                    while (true)
                    {
                        RenderConsole.Write("구매할 아이템 번호를 입력하세요 (0 입력 시 나가기): ");
                        string equipInput = Console.ReadLine();
                        if (equipInput == "0")
                        {
                            RenderConsole.WriteLineWithSpacing("상점을 나갑니다.");
                            break;
                        }
                        if (int.TryParse(equipInput, out int itemIndex) && itemIndex > 0)
                        {
                            shopScene.BuyItem(itemIndex - 1);
                        }
                        else
                        {
                            RenderConsole.WriteLineWithSpacing("잘못된 입력입니다. 다시 시도하세요.");
                        }
                    }
                }
                else if (input == "2")
                {
                    ConsumablesShop potionShop = new ConsumablesShop();
                    potionShop.ShowShop();
                }
                else
                {
                    RenderConsole.WriteLineWithSpacing("잘못된 입력입니다. 다시 시도하세요.");
                }
            }
        }

        private void QuitVillage()
        {
            RenderConsole.WriteLineWithSpacing("마을을 나갑니다.");
        }
    }
}
