using NewTxtRPG.Entitys;
using NewTxtRPG.etc;
using NewTxtRPG.Interface;
using NewTxtRPG.Structs;

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

        public void ShowPlayerStatus()
        {
            RenderConsole.WriteLine($"이름: {Player.Name}");
            RenderConsole.WriteLine($"체력: {Player.CurrentHP} / {Player.Stat.MaxHP}");
            RenderConsole.WriteLine($"마나: {Player.CurrentMP} / {Player.Stat.MaxMP}");

            // 아이템 추가 공격력/방어력이 있을 때만 괄호로 표시
            string attackText = Player.ItemAttackBonus > 0
                ? $" (+{Player.ItemAttackBonus})"
                : "";
            string defenseText = Player.ItemDefenseBonus > 0
                ? $" (+{Player.ItemDefenseBonus})"
                : "";

            RenderConsole.WriteLine($"공격력: {Player.Stat.Attack + Player.ItemAttackBonus}{attackText}");
            RenderConsole.WriteLine($"방어력: {Player.Stat.Defense + Player.ItemDefenseBonus}{defenseText}");
            RenderConsole.WriteLine($"골드: {Player.Gold}");

            if (Player.Job != null)
            {
                RenderConsole.WriteLine("스킬 목록:");
                RenderConsole.WriteLine($"  1. {Player.Job.Skill1.Name} - {Player.Job.Skill1.Effect} (배수: {Player.Job.Skill1.Multiplier}, 마나 소모: {Player.Job.Skill1.ManaCost})");
                RenderConsole.WriteLine($"  2. {Player.Job.Skill2.Name} - {Player.Job.Skill2.Effect} (배수: {Player.Job.Skill2.Multiplier}, 마나 소모: {Player.Job.Skill2.ManaCost})");
            }

            RenderConsole.WriteLine("계속하려면 Enter를 누르세요...");
            Console.ReadLine();
            Console.Clear();
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
