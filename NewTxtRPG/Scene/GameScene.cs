using NewTxtRPG.Entitys;
using NewTxtRPG.etc;
using NewTxtRPG.Interface;
using NewTxtRPG.Structs;

namespace NewTxtRPG.Scene
{
    internal class GameScene
    {
        VillageScene villageScene = new VillageScene();
        DungeonEventManager dungeon = new DungeonEventManager();
        private Random rand = new Random();

        //던전 신
        public void StartGameScene()
        {
            while (true)
            {
                RenderConsole.WriteEmptyLine();
                RenderConsole.WriteLineWithSpacing("무엇을 하시겠습니까?");
                RenderConsole.WriteLineWithSpacing("1. 상태보기 ");
                RenderConsole.WriteLineWithSpacing("2. 마을 (상점, 여관 등) ");
                RenderConsole.WriteLineWithSpacing("3. 던전");
                RenderConsole.WriteLineWithSpacing("0. 게임 종료");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                Console.Clear(); // 선택 후 콘솔 전체 지우기

                switch (input)
                {
                    case "1":
                        ShowPlayerStatus();
                        break;
                    case "2":
                        GoVillage();
                        break;
                    case "3":
                        int RandomEvent = rand.Next(0, 2);
                        if (RandomEvent == 0 )  GoDungeon();
                        else dungeon.TriggerRandomEvent();
                        break;
                    case "0":
                        EndGame();
                        return;
                    default:
                        RenderConsole.WriteLineWithSpacing("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }
        }

        public void ShowPlayerStatus()
        {
            // 아이템 추가 공격력/방어력이 있을 때만 괄호로 표시
            string attackText = Player.ItemAttackBonus > 0
                ? $" (+{Player.ItemAttackBonus})"
                : "";
            string defenseText = Player.ItemDefenseBonus > 0
                ? $" (+{Player.ItemDefenseBonus})"
                : "";

            //Lv.1
            //Player (전사)
            //공격력: 10
            //방어력: 5
            //체력: 100 / 100
            //마나: 30 / 30
            //골드: 100
            RenderConsole.WriteLineWithSpacing($"LV. {Player.Level}");
            RenderConsole.WriteLineWithSpacing($"{Player.Name} ({Player.Job.Name})");

            RenderConsole.WriteLineWithSpacing($"공격력: {Player.Stat.Attack + Player.ItemAttackBonus}{attackText}");
            RenderConsole.WriteLineWithSpacing($"방어력: {Player.Stat.Defense + Player.ItemDefenseBonus}{defenseText}");
            RenderConsole.WriteLineWithSpacing($"체력: {Player.CurrentHP} / {Player.Stat.MaxHP}");
            RenderConsole.WriteLineWithSpacing($"마나: {Player.CurrentMP} / {Player.Stat.MaxMP}");

            RenderConsole.WriteLineWithSpacing($"골드: {Player.Gold}");

            if (Player.Job != null)
            {
                RenderConsole.WriteLineWithSpacing("스킬 목록:");
                RenderConsole.WriteLineWithSpacing($"  1. {Player.Job.Skill1.Name} - {Player.Job.Skill1.Effect} (배수: {Player.Job.Skill1.Multiplier}, 마나 소모: {Player.Job.Skill1.ManaCost})");
                RenderConsole.WriteLineWithSpacing($"  2. {Player.Job.Skill2.Name} - {Player.Job.Skill2.Effect} (배수: {Player.Job.Skill2.Multiplier}, 마나 소모: {Player.Job.Skill2.ManaCost})");
            }

            RenderConsole.WriteLineWithSpacing("계속하려면 Enter를 누르세요...");
            Console.ReadLine();
            Console.Clear();
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

                Console.WriteLine("\n1. 쉬움   - 약합니다");
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
            RenderConsole.WriteLineWithSpacing("던전으로 이동합니다. 적과 싸우고 보물을 찾을 수 있습니다.");
        }

        private void EndGame()
        {
            RenderConsole.WriteLineWithSpacing("게임을 종료합니다. 감사합니다!");
            // 여기서 게임 종료 로직을 추가할 수 있습니다.
        }
    }
}
