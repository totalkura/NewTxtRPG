using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTxtRPG.Gwangho
{
    public class PotionList
    {
        public static List<Potion> potionlist = new List<Potion>();

        public static List<Potion> inventory = new List<Potion>();

        static PotionList()
        {
            potionlist.Add(new Potion("체력 포션", 50, 30, 0, "체력을 회복하는 물약입니다.", false, false));
            potionlist.Add(new Potion("마나 포션", 50, 0, 30, "마나를 회복하는 물약입니다.", false, false));
            potionlist.Add(new Potion("슈퍼 엘릭서", 150, 50, 50, "체력과 마나를 동시에 회복하는 만능 물약입니다.", false, false));
        }
    }
}
