using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Gwangho
{
    internal class Item
    {
        public string Name
        {
            get;
            set;
        }
        public string itemPower
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }

        public Item(string name, string power, string desc, int price)
        {
            Name = name;
            itemPower = power;
            Description = desc;
            Price = price;
        }


    }

    static List<Item> inventory = new List<Item>();
    static List<Item> storeItems = new List<Item>();

    static Item equippedWeapon = null;
    static Item equippedArmor = null;
}
