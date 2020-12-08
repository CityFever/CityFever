using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using Newtonsoft.Json;
using Library;

namespace Database
{
    public class MapsRepository : MonoBehaviour
    {
       void Start()
        {
        }

        string CreateMap(Map map)
        {
            Debug.Log(Config.ID_TOKEN);
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/maps/.json?auth={Config.ID_TOKEN}", map).Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                Debug.Log(returnedJson["name"]);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
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