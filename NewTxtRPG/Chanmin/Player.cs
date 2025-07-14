using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    // 플레이어 클래스: ICreture 인터페이스 구현
    internal class Player : ICreture
    {
        public string Name { get; set; }                // 플레이어 이름
        public int CurrentHP { get; set; }              // 현재 체력
        public int CurrentMP { get; set; }              // 현재 마나
        public StatStruct Stat { get; set; }            // 스탯
        public int Gold { get; set; }                   // 보유 골드
        public Inventory Inventory { get; set; }        // 인벤토리
        public IJob Job { get; set; }                   // 직업 정보

        // 아이템으로 인한 추가 스탯
        public int ItemAttackBonus { get; set; }        // 아이템 공격력 보너스
        public int ItemDefenseBonus { get; set; }       // 아이템 방어력 보너스

        public Player(string name, int gold, IJob job)
        {
            Name = name;
            Stat = job.BaseStat;
            Gold = gold;
            CurrentHP = Stat.MaxHP;
            CurrentMP = Stat.MaxMP;
            Inventory = new Inventory();
            Job = job;
            ItemAttackBonus = 0;
            ItemDefenseBonus = 0;
        }
    }
}
