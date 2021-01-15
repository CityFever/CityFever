using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;

namespace Database
{
    public class MapsRepository : MonoBehaviour
    {
        public delegate void StringOperationSuccess(string message);
        public delegate void MapOperationSuccess(MapConfig map);
        public delegate void MapListOperationSuccess(List<MapConfig> maps);
        public delegate void OperationFail();

        void Start()
        {

        }

        public static void CreateMap(MapConfig map, StringOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}.json?auth={Config.ID_TOKEN}", map).Then(response =>
            {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                callback(returnedJson["name"]);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void GetMap(string id, MapOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}").Then(response => {
                MapConfig returnedMap = JsonConvert.DeserializeObject<MapConfig>(response.Text);
                callback(returnedMap);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }
        public static void GetAllUsersMaps(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, MapConfig>>>(response.Text);
                var maps = new List<MapConfig>();
                foreach (Dictionary<string, MapConfig> dict1 in returnedJson.Values)
                {
                    foreach (MapConfig map in dict1.Values)
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

        public static void GetAllMaps(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var maps = new List<MapConfig>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, MapConfig>>(response.Text);
                callback(new List<MapConfig>(returnedJson.Values));

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void UpdateMap(MapConfig map, string id)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}", map).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void DeleteMap(string id)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }
    }
}