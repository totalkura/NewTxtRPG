using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    // 스킬의 공통적인 속성을 담는 구조체
    internal struct Skill
    {
        public string Name;        // 스킬 이름
        public string Effect;      // 스킬 효과 설명
        public double Multiplier;  // 스킬 배수 (공격력에 곱해지는 값)
        public int ManaCost;       // 스킬 소비 마나

        public Skill(string name, string effect, double multiplier, int manaCost)
        {
            Name = name;
            Effect = effect;
            Multiplier = multiplier;
            ManaCost = manaCost;
        }
    }
}
