using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Library;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Database
{
    public class UsersRepository : MonoBehaviour
    {
        public delegate void OperationSuccess();
        public delegate void OperationFail();

        public static void Register(string email, string password, User user, OperationSuccess callback = null, OperationFail fallback = null)
        {
            var payload = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
            RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={Config.API_KEY}", payload).Then(
                response =>
                {
                    Debug.Log("Created User");

                    var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                    Config.ID_TOKEN = returnedJson["idToken"];
                    Config.USER_ID = returnedJson["localId"];
                    Debug.Log("Got the id token for user " + email);
                    callback();
                }).Catch(err => 
                {
                    var error = err as RequestException;
                    Debug.Log(error.Response);
                    fallback();
                });
        }

        public static void Login(string email, string password, OperationSuccess callback = null, OperationFail fallback = null)
        {
            var payload = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
            RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={Config.API_KEY}", payload).Then(
                response =>
                {
                    var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                    Config.ID_TOKEN = returnedJson["idToken"];
                    Config.USER_ID = returnedJson["localId"];
                    Debug.Log(response.Text);
                    Debug.Log("Got the id token for user " + email);
                    callback();
                }).Catch(err =>
                {
                    var error = err as RequestException;
                    Debug.Log(error.Response);
                    fallback();
                });
        }

        public static void Logout()
        {
            Config.ID_TOKEN = "";
            Config.USER_ID = "";
        }
    }
}