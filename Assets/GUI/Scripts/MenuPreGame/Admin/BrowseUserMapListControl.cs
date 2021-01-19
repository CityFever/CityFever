using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using UnityEngine;

namespace Assets.GUI.Scripts.MenuPreGame.Admin
{
    public class BrowseUserMapListControl : MonoBehaviour
    {
        [SerializeField] private GameObject mapButtonTemplate;
        
        private List<GameObject> mapButtons;
        private List<string> userMapIds = new List<string>();
        private List<MapConfig> maps = new List<MapConfig>();

        void Start()
        {
            Debug.Log("USER MAP BrowseUserMapListControl");
            mapButtons = new List<GameObject>();

            if (mapButtons.Count > 0)
            {
                foreach (GameObject button in mapButtons)
                {
                    Destroy(button.gameObject);
                }
                mapButtons.Clear();
            }
        }

        public void FetchData()
        {
            ClearData();
            FetchUserMapIds();
        }

        private void FetchUserMapIds()
        {
            UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () =>
            {
                Debug.Log("start FetchUserMapIds");

                MapsRepository.GetAllUsersMapIds((list) =>
                {
                    foreach (var mapId in list)
                    {
                        userMapIds.Add(mapId);
                        Debug.Log(mapId);
                    }
                    InstantiateButtons(userMapIds);
                });
            },
                () =>
                {
                    Debug.Log("Too heavy load");
                });
        }

        public void InstantiateButtons(List<string> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                GameObject button = Instantiate(mapButtonTemplate) as GameObject;
                button.SetActive(true);
                button.GetComponent<BrowseUserMapListMap>().SetId(i.ToString());
                button.GetComponent<BrowseUserMapListMap>().DatabaseId = ids[i];
                mapButtons.Add(button);
                button.GetComponent<BrowseUserMapListMap>().SetText();
                button.transform.SetParent(mapButtonTemplate.transform.parent, false);
            }
        }

        public void OnEnable()
        {
            FetchData();
        }

        public void OnDisable()
        {
            ClearData();
        }
        public void ClearData()
        {
            maps.Clear();
        }
    }
}
