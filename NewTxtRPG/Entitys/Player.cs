using NewTxtRPG.Interface;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{
    // 플레이어 클래스: static으로 변경
   

    internal static class Player
    {
        public static string Name { get; set; }                // 플레이어 이름
        public static int Level { get; set; }                  // 플레이어 레벨
        public static int CurrentHP { get; set; }              // 현재 체력
        public static int CurrentMP { get; set; }              // 현재 마나
        public static float CurrentSpeed { get; set; }         // 현재 속도
        public static StatStruct Stat { get; set; }            // 스탯
        public static int Gold { get; set; }                   // 보유 골드
        public static int Exp { get; set; }                    // 보유 경험치
        public static Inventory Inventory { get; set; }        // 인벤토리
        public static IJob Job { get; set; }                   // 직업 정보

        public static int DungeonCleared = 0;                  // 던전 클리어 여부

        // 아이템으로 인한 추가 스탯
        public static int ItemAttackBonus { get; set; }        // 아이템 공격력 보너스
        public static int ItemDefenseBonus { get; set; }       // 아이템 방어력 보너스

        // 초기화 메서드
        public static void Initialize(string name, int gold, IJob job)
        {
            Name = name;
            Level = 1;
            Stat = job.BaseStat;
            Gold = gold;
            Exp = 0;
            CurrentHP = Stat.MaxHP;
            CurrentMP = Stat.MaxMP;
            CurrentSpeed = 0;
            Inventory = new Inventory();
            Job = job;
            ItemAttackBonus = 0;
            ItemDefenseBonus = 0;
        }

        public static void LevelUp()
        {
            for (int i = 0; i < 10; i++)
            {
                if (Exp >= i * 30 && Level <= i)
                    Player_LevelUp();
            }
        }

        public static void Player_LevelUp()
        {
            Level += 1;
            Stat += new StatStruct(2,1,10,10,0);
            Console.WriteLine($"\n 레벨업! | {Level - 1} => {Level}");
            Console.WriteLine($"\n 최대체력 + 10");
            Console.WriteLine($" 최대마나 + 10");
            Console.WriteLine($" 공격력 + 2");
            Console.WriteLine($" 방어력 + 1");
        }
    }
}
