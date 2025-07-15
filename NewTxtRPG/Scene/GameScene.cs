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
