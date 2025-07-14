using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace NewTxtRPG.Gwangho
{
    
    static class ItemDrop
    {
        static void TryDropPotion()
        {
            Random rand = new Random();
            int dropChance = rand.Next(0, 100); // 0~99

            if (dropChance < 30) // 30% 확률
            {
                var droppablePotions = PotionList.potionlist
            .Where(p => p.name != "슈퍼 엘릭서")
            .ToList();

                int index = rand.Next(droppablePotions.Count);

                Potion dropped = PotionList.potionlist[index];
                PotionList.inventory.Add(dropped);

                Console.WriteLine($"당신은 '{dropped.name}' 포션을 획득했습니다!");
            }
            else
            {
                Console.WriteLine("포션을 획득하지 못했습니다.");
            }
        }

    }
}

