using System.Collections.Generic;
using System.Diagnostics;

namespace AoC._2019.Day06
{
    [DebuggerDisplay("{Name} {Level}")]
    public class SpaceObject
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public HashSet<SpaceObject> DirectOrbits { get; set; }

        public SpaceObject(string name, int level)
        {
            Name = name;
            Level = level;
            DirectOrbits = new HashSet<SpaceObject>();
        }
    }
}
