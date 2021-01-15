using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Assets.AdminMap.Scripts.MapConfiguration;

namespace Database
{
    public class ObjectsRepository : MonoBehaviour
    {
        public delegate void StringOperationSuccess(string message);
        public delegate void ObjectOperationSuccess(ObjectConfig gameObject);
        public delegate void ObjectListOperationSuccess(List<ObjectConfig> objects);
        public delegate void OperationFail();

        void Start()
        {
        }

        public static void CreateObject(ObjectConfig gameObject, StringOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Post($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.OBJECTS_FOLDER}.json?auth={Config.ID_TOKEN}", gameObject).Then(response =>
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

        public static void GetObject(string objectId, ObjectOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.OBJECTS_FOLDER}{objectId}/.json?auth={Config.ID_TOKEN}").Then(response => {
                ObjectConfig returnedObject = JsonConvert.DeserializeObject<ObjectConfig>(response.Text);
                callback(returnedObject);
            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void GetAllObjects(ObjectListOperationSuccess callback = null, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.OBJECTS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var objects = new List<ObjectConfig>();
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, ObjectConfig>>(response.Text);
                callback(new List<ObjectConfig>(returnedJson.Values));

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void GetAllUsersObjects(ObjectListOperationSuccess callback, OperationFail fallback = null)
        {
            RestClient.Get($"{Config.DATABASE_URL}{Config.USERS_FOLDER}.json?auth={Config.ID_TOKEN}").Then(response => {
                var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, ObjectConfig>>>(response.Text);
                var objects = new List<ObjectConfig>();
                foreach (Dictionary<string, ObjectConfig> dict1 in returnedJson.Values)
                {
                    foreach (ObjectConfig obj in dict1.Values)
                    {
                        objects.Add(obj);
                    }
                }
                callback(objects);

            }).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
                fallback();
            });
        }

        public static void UpdateObject(ObjectConfig gameObject, string objectId)
        {
            RestClient.Put($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.OBJECTS_FOLDER}{objectId}/.json?auth={Config.ID_TOKEN}", gameObject).Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }

        public static void DeleteObject(string objectId)
        {
            RestClient.Delete($"{Config.DATABASE_URL}{Config.USERS_FOLDER}{Config.USER_ID}/{Config.OBJECTS_FOLDER}{objectId}/.json?auth={Config.ID_TOKEN}").Catch(err =>
            {
                var error = err as RequestException;
                Debug.Log(error.Response);
            });
        }
    }
}