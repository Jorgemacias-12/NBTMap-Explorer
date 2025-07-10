using fNbt;
using Serilog;
using NBTMap_Explorer.Helpers;
using NBTMap_Explorer.Models;
using NBTMap_Explorer.Services.Contracts;
using System.IO;

namespace NBTMap_Explorer.Services
{
    public class JavaWorldLoader : IWorldLoader
    {
        private static readonly ILogger _log = Log.ForContext<JavaWorldLoader>();

        public Task<List<MinecraftWorld>> LoadAllWorldsAsync()
        {
            return Task.Run(() =>
            {
                if (!Directory.Exists(Minecraft.JAVA_SAVES_PATH))
                {
                    return new List<MinecraftWorld>();
                }

                string[] worldDirectories = Directory.GetDirectories(Minecraft.JAVA_SAVES_PATH);

                var worlds = new List<MinecraftWorld>();

                const string splashIconFileName = "icon.png";
                const string levelDatFileName = "level.dat";

                foreach (var dir in worldDirectories)
                {
                    string levelDatPath = Path.Combine(dir, levelDatFileName);
                    string splashIconPath = Path.Combine(dir, splashIconFileName);
                    bool IsValidWorld = File.Exists(levelDatPath);

                    if (!IsValidWorld)
                    {
                        continue;
                    }

                    try
                    {
                        var worldData = new NbtFile();

                        worldData.LoadFromFile(levelDatPath);

                        string worldVersion = worldData.RootTag?
                                                       .Get<NbtCompound>("Data")?
                                                       .Get<NbtCompound>("Version")?
                                                       .Get<NbtString>("Name")?.Value ?? "Unknown";

                        var world = new MinecraftWorld
                        {
                            Name = Path.GetFileName(dir),
                            Path = dir,
                            Version = worldVersion,
                            SplashIconPath = File.Exists(splashIconPath) ? splashIconPath : string.Empty,
                        };

                        worlds.Add(world);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error reading world data from {LevelDatPath} {e}", levelDatPath, e);
                        continue;
                    }
                }

                return worlds;
            });
        }

        public Task<MinecraftWorld?> LoadWorldAsync(string worldPath)
        {
            if (String.IsNullOrEmpty(worldPath) || !Directory.Exists(worldPath))
            {
                return Task.FromResult<MinecraftWorld?>(null);
            }

            return Task.Run(() =>
            {
                const string splashIconFileName = "icon.png";
                const string levelDatFileName = "level.dat";

                string levelDatPath = Path.Combine(worldPath, levelDatFileName);
                string splashIconPath = Path.Combine(worldPath, splashIconFileName);
                bool IsValidWorld = File.Exists(levelDatPath);

                if (!IsValidWorld)
                {
                    return null;
                }
                
                try
                {
                    var worldData = new NbtFile();
                    
                    worldData.LoadFromFile(levelDatPath);
                    
                    string worldVersion = worldData.RootTag?
                                                       .Get<NbtCompound>("Data")?
                                                       .Get<NbtCompound>("Version")?
                                                       .Get<NbtString>("Name")?.Value ?? "Unknown";
                    var world = new MinecraftWorld
                    {
                        Name = Path.GetFileName(worldPath),
                        Path = worldPath,
                        Version = worldVersion,
                        SplashIconPath = File.Exists(splashIconPath) ? splashIconPath : string.Empty,
                    };
                    
                    return world;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
    }
}