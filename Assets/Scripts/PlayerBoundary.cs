using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [System.Serializable]
    public class PlayerBoundary
    {
        public float xMax, yMax;

        public float xMin { get { return -xMax; } }
        public float yMin { get { return -yMax; } }
    }
}
