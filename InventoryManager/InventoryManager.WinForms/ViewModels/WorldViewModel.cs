using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManager.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.IO;

namespace InventoryManager.WinForms.ViewModels
{
    public class WorldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string FileName { get; set; }
        public BindingList<Player> Players { get; set; }
        public BindingList<Item> Items { get; set; }

        public World World
        {
            set
            {
                if (mWorld != value)
                {
                    mWorld = value;
                    if (mWorld != null)
                    {

                        Players = new BindingList<Player>(mWorld.Players);
                        Items = new BindingList<Item>(mWorld.Items);
                    }
                    else
                    {
                        Players = new BindingList<Player>(Array.Empty<Player>());
                        Items = new BindingList<Item>(Array.Empty<Item>());
                    }
                }
            }
        }

        public WorldViewModel(World world = null)
        {
            World = world;
        }

        public void SaveWorld()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new InvalidProgramException("Filename expected.");
            }

            JsonSerializer serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
            using (StreamWriter streamWriter = new StreamWriter(FileName))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, mWorld);
            }
        }

        private World mWorld;
    }
}
