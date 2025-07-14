using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    // 크리쳐의 공통적인 속성을 정의하는 인터페이스
    internal interface ICreture
    {
        int CurrentHP { get; set; }         // 현재 체력
        StatStruct Stat { get; set; }       // 스탯 (공격력, 방어력, 최대 체력 등)
        int Gold { get; set; }              // 보유 골드
    }
}
