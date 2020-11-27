using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Library;

namespace Database
{
    public class MapsRepository : MonoBehaviour
    {
        private const string projectId = "testproject-ffbca"; // Put your projectId here
        private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

        void Start()
        {
        }

        string CreateMap(Map map)
        {
            RestClient.Post($"{databaseURL}test.json", map).Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                Debug.Log(returnedJson["name"]);
            });
            return null;
        }

        Map GetMap(string id)
        {
            return null;
        }

        List<Map> GetAllUsersMaps()
        {
            return null;
        }

        List<Map> GetAllMaps()
        {
            return null;
        }

        void UpdateMap(Map map, string id)
        {

        }

        void DeleteMap(string id)
        {

        }
    }
}