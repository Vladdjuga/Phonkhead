using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helpers
{
    [Serializable]
    public class TileMapHelper
    {
        public TileObjectHelper[][] tiles;
        public TileMapHelper(TileMap exp)
        {
            tiles = new TileObjectHelper[exp.tiles.Length][];
            for (int i = 0; i < exp.tiles.Length; i++)
            {
                tiles[i] = new TileObjectHelper[exp.tiles[i].Length];
                for (int j = 0; j < exp.tiles[i].Length; j++)
                {
                    tiles[i][j] = new TileObjectHelper(exp.tiles[i][j]);
                }
            }
        }
    }
}
