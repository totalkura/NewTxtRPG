using NewTxtRPG.etc;
using NewTxtRPG.Structs;
using System.Reflection.Metadata.Ecma335;

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

        public IReadOnlyList<ItemInfo> GetAllItems()
        {
            return Equipment.Concat(Consumables).ToList().AsReadOnly();
        }

        public IReadOnlyList<ItemInfo> GetEquipment()
        {
            return Equipment.AsReadOnly();
        }
        public IReadOnlyList<ItemInfo> GetConsumables()
        {
            return Consumables.AsReadOnly();
        }

        public IEnumerable<string> GetEquippedItemNames()
        {
            return equippedItems.ToList();
        }

        public void SetEquippedItemNames(IEnumerable<string> names)
        {
            equippedItems = new HashSet<string>(names);
        }

        public int Count => Equipment.Count + Consumables.Count;

        // ������ ����
        private void EquipItem(ItemInfo item)
        {
            if (item.AttackBonus > 0)
            {
                var equippedAttackItem = Equipment.FirstOrDefault(x => x.AttackBonus > 0 && equippedItems.Contains(x.Name));
                if (equippedAttackItem.Name != null)
                {
                    UnequipItem(equippedAttackItem);
                    RenderConsole.WriteLineWithSpacing($"(���ݷ� �������� �ϳ��� ���� ����)");
                }
                Player.ItemAttackBonus += item.AttackBonus;
            }
            else if (item.DefenseBonus > 0)
            {
                var equippedDefenseItem = Equipment.FirstOrDefault(x => x.DefenseBonus > 0 && equippedItems.Contains(x.Name));
                if (equippedDefenseItem.Name != null)
                {
                    UnequipItem(equippedDefenseItem);
                    RenderConsole.WriteLineWithSpacing($"(���� �������� �ϳ��� ���� ����)");
                }
                Player.ItemDefenseBonus += item.DefenseBonus;
            }
            equippedItems.Add(item.Name);
            RenderConsole.WriteLineWithSpacing($"{item.Name}��(��) �����߽��ϴ�.");
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
            RenderConsole.WriteLineWithSpacing($"{item.Name}��(��) �����߽��ϴ�.");
        }

        public void ShowConsumablesItem()
        {
            int cursorY = Console.CursorTop;
            
            while (true)
            {
                Console.SetCursorPosition(0, cursorY);
                RenderConsole.WriteLine("����������������������������������������������������������������������������������������������������������������������������������", ConsoleColor.DarkGray);

                RenderConsole.WriteLineWithSpacing($" < ����� �Ҹ�ǰ�� ������ �ּ��� >",ConsoleColor.Yellow);

                for (int i = 0; i < Consumables.Count; i++)
                {
                    var potion = Consumables[i];
                    string recoverText = potion.HpBonus > 0
                        ? $"ü�� ȸ��: {potion.HpBonus}"
                        : potion.MpBonus > 0
                            ? $"���� ȸ��: {potion.MpBonus}"
                            : "";
                    RenderConsole.Write($"  {i + 1}. ");
                    RenderConsole.Write($"{potion.Name}",ConsoleColor.Cyan);
                    RenderConsole.WriteLine($" ( {recoverText})",ConsoleColor.Gray);

                }
                RenderConsole.WriteLine("����������������������������������������������������������������������������������������������������������������������������������", ConsoleColor.DarkGray);

                RenderConsole.Write("\n>> ");

                string input = Console.ReadLine();

                int a = int.TryParse(input, out a) ? int.Parse(input) : 0;

                if (input == "0") break;
                    
                if (0 < Consumables.Count)
                {
                    var potions = Consumables[int.Parse(input)-1];
                
                    bool used = false;
                
                    if (potions.HpBonus > 0)
                    {
                        Player.CurrentHP = Math.Min(Player.Stat.MaxHP, Player.CurrentHP + potions.HpBonus);
                        RenderConsole.Write($"{potions.Name}", ConsoleColor.Cyan);
                        RenderConsole.Write("��(��) ����Ͽ� ");
                        RenderConsole.Write("ü��", ConsoleColor.DarkBlue);
                        RenderConsole.WriteLineWithSpacing($"�� {potions.HpBonus} ȸ���߽��ϴ�.");
                        used = true;
                    }
                    else if (potions.MpBonus > 0)
                    {
                        Player.CurrentMP = Math.Min(Player.Stat.MaxMP, Player.CurrentMP + potions.MpBonus);
                        RenderConsole.Write($"{potions.Name}",ConsoleColor.Cyan);
                        RenderConsole.Write("��(��) ����Ͽ� ");
                        RenderConsole.Write("����",ConsoleColor.DarkBlue);
                        RenderConsole.WriteLineWithSpacing($"�� {potions.MpBonus} ȸ���߽��ϴ�.");
                        used = true;
                    }
                    else
                    {
                        RenderConsole.WriteLineWithSpacing($"{potions.Name}��(��) ����� �� ���� �������Դϴ�.");
                    }
                
                    if (used)
                    {
                        Consumables.Remove(potions);
                    }
                }
                else
                {
                    RenderConsole.WriteLineWithSpacing("�߸��� �Է��Դϴ�. �ٽ� �Է��ϼ���.");
                    continue;
                }
                Thread.Sleep(1250);
                break;
                
            }
        }


        // �κ��丮 ��� �� ����/���� ���
        public void ShowAndEquip()
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLineWithSpacing("�κ��丮:");
                if (Equipment.Count + Consumables.Count == 0)
                {
                    RenderConsole.WriteLineWithSpacing("  (��� ����)");
                    RenderConsole.WriteLineWithSpacing("����Ϸ��� Enter�� ��������...");
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
                    RenderConsole.WriteLineWithSpacing($"  {i + 1}. {item.Name} (����: {item.Price}, {statText}){equippedText}");
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
                    RenderConsole.WriteLineWithSpacing($"  {i + 1 + Equipment.Count}. {potion.Name} (����: {potion.Price}, {recoverText})");
                }

                RenderConsole.WriteLineWithSpacing("������ ��ȣ�� �Է��ϸ� ����/����, �Ǵ� �Һ��� �� �ֽ��ϴ�. (0 �Է� �� ������)");
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
                else if (idx > Equipment.Count && idx <= Equipment.Count + Consumables.Count)
                {
                    var potion = Consumables[idx - Equipment.Count - 1];

                    bool used = false;

                    if (potion.HpBonus > 0)
                    {
                        if (Player.CurrentHP == Player.Stat.MaxHP)
                        {
                            RenderConsole.WriteLineWithSpacing("ü���� �̹� ���� á���ϴ�.");
                            return;
                        }
                        else
                        {
                            Player.CurrentHP = Math.Min(Player.Stat.MaxHP, Player.CurrentHP + potion.HpBonus);
                            RenderConsole.WriteLineWithSpacing($"{potion.Name}��(��) ����Ͽ� ü���� {potion.HpBonus} ȸ���߽��ϴ�.");
                            used = true;
                        }
                        
                    }
                    else if (potion.MpBonus > 0)
                    {
                        Player.CurrentMP = Math.Min(Player.Stat.MaxMP, Player.CurrentMP + potion.MpBonus);
                        RenderConsole.WriteLineWithSpacing($"{potion.Name}��(��) ����Ͽ� ������ {potion.MpBonus} ȸ���߽��ϴ�.");
                        used = true;
                    }
                    else
                    {
                        RenderConsole.WriteLineWithSpacing($"{potion.Name}��(��) ����� �� ���� �������Դϴ�.");
                    }

                    if (used)
                    {
                        Consumables.Remove(potion);
                    }
                }
                else
                {
                    RenderConsole.WriteLineWithSpacing("�߸��� �Է��Դϴ�. �ٽ� �Է��ϼ���.");
                }

                RenderConsole.WriteLineWithSpacing("����Ϸ��� Enter�� ��������...");
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

