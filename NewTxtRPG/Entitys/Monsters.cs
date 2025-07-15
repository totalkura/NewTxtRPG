using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{

    internal class Monsters
    {
        public static List<Monsters> Monster { get; set; } = new List<Monsters>();
        
        public string Name { get; set; }
        public int CurrentHP { get; set; } // 현재 체력
        public float CurrentSpeed { get; set; } // 현재 속도
        public StatStruct Stat { get; set; }
        public int Gold {  get; set; }
        public Inventory Inventory { get; set; }
        public bool DeathCheck { get; set; }

        public Monsters(string name, int currentHP, StatStruct stat, int gold)
        {
            Name = name;
            CurrentHP = currentHP;
            CurrentSpeed = 0;
            Stat = stat;
            Gold = gold;
            Inventory = new Inventory();
            DeathCheck = false;
        }

        public static Monsters CreateMonster(string name, int hp, int att, int def, float speed, int gold )
        {
            var stat = new StatStruct()
            {
                Attack = att,
                Defense = def,
                Speed = speed,
                MaxHP = hp
            };
            return new Monsters(name,hp,stat, gold);
            
        }

        public static void MonsterList()
        {
            Monster.Clear();   

            Monster.Add(CreateMonster("다람쥐", 20, 10, 0, 10, 100)); //0
            Monster.Add(CreateMonster("토끼", 15, 5, 0, 30, 100));  //1
            Monster.Add(CreateMonster("박쥐", 40, 15, 5, 20, 300));  //2
            Monster.Add(CreateMonster("소", 100, 30, 15, 15, 500));   //3
        }

        public Monsters copy()
        {
            return new Monsters(Name, CurrentHP, Stat, Gold);
        }
    }


        

 
}
