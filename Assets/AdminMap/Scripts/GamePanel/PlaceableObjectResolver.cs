using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AdminMap.Scripts.GamePanel
{
    public class PlaceableObjectResolver
    {
        private List<UnityObject> PlaceableObjects { get; set; }
        void Start()
        {
            PlaceableObjects = new List<UnityObject>();
        }

        public bool TryAddObject(UnityObject prefab)
        {
            if (!ContainsObject(prefab))
            {
                PlaceableObjects.Add(prefab);
                return true;
            }

            return false;
        }

        public bool TryRemoveObject(UnityObject prefab)
        {
            if (ContainsObject(prefab))
            {
                PlaceableObjects.Remove(prefab);
                return true;
            }

            return false;
        }

        private bool ContainsObject(UnityObject prefab)
        {
            return PlaceableObjects.Exists(o => o.GetType() == prefab.GetType());
        }

        public int GetObjectCount()
        {
            return PlaceableObjects.Count;
        }
    }
}
