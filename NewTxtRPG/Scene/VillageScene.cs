using NewTxtRPG.Entitys;
using NewTxtRPG.etc;

namespace NewTxtRPG.Scene
{
    internal class VillageScene
    {
        public void StartVillageScene()
        {
            while (true)
            {
                RenderConsole.WriteLineWithSpacing("\x1b[1m\x1b[38;5;208m마을\x1b[0m");
                RenderConsole.WriteLineWithSpacing("무엇을 하시겠습니까?");
                RenderConsole.WriteLineWithSpacing("1. 인벤토리");
                RenderConsole.WriteLineWithSpacing("2. 여관 이용하기");
                RenderConsole.WriteLineWithSpacing("3. 상점 이용하기");
                RenderConsole.WriteLineWithSpacing("0. 마을 나가기");

                Console.Write("선택: ");
                string input = Console.ReadLine();

                Console.Clear(); // 선택 후 콘솔 전체 지우기

                switch (input)
                {
                    case "1":
                        UseInventory();
                        break;
                    case "2":
                        UseInn();
                        break;
                    case "3":
                        UseShop();
                        break;
                    case "0":
                        QuitVillage();
                        return;
                    default:
                        RenderConsole.WriteLineWithSpacing("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }
        }
        private void UseInventory()
        {
            Player.Inventory.ShowAndEquip();
        }

        private void UseInn()
        {
            RenderConsole.WriteLineWithSpacing("여관에서 휴식을 취합니다.");
            // 여관 이용 로직 추가
        }
        private void UseShop()
        {
            RenderConsole.WriteLineWithSpacing("상점에서 아이템을 구매합니다.");
            // 상점 이용 로직 추가
        }
        private void QuitVillage()
        {
            RenderConsole.WriteLineWithSpacing("마을을 나갑니다.");
        }
    }
}
