using UnityEngine;

namespace Database
{
    public class UserSingleton : MonoBehaviour
    {
        public static UserSingleton Instance = null;
        public string Email { get; set; }
        public string Password { get; set; }

       // private UserSingleton() { }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
