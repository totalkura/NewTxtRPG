using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Legacy.Gwangho
{
    internal class Load
    {
        public static void Loadgame()
        {
            if (!File.Exists("save.txt"))
            {
                Console.WriteLine("저장된 게임이 없습니다.");
                Thread.Sleep(1000);
                return;
            }

            using (StreamReader reader = new StreamReader("save.txt"))
            {
                level = int.Parse(reader.ReadLine());
                atk = float.Parse(reader.ReadLine());
                def = int.Parse(reader.ReadLine());
                hp = int.Parse(reader.ReadLine());
                gold = int.Parse(reader.ReadLine());
                exp = int.Parse(reader.ReadLine());
                maxexp = int.Parse(reader.ReadLine());
                nickname = reader.ReadLine();
                job = reader.ReadLine();

                string weaponName = reader.ReadLine();
                string armorName = reader.ReadLine();

                int invCount = int.Parse(reader.ReadLine());
                inventory.Clear();
                for (int i = 0; i < invCount; i++)
                {
                    string itemName = reader.ReadLine();
                    var item = storeItems.Find(x => x.Name == itemName);
                    if (item != null) inventory.Add(item);
                }

                equippedWeapon = inventory.Find(x => x.Name == weaponName);
                equippedArmor = inventory.Find(x => x.Name == armorName);
            }

            Console.WriteLine("게임을 불러왔습니다.");
            Thread.Sleep(1000);
        }
    }
}
