using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OcclusionGenerator {
    public class AudioGameData {
        public int ver = 25564071;
        public List<ItemEntry> Items { get; }

        public AudioGameData(MloInterior interior) {
            Items = new List<ItemEntry>();

        }

        public static void SaveGameDatXml(string directory, string fileName, MloInterior interior) {
            AudioGameData audioGameData = new AudioGameData(interior);
            InteriorEntry interiorEntry = new InteriorEntry();
            List<RoomEntry> roomEntries = new List<RoomEntry>();
            interiorEntry.name = interior.name;



            for (int i = 0; i < interior.Rooms.Count; i++) {
                RoomEntry roomEntry = new RoomEntry();
                roomEntry.name = "prefix_int_room_" + interior.Rooms[i].name;
                roomEntry.mloRoom = interior.Rooms[i].name;

                interiorEntry.Items.Add(roomEntry);
                roomEntries.Add(roomEntry);

            }
            audioGameData.Items.Add(interiorEntry);
            for (int i = 0; i < roomEntries.Count; i++) {
                audioGameData.Items.Add(roomEntries[i]);
            }

            Version version = new Version();
            XElement gameDatFile = new XElement("Dat151");
            gameDatFile.Add(version.XML());
            gameDatFile.Add(audioGameData.XML());
            gameDatFile.Save(Path.Combine(directory, fileName + ".dat151.rel.xml"));
        }

        public XElement XML() {
            XElement itemList = new XElement("Items");

            for (int i = 0; i < Items.Count; i++) {
                itemList.Add(Items[i].XML());
            }


            return itemList;
        }
    }

    public class Version {
        public int ver = 25564071;
        public XElement XML() {
            return new XElement("Version",
                new XAttribute("value", ver)
            );
        }
    }


    public class ItemEntry {
        public virtual XElement XML() {
            XElement item = new XElement("Item",
               new XAttribute("type", "Unknown"),
               new XAttribute("ntOffset", "0")
           );
            return item;
        }
    }

    public class RoomEntry : ItemEntry {
        public string name;
        public string mloRoom;
        public override XElement XML() {
            XElement roomItem = new XElement("Item", new XAttribute("type", "InteriorRoom"), new XAttribute("ntOffset", "0"),
            new XElement("Name", name),
            new XElement("Flags0", new XAttribute("value", "0xAAAAAAAA")),
            new XElement("MloRoom", mloRoom),
            new XElement("Hash1"),
            new XElement("Unk02", new XAttribute("value", "0")),
            new XElement("Unk03", new XAttribute("value", "0.5")),
            new XElement("Unk04", new XAttribute("value", "0.05")),
            new XElement("Unk05", new XAttribute("value", "0")),
            new XElement("Unk06", "hash_25B9E10A"),
            new XElement("Unk07", new XAttribute("value", "0")),
            new XElement("Unk08", new XAttribute("value", "0")),
            new XElement("Unk09", new XAttribute("value", "0")),
            new XElement("Unk10", new XAttribute("value", "0.65")),
            new XElement("Unk11", new XAttribute("value", "0")),
            new XElement("Unk12", new XAttribute("value", "20")),
            new XElement("Unk13", "hash_43BD9A63"),
            new XElement("Unk14", "hash_D4855127")
            );
            return roomItem;
        }
    }

    public class InteriorEntry : ItemEntry {
        public string name;
        public string Unk00;
        public string Unk01;
        public string Unk02;
        public List<RoomEntry> Items;
        public InteriorEntry() {
            Unk00 = "0xAAAAA044";
            Unk01 = "0xD4855127";
            Unk02 = "0x00000000";
            Items = new List<RoomEntry>();
        }
        public override XElement XML() {
            XElement item = new XElement("Item", new XAttribute("type", "Interior"), new XAttribute("ntOffset", "0"),
            new XElement("Name", name
            ),
            new XElement("Unk0",
                new XAttribute("value", Unk00)
            ),
            new XElement("Unk1",
                new XAttribute("value", Unk01)
            ),
            new XElement("Unk2",
                new XAttribute("value", Unk02)
            ),
             new XElement("Rooms"
            )
           );

            for (int i = 0; i < Items.Count; i++) {
                XElement roomItem = new XElement("Item", Items[i].name);
                item.Element("Rooms").Add(roomItem);
            }


            return item;
        }
    }
}
