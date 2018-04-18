using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Maps;

namespace TheRiftCaller.Maps
{
    public class Map : AMap
    {
        public Map()
        {
            
        }

        public override void SetTextMaps()
        {
            getSetMapTextPath = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), getSetMergedFilePath));
        }

        public override void RebuildTree()
        {
            base.RebuildTree();
        }
    }
}
