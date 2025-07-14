using NewTxtRPG.etc;
using NewTxtRPG.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NewTxtRPG
{
    internal class MainClass
    {
        //public static Player Player { get; private set; }
        public static void Main(string[] args)
        {
            Console.WriteLine("스파르타 던전에 오신 것을 환영합니다!");

            InitializeGame();
            StartGame();

            Console.WriteLine("아무 키나 누르면 종료합니다...");
            Console.ReadKey();
        }

        // 게임 초기화
        private static void InitializeGame()
        {
            //if 세이브 파일이 있을때
            //LoadGame();
            //else 
            SetPlayer();

            Thread.Sleep(1000); // 1초 대기
            Console.Clear();    // 로그 클리어
        }

        private static void StartGame()
        {
            GameScene gameScene = new GameScene();
            gameScene.StartGameScene();
        }

        private static void SetPlayer()
        {
            Console.Write("플레이어 이름을 입력하세요: ");
            string name = Console.ReadLine();

            RenderConsole.WriteLine("직업을 선택하세요:");
            RenderConsole.WriteLine("1. 전사");
            RenderConsole.WriteLine("   - 공격력: 10, 방어력: 5, 체력: 100, 마나: 30");
            RenderConsole.WriteLine("   - 설명: 튼튼한 방어와 무난한 공격력을 가진 근접 전투 전문가");
            RenderConsole.WriteLine("   - 초기 장비: 가죽 튜닉");
            RenderConsole.WriteLine("2. 도적");
            RenderConsole.WriteLine("   - 공격력: 14, 방어력: 3, 체력: 100, 마나: 40");
            RenderConsole.WriteLine("   - 설명: 빠른 공격과 높은 기동성을 가진 치명적인 암살자");
            RenderConsole.WriteLine("   - 초기 장비: 초보자의 검");

            //IJob selectedJob = null;
            //ItemInfo? initialItem = null;
            //while (selectedJob == null)
            //{
            //    Console.Write("번호를 입력하세요 (1 또는 2): ");
            //    string input = Console.ReadLine();
            //    if (input == "1")
            //    {
            //        selectedJob = new WarriorJob();
            //        initialItem = Items.ItemList.FirstOrDefault(x => x.Name == "가죽 튜닉");
            //    }
            //    else if (input == "2")
            //    {
            //        selectedJob = new ThiefJob();
            //        initialItem = Items.ItemList.FirstOrDefault(x => x.Name == "초보자의 검");
            //    }
            //    else
            //    {
            //        RenderConsole.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
            //    }
            //}

            //Player = new Player(name, beginningGold, selectedJob);
            //if (initialItem.HasValue)
            //{
            //    Player.Inventory.AddItem(initialItem.Value);
            //    // 바로 장착
            //    if (initialItem.Value.AttackBonus > 0)
            //        Player.ItemAttackBonus += initialItem.Value.AttackBonus;
            //    if (initialItem.Value.DefenseBonus > 0)
            //        Player.ItemDefenseBonus += initialItem.Value.DefenseBonus;
            //    // 장착 상태로 표시
            //    var equipField = typeof(Inventory).GetField("equippedItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //    var equippedSet = equipField?.GetValue(Player.Inventory) as HashSet<string>;
            //    equippedSet?.Add(initialItem.Value.Name);
            //}
            //RenderConsole.WriteLine($"{name}님, {(selectedJob is WarriorJob ? "전사" : "도적")}로 게임을 시작합니다!");
        }

        private static void LoadGame()
        {
            // 세이브 파일 로드 로직
            // 예: Player = SaveManager.LoadPlayer();
            RenderConsole.WriteLine("세이브 파일을 불러왔습니다.");
        }
    }
}
