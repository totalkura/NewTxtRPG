using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{

    // todo. 나중에 iCreture 인터페이스 이어서 구현 해주세요.
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
        public int Exp { get; set; }
        
        public Monsters(string name, int currentHP, StatStruct stat, int gold, int exp)
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

        public static Monsters CreateMonster(string name, int hp, int att, int def, float speed, int gold, int exp)
        {
            var stat = new StatStruct()
            {
                Attack = att,
                Defense = def,
                Speed = speed,
                MaxHP = hp
            };
            return new Monsters(name,hp,stat, gold,exp);
            
        }

        public static void MonsterList()
        {
            Monster.Clear();   

            Monster.Add(CreateMonster("다람쥐", 20, 10, 0, 10, 100,2)); //0
            Monster.Add(CreateMonster("토끼", 15, 5, 0, 30, 100,4));  //1
            Monster.Add(CreateMonster("박쥐", 40, 15, 5, 20, 300,8));  //2
            Monster.Add(CreateMonster("소", 100, 30, 15, 15, 500,12));   //3
        }

        public Monsters copy()
        {
            return new Monsters(Name, CurrentHP, Stat, Gold,Exp);
        }
    }


        

 
}
