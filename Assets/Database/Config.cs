using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;

namespace Database
{
    public class Config : MonoBehaviour
    {
        internal static readonly string PROJECT_ID = ""; // Put your projectId here
        internal static readonly string DATABASE_URL = $"https://{PROJECT_ID}.firebaseio.com/";
        internal static readonly string API_KEY = "";
        internal static string ID_TOKEN = "";
        internal static string USER_ID = "";
        internal static readonly string USERS_FOLDER = "users/";
    }
}
