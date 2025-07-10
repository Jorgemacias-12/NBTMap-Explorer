namespace NBTMap_Explorer.Models
{
    public class MinecraftWorld
    {
        public string Name { get; init; }
        public string Path { get; init; }
        public string Version { get; init; } 
        public string SplashIconPath { get; init; }
        public bool HasMaps { get; init; }
        public List<MinecraftMap> Maps { get; init; } = [];
    }
}
