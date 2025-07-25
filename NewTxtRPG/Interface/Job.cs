﻿using NewTxtRPG.Entitys;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Interface
{
    // 직업의 공통적인 속성을 정의하는 인터페이스
    internal interface IJob
    {
        string Name { get; } // 직업 이름
        // 각 직업의 기본 스탯
        StatStruct BaseStat { get; }

        // 각 직업이 보유한 스킬 2개
        Skill Skill1 { get; }
        Skill Skill2 { get; }
        Skill Skill3 { get; }

        void UseSkill(int skillNumber, List<Monsters> monster,ref int gold, ref int exp);
        void UseSkill(int skillNumber, List<MonsterBoss> monster, ref int gold, ref int exp);
    }
}
