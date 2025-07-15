//using System;
//using System.Collections.Generic;

//namespace NewTxtRPG.Hyunsu
//{
//    public class DungeonEvent
//    {
//        public string Description; // 상황 묘사
//        public string Option1;     // 선택지 1
//        public string Option2;     // 선택지 2
//        public Action<Player> Option1Result; // 선택 결과 1 실행할 코드
//        public Action<Player> Option2Result; // 선택 결과 2 실행할 코드

//        public DungeonEvent(string description, string option1, string option2, Action<Player> option1Result, Action<Player> option2Result)
//        {
//            Description = description;
//            Option1 = option1;
//            Option2 = option2;
//            Option1Result = option1Result;
//            Option2Result = option2Result;
//        }
//    }
//    public class DungeonEventManager
//    {
//        private List<DungeonEvent> events = new List<DungeonEvent>();
//        private Random rand = new Random();

//        public DungeonEventManager()
//        {
//            events.Add(new DungeonEvent(
//                "앞에 맑은 샘물이 보입니다. 당신은 참을 수 없는 갈증을 느끼고 있습니다. 샘물을 마시겠습니까?",
//                "1. 마신다",
//                "2. 마시지 않는다",
//                (player) => {
//                    int chance = rand.Next(0, 2); // 0 또는 1
//                    if (chance == 0)
//                    {
//                        Console.WriteLine("샘물을 마시고 체력이 회복되었습니다! HP +10");
//                        player.Hp += 10;
//                    }
//                    else
//                    {
//                        Console.WriteLine("샘물이 썩어 있었습니다... 식중독! HP -5");
//                        player.Hp -= 5;
//                    }
//                },
//                (player) => {
//                    Console.WriteLine("당신은 샘물을 마시지 않았습니다. 아무 일도 일어나지 않았습니다.");
//                }
//            ));

//            events.Add(new DungeonEvent(
//                "낡은 상자가 눈앞에 놓여 있습니다. 당신은 묘한 위화감을 느끼고 있습니다..열어보시겠습니까?",
//                "1. 연다",
//                "2. 그냥 간다",
//                (player) => {
//                    int chance = rand.Next(0, 2);
//                    if (chance == 0)
//                    {
//                        Console.WriteLine("상자 안에서 300G를 발견했습니다! Gold +300");
//                        player.Gold += 300;
//                    }
//                    else
//                    {
//                        Console.WriteLine("이런..! 미믹의 공격으로 체력이 감소했습니다! HP -10");
//                        player.Hp -= 10;
//                    }
//                },
//                (player) => {
//                    Console.WriteLine("상자를 무시하고 지나갔습니다. 아무 일도 일어나지 않았습니다.");
//                }
//            ));

//            events.Add(new DungeonEvent(
//                "당신은 허름한 집을 발견합니다. 집주인이 당신에게 호의를 보입니다. \n" +
//                "“잠깐 쉬었다 가시겠어요?”",
//                "1. 휴식을 취한다.",
//                "2. 그냥 간다",
//                (player) => {
//                    int chance = rand.Next(0, 2);
//                    if (chance == 0)
//                    {
//                        Console.WriteLine("휴식을 취해 피로를 회복하였습니다. 몸이 개운해졌습니다! HP +15");
//                        player.Hp += 10;
//                    }
//                    else
//                    {
//                        Console.WriteLine("휴식을 취해 피로를 회복하였습니다. 어째서인지 당신의 가방이 가벼워진 느낌이 듭니다.. Gold -100");
//                        if (player.Gold > 0)
//                        {
//                            player.Gold -= 100;
//                        }
//                    }
//                },
//                (player) => {
//                    Console.WriteLine("집주인의 호의를 무시하고 지나갔습니다. 아무 일도 일어나지 않았습니다.");
//                }
//            ));

//            events.Add(new DungeonEvent(
//                "떠돌이 도박꾼이 말을 겁니다. \n" +
//                "“나랑 도박 한 번 해보겠어? 동전의 앞면이 나오면 자네가 300G 를, 뒷면이 나오면 내가 300G 를 가지는 걸로 하지..크크”",
//                "1. 도박을 한다.",
//                "2. 그냥 간다",
//                (player) => {
//                    int chance = rand.Next(0, 2);
//                    if (chance == 0)
//                    {
//                        if (player.Gold < 300)
//                            Console.WriteLine("뭐야?! 판돈도 없으면서 도박을 하겠다고 한거야?! 저리 꺼져!");
//                        else
//                        {
//                            Console.WriteLine("앞면이 나왔습니다! 당신은 300G 를 획득했습니다! Gold +300");
//                            player.Gold += 300;
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("뒷면이 나왔습니다.. 당신은 도박꾼에게 300G 를 넘겼습니다. Gold -300");
//                        if (player.Gold > 0)
//                        {
//                            player.Gold -= 300;
//                        }
//                    }
//                },
//                (player) => {
//                    Console.WriteLine("도박꾼의 회유를 무시하고 지나갔습니다. 아무 일도 일어나지 않았습니다.");
//                }
//            ));

//            events.Add(new DungeonEvent(
//                "던전 입구 근처에 죽은 모험가의 유해가 보입니다. 조사하시겠습니까?",
//                "1. 조사한다.",
//                "2. 그냥 간다",
//                (player) => {
//                    int chance = rand.Next(0, 2);
//                    if (chance == 0)
//                    {
//                        Console.WriteLine("유해를 땅에 묻어주던 중 죽은 모험가의 가방속에서 200G 를 발견했습니다! Gold +200G");
//                        player.Gold += 200;
//                    }
//                    else
//                    {
//                        Console.WriteLine("주변에서 거미떼가 몰려옵니다..! 당신은 도망치면서 피해를 입었습니다. HP -5");
//                        player.Hp -= 5;
//                    }
//                },
//                (player) => {
//                    Console.WriteLine("당신은 조사하지 않도록 결정했습니다. 아무 일도 일어나지 않았습니다.");
//                }
//            ));

//        }

//        public void TriggerRandomEvent(Player player)
//        {
//            if (events.Count == 0)
//            {
//                Console.WriteLine("이제 더 이상 남은 이벤트가 없습니다.");
//                Console.ReadLine();
//                return;
//            }

//            int index = rand.Next(events.Count);
//            DungeonEvent e = events[index];
//            events.RemoveAt(index);

//            Console.Clear();
//            Console.WriteLine($"[이벤트 발생]\n{e.Description}\n");
//            Console.WriteLine(e.Option1);
//            Console.WriteLine(e.Option2);

//            string choice = "";

//            // 올바른 선택을 할 때까지 반복
//            while (true)
//            {
//                Console.Write("\n>> ");
//                choice = Console.ReadLine();

//                if (choice == "1")
//                {
//                    e.Option1Result(player);
//                    break;
//                }
//                else if (choice == "2")
//                {
//                    e.Option2Result(player);
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine("\n잘못된 입력입니다. 다시 선택해주세요.");
//                }
//            }
//            Console.WriteLine("\n엔터를 누르면 계속...");
//            Console.ReadLine();
//        }
//    }
//}
