﻿using NewTxtRPG.Entitys;
using NewTxtRPG.etc;
using NewTxtRPG.Interface;
using NewTxtRPG.Scene;
using NewTxtRPG.Structs;

namespace NewTxtRPG
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("스파르타 던전에 오신 것을 환영합니다!");

            InitializeGame();
            StartGame();

            // 게임 종료 시 세이브 여부 확인
            Console.WriteLine();
            Console.Write("게임을 저장하시겠습니까? (저장 : 1, 저장없이 나가기 : 0): ");
            string saveInput = Console.ReadLine();
            if (saveInput.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                JsonManager.SavePlayer();
                Console.WriteLine("게임이 저장되었습니다.");
            }

            Console.WriteLine("아무 키나 누르면 종료합니다...");
            Console.ReadKey();
        }


        // 게임 초기화
        private static void InitializeGame()
        {
            if (File.Exists("player_save.json"))
            {
                Console.WriteLine("세이브 파일이 발견되었습니다.");
                Console.WriteLine("게임을 불러오시겠습니까?");
                while (true)
                {
                    Console.WriteLine("1. 불러오기 2. 새로 시작하기");

                    string choice = Console.ReadLine();

                    if (!(choice == "1" || choice == "2"))
                    {
                        Console.WriteLine("잘못 된 입력입니다.");
                    }

                    if (choice == "1")
                    {
                        JsonManager.LoadPlayer();
                        Thread.Sleep(1000); // 1초 대기
                        Console.Clear();    // 로그 클리어
                        return; // 게임 불러오기 후 루프 종료
                    }
                    Console.WriteLine("새로운 게임을 시작합니다.");
                    SetPlayer();
                    Thread.Sleep(1000); // 1초 대기
                    Console.Clear();    // 로그 클리어
                    return; // 새 게임 시작 후 루프 종료
                }
            }
            Console.WriteLine("새로운 게임을 시작합니다.");
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

            // 직업 정보 및 초기 아이템 정의
            var jobs = new[]
            {
        new {
            Job = (IJob)new WarriorJob(),
            Description = "튼튼한 방어와 무난한 공격력을 가진 근접 전투 전문가",
            InitialItems = new[] {
                Items.ItemList.First(x => x.Name == "가죽 튜닉"),
                Items.ItemList.First(x => x.Name == "치유의 물약"),
                Items.ItemList.First(x => x.Name == "치유의 물약"),
                Items.ItemList.First(x => x.Name == "치유의 물약")
            }
        },
        new {
            Job = (IJob)new ThiefJob(),
            Description = "빠른 공격과 높은 기동성을 가진 치명적인 암살자",
            InitialItems = new[] {
                Items.ItemList.First(x => x.Name == "초보자의 검"),
                Items.ItemList.First(x => x.Name == "치유의 물약"),
                Items.ItemList.First(x => x.Name == "치유의 물약"),
                Items.ItemList.First(x => x.Name == "마나의 물약")
            }
        }
    };

            RenderConsole.WriteLineWithSpacing("직업을 선택하세요:");
            for (int i = 0; i < jobs.Length; i++)
            {
                var job = jobs[i].Job;
                var items = jobs[i].InitialItems;
                RenderConsole.WriteLine($"{i + 1}. {job.Name}");
                RenderConsole.WriteLine($"   - 공격력: {job.BaseStat.Attack}, 방어력: {job.BaseStat.Defense}, 체력: {job.BaseStat.MaxHP}, 마나: {job.BaseStat.MaxMP}, 공격 속도: {job.BaseStat.Speed:F1}");
                RenderConsole.WriteLine($"   - 설명: {jobs[i].Description}");
                string itemList = string.Join(", ", items.Select(x => x.Name));
                RenderConsole.WriteLine($"   - 초기 장비: {itemList}");
                RenderConsole.WriteLine();
            }

            IJob selectedJob = null;
            ItemInfo[] initialItems = null;
            while (selectedJob == null)
            {
                Console.Write("\n번호를 입력하세요 (1 또는 2): ");
                string input = Console.ReadLine();
                if (input == "1" || input == "2")
                {
                    int idx = int.Parse(input) - 1;
                    selectedJob = jobs[idx].Job;
                    initialItems = jobs[idx].InitialItems;
                }
                else
                {
                    RenderConsole.WriteLineWithSpacing("잘못된 입력입니다. 다시 입력하세요.");
                }
            }

            Player.Initialize(name, 1599, selectedJob);

            if (initialItems != null)
            {
                foreach (var item in initialItems)
                {
                    Player.Inventory.AddItem(item);
                    // 바로 장착 (장비만)
                    if (item.ItemType == ItemType.Equipment)
                    {
                        if (item.AttackBonus > 0)
                            Player.ItemAttackBonus += item.AttackBonus;
                        if (item.DefenseBonus > 0)
                            Player.ItemDefenseBonus += item.DefenseBonus;
                        var equipField = typeof(Inventory).GetField("equippedItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        var equippedSet = equipField?.GetValue(Player.Inventory) as HashSet<string>;
                        equippedSet?.Add(item.Name);
                    }
                }
            }
            RenderConsole.WriteLineWithSpacing($"{name}님, {selectedJob.Name}(으)로 게임을 시작합니다!");
        }

    }
}
