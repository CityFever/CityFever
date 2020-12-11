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
       void Start()
        {
        }

        string CreateMap(Map map)
        {
            string id = "XD";
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/maps/.json?auth={Config.ID_TOKEN}", map).Then(response =>
            {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                //Debug.Log(returnedJson["name"]);
                id = returnedJson["name"];
                Debug.Log("1" + id);
            });
            Debug.Log("2" + id);
            return id;
        }

        Map GetMap(string id)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/maps/{id}/.json?auth={Config.ID_TOKEN}").Then(response => {
                Debug.Log(response);
                return response;
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
            return null;
        }

        List<Map> GetAllUsersMaps()
        {
            return null;
        }

        List<Map> GetAllMaps()
        {
            RestClient.GetArray<Map>($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/maps/.json?auth={Config.ID_TOKEN}").Then(response => {
                Debug.Log(response);
                return response;
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
            return new List<Map>();
        }

        void UpdateMap(Map map, string id)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/maps/{id}/.json?auth={Config.ID_TOKEN}", map).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        void DeleteMap(string id)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/maps/{id}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public void OnClick()
        {
            UsersRepository.Login("jaksamateusz@gmail.com", "admin12",
                () => {
                    //Map map = new Map();
                    //var id = CreateMap(map);
                   Map map = GetMap("-MODkyJcdEDFwPWpVh4H");
                    //var x = map.zoneSize;
                    //GetAllMaps();
                    //Debug.Log("elo");
                });
        }
    }
}