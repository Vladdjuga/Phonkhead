using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helpers
{
    [Serializable]
    public class TileObjectHelper
    {
        public string sprite;
        public bool is_breakable;
        public bool is_bg;
        public int def;
        public Vector3Helper position;
        public TileObjectHelper(TileObject exp)
        {
            if (exp != null)
            {
                this.sprite = exp.sprite;
                this.is_breakable = exp.is_breakable;
                this.is_bg = exp.is_bg;
                this.def = exp.def;
                this.position = new Vector3Helper(exp.position.x, exp.position.y, exp.position.z);
            }
        }
    }
}
