using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    // 직업의 공통적인 속성을 정의하는 인터페이스
    internal interface IJob
    {
        // 각 직업의 기본 스탯
        StatStruct BaseStat { get; }

        // 각 직업이 보유한 스킬 2개
        Skill Skill1 { get; }
        Skill Skill2 { get; }
    }
}
