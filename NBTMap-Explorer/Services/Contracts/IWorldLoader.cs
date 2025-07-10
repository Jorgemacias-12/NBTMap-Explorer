using NBTMap_Explorer.Models;

namespace NBTMap_Explorer.Services.Contracts
{
    public interface IWorldLoader
    {
        Task<MinecraftWorld?> LoadWorldAsync(string worldPath);
        Task<List<MinecraftWorld>> LoadAllWorldsAsync();
    }
}
