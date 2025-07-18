using NewTxtRPG.Entitys;
using NewTxtRPG.Structs;
using NewTxtRPG.etc;
using System.Text;

namespace NewTxtRPG.Managers
{
    internal class QuestManager
    {
        // Singleton 인스턴스
        private static QuestManager _instance;
        public static QuestManager Instance => _instance ??= new QuestManager();

        // 퀘스트 목록
        public List<Quest> Quests { get; private set; }

        // 생성자 private (외부에서 new 못하게 막음)
        private QuestManager()
        {
            Quests = new List<Quest>();

            Quests.Add(new Quest(
                "나혼자만 레벨업",
                "\r\n⢐⠐⣦⣢⣬⣾⢿⣯⣿⣻⣽⣻⡽⣿⢽⡽⣽⢽⣻⣝⢷⢽⣝⡽⣆⢄⠂⠐⡀⠂\r\n⠐⡐⠈⠗⠿⣾⣿⣻⣞⡷⣿⣺⣽⢽⡯⡿⡽⣽⣺⣞⢿⣽⣺⡽⣺⢵⡱⠈⠀⠈\r\n⠡⠠⣑⣒⣿⣽⣿⣳⣯⣯⢿⣞⣾⢽⢏⣯⢯⢷⢳⢯⣟⣮⣗⣯⣗⣯⢯⣇⠠⠁\r\n⠅⢂⢈⠁⢫⣾⣿⣽⣾⣾⣟⣷⢿⡽⡯⡾⡽⡕⡯⣟⣾⢾⣺⣞⡷⡯⣟⠮⠐⢀\r\n⠐⡀⢂⠠⠽⢩⣾⢿⣿⣽⡿⣟⣿⣽⣯⢿⡯⣟⡧⣿⢯⡿⣵⡷⣿⢽⡏⢂⠈⠠\r\n⠐⡐⠠⠐⠈⡈⢼⣿⣻⡿⣟⢿⣿⣿⡽⣯⣿⢽⣯⣻⣽⢕⣿⢿⣽⢯⠁⠄⢀⠡\r\n⢂⠐⡐⢀⢁⠀⢣⣿⣻⡽⣟⠯⡷⣟⣿⣿⢯⣿⣳⡻⡺⡺⣝⡯⠟⠔⡅⠠⠀⠄\r\n⣆⠂⢂⠂⠄⡂⢄⢗⠟⠽⡝⡕⡜⣟⡞⣟⡿⣗⡞⡅⠡⢕⠨⣎⡎⣇⡥⡢⣐⠀\r\n⣱⢍⠆⢕⢱⠸⠨⡢⠱⡠⠑⢕⢕⢅⢇⡣⣋⢳⠱⡑⡍⡊⡸⡸⡼⣺⡪⣣⣷⣿\r\n⣜⣕⣙⢍⢢⠱⡑⢌⠬⡂⢆⠀⠑⡅⡇⡇⢆⢇⢣⠣⠃⣰⢝⣞⣽⡣⣯⣾⢷⣿\r\n⣮⣯⢷⣕⠡⡪⢰⠱⡑⢌⢢⡂⢸⢈⠨⡘⢌⠪⡘⢌⢌⣞⢽⣺⣪⣾⣿⣾⣿⣿\r\n⣷⣟⣯⣯⣷⣕⡥⢱⢨⡾⣝⢾⣸⣸⡰⡪⡐⣙⠎⢎⡲⣽⡹⠺⣾⣟⣷⣟⣯⣷\r\n⣯⣿⣽⡷⣿⣞⣿⢷⣗⣟⢽⢽⡱⢗⢮⢧⡳⣱⠡⢅⠚⢮⢻⢍⠆⡋⢷⣻⣻⣽\r\n⣿⣽⣟⣿⣗⣯⢿⣯⢿⣾⢿⡿⣿⢿⣳⣟⣮⣏⣛⢾⢺⢲⣳⡵⣣⢣⡣⣳⠯⡺\r\n⣯⣿⣟⣿⢷⣻⣽⣾⡻⣟⣿⣻⣟⣿⣿⣽⣿⣻⡟⢿⢻⢿⢮⣿⣋⣯⢷⢽⢱⡏\r\n",
                "레벨 3 이상을 달성하세요! 강해지는 기분이 들 거예요!",
                "레벨 3 이상 달성",
                3, 100, "치유의 물약", Quest.ConditionType.Level
            ));

            Quests.Add(new Quest(
                "압도적인 힘으로",
                "\r\n⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢀⣤⠉⠻⣿⣿⣧⡀⠈⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⡘⡛⠫⡀⠨⣟⢻⣷⢸⣿⣿⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢿⣿⠂⣻⢸⠃⢺⡯⣾⣿⡏⠀⠿⢿⣿⢺⡝⣿⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⣿⣿⣿⠹⣿⠃⠀⡜⡁⣸⣙⣾⡇⣿⢿⡏⣿⣴⢰⣀⠀⠀⢹⣷⣻⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⣿⣿⡟⢰⣿⡆⠀⠀⠑⠟⣿⣿⣴⣿⡜⣿⣾⣿⣿⣿⣾⣶⣦⢻⡿⠫⣿⣿⣿⣿\r\n⣿⣼⣿⣿⣿⣿⣿⣿⣿⠼⣿⣷⣦⡀⢢⢰⡟⣿⣿⣿⣗⣿⣿⣿⣿⣿⣿⣿⣿⣼⠏⣹⣷⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⣿⡿⣿⡦⠱⡹⣫⣟⠀⠙⣿⢿⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣤⣹⣯⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⠏⣘⡙⠁⣸⣷⣿⣿⣤⣠⠈⠾⣿⡟⠁⢋⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⡏⠁⣼⣫⣘⣿⣿⣿⣿⣿⣿⣿⣴⣾⣁⢀⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣷⣴⣾⡾⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n",
                "공격력 20 이상을 달성하세요!",
                "공격력 20 이상 달성",
                20, 300, "마나의 물약", Quest.ConditionType.Attack
            ));

            Quests.Add(new Quest(
                "마을을 위협하는 몬스터 처치",
                "\r\n⠀⠀⠀⠀⠀⠀⡠⠖⡩⢩⢩⠫⠦⡤⡤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⡠⡼⠘⠠⡨⠊⠀⡨⢐⠌⡪⠺⢙⠖⣄⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⡼⡐⡅⠀⢅⠇⢀⠪⡐⠕⢀⡐⠌⡀⡪⢸⢱⣄⠀⠀⠀⠀\r\n⠀⠀⢸⠈⢄⠝⡄⢌⢐⢔⢕⢡⠕⡡⢂⡕⢌⠨⡨⡱⢵⠀⠀⠀⠀\r\n⠀⠀⢀⠗⡴⠵⢌⡢⠞⠵⠚⠓⠃⠉⢉⠪⢇⠗⢃⢏⣇⠀⠀⠀⠀\r\n⠀⠀⢸⢠⠧⢄⡀⢠⢀⠠⢀⡠⠤⠦⠴⡔⢇⢜⢊⡵⡽⡀⠀⠀⠀\r\n⠀⠀⡜⡜⣎⣓⣭⡾⢘⠮⢥⣮⣚⣚⣪⣞⢼⣚⡕⣏⠭⢱⠀⠀⠀\r\n⠀⠀⢇⢱⢽⠢⠭⠊⠀⠀⠑⠎⠤⠩⠔⠂⡊⡆⡯⠓⢂⠼⠀⠀⠀\r\n⠀⠀⠈⠪⣟⢠⢘⡸⡜⡘⡊⢎⢄⢄⢀⢢⢨⠣⣛⡾⣻⣺⣺⣄⠀\r\n⠀⠀⠀⠉⡳⢎⠱⠑⢔⠹⣘⠢⡓⡗⡭⡢⡫⣪⠗⣕⡗⠯⡷⡟⠀\r\n⠀⠀⠀⠰⢥⠊⠑⢪⠤⠢⠲⡕⠡⣁⢂⡷⡫⣮⡺⣞⢑⠡⠹⡆⠀\r\n⠀⠀⠀⠀⡔⠳⡨⣃⢆⢌⣆⣽⢼⢴⣻⢚⠊⢷⢟⠅⠅⠠⠩⢺⢄\r\n⠀⠀⠀⣸⣀⢊⡎⢇⠑⡙⢆⠪⢹⢙⠺⢄⣅⡽⣳⡡⠨⢐⣨⡬⠊\r\n⡤⠔⠊⡀⡌⣏⣣⡤⠬⠬⡦⠧⠵⡕⡗⡺⡜⡪⡪⣻⣍⢃⢢⠃⠀\r\n⢨⣟⣟⡿⠜⠁⢀⠇⠈⡐⠅⠡⠁⠕⡕⠐⣇⢳⣕⣕⡗⣳⠁⠀⠀\r\n⠈⢳⢯⡇⠀⠀⢸⠀⠰⡄⠂⠄⠁⢌⠢⡈⠉⠍⡃⠪⡘⡜⡄⠀⠀\r\n⠀⢸⢽⡇⠀⠀⢸⣀⣌⡇⢀⠂⡁⡐⡕⡅⡅⡐⠠⢑⠔⢕⡇⠀⠀\r\n⠀⠘⣔⡇⠀⢰⣥⣽⣷⣵⣶⣾⡖⠒⢷⣭⣭⣽⣽⣿⠏⠉⠀⠀⠀\r\n",
                "이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나?\n" +
                "마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                "모험가인 자네가 좀 처치해주게!",
                "던전 1회 클리어",
                1, 500, "슈퍼 엘릭서", Quest.ConditionType.DungeonClear
            ));
        }

        // 전체 퀘스트 출력 및 수령/완료 처리
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLineWithSpacing("※ 퀘스트 목록 ※");
                for (int i = 0; i < Quests.Count; i++)
                {
                    var quest = Quests[i];
                    string status = quest.IsComplete ? "(완료됨)" :
                                    quest.IsAccepted ? "(진행 중)" : "(미수락)";
                    RenderConsole.WriteLineWithSpacing($"{i + 1}. {quest.Name} {status}");
                }
                RenderConsole.WriteLineWithSpacing("0. 나가기");
                Console.Write("선택: ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                if (int.TryParse(input, out int idx) && idx >= 1 && idx <= Quests.Count)
                {
                    ShowQuestDetail(Quests[idx - 1]);
                }
                else
                {
                    RenderConsole.WriteLineWithSpacing("잘못된 입력입니다.");
                }

                RenderConsole.WriteLineWithSpacing("계속하려면 Enter...");
                Console.ReadLine();
            }
        }

        // 퀘스트 상세 확인 및 수락/완료 처리
        private void ShowQuestDetail(Quest quest)
        {
            Console.Clear();
            RenderConsole.WriteLineWithSpacing($"● {quest.Name}");
            RenderConsole.WriteLineWithSpacing($"{quest.Image}");
            RenderConsole.WriteLineWithSpacing($"{quest.Description}");
            RenderConsole.WriteLineWithSpacing($"조건: {quest.ConditionDescription}");
            RenderConsole.WriteLineWithSpacing($"보상: 골드 {quest.RewardGold}, 아이템 {quest.RewardItemName}");

            // 퀘스트 진행도 갱신
            UpdateQuestProgress(quest);

            if (!quest.IsAccepted)
            {
                RenderConsole.WriteLineWithSpacing("\n퀘스트를 수락하시겠습니까? (Y/N)");
                RenderConsole.WriteLineWithSpacing("\n퀘스트 수락 : 1");
                RenderConsole.WriteLineWithSpacing("퀘스트 거절 : 2\n");
                RenderConsole.Write(">> ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    quest.IsAccepted = true;
                    RenderConsole.WriteLineWithSpacing("\n퀘스트를 수락했습니다.");
                }else if (input == "2")
                {
                    RenderConsole.WriteLineWithSpacing("\n퀘스트를 거절했습니다.");
                }
                else
                {
                    RenderConsole.WriteLineWithSpacing("\n잘못된 입력입니다. 퀘스트를 받지 않았습니다.");
                }
            }
            else if (quest.IsAccepted && quest.ConditionProgress >= quest.ConditionTarget && !quest.IsComplete)
            {
                CompleteQuest(quest);
            }
            else if (quest.IsComplete)
            {
                RenderConsole.WriteLineWithSpacing("이미 완료한 퀘스트입니다.");
            }
            else
            {
                RenderConsole.WriteLineWithSpacing($"진행도: {quest.ConditionProgress} / {quest.ConditionTarget}");
            }
        }

        // 퀘스트 완료 처리
        private void CompleteQuest(Quest quest)
        {
            quest.IsComplete = true;
            Player.Gold += quest.RewardGold;

            // 아이템 보상 지급
            ItemInfo? reward = Items.ItemList.FirstOrDefault(item => item.Name == quest.RewardItemName);
            if (reward != null)
            {
                Player.Inventory.AddItem(reward.Value);
                RenderConsole.WriteLineWithSpacing($"{quest.RewardItemName}을(를) 획득했습니다!");
            }

            RenderConsole.WriteLineWithSpacing($"퀘스트 완료! 골드 {quest.RewardGold} 획득!");
        }

        // 퀘스트 진행도 갱신 (전체 업데이트용)
        public void UpdateAllQuestProgress()
        {
            foreach (var quest in Quests)
            {
                UpdateQuestProgress(quest);
            }
        }

        // 퀘스트 개별 조건 갱신
        public void UpdateQuestProgress(Quest quest)
        {
            if (!quest.IsAccepted || quest.IsComplete)
                return;

            switch (quest.Type)
            {
                case Quest.ConditionType.Level:
                    quest.ConditionProgress = Player.Level;
                    break;
                case Quest.ConditionType.Attack:
                    quest.ConditionProgress = Player.Stat.Attack + Player.ItemAttackBonus;
                    break;
                case Quest.ConditionType.DungeonClear:
                    quest.ConditionProgress = Player.DungeonCleared;
                    break;
                default:
                    break;
            }
        }
    }
}
