using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    // 직업의 기본 베이스 클래스
    internal abstract class JobBase : IJob
    {
        public abstract StatStruct BaseStat { get; }
        public abstract Skill Skill1 { get; }
        public abstract Skill Skill2 { get; }

        // 스킬 사용 메서드
        public virtual void UseSkill(Player player, int skillNumber)
        {
            Skill skill = skillNumber == 1 ? Skill1 : Skill2;

            if (player.CurrentMP < skill.ManaCost)
            {
                Console.WriteLine("마나가 부족하여 스킬을 사용할 수 없습니다.");
                return;
            }

            player.CurrentMP -= skill.ManaCost;
            Console.WriteLine($"{player.Name}이(가) '{skill.Name}' 스킬을 사용했습니다! (데미지: {player.Stat.Attack * skill.Multiplier}, 마나 소모: {skill.ManaCost})");
            // 실제 효과 적용(예: 적에게 데미지 등)은 추후 구현
        }
    }

    // 전사 직업 클래스
    internal class WarriorJob : JobBase
    {
        public override StatStruct BaseStat => new StatStruct(10, 5, 100, 30);

        public override Skill Skill1 => new Skill("베기", "검으로 적을 벱니다.", 1, 0);
        public override Skill Skill2 => new Skill("강타", "마나를 소모해 강력한 일격을 가합니다.", 2, 10);
    }

    // 도적 직업 클래스
    internal class ThiefJob : JobBase
    {
        public override StatStruct BaseStat => new StatStruct(14, 3, 100, 40);

        public override Skill Skill1 => new Skill("찌르기", "단검으로 적을 빠르게 찌릅니다.", 1, 0);
        public override Skill Skill2 => new Skill("연속 찌르기", "마나를 소모해 여러 번 적을 찌릅니다.", 2, 8);
    }
}
