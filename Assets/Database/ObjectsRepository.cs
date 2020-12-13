using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Library;

namespace Database
{
    public class ObjectsRepository : MonoBehaviour
    {
        public delegate void StringOperationSuccess(string message);
        public delegate void ObjectOperationSuccess(GameObj gameObject);
        public delegate void ObjectListOperationSuccess(List<GameObj> objects);
        public delegate void OperationFail();

        void Start()
        {
        }

        void CreateObject(GameObj gameObject, string mapId, StringOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{mapId}/.json?auth={Config.ID_TOKEN}", gameObject).Then(response =>
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

        void GetObject(string mapId, string objectId, ObjectOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{mapId}/{objectId}/.json?auth={Config.ID_TOKEN}").Then(response => {
                GameObj returnedObject = JsonConvert.DeserializeObject<GameObj>(response.Text);
                callback(returnedObject);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        void GetAllObjects(string mapID, ObjectListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{mapID}/.json?auth={Config.ID_TOKEN}").Then(response => {
                var objects = new List<GameObj>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, GameObj>>(response.Text);
                callback(new List<GameObj>(returnedJson.Values));

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        void UpdateObject(GameObj gameObject, string mapId, string objectId)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{mapId}/{objectId}/.json?auth={Config.ID_TOKEN}", gameObject).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        void DeleteObject(string mapId, string objectId)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{mapId}/{objectId}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }
    }
}