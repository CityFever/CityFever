using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GUI.Scripts.MapArea.ImageResolvers
{
    public class ImageResolverItem : MonoBehaviour
    {
        [SerializeField] public GameObjectType Type;
        [SerializeField] public Sprite Sprite;
    }
}
