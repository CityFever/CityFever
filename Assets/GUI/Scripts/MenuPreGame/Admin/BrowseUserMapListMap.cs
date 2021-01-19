using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Application = Assets.AdminMap.Scripts.Application;

namespace Assets.GUI.Scripts.MenuPreGame.Admin
{
    public class BrowseUserMapListMap : MonoBehaviour
    {
        [SerializeField]
        private Text myText;

        [SerializeField]
        private Button mapButton;

        [SerializeField]
        private BrowseUserMapListControl mapButtonControl;

        public BrowseUserMapSelection uIManager;

        private string id;

        public string DatabaseId { get; set; }
        public MapConfig SelectedMapConfig { get; set; }
        void Start()
        {
            mapButton = GetComponent<Button>();
        }


        public void SetText()
        {
            myText.text = "Map #" + id.ToString();
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public string GetId()
        {
            return id;
        }

        public void PassId()
        {
            uIManager.SetMapId(id);
            Application.application.SelectedAdminMapId = DatabaseId;
            Debug.Log("Database Id: " + Application.application.SelectedAdminMapId);
        }
    }
}
