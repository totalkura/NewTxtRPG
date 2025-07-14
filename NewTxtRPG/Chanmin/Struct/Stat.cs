using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    // 다양한 대상(플레이어, 몬스터, 아이템)에 공통적으로 사용할 수 있는 스탯 구조체
    /// <summary>
    /// StatStruct는 공격력, 방어력, 최대 체력, 최대 마나를 포함하는 구조체입니다.
    /// </summary>
    internal struct StatStruct
    {
        public int Attack;     // 공격력
        public int Defense;    // 방어력
        public int MaxHP;      // 최대 체력
        public int MaxMP;      // 최대 마나

        /// <summary>
        /// StatStruct 생성자
        /// </summary>
        /// <param name="attack">공격력</param>
        /// <param name="defense">방어력</param>
        /// <param name="maxHP">최대 HP</param>
        /// <param name="maxMP">최대 MP</param>
        public StatStruct(int attack, int defense, int maxHP, int maxMP)
        {
            Attack = attack;
            Defense = defense;
            MaxHP = maxHP;
            MaxMP = maxMP;
        }

        // 두 스탯을 더하는 연산자 오버로딩
        public static StatStruct operator +(StatStruct a, StatStruct b)
        {
            return new StatStruct(
                a.Attack + b.Attack,
                a.Defense + b.Defense,
                a.MaxHP + b.MaxHP,
                a.MaxMP + b.MaxMP
            );
        }

        // 두 스탯을 빼는 연산자 오버로딩
        public static StatStruct operator -(StatStruct a, StatStruct b)
        {
            return new StatStruct(
                a.Attack - b.Attack,
                a.Defense - b.Defense,
                a.MaxHP - b.MaxHP,
                a.MaxMP - b.MaxMP
            );
        }
    }

    internal class Stat
    {
        public StatStruct Value { get; set; }

        public Stat(int attack, int defense, int maxHP, int maxMP)
        {
            Value = new StatStruct(attack, defense, maxHP, maxMP);
        }
    }
}
