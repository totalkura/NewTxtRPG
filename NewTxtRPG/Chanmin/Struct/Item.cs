using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon.Structs
{
    // 아이템 정보 구조체
    internal struct ItemInfo
    {
        public string Name;         // 아이템 이름
        public int Price;           // 아이템 가격 (구매완료는 0으로 표시)
        public string Description;  // 아이템 설명
        public int AttackBonus;     // 공격력 상승
        public int DefenseBonus;    // 방어력 상승

        public ItemInfo(string name, int price, string description, int attackBonus, int defenseBonus)
        {
            Name = name;
            Price = price;
            Description = description;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
        }
    }
}
