using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTxtRPG.Gwangho
{
    public class Potion
    {
        public string name;
        public int gold;
        public float hpup;
        public float mpup;
        public string ex;
        public bool sell;
        public bool equip;
        public Potion(string name, int gold, float hpup, float mpup, string ex, bool sell, bool equip)
        {
            this.name = name;
            this.gold = gold;
            this.hpup = hpup;
            this.mpup = mpup;
            this.ex = ex;
            this.sell = false;
            this.equip = false;
        }
        /* 
         * 0 이름 
         * 4 금액
         * 5 HP회복량
         * 6 MP회복량
         * 7 설명
         * 8 판매유무
         * 9 장착유무
         */
    }
}
