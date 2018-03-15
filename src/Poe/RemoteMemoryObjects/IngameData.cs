using System;
using System.Text;
using PoeHUD.Controllers;
using PoeHUD.Poe.FilesInMemory;
using System.Collections.Generic;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class IngameData : RemoteMemoryObject
    {
        public AreaTemplate CurrentArea => ReadObject<AreaTemplate>(Address + 0x28);
        public WorldArea CurrentWorldArea => GameController.Instance.Files.WorldAreas.GetByAddress(M.ReadLong(Address + 0x28));
        public int CurrentAreaLevel => (int)M.ReadByte(Address + 0x40);
        public uint CurrentAreaHash => M.ReadUInt(Address + 0x60);

        public Entity LocalPlayer => GameController.Instance.Cache.Enable && GameController.Instance.Cache.LocalPlayer != null
            ? GameController.Instance.Cache.LocalPlayer 
            : GameController.Instance.Cache.Enable? GameController.Instance.Cache.LocalPlayer=LocalPlayerReal: LocalPlayerReal;
        private Entity LocalPlayerReal => ReadObject<Entity>(Address + 0x228);
        public EntityList EntityList => GetObject<EntityList>(Address + 0x2D8);

        private long LabDataPtr => M.ReadLong(Address + 0x70);
        public LabyrinthData LabyrinthData => LabDataPtr == 0 ? null : GetObject<LabyrinthData>(LabDataPtr);
    }
}