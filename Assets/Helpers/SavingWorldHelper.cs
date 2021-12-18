using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Helpers
{
    class SavingWorldHelper
    {
        public static void Save(TileMap tileMap, string world_name)
        {
            string assetsFolderPath = Application.dataPath;
            string levelFolder = assetsFolderPath + "/Resources/worlds/";
            if (!Directory.Exists(levelFolder))
            {
                Directory.CreateDirectory(levelFolder);
            }
            string world = levelFolder + world_name + ".voxelwbf";
            using (var file = File.Open(world, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                TileMapHelper helper = new TileMapHelper(tileMap);
                formatter.Serialize(file, helper);
            }
        }
        public static TileMap Deserialize(string world_name)
        {
            string assetsFolderPath = Application.dataPath;
            string levelFolder = assetsFolderPath + "/Resources/worlds/";
            string world = levelFolder + world_name + ".voxelwbf";
            using (var file = File.Open(world, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                TileMapHelper helper= (TileMapHelper)formatter.Deserialize(file);
                TileMap tileMap=new TileMap(helper);
                return tileMap;
            }
        }
    }
}
