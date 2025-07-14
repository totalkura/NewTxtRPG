using System.ComponentModel;

namespace TxtRPG
{
    public class Item
    {
        public string name;
        public int level;
        public int att;
        public int def;
        public int gold;
        public float hp;
        public string job;
        public string ex;
        public bool sell;
        public bool equip;

        public Item(string name, int level, int att, int def, int gold, float hp, string job, string ex,bool sell,bool equip)
        {
            this.name = name;
            this.level = level;
            this.att = att;
            this.def = def;
            this.gold = gold;
            this.hp = hp;
            this.job = job;
            this.ex = ex;
            this.sell = false;
            this.equip = false;
        }
        /* 
         * 0 이름 
         * 1 레벨
         * 2 공격력
         * 3 방어력
         * 4 금액
         * 5 HP
         * 6 직업제한
         * 7 설명
         * 8 판매유무
         * 9 장착유무
         */
    }

    public class Item_List
    {
        public static List<Item> item_list = new List<Item>();

        public static List<Item> inventory = new List<Item>();
        static Item_List()
        {
            item_list.Add(new Item("수련자 갑옷", 1, 0, 5, 500, 0, "전체", "수련에 도움을 주는 갑옷입니다.",false, false));
            item_list.Add(new Item("무쇠 갑옷", 2, 0, 9, 1000, 10, "전체", "무쇠로 만들어져 튼튼한 갑옷입니다.", false, false));
            item_list.Add(new Item("스파르타의 갑옷", 3, 0, 15, 1700, 20, "전체", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", false, false));

            item_list.Add(new Item("낡은 검", 1, 2, 0, 500, 0, "전체", "쉽게 볼 수 있는 낡은 검 입니다.", false, false));
            item_list.Add(new Item("청동 도끼", 2, 5, 0, 1500, 0, "전체", "어디선가 사용했던거 같은 도끼입니다.", false, false)  );
            item_list.Add(new Item("스파르타의 창", 3, 7, 0, 2300, 0, "전체", "스파르타의 전사들이 사용했다는 전설의 창입니다.", false, false));

        }

    

    }
}
