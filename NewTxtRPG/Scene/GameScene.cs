using NewTxtRPG.Entitys;
using NewTxtRPG.etc;

namespace NewTxtRPG.Scene
{
    internal class GameScene
    {
        VillageScene villageScene = new VillageScene();
        //던전 신
        public void StartGameScene()
        {
            while (true)
            {
                RenderConsole.WriteEmptyLine();
                RenderConsole.WriteLine("무엇을 하시겠습니까?");
                RenderConsole.WriteLine("1. 상태보기 ");
                RenderConsole.WriteLine("2. 마을 (상점, 여관 등) ");
                RenderConsole.WriteLine("3. 던전");
                RenderConsole.WriteLine("0. 게임 종료");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                Console.Clear(); // 선택 후 콘솔 전체 지우기

                switch (input)
                {
                    case "1":
                        ShowPlayerStatus();
                        RenderConsole.WriteLine("플레이어 상태를 보여주는 기능은 아직 구현되지 않았습니다.");
                        break;
                    case "2":
                        GoVillage();
                        break;
                    case "3":
                        GoDungeon();
                        RenderConsole.WriteLine("던전으로 이동하는 기능은 아직 구현되지 않았습니다.");
                        break;
                    case "0":
                        EndGame();
                        return;
                    default:
                        RenderConsole.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }
        }

        private void ShowPlayerStatus()
        {
            RenderConsole.WriteLine("플레이어 상태를 보여주는 기능은 아직 구현되지 않았습니다.");
            // 여기서 플레이어 상태를 보여주는 로직을 추가할 수 있습니다.
        }

        private void GoVillage()
        {
            villageScene.StartVillageScene();
        }

        private void GoDungeon()
        {
            DungeonScene dungeon = new DungeonScene();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 던전 입구 >");
                Console.ResetColor();
                Console.WriteLine("던전을 들어가기전 난이도를 설정 하실 수 있습니다\n");

                Console.WriteLine("[ 던전 난이도 ]");

                Console.WriteLine("\n1. 쉬움   - 합니다");
                Console.WriteLine("2. 일반   - 강합니다");
                Console.WriteLine("3. 어려움 - 강력합니다");
                Console.WriteLine("0. 나가기\n");

                if (Player.CurrentHP < 30)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("< 체력이 낮을때 던전에 들어가는건 위험합니다>\n");
                    Console.ResetColor();

                }

                Console.Write("원하시는 행동을 입력해 주세요.\n>>");
                string actionInput = Console.ReadLine();

                if (actionInput == "0") break;
                if (actionInput == "1" || actionInput == "2" || actionInput == "3")
                {
                    dungeon.Battle(actionInput);
                }

            }
            RenderConsole.WriteLine("던전으로 이동합니다. 적과 싸우고 보물을 찾을 수 있습니다.");
            // 여기서 던전으로 이동하는 로직을 추가할 수 있습니다.
        }

        private void EndGame()
        {
            RenderConsole.WriteLine("게임을 종료합니다. 감사합니다!");
            // 여기서 게임 종료 로직을 추가할 수 있습니다.
        }
    }
}
