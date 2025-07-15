using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon.Structs
{
    public enum ItemType
    {
        Consumables, // 소모품
        Equipment    // 장비
    }
    // 아이템 정보 구조체
    internal struct ItemInfo
    {
        public int Id;             // 아이템 ID
        public ItemType ItemType; // 소모품, 장비구분 여부
        public string Name;         // 아이템 이름
        public int Price;           // 아이템 가격 (구매완료는 0으로 표시)
        public string Description;  // 아이템 설명
        public int AttackBonus;     // 공격력 상승
        public int DefenseBonus;    // 방어력 상승
        public int HpBonus;        // 체력 상승 (소모품에만 적용)
        public int MpBonus;        // 마나 상승 (소모품에만 적용)

        public ItemInfo(int id, ItemType itemType, string name, int price, string description, int attackBonus, int defenseBonus, int hpBonus, int mpBonus)
        {
            Id = id;
            ItemType = itemType;
            Name = name;
            Price = price;
            Description = description;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
            HpBonus = hpBonus;
            MpBonus = mpBonus;
        }
    }
}
