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

        public static void Register(string email, string password, OperationSuccess callback = null, OperationFail fallback = null)
        {
            string payload = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
            RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={Config.API_KEY}", payload).Then(
                response =>
                {
                    Debug.Log("Created User");

                    var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                    Config.ID_TOKEN = returnedJson["idToken"];
                    Config.USER_ID = returnedJson["localId"];
                    Debug.Log("Got the id token for user " + email);
                    SendEmailVerification(returnedJson["idToken"]);
                    callback();
                }).Catch(err => 
                {
                    var error = err as RequestException;
                    Debug.Log(error.Response);
                    fallback();
                });
        }

        public static void Login(string email, string password, OperationSuccess callback, OperationFail fallback = null)
        {
            var payload = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
            RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={Config.API_KEY}", payload).Then(
                response =>
                {
                    var returnedJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Text);
                    
                    Debug.Log("Got the id token for user " + email);
                    CheckEmailVerification(returnedJson["idToken"], () =>
                    {
                        Debug.Log("Login successful");
                        Config.ID_TOKEN = returnedJson["idToken"];
                        Config.USER_ID = returnedJson["localId"];
                        callback();
                    }, () => { Debug.Log("Email not verified"); });
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

        private static void SendEmailVerification(string idToken)
        {
            string payLoad = $"{{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"{idToken}\"}}";
            RestClient.Post(
                $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key={Config.API_KEY}", payLoad);
        }

        private static void CheckEmailVerification(string idToken, OperationSuccess callback, OperationFail fallback)
        {
            var payLoad = $"{{\"idToken\":\"{idToken}\"}}";
            RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={Config.API_KEY}",
                payLoad).Then(
                response =>
                {
                    var userInfos = JsonConvert.DeserializeObject<UsersInfo>(response.Text);
                    Debug.Log(response.Text);
                    if (userInfos.users[0].emailVerified)
                    {
                        callback();
                    }
                    else
                    {
                        fallback();
                    }
                });
        }
    }
}