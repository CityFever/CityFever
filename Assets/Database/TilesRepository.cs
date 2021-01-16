using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Assets.AdminMap.Scripts.MapConfiguration;

namespace Database
{
    public class TilesRepository : MonoBehaviour
    {
        public delegate void StringOperationSuccess(string message);
        public delegate void TileOperationSuccess(TileConfig tile);
        public delegate void TileListOperationSuccess(List<TileConfig> tiles);
        public delegate void OperationFail();

        void Start()
        {
        }

        public static void CreateTile(TileConfig tile, StringOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.TILES_FOLDER}.json?auth={Config.ID_TOKEN}", tile).Then(response =>
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

        public static void GetTile(string tileId, TileOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.TILES_FOLDER}{tileId}/.json?auth={Config.ID_TOKEN}").Then(response => {
                TileConfig returnedTile = JsonConvert.DeserializeObject<TileConfig>(response.Text);
                callback(returnedTile);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void GetAllTiles(TileListOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.TILES_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var tiles = new List<TileConfig>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, TileConfig>>(response.Text);
                callback(new List<TileConfig>(returnedJson.Values));

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void GetAllUsersTiles(TileListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, TileConfig>>>(response.Text);
                var tiles = new List<TileConfig>();
                foreach (Dictionary<string, TileConfig> dict1 in returnedJson.Values)
                {
                    foreach (TileConfig tile in dict1.Values)
                    {
                        tiles.Add(tile);
                    }
                }
                callback(tiles);

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void UpdateTile(TileConfig tile, string tileId)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.TILES_FOLDER}{tileId}/.json?auth={Config.ID_TOKEN}", tile).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void DeleteTile(string tileId)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.TILES_FOLDER}{tileId}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }
    }
}