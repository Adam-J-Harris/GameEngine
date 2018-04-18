using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Engine.Objects.Entities;

namespace TheRiftCaller.Objects.Entities
{
    public class Filter : AEntity
    {
        public Filter()
        {
            getSetName = "Filter";
            getSetTexture = ImageM.getInstance.getAsset("RFilterLight");
           // getSetTexture = ImageM.getInstance.getAsset("RFilterTorch");           
        }
    }
}
