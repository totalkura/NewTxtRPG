using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STDungeon.Structs;

namespace STDungeon
{
    internal class Inventory
    {
        private List<ItemInfo> items;
        private HashSet<string> equippedItems; // 장착된 아이템 이름 저장

        public Inventory()
        {
            items = new List<ItemInfo>();
            equippedItems = new HashSet<string>();
        }

        public void AddItem(ItemInfo item)
        {
            items.Add(item);
        }

        public bool RemoveItem(ItemInfo item)
        {
            return items.Remove(item);
        }

        public IReadOnlyList<ItemInfo> GetItems()
        {
            return items.AsReadOnly();
        }

        public int Count => items.Count;

        // 인벤토리 출력 및 장착/해제 기능
        public void ShowAndEquip(Player player)
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("인벤토리:");
                if (items.Count == 0)
                {
                    RenderConsole.WriteLine("  (비어 있음)");
                    RenderConsole.WriteLine("계속하려면 Enter를 누르세요...");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    string statText = item.AttackBonus > 0
                        ? $"공격력 상승: {item.AttackBonus}"
                        : item.DefenseBonus > 0
                            ? $"방어력 상승: {item.DefenseBonus}"
                            : "";
                    string equippedText = equippedItems.Contains(item.Name) ? " (장착됨)" : "";
                    RenderConsole.WriteLine($"  {i + 1}. {item.Name} (가격: {item.Price}, {statText}){equippedText}");
                }

                RenderConsole.WriteLine("아이템 번호를 입력하면 장착/해제할 수 있습니다. (0 입력 시 나가기)");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                int idx;
                if (int.TryParse(input, out idx) && idx >= 1 && idx <= items.Count)
                {
                    var item = items[idx - 1];

                    // 공격력/방어력 아이템 중복 장착 방지
                    if (item.AttackBonus > 0)
                    {
                        // 이미 공격력 아이템이 장착되어 있다면 해제
                        var equippedAttackItem = items.FirstOrDefault(x => x.AttackBonus > 0 && equippedItems.Contains(x.Name));
                        if (equippedAttackItem.Name != null)
                        {
                            player.ItemAttackBonus -= equippedAttackItem.AttackBonus;
                            equippedItems.Remove(equippedAttackItem.Name);
                            RenderConsole.WriteLine($"{equippedAttackItem.Name}을(를) 해제했습니다. (공격력 아이템은 하나만 장착 가능)");
                        }
                        // 장착
                        player.ItemAttackBonus += item.AttackBonus;
                        equippedItems.Add(item.Name);
                        RenderConsole.WriteLine($"{item.Name}을(를) 장착했습니다.");
                    }
                    else if (item.DefenseBonus > 0)
                    {
                        // 이미 방어력 아이템이 장착되어 있다면 해제
                        var equippedDefenseItem = items.FirstOrDefault(x => x.DefenseBonus > 0 && equippedItems.Contains(x.Name));
                        if (equippedDefenseItem.Name != null)
                        {
                            player.ItemDefenseBonus -= equippedDefenseItem.DefenseBonus;
                            equippedItems.Remove(equippedDefenseItem.Name);
                            RenderConsole.WriteLine($"{equippedDefenseItem.Name}을(를) 해제했습니다. (방어력 아이템은 하나만 장착 가능)");
                        }
                        // 장착
                        player.ItemDefenseBonus += item.DefenseBonus;
                        equippedItems.Add(item.Name);
                        RenderConsole.WriteLine($"{item.Name}을(를) 장착했습니다.");
                    }
                    else
                    {
                        // 기타 아이템(공격력/방어력 보너스가 없는 경우) 장착/해제
                        if (!equippedItems.Contains(item.Name))
                        {
                            equippedItems.Add(item.Name);
                            RenderConsole.WriteLine($"{item.Name}을(를) 장착했습니다.");
                        }
                        else
                        {
                            equippedItems.Remove(item.Name);
                            RenderConsole.WriteLine($"{item.Name}을(를) 해제했습니다.");
                        }
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
            foreach (var item in items)
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
