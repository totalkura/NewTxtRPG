using NewTxtRPG.etc;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{
    internal class Inventory
    {
        private List<ItemInfo> Equipment; // ��� ���
        private List<ItemInfo> Consumables; // �Ҹ�ǰ ���
        private HashSet<string> equippedItems; // ������ ������ �̸� ����

        public Inventory()
        {
            Equipment = new List<ItemInfo>();
            equippedItems = new HashSet<string>();
        }

        public void AddItem(ItemInfo item)
        {
            Equipment.Add(item);
        }

        public bool RemoveItem(ItemInfo item)
        {
            return Equipment.Remove(item);
        }

        public IReadOnlyList<ItemInfo> GetItems()
        {
            return Equipment.AsReadOnly();
        }

        public int Count => Equipment.Count;

        // ������ ����
        private void EquipItem(ItemInfo item)
        {
            if (item.AttackBonus > 0)
            {
                var equippedAttackItem = Equipment.FirstOrDefault(x => x.AttackBonus > 0 && equippedItems.Contains(x.Name));
                if (equippedAttackItem.Name != null)
                {
                    UnequipItem(equippedAttackItem);
                    RenderConsole.WriteLine($"{equippedAttackItem.Name}��(��) �����߽��ϴ�. (���ݷ� �������� �ϳ��� ���� ����)");
                }
                Player.ItemAttackBonus += item.AttackBonus;
            }
            else if (item.DefenseBonus > 0)
            {
                var equippedDefenseItem = Equipment.FirstOrDefault(x => x.DefenseBonus > 0 && equippedItems.Contains(x.Name));
                if (equippedDefenseItem.Name != null)
                {
                    UnequipItem(equippedDefenseItem);
                    RenderConsole.WriteLine($"{equippedDefenseItem.Name}��(��) �����߽��ϴ�. (���� �������� �ϳ��� ���� ����)");
                }
                Player.ItemDefenseBonus += item.DefenseBonus;
            }
            equippedItems.Add(item.Name);
            RenderConsole.WriteLine($"{item.Name}��(��) �����߽��ϴ�.");
        }

        // ������ ����
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
            RenderConsole.WriteLine($"{item.Name}��(��) �����߽��ϴ�.");
        }

        // �κ��丮 ��� �� ����/���� ���
        public void ShowAndEquip()
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("�κ��丮:");
                if (Equipment.Count == 0)
                {
                    RenderConsole.WriteLine("  (��� ����)");
                    RenderConsole.WriteLine("����Ϸ��� Enter�� ��������...");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < Equipment.Count; i++)
                {
                    var item = Equipment[i];
                    string statText = item.AttackBonus > 0
                        ? $"���ݷ� ���: {item.AttackBonus}"
                        : item.DefenseBonus > 0
                            ? $"���� ���: {item.DefenseBonus}"
                            : "";
                    string equippedText = equippedItems.Contains(item.Name) ? " (������)" : "";
                    RenderConsole.WriteLine($"  {i + 1}. {item.Name} (����: {item.Price}, {statText}){equippedText}");
                }

                RenderConsole.WriteLine("������ ��ȣ�� �Է��ϸ� ����/������ �� �ֽ��ϴ�. (0 �Է� �� ������)");
                Console.Write("����: ");
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
                else
                {
                    RenderConsole.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �Է��ϼ���.");
                }

                RenderConsole.WriteLine("����Ϸ��� Enter�� ��������...");
                Console.ReadLine();
            }
            Console.Clear();
        }

        // ������ �������� �� ���ʽ� ��ȯ
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

