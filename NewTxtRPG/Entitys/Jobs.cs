using NewTxtRPG.etc;
using NewTxtRPG.Interface;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Entitys
{
    // 직업의 기본 베이스 클래스
    internal abstract class JobBase : IJob
    {
        public abstract StatStruct BaseStat { get; }
        public abstract Skill Skill1 { get; }
        public abstract Skill Skill2 { get; }

        // 스킬 사용 메서드
        public virtual void UseSkill(int skillNumber, List<Monsters> monster,int gold, int exp)
        {
            Random rnd = new Random();
            int rand;

            Skill skill = skillNumber == 1 ? Skill1 : Skill2;

            if (Player.CurrentMP < skill.ManaCost)
            {
                RenderConsole.WriteLine("마나가 부족하여 스킬을 사용할 수 없습니다.");
                return;
            }

            Player.CurrentMP -= skill.ManaCost;

            RenderConsole.Write("[");
            RenderConsole.Write($"{Player.Name}",ConsoleColor.Blue);
            RenderConsole.Write(" ] 이(가) '");
            RenderConsole.Write($"{skill.Name}",ConsoleColor.Cyan);
            RenderConsole.Write("' 스킬을 사용했습니다! ");
            RenderConsole.Write("MP", ConsoleColor.DarkBlue);
            RenderConsole.WriteLineWithSpacing($" - {skill.ManaCost}");

            do
            {
                rand = rnd.Next(0, monster.Count);
            }
            while (monster[rand].DeathCheck);
            
            RenderConsole.WriteLineWithSpacing("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);

            RenderConsole.Write($"{monster[rand].Name}", ConsoleColor.Green);
            RenderConsole.Write("은(는) ");
            RenderConsole.Write($"{Player.Stat.Attack * skill.Multiplier}", ConsoleColor.DarkRed);
            RenderConsole.Write("만큼 ");
            RenderConsole.Write("데미지", ConsoleColor.Red);
            RenderConsole.WriteLineWithSpacing("를 입었습니다.");

            monster[rand].CurrentHP -= (int)(Player.Stat.Attack * skill.Multiplier);

            if (monster[rand].CurrentHP <= 0)
            {
                RenderConsole.WriteLineWithSpacing("─────────────────────────────────────────────────────────────────", ConsoleColor.DarkGray);

                RenderConsole.Write($"\n{monster[rand].Name}", ConsoleColor.Green);
                RenderConsole.Write("은(는) ");
                RenderConsole.WriteLine("기력이 다했다...");
                monster[rand].CurrentHP = monster[rand].CurrentHP < 0 ? 0 : monster[rand].CurrentHP;
                monster[rand].DeathCheck = true;
                gold += monster[rand].Gold;
                exp += monster[rand].Exp;
                Thread.Sleep(700);
            }

        }
    }

    // 전사 직업 클래스
    internal class WarriorJob : JobBase
    {
        public override StatStruct BaseStat => new StatStruct(10, 5, 100, 30,10);

        public override Skill Skill1 => new Skill("베기", "검으로 적을 벱니다.", 1, 0);
        public override Skill Skill2 => new Skill("강타", "마나를 소모해 강력한 일격을 가합니다.", 2, 10);
    }

    // 도적 직업 클래스
    internal class ThiefJob : JobBase
    {
        public override StatStruct BaseStat => new StatStruct(14, 3, 100, 40,20);

        public override Skill Skill1 => new Skill("찌르기", "단검으로 적을 빠르게 찌릅니다.", 1, 0);
        public override Skill Skill2 => new Skill("연속 찌르기", "마나를 소모해 여러 번 적을 찌릅니다.", 2, 8);
    }
}
