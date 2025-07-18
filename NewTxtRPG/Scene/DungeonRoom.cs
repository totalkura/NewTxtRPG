using NewTxtRPG.Entitys;
using NewTxtRPG.etc;

namespace NewTxtRPG.Scene
{
    public class DungeonRooms
    {
        public int roomX, roomY;

        public DungeonRooms(int x, int y)
        {
            roomX = x;
            roomY = y;
        }
    };
    public class DungeonRoom
    {
        Random rand = new Random();

        DungeonRoom[,] dungeonRooms;

        int width;
        int height;        
        int playerX, playerY;
        int bossX, bossY;

        bool[,] visit;

        bool vistiBoss;

        public void Move(string difficult)
        {
            DungeonScene dungeon = new DungeonScene();
            DungeonEventManager dungeonEvent = new DungeonEventManager();
            vistiBoss = false;

            while (true)
            {
                Console.Clear();
                RenderConsole.WriteLine("< 던전 지도 >");
                MapView();
                RenderConsole.WriteEmptyLine();

                if (playerX == bossX && playerY == bossY)
                {
                    RenderConsole.WriteEmptyLine();
                    RenderConsole.WriteLine("< < < 보스를 발견하였습니다 > > >".PadLeft(3),ConsoleColor.Red);
                    vistiBoss = true;
                    Thread.Sleep(1500);
                    dungeon.BossBattle(difficult);
                    

                    break;
                }

                RenderConsole.Write("이동하세요 ( 1. 위 2. 아래 3. 왼쪽 4. 오른쪽 )\n>>");
                string write = Console.ReadLine();

                int playerMoveX = playerX, playerMoveY = playerY;

                switch (write)
                {
                    case "1":
                        playerMoveY--;
                        break;
                    case "2":
                        playerMoveY++;
                        break;
                    case "3":
                        playerMoveX--;
                        break;
                    case "4":
                        playerMoveX++;
                        break;
                    default:
                        RenderConsole.WriteLine("다시 입력하세요");
                        Thread.Sleep(1000);
                        continue;
                }

                if (playerMoveX >= 0 &&
                    playerMoveX < width &&
                    playerMoveY >= 0 &&
                    playerMoveY < height)
                {
                    playerX = playerMoveX;
                    playerY = playerMoveY;
                }
                else
                {
                    RenderConsole.WriteLine("그곳으로는 갈 수 없습니다");
                    Thread.Sleep(1000);
                    continue;
                }

                if (!visit[playerMoveX, playerMoveY])
                {
                    //이곳에 이벤트 및 배틀 추가
                    //재방문 지역 이벤트 및 배틀 X
                    int RandomEvent = rand.Next(0, 100);

                    if (RandomEvent < 20)
                        dungeonEvent.TriggerRandomEvent();
                    else if (RandomEvent >= 20 && RandomEvent < 80 && difficult == "1")
                        dungeon.Battle(difficult);
                    else if (RandomEvent >= 20 && RandomEvent < 85 && difficult == "2")
                        dungeon.Battle(difficult);
                    else if (RandomEvent >= 20 && RandomEvent < 90 && difficult == "3")
                        dungeon.Battle(difficult);
                    visit[playerMoveX, playerMoveY] = true;
                }

                if (Player.CurrentHP <= 0)
                {
                    dungeon.Lose();
                    break;
                }
            }
        }

        public void CreateMap(string difficult)
        {
            switch (difficult)
            {
                case "1":
                    width = 3;
                    height = 3;
                    break;
                case "2":
                    width = 4;
                    height = 5;
                    break;
                case "3":
                    width = 8;
                    height = 9;
                    break;
                default:
                    width = 5;
                    height = 5;
                    break;
            }

            dungeonRooms = new DungeonRoom[width, height];
            visit = new bool[width, height];

            playerX = rand.Next(0, width);
            playerY = rand.Next(0, height);

            do
            {
                bossX = rand.Next(0, width);
                bossY = rand.Next(0, height);
            } while (bossX == playerX && bossY == playerY);
            visit[playerX, playerY] = true;
            visit[bossX, bossY] = true;

        }

        private void MapView()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            RenderConsole.WriteLine("던전 맵 (★ : 플레이어, ※ : 보스)\n");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0)
                        Console.Write("       ");

                    if (x == playerX && y == playerY)
                        Console.Write("★ ");
                    else if (x == bossX && y == bossY)
                        Console.Write("※ ");
                    else
                        Console.Write("▒▒");
                }
                Console.WriteLine();
            }
        }

    };

    
}
