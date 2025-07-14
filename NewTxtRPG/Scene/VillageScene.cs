using NewTxtRPG.etc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTxtRPG.Scene
{
    internal class VillageScene
    {
        public void StartVillageScene()
        {
            while (true)
            {
                RenderConsole.WriteLine("\x1b[1m\x1b[38;5;208m마을\x1b[0m");
                RenderConsole.WriteLine("무엇을 하시겠습니까?");
                RenderConsole.WriteLine("1. 여관 이용하기");
                RenderConsole.WriteLine("2. 상점 이용하기");
                RenderConsole.WriteLine("0. 마을 나가기");

                Console.Write("선택: ");
                string input = Console.ReadLine();

                Console.Clear(); // 선택 후 콘솔 전체 지우기

                switch (input)
                {
                    case "1":
                        UseInn();
                        break;
                    case "2":
                        UseShop();
                        break;
                    case "0":
                        QuitVillage();
                        return;
                    default:
                        RenderConsole.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }
        }
        private void UseInn()
        {
            RenderConsole.WriteLine("여관에서 휴식을 취합니다.");
            // 여관 이용 로직 추가
        }
        private void UseShop()
        {
            RenderConsole.WriteLine("상점에서 아이템을 구매합니다.");
            // 상점 이용 로직 추가
        }
        private void QuitVillage()
        {
            RenderConsole.WriteLine("마을을 나갑니다.");
        }
    }
}
