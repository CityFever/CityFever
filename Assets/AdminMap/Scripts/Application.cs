using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdminMap.Scripts
{
    public class Application : MonoBehaviour
    {
        public static Application application;

        public GameObjectType SelectedGameObjectType { get; set; } = GameObjectType.Default;
        public string SelectedAdminMapId { get; set; }

        public string SelectedUserMadId { get; set; }

        public MapConfig SelectedMapConfig { get; set; }

        public string SelectedUserMapId { get; set; }

        public bool IsAdminMapShown; 

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (application == null)
            {
                application = this;
            }
            else if (application != this)
            {
                Destroy(gameObject);
            }
        }

        public void SetDefaultType()
        {
            SelectedGameObjectType = GameObjectType.Default;
        }

        public void ShowAdminMaps(bool isAdminMapShown)
        {
            Debug.Log("isAdminMapShown: " + isAdminMapShown);
            IsAdminMapShown = isAdminMapShown;
        }
    }
}
