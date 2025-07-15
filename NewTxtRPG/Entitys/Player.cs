using NewTxtRPG.Interface;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{
    // 플레이어 클래스: static으로 변경
    internal static class Player
    {
        public static string Name { get; set; }                // 플레이어 이름
        public static int CurrentHP { get; set; }              // 현재 체력
        public static int CurrentMP { get; set; }              // 현재 마나
        public static float CurrentSpeed { get; set; }         // 현재 속도
        public static StatStruct Stat { get; set; }            // 스탯
        public static int Gold { get; set; }                   // 보유 골드
        public static Inventory Inventory { get; set; }        // 인벤토리
        public static IJob Job { get; set; }                   // 직업 정보

        // 아이템으로 인한 추가 스탯
        public static int ItemAttackBonus { get; set; }        // 아이템 공격력 보너스
        public static int ItemDefenseBonus { get; set; }       // 아이템 방어력 보너스

        // 초기화 메서드
        public static void Initialize(string name, int gold, IJob job)
        {
            Name = name;
            Stat = job.BaseStat;
            Gold = gold;
            CurrentHP = Stat.MaxHP;
            CurrentMP = Stat.MaxMP;
            CurrentSpeed = 0;
            Inventory = new Inventory();
            Job = job;
            ItemAttackBonus = 0;
            ItemDefenseBonus = 0;
        }
    }
}
