using NewTxtRPG.Structs;
using NewTxtRPG.Interface;

namespace NewTxtRPG.Entitys
{

    internal class MonsterBoss : ICreture
    {
        public static List<MonsterBoss> _MonsterBoss { get; set; } = new List<MonsterBoss>();
        
        public string Name { get; set; }
        public int CurrentHP { get; set; } // 현재 체력
        public float CurrentSpeed { get; set; } // 현재 속도
        public StatStruct Stat { get; set; }
        public int Gold {  get; set; }
        public Inventory Inventory { get; set; }
        public bool DeathCheck { get; set; }
        public int Exp { get; set; }
        
        public MonsterBoss(string name, int currentHP, StatStruct stat, int gold, int exp)
        {
            Name = name;
            CurrentHP = currentHP;
            CurrentSpeed = 0;
            Stat = stat;
            Gold = gold;
            Exp = exp;
            Inventory = new Inventory();
            DeathCheck = false;
        }

        public static MonsterBoss CreateMonster(string name, int hp, int att, int def, float speed, int gold, int exp)
        {
            var stat = new StatStruct()
            {
                Attack = att,
                Defense = def,
                Speed = speed,
                MaxHP = hp
            };
            return new MonsterBoss(name,hp,stat, gold,exp);
            
        }

        public static void MonsterList()
        {
            _MonsterBoss.Clear();


            _MonsterBoss.Add(CreateMonster("만렙토끼", 150, 20, 10, 15, 1000, 60)); //0
            _MonsterBoss.Add(CreateMonster("???", 100, 0, 0, 5, 500,20)); //0
        }

        public MonsterBoss copy()
        {
            return new MonsterBoss(Name, CurrentHP, Stat, Gold,Exp);
        }


        public virtual void Damage(ICreture target, int damege)
        {
            int checkDamege = damege - target.Stat.Defense;
            if (checkDamege > 0)
                target.CurrentHP -= checkDamege;
        }

    }
}
