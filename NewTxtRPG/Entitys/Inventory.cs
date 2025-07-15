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
            Consumables = new List<ItemInfo>();
            equippedItems = new HashSet<string>();
        }

        public void AddItem(ItemInfo item)
        {
            Equipment.Add(item);
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

        public int Count => Equipment.Count + Consumables.Count;

        // �κ��丮 ��� �� ����/���� ���
        public void ShowAndEquip(Player player)
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("�κ��丮:");
                if (Equipment.Count + Consumables.Count == 0)
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

                // �Ҹ�ǰ�� ���� ������ �ڿ� ���
                for (int i = 0; i < Consumables.Count; i++)
                {
                    var potion = Consumables[i];
                    string recoverText = potion.HpBonus > 0
                        ? $"ü�� ȸ��: {potion.HpBonus}"
                        : potion.MpBonus > 0
                            ? $"���� ���: {potion.MpBonus}"
                            : "";
                    RenderConsole.WriteLine($"  {i + 1 + Equipment.Count}. {potion.Name} (����: {potion.Price}, {recoverText})");
                }

                RenderConsole.WriteLine("������ ��ȣ�� �Է��ϸ� ����/����, �Ǵ� �Һ��� �� �ֽ��ϴ�. (0 �Է� �� ������)");
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
                else if (idx > Equipment.Count && idx <= Equipment.Count + Consumables.Count)
                {
                    var potion = Consumables[idx - Equipment.Count - 1];

                    bool used = false;

                    if (potion.HpBonus > 0)
                    {
                        player.CurrentHp = Math.Min(player.MaxHp, player.CurrentHp + potion.HpBonus);
                        RenderConsole.WriteLine($"{potion.Name}��(��) ����Ͽ� ü���� {potion.HpBonus} ȸ���߽��ϴ�.");
                        used = true;
                    }
                    else if (potion.MpBonus > 0)
                    {
                        player.CurrentMp = Math.Min(player.MaxMp, player.CurrentMp + potion.MpBonus);
                        RenderConsole.WriteLine($"{potion.Name}��(��) ����Ͽ� ������ {potion.MpBonus} ȸ���߽��ϴ�.");
                        used = true;
                    }
                    else
                    {
                        RenderConsole.WriteLine($"{potion.Name}��(��) ����� �� ���� �������Դϴ�.");
                    }

                    if (used)
                    {
                        Consumables.Remove(potion);
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
