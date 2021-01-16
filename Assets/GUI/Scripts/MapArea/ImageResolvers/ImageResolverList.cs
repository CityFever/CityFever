using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.GUI.Scripts.MapArea.ImageResolvers;
using UnityEngine;

namespace Assets.GUI.Scripts.MapArea
{
    public class ImageResolverList : MonoBehaviour
    {
        [SerializeField] public List<ImageResolverItem> imageResolverItems;
    }
}
