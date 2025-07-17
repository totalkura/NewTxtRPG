using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NewTxtRPG.Entitys;
using NewTxtRPG.Structs;
using NewTxtRPG.Interface;

namespace NewTxtRPG.etc
{
    internal class JsonManager
    {
        private const string PlayerSaveFile = "player_save.json";

        // 저장
        public static void SavePlayer()
        {
            var dto = new PlayerDTO
            {
                Name = Player.Name,
                CurrentHP = Player.CurrentHP,
                CurrentMP = Player.CurrentMP,
                CurrentSpeed = Player.CurrentSpeed,
                Gold = Player.Gold,
                Level = Player.Level,
                Exp = Player.Exp,
                Stat_Attack = Player.Stat.Attack,
                Stat_Defense = Player.Stat.Defense,
                Stat_MaxHP = Player.Stat.MaxHP,
                Stat_MaxMP = Player.Stat.MaxMP,
                Stat_Speed = Player.Stat.Speed,
                ItemAttackBonus = Player.ItemAttackBonus,
                ItemDefenseBonus = Player.ItemDefenseBonus,
                JobName = Player.Job?.Name ?? "",
                InventoryItems = Player.Inventory.GetAllItems()
                    .Select(item => new ItemInfoDTO
                    {
                        Id = item.Id,
                        ItemType = (int)item.ItemType,
                        Name = item.Name,
                        Price = item.Price,
                        Description = item.Description,
                        AttackBonus = item.AttackBonus,
                        DefenseBonus = item.DefenseBonus,
                        HpBonus = item.HpBonus,
                        MpBonus = item.MpBonus
                    }).ToList(),
                EquippedItemNames = Player.Inventory.GetEquippedItemNames().ToList()
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(dto, options);
            File.WriteAllText(PlayerSaveFile, json);
        }

        // 복원
        public static void LoadPlayer()
        {
            if (!File.Exists(PlayerSaveFile))
                return;

            string json = File.ReadAllText(PlayerSaveFile);
            var dto = JsonSerializer.Deserialize<PlayerDTO>(json);
            if (dto == null) return;

            Player.Name = dto.Name;
            Player.CurrentHP = dto.CurrentHP;
            Player.CurrentMP = dto.CurrentMP;
            Player.CurrentSpeed = dto.CurrentSpeed;
            Player.Gold = dto.Gold;
            Player.Level = dto.Level;
            Player.Exp = dto.Exp;
            Player.Stat = new StatStruct(dto.Stat_Attack, dto.Stat_Defense, dto.Stat_MaxHP, dto.Stat_MaxMP, dto.Stat_Speed);
            Player.ItemAttackBonus = dto.ItemAttackBonus;
            Player.ItemDefenseBonus = dto.ItemDefenseBonus;

            // 직업 복원
            if (dto.JobName == "전사")
                Player.Job = new WarriorJob();
            else if (dto.JobName == "도적")
                Player.Job = new ThiefJob();

            // 인벤토리 복원
            Player.Inventory = new Inventory();
            foreach (var itemDto in dto.InventoryItems)
            {
                var item = new ItemInfo(
                    itemDto.Id,
                    (ItemType)itemDto.ItemType,
                    itemDto.Name,
                    itemDto.Price,
                    itemDto.Description,
                    itemDto.AttackBonus,
                    itemDto.DefenseBonus,
                    itemDto.HpBonus,
                    itemDto.MpBonus
                );
                Player.Inventory.AddItem(item);
            }
            // 장착 아이템 복원
            Player.Inventory.SetEquippedItemNames(dto.EquippedItemNames);
        }

        private class PlayerDTO
        {
            public string Name { get; set; }
            public int CurrentHP { get; set; }
            public int CurrentMP { get; set; }
            public float CurrentSpeed { get; set; }
            public int Gold { get; set; }
            public int Level { get; set; }
            public int Exp { get; set; }
            public int Stat_Attack { get; set; }
            public int Stat_Defense { get; set; }
            public int Stat_MaxHP { get; set; }
            public int Stat_MaxMP { get; set; }
            public float Stat_Speed { get; set; }
            public int ItemAttackBonus { get; set; }
            public int ItemDefenseBonus { get; set; }
            public string JobName { get; set; }
            public List<ItemInfoDTO> InventoryItems { get; set; }
            public List<string> EquippedItemNames { get; set; }
        }

        // ItemInfo용 DTO
        private class ItemInfoDTO
        {
            public int Id { get; set; }
            public int ItemType { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Description { get; set; }
            public int AttackBonus { get; set; }
            public int DefenseBonus { get; set; }
            public int HpBonus { get; set; }
            public int MpBonus { get; set; }
        }
    }
}
