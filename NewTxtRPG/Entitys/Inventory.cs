using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewTxtRPG.etc;
using STDungeon.Structs;

namespace STDungeon
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

        // �κ��丮 ��� �� ����/���� ���
        public void ShowAndEquip(Player player)
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

                    // ���ݷ�/���� ������ �ߺ� ���� ����
                    if (item.AttackBonus > 0)
                    {
                        // �̹� ���ݷ� �������� �����Ǿ� �ִٸ� ����
                        var equippedAttackItem = Equipment.FirstOrDefault(x => x.AttackBonus > 0 && equippedItems.Contains(x.Name));
                        if (equippedAttackItem.Name != null)
                        {
                            player.ItemAttackBonus -= equippedAttackItem.AttackBonus;
                            equippedItems.Remove(equippedAttackItem.Name);
                            RenderConsole.WriteLine($"{equippedAttackItem.Name}��(��) �����߽��ϴ�. (���ݷ� �������� �ϳ��� ���� ����)");
                        }
                        // ����
                        player.ItemAttackBonus += item.AttackBonus;
                        equippedItems.Add(item.Name);
                        RenderConsole.WriteLine($"{item.Name}��(��) �����߽��ϴ�.");
                    }
                    else if (item.DefenseBonus > 0)
                    {
                        // �̹� ���� �������� �����Ǿ� �ִٸ� ����
                        var equippedDefenseItem = Equipment.FirstOrDefault(x => x.DefenseBonus > 0 && equippedItems.Contains(x.Name));
                        if (equippedDefenseItem.Name != null)
                        {
                            player.ItemDefenseBonus -= equippedDefenseItem.DefenseBonus;
                            equippedItems.Remove(equippedDefenseItem.Name);
                            RenderConsole.WriteLine($"{equippedDefenseItem.Name}��(��) �����߽��ϴ�. (���� �������� �ϳ��� ���� ����)");
                        }
                        // ����
                        player.ItemDefenseBonus += item.DefenseBonus;
                        equippedItems.Add(item.Name);
                        RenderConsole.WriteLine($"{item.Name}��(��) �����߽��ϴ�.");
                    }
                    else
                    {
                        // ��Ÿ ������(���ݷ�/���� ���ʽ��� ���� ���) ����/����
                        if (!equippedItems.Contains(item.Name))
                        {
                            equippedItems.Add(item.Name);
                            RenderConsole.WriteLine($"{item.Name}��(��) �����߽��ϴ�.");
                        }
                        else
                        {
                            equippedItems.Remove(item.Name);
                            RenderConsole.WriteLine($"{item.Name}��(��) �����߽��ϴ�.");
                        }
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
