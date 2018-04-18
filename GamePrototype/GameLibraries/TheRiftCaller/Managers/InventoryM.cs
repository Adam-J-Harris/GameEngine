using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Objects.Entities.Interfaces;

namespace TheRiftCaller.Managers
{
    public class InventoryM
    {
        private static InventoryM instance;

        public IDictionary<string, ISmartObject> inv = new Dictionary<string, ISmartObject>();

        private InventoryM()
        {

        }

        public static InventoryM getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InventoryM();
                }
                return instance;
            }
        }
    }
}
