using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helpers
{
    [Serializable]
    public struct Vector3Helper
    {
        public float x;
        public float y;
        public float z;
        public Vector3Helper(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
