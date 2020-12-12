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
        public delegate void StringOperationSuccess(string message);
        public delegate void MapOperationSuccess(Map map);
        public delegate void MapListOperationSuccess(List<Map> maps);
        public delegate void OperationFail();

        void Start()
        {
        }

        void CreateMap(Map map, StringOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/.json?auth={Config.ID_TOKEN}", map).Then(response =>
            {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                Debug.Log(returnedJson["name"]);
                callback(returnedJson["name"]);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        void GetMap(string id, MapOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{id}/.json?auth={Config.ID_TOKEN}").Then(response => {
                Map returnedMap = JsonConvert.DeserializeObject<Map>(response.Text);
                callback(returnedMap);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }
        void GetAllUsersMaps(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<String, Map>>>(response.Text);
                var maps = new List<Map>();
                foreach (Dictionary<string, Map> dict1 in returnedJson.Values)
                {
                    foreach (Map map in dict1.Values)
                    {
                        maps.Add(map);
                    }
                }
                callback(maps);

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        void GetAllMaps(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/.json?auth={Config.ID_TOKEN}").Then(response => {
                var maps = new List<Map>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Map>>(response.Text);
                callback(new List<Map>(returnedJson.Values));
                
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        void UpdateMap(Map map, string id)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{id}/.json?auth={Config.ID_TOKEN}", map).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        void DeleteMap(string id)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{id}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        
    }
}