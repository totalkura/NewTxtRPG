using NewTxtRPG.etc;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{
    internal class Inventory
    {
        private List<ItemInfo> Equipment; // 장비 목록
        private List<ItemInfo> Consumables; // 소모품 목록
        private HashSet<string> equippedItems; // 장착된 아이템 이름 저장

        public Inventory()
        {
            Equipment = new List<ItemInfo>();
            Consumables = new List<ItemInfo>();
            equippedItems = new HashSet<string>();
        }

        public void AddItem(ItemInfo item)
        {
            if (item.ItemType == ItemType.Equipment)
                Equipment.Add(item);
            else if (item.ItemType == ItemType.Consumables)
                Consumables.Add(item);
        }

        public bool RemoveItem(ItemInfo item)
        {
            bool removedFromEquipment = Equipment.Remove(item);
            bool removedFromConsumables = Consumables.Remove(item);
            return removedFromEquipment || removedFromConsumables;
        }

        public IReadOnlyList<ItemInfo> GetItems()
        {
            return Equipment.AsReadOnly();
        }
        public IReadOnlyList<ItemInfo> GetConsumables()
        {
            return Consumables.AsReadOnly();
        }

        public int Count => Equipment.Count + Consumables.Count;

        // 아이템 장착
        private void EquipItem(ItemInfo item)
        {
            if (item.AttackBonus > 0)
            {
                var equippedAttackItem = Equipment.FirstOrDefault(x => x.AttackBonus > 0 && equippedItems.Contains(x.Name));
                if (equippedAttackItem.Name != null)
                {
                    UnequipItem(equippedAttackItem);
                    RenderConsole.WriteLine($"{equippedAttackItem.Name}을(를) 해제했습니다. (공격력 아이템은 하나만 장착 가능)");
                }
                Player.ItemAttackBonus += item.AttackBonus;
            }
            else if (item.DefenseBonus > 0)
            {
                var equippedDefenseItem = Equipment.FirstOrDefault(x => x.DefenseBonus > 0 && equippedItems.Contains(x.Name));
                if (equippedDefenseItem.Name != null)
                {
                    UnequipItem(equippedDefenseItem);
                    RenderConsole.WriteLine($"{equippedDefenseItem.Name}을(를) 해제했습니다. (방어력 아이템은 하나만 장착 가능)");
                }
                Player.ItemDefenseBonus += item.DefenseBonus;
            }
            equippedItems.Add(item.Name);
            RenderConsole.WriteLine($"{item.Name}을(를) 장착했습니다.");
        }

        // 아이템 해제
        private void UnequipItem(ItemInfo item)
        {
            if (item.AttackBonus > 0)
            {
                Player.ItemAttackBonus -= item.AttackBonus;
            }
            else if (item.DefenseBonus > 0)
            {
                Player.ItemDefenseBonus -= item.DefenseBonus;
            }
            equippedItems.Remove(item.Name);
            RenderConsole.WriteLine($"{item.Name}을(를) 해제했습니다.");
        }

        // 인벤토리 출력 및 장착/해제 기능
        public void ShowAndEquip()
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("인벤토리:");
                if (Equipment.Count + Consumables.Count == 0)
                {
                    RenderConsole.WriteLine("  (비어 있음)");
                    RenderConsole.WriteLine("계속하려면 Enter를 누르세요...");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < Equipment.Count; i++)
                {
                    var item = Equipment[i];
                    string statText = item.AttackBonus > 0
                        ? $"공격력 상승: {item.AttackBonus}"
                        : item.DefenseBonus > 0
                            ? $"방어력 상승: {item.DefenseBonus}"
                            : "";
                    string equippedText = equippedItems.Contains(item.Name) ? " (장착됨)" : "";
                    RenderConsole.WriteLine($"  {i + 1}. {item.Name} (가격: {item.Price}, {statText}){equippedText}");
                }

                // 소모품은 장착 아이템 뒤에 출력
                for (int i = 0; i < Consumables.Count; i++)
                {
                    var potion = Consumables[i];
                    string recoverText = potion.HpBonus > 0
                        ? $"체력 회복: {potion.HpBonus}"
                        : potion.MpBonus > 0
                            ? $"방어력 상승: {potion.MpBonus}"
                            : "";
                    RenderConsole.WriteLine($"  {i + 1 + Equipment.Count}. {potion.Name} (가격: {potion.Price}, {recoverText})");
                }

                RenderConsole.WriteLine("아이템 번호를 입력하면 장착/해제, 또는 소비할 수 있습니다. (0 입력 시 나가기)");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                int idx;
                if (int.TryParse(input, out idx) && idx >= 1 && idx <= Equipment.Count)
                {
                    var item = Equipment[idx - 1];
                    if (!equippedItems.Contains(item.Name))
                    {
                        EquipItem(item);
                    }
                    else
                    {
                        UnequipItem(item);
                    }
                }
                else if (idx > Equipment.Count && idx <= Equipment.Count + Consumables.Count)
                {
                    var potion = Consumables[idx - Equipment.Count - 1];

                    bool used = false;

                    if (potion.HpBonus > 0)
                    {
                        player.CurrentHp = Math.Min(player.MaxHp, player.CurrentHp + potion.HpBonus);
                        RenderConsole.WriteLine($"{potion.Name}을(를) 사용하여 체력을 {potion.HpBonus} 회복했습니다.");
                        used = true;
                    }
                    else if (potion.MpBonus > 0)
                    {
                        player.CurrentMp = Math.Min(player.MaxMp, player.CurrentMp + potion.MpBonus);
                        RenderConsole.WriteLine($"{potion.Name}을(를) 사용하여 마나를 {potion.MpBonus} 회복했습니다.");
                        used = true;
                    }
                    else
                    {
                        RenderConsole.WriteLine($"{potion.Name}은(는) 사용할 수 없는 아이템입니다.");
                    }

                    if (used)
                    {
                        Consumables.Remove(potion);
                    }
                }
                else
                {
                    RenderConsole.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                }

                RenderConsole.WriteLine("계속하려면 Enter를 누르세요...");
                Console.ReadLine();
            }
            Console.Clear();
        }

        // 장착된 아이템의 총 보너스 반환
        public (int attackBonus, int defenseBonus) GetEquippedBonus()
        {
            int attack = 0, defense = 0;
            foreach (var item in Equipment)
            {
                if (equippedItems.Contains(item.Name))
                {
                    attack += item.AttackBonus;
                    defense += item.DefenseBonus;
                }
            }
            return (attack, defense);
        }
    }
}

