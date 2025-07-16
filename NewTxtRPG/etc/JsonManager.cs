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

        // Player의 static 값들을 DTO로 저장
        public static void SavePlayer()
        {
            var dto = new PlayerDTO
            {
                Name = Player.Name,
                CurrentHP = Player.CurrentHP,
                CurrentMP = Player.CurrentMP,
                CurrentSpeed = Player.CurrentSpeed,
                Gold = Player.Gold,
                Stat = Player.Stat,
                ItemAttackBonus = Player.ItemAttackBonus,
                ItemDefenseBonus = Player.ItemDefenseBonus,
                JobName = Player.Job?.Name ?? "",
                InventoryItems = new List<ItemInfo>(Player.Inventory.GetItems())
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(dto, options);
            File.WriteAllText(PlayerSaveFile, json);
        }

        // 저장된 값을 Player static 필드에 복원
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
            Player.Stat = dto.Stat;
            Player.ItemAttackBonus = dto.ItemAttackBonus;
            Player.ItemDefenseBonus = dto.ItemDefenseBonus;

            // 직업 복원 (직업명으로 객체 생성)
            if (dto.JobName == "전사")
                Player.Job = new WarriorJob();
            else if (dto.JobName == "도적")
                Player.Job = new ThiefJob();
            // 필요시 직업 추가

            // 인벤토리 복원
            Player.Inventory = new Inventory();
            foreach (var item in dto.InventoryItems)
            {
                Player.Inventory.AddItem(item);
            }
        }

        // Player의 값을 저장/복원하기 위한 DTO
        private class PlayerDTO
        {
            public string Name { get; set; }
            public int CurrentHP { get; set; }
            public int CurrentMP { get; set; }
            public float CurrentSpeed { get; set; }
            public int Gold { get; set; }
            public StatStruct Stat { get; set; }
            public int ItemAttackBonus { get; set; }
            public int ItemDefenseBonus { get; set; }
            public string JobName { get; set; }
            public List<ItemInfo> InventoryItems { get; set; }
        }
    }
}
