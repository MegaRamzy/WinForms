using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InventoryManager.Data
{
    public class Player : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public int Health { get; set; }

        public int Score { get; set; }

        [JsonProperty(PropertyName = "Inventory")]
        private List<string> InventoryNames { get; set; }
        
        [JsonIgnore]
        public List<Item> Inventory { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Player()
        {
            InventoryNames = new List<string>();
            Inventory = new List<Item>();
        }

        public void BuildInventoryFromName(List<Item> items)
        {
            Inventory = (from itemName in InventoryNames
                         let item = items.Find(i => i.Name.Equals(itemName, System.StringComparison.InvariantCultureIgnoreCase))
                         where item != null
                         select item).ToList();
        }
        public override string ToString() => Name;
    }
}
