using fNbt;
using NBTMap_Explorer.Helpers;
using NBTMap_Explorer.Models;
using NBTMap_Explorer.Services.Contracts;
using System.IO;

namespace NBTMap_Explorer.Services
{
    public class JavaMapLoader : IMapLoader
    {
        public Task<MinecraftMap[]> LoadMapsAsync(string worldPaths)
        {
            throw new NotImplementedException();
        }
    }
}
