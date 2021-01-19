using System;
using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;

namespace Database
{
    public class MapsRepository : MonoBehaviour
    {
        public delegate void StringOperationSuccess(string message);
        public delegate void StringListOperationSuccess(List<string> strings);
        public delegate void MapOperationSuccess(MapConfig map);
        public delegate void MapListOperationSuccess(List<MapConfig> maps);
        public delegate void OperationFail();

        void Start() { }

        public static void CreateAdminMap(MapConfig map, StringOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Post($"{Config.DATABASE_URL}{Config.ADMINS_FOLDER}{Config.MAPS_FOLDER}.json?auth={Config.ID_TOKEN}", map).Then(response =>
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

        public static void CreateUserMap(MapConfig map, StringOperationSuccess callback = null, OperationFail fallback = null)
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

        //Get a map from users/user_id with a selected id
        public static void GetUserMap(string id, MapOperationSuccess callback, OperationFail fallback = null)
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

        //Get a map from admins with a selected id
        public static void GetAdminMap(string id, MapOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.ADMINS_FOLDER}{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}").Then(response => {
                MapConfig returnedMap = JsonConvert.DeserializeObject<MapConfig>(response.Text);
                callback(returnedMap);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        //Get all maps of all users from users/
        public static void GetAllUsersMaps(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, MapConfig>>>>(response.Text);
                var maps = new List<MapConfig>();
                foreach (Dictionary<string, Dictionary<string, MapConfig>> dict1 in returnedJson.Values)
                {
                    foreach (Dictionary<string, MapConfig> dict2 in dict1.Values)
                    {
                        foreach (MapConfig map in dict2.Values)
                        {
                            maps.Add(map);
                        }
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

        public static void GetAllUsersMapIds(StringListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var strings = new List<string>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, MapConfig>>>>(response.Text);

                foreach (Dictionary<string, Dictionary<string, MapConfig>> dict1 in returnedJson.Values)
                {
                    foreach (Dictionary<string, MapConfig> dict2 in dict1.Values)
                    {
                        foreach (KeyValuePair<string, MapConfig> kvp in dict2)
                        {
                            strings.Add(kvp.Key);
                        }
                    }
                }
                callback(strings);

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        //Get all maps of signed user from users/
        private static void GetAllMapsOfSignedUser(MapListOperationSuccess callback, OperationFail fallback = null)
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

        //Get all map ids of signed user from users/
        public static void GetAllMapOfSignedUserIds(StringListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var strings = new List<String>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response.Text);
                foreach (KeyValuePair<string, Dictionary<string, string>> kvp in returnedJson)
                {
                    strings.Add(kvp.Key);
                }
                callback(strings);

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        //Get all maps from admins/
        public static void GetAllAdminMaps(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.ADMINS_FOLDER}{Config.MAPS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
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

        //Get all map ids from admins/
        public static void GetAllAdminMapIds(StringListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.ADMINS_FOLDER}{Config.MAPS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var strings = new List<String>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, TileConfig>>>(response.Text);
                foreach (KeyValuePair<string, Dictionary<string, TileConfig>> kvp in returnedJson)
                {
                    strings.Add(kvp.Key);
                }
                callback(strings);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        //Get all maps from admins/ and to a correct signed user/user_id
        public static void GetAllMapsForSignedUser(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            List<MapConfig> maps = new List<MapConfig>();
            GetAllAdminMaps((adminMaps) => {
                maps.AddRange(adminMaps);
                GetAllMapsOfSignedUser((userMaps) => {
                    maps.AddRange(userMaps);
                    callback(maps);
                }, fallback);
            }, fallback);
        }

        //Get all maps from admins/ and users/
        public static void GetAllMapsForAdmin(MapListOperationSuccess callback, OperationFail fallback = null)
        {
            List<MapConfig> maps = new List<MapConfig>();
            GetAllAdminMaps((adminMaps) => { 
                maps.AddRange(adminMaps);
                GetAllUsersMaps((userMaps) => {
                    maps.AddRange(userMaps);
                    callback(maps);
                }, fallback);
            }, fallback);
        }

        public static void UpdateAdminMap(MapConfig map, string id)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.ADMINS_FOLDER}{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}", map).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void UpdateUserMap(MapConfig map, string id)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}{id}.json?auth={Config.ID_TOKEN}", map).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void UpdateUserMapTileSet(MapConfig map, string id)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}{id}{"tileConfigs"}//.json?auth={Config.ID_TOKEN}", map).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void DeleteAdminMap(string id)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.ADMINS_FOLDER}{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void DeleteUserMap(string id)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.MAPS_FOLDER}{id}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }
    }
}