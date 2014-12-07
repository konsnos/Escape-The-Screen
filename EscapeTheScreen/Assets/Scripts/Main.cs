using UnityEngine;
using System.Collections;

namespace EscapeTheScreen
{
    public class Main : MonoBehaviour
    {
        public static float WIDTH = 800f;
        /// <summary>
        /// 
        /// <b>Negative height.</b>
        /// </summary>
        public static float HEIGHT = -600f;

        public static Main StaticSelf;
        [SerializeField]
        private Sprite logInScreen;

        void Awake()
        {
            StaticSelf = this;
        }


    }
}