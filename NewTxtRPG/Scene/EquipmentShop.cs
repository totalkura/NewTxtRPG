using NewTxtRPG.Entitys;
using NewTxtRPG.etc;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Scene
{
    internal class EquipmentShop
    {
        //이거는 하드코딩으로 지정해주는게 맞는듯 판매 가능한 물품을 지정하는거니까
        private List<ItemInfo> itemsForSale;

        public EquipmentShop()
        {
            // 판매 가능한 아이템 목록 초기화 (장비만, 구매완료 아이템은 제외)
            itemsForSale = Items.ItemList
                .Where(item => item.ItemType == ItemType.Equipment && item.Price > 0)
                .Where(item => item.Id != 20)
                .Where(item => item.Id != 21)
                .ToList();
        }


        public void ShowShopItems()
        {
            Console.Clear();
            RenderConsole.WriteLineWithSpacing("=== 장비 상점 ===", ConsoleColor.DarkCyan);
            for (int i = 0; i < itemsForSale.Count; i++)
            {
                var item = itemsForSale[i];
                string statText = item.AttackBonus > 0
                    ? $"공격력 +{item.AttackBonus}"
                    : item.DefenseBonus > 0
                        ? $"방어력 +{item.DefenseBonus}"
                        : "";

                // 인벤토리에 이미 있는지 확인
                bool alreadyOwned = Player.Inventory.GetEquipment().Any(invItem => invItem.Name == item.Name);

                string ownText = alreadyOwned ? " (이미 보유)" : "";
                var color = alreadyOwned ? ConsoleColor.DarkGray : ConsoleColor.White;
                RenderConsole.WriteLineWithSpacing($"{i + 1}. {item.Name} | {statText} | {item.Description} | {item.Price} G{ownText}", color);
            }
        }


        // 아이템 구매
        public void BuyItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= itemsForSale.Count)
            {
                RenderConsole.WriteLine("잘못된 번호입니다.");
                return;
            }

            var item = itemsForSale[itemIndex];

            // 인벤토리에 이미 있는지 확인
            if (Player.Inventory.GetEquipment().Any(invItem => invItem.Name == item.Name))
            {
                RenderConsole.WriteLine("이미 보유한 아이템입니다.");
                return;
            }

            if (Player.Gold < item.Price)
            {
                RenderConsole.WriteLine("골드가 부족합니다.");
                return;
            }

            Player.Gold -= item.Price;
            Player.Inventory.AddItem(item);
            RenderConsole.WriteLine($"{item.Name}을(를) 구매하였습니다!");
        }
        // 아이템 판매
        public void SellItem()
        {
            var items = Player.Inventory.GetEquipment();
            if (items.Count == 0)
            {
                RenderConsole.WriteLine("판매할 아이템이 없습니다.");
                return;
            }

            RenderConsole.WriteLine("판매할 아이템을 선택하세요:");
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                int sellPrice = (int)(item.Price * 0.8);
                RenderConsole.WriteLine($"{i + 1}. {item.Name} (판매가: {sellPrice} G, 원가: {item.Price} G)");
            }
            RenderConsole.WriteLine("0. 취소");

            Console.Write("선택: ");
            string input = Console.ReadLine();
            if (input == "0")
                return;

            int idx;
            if (int.TryParse(input, out idx) && idx >= 1 && idx <= items.Count)
            {
                var item = items[idx - 1];
                int sellPrice = (int)(item.Price * 0.8);

                // 장착 여부 확인
                var equipField = typeof(Inventory).GetField("equippedItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var equippedSet = equipField?.GetValue(Player.Inventory) as HashSet<string>;
                bool isEquipped = equippedSet != null && equippedSet.Contains(item.Name);

                if (isEquipped)
                {
                    RenderConsole.WriteLine("장착되어있는 아이템입니다. 정말 판매하시겠습니까?");
                    RenderConsole.WriteLine("1. 판매한다");
                    RenderConsole.WriteLine("0. 판매하지 않는다");
                    Console.Write("선택: ");
                    string confirm = Console.ReadLine();
                    if (confirm != "1")
                    {
                        RenderConsole.WriteLine("판매를 취소했습니다.");
                        return;
                    }
                    // 장착 해제 및 능력치 반영
                    equippedSet.Remove(item.Name);
                    if (item.AttackBonus > 0)
                        Player.ItemAttackBonus -= item.AttackBonus;
                    if (item.DefenseBonus > 0)
                        Player.ItemDefenseBonus -= item.DefenseBonus;
                }

                Player.Gold += sellPrice;
                Player.Inventory.RemoveItem(item);
                RenderConsole.WriteLine($"{item.Name}을(를) 판매하였습니다! {sellPrice} G를 획득했습니다.");
            }
            else
            {
                RenderConsole.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
