using System;
using System.Linq;
using NewTxtRPG.Entitys;
using NewTxtRPG.etc;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Scene
{
    internal class ConsumablesShop
    {
        // 샵에서 판매하는 소모품 목록
        private static readonly ItemInfo[] ShopItems = Items.ItemList
            .Where(item => item.ItemType == ItemType.Consumables && item.Price > 0)
            .ToArray();

        // 플레이어가 해당 포션을 몇 개 가지고 있는지 반환
        private int GetPlayerConsumableCount(string itemName)
        {
            return Player.Inventory.GetConsumables()
                .Count(x => x.ItemType == ItemType.Consumables && x.Name == itemName);
        }

        public void ShowShop()
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("=== 포션 상점 ===", ConsoleColor.Blue);
                for (int i = 0; i < ShopItems.Length; i++)
                {
                    var item = ShopItems[i];
                    int owned = GetPlayerConsumableCount(item.Name);
                    Console.WriteLine($"{i + 1}. {item.Name} (가격: {item.Price}, 설명: {item.Description}) [보유: {owned}/3]");
                }
                Console.WriteLine("구매할 포션 번호를 입력하세요. (0 입력 시 나가기)");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                int idx;
                if (int.TryParse(input, out idx) && idx >= 1 && idx <= ShopItems.Length)
                {
                    var item = ShopItems[idx - 1];
                    int owned = GetPlayerConsumableCount(item.Name);
                    if (owned >= 3)
                    {
                        Console.WriteLine($"이미 {item.Name}을(를) 최대 3개까지 보유하고 있습니다.");
                    }
                    else if (Player.Gold < item.Price)
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                    else
                    {
                        Player.Gold -= item.Price;
                        Player.Inventory.AddItem(item);
                        Console.WriteLine($"{item.Name}을(를) 구매했습니다! [보유: {owned + 1}/3]");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                }
                Console.WriteLine("계속하려면 Enter를 누르세요...");
                Console.ReadLine();
            }
            Console.Clear();
        }
    }
}
