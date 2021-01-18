using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;

namespace Database
{
    public class Config : MonoBehaviour
    {
        internal static readonly string PROJECT_ID = "softwareengineering-4eea1-default-rtdb"; // Put your projectId here
        internal static readonly string DATABASE_URL = $"https://{PROJECT_ID}.firebaseio.com/";
        internal static readonly string API_KEY = "AIzaSyCyT3fHJ6f-56XjRtig9g1z1EciQL70LQ4";
        internal static string ID_TOKEN = "";
        internal static string USER_ID = "";
        internal static readonly string USERS_FOLDER = "users/";
        internal static readonly string MAPS_FOLDER = "maps/";
        internal static readonly string OBJECTS_FOLDER = "objects/";
        internal static readonly string TILES_FOLDER = "tiles/";
    }
}
