using NBTMap_Explorer.Helpers;
using NBTMap_Explorer.Models;
using NBTMap_Explorer.Services.Contracts;
using System.IO;
using System.Windows;

namespace NBTMap_Explorer.Services
{
    public class BedrockWorldLoader : IWorldLoader
    {
        public Task<List<MinecraftWorld>> LoadAllWorldsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MinecraftWorld?> LoadWorldAsync(string worldPath)
        {
            throw new NotImplementedException();
        }
    }
}
