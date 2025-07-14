using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace TxtRPG
{

    internal class SaveLoad
    {
        int select = 0;

        string set = "";
        public string filepath = $@"C:\Users\{Environment.UserName}\Downloads\TxtRPG.txt";

        public void save_view()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("< 데이터 저장 >");
                Console.ResetColor();
                Console.WriteLine("이곳에서 데이터를 저장 할 수 있습니다\n");
                Console.WriteLine("1. 저장");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요\n>> ");

                set = Console.ReadLine();

                if (int.TryParse(set, out select))
                {
                    select = int.Parse(set);
                }

                if (select == 0) break;

                switch (select)
                {
                    case 1:
                        save();
                        break;
                    default:
                        OutRangeError.check();
                        continue;
                }
            }
        }

        public void save()
        {
            List<string> list = Main_Stage.status.Player_Status_Save();
            
            File.WriteAllLines(filepath, list);

            Console.WriteLine("\n저장이 완료되었습니다.");
            Thread.Sleep(1000);
        }

        public void load()
        {
            List<string> loads = File.ReadAllLines(filepath).ToList();
            int count = 0;
            foreach (string a in loads)
            {
                if (!a.StartsWith("i_"))
                {
                    string[] splits = a.Split("=");

                    switch (splits[0])
                    {
                        case "level":
                            Player.Instance.level = int.Parse(splits[1]);
                            break;
                        case "exp":
                            Player.Instance.exp = int.Parse(splits[1]);
                            break;
                        case "max_hp":
                            Player.Instance.max_hp = float.Parse(splits[1]);
                            break;
                        case "now_hp":
                            Player.Instance.now_hp = float.Parse(splits[1]);
                            break;
                        case "att":
                            Player.Instance.att = float.Parse(splits[1]);
                            break;
                        case "def":
                            Player.Instance.def = float.Parse(splits[1]);
                            break;
                        case "gold":
                            Player.Instance.gold = int.Parse(splits[1]);
                            break;
                        case "job":
                            Player.Instance.job = splits[1];
                            break;
                        case "att_we":
                            Player.Instance.att_we = bool.Parse(splits[1]);
                            break;
                        case "def_def":
                            Player.Instance.def_def = bool.Parse(splits[1]);
                            break;
                        case "add_att":
                            Player.Instance.add_att = float.Parse(splits[1]);
                            break;
                        case "add_def":
                            Player.Instance.add_def = float.Parse(splits[1]);
                            break;
                    }
                }
                else
                {
                    string[] x = a.Split(",");

                    string name = "", job = "", ex = "";
                    int level = 0, att = 0, def = 0, gold = 0;
                    float hp = 0;
                    bool sell = false, equip = false;
                    
                    foreach (string add in x)
                    {
                        string[] final_split = add.Split("=");

                        switch (final_split[0]) 
                        {
                            case "i_name":
                                name = final_split[1];
                                break;
                            case "i_level":
                                level = int.Parse(final_split[1]);
                                break;
                            case "i_att":
                                att = int.Parse(final_split[1]);
                                break;
                            case "i_def":
                                def = int.Parse(final_split[1]);
                                break;
                            case "i_gold":
                                gold = int.Parse(final_split[1]);
                                break;
                            case "i_hp":
                                hp = float.Parse(final_split[1]);
                                break;
                            case "i_job":
                                job = final_split[1];
                                break;
                            case "i_ex":
                                ex = final_split[1];
                                break;
                            case "i_sell":
                                sell = bool.Parse(final_split[1]);
                                break;
                            case "i_equip":
                                equip = bool.Parse(final_split[1]);
                                break;
                        }

                    }
                    Item_List.inventory.Add(new Item(name, level, att, def, gold, hp, job, ex, sell, equip));
                    if (sell) Item_List.inventory[count].sell = true;
                    if (equip) Item_List.inventory[count].equip = true;
                    //Console.WriteLine(Item_List.inventory[0].sell + " 확인용 " + sell);
                    count++;
                }
            }
            for (int i = 0; i < Item_List.inventory.Count; i++)
                if (Item_List.inventory[i].sell)
                    Item_List.item_list.Find(item => item.name == Item_List.inventory[i].name).sell = true;

            Console.WriteLine("\n불러오기가 완료되었습니다.");
            //Console.WriteLine(Item_List.inventory[0].sell +" 확인용2 " + Item_List.inventory[0].equip);
            Thread.Sleep(2500);
        }

    }

}
