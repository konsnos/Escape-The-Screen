using UnityEngine;
using UnityEngine.UI;
using EscapeTheScreen;
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
        private GameObject logInScreen;
        [SerializeField]
        private GameObject bootText;

        #region LOG IN SCREEN VARS

        [SerializeField]
        private Text inputText;
        [SerializeField]
        private RectTransform enterBtn;
        [SerializeField]
        private string logInPassword = "yellow";
        [SerializeField]
        private GameObject HintObj;

        #endregion

        /// <summary>
        /// Current screen the game is in.
        /// </summary>
        private Library.SCREEN_STATES activeScreen;

        void Awake()
        {
            StaticSelf = this;
        }

        void Start()
        {
            activeScreen = Library.SCREEN_STATES.BOOT_SCREEN;
            HeroController.StaticSelf.ShowHide(false);
            //ShowLogInScreen();
        }

        public void HideBootScreen()
        {
            bootText.SetActive(false);
        }

        public void ShowLogInScreen(bool show)
        {
            if (show)
            {
                activeScreen = Library.SCREEN_STATES.LOG_IN_SCREEN;
                logInScreen.SetActive(true);
                HeroController.StaticSelf.Controlled = true;
                HeroController.StaticSelf.ShowHide(true);
                HeroController.StaticSelf.SetToPosition(Main.WIDTH / 2f, -500f);
            }
        }

        void Update()
        {
            switch(activeScreen)
            {
                case Library.SCREEN_STATES.LOG_IN_SCREEN:
                    checkLogInScreenInput();
                    break;
                default:
                    break;
            }
        }

        private void checkLogInScreenInput()
        {
            if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                if (inputText.text == logInPassword)
                {
                    Debug.Log("Password correct!");
                }
                else
                    HintObj.SetActive(true);
            }
        }
    }
}