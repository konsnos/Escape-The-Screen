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

        #region SCREENS

        [SerializeField]
        private GameObject logInScreen;
        [SerializeField]
        private GameObject DesktopScreen;
        [SerializeField]
        private GameObject bootText;
        [SerializeField]
        private GameObject RecycleBinScreen;

        #endregion

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
        private static SCREEN_STATES activeScreen;
        public static SCREEN_STATES ActiveScreen { get { return activeScreen; } }

        void Awake()
        {
            StaticSelf = this;
        }

        void Start()
        {
            activeScreen = SCREEN_STATES.BOOT_SCREEN;
            HeroController.StaticSelf.ShowHide(false);
            ShowDesktopScreen(true);
        }

        public void HideBootScreen()
        {
            bootText.SetActive(false);
        }

        public void ShowLogInScreen(bool show)
        {
            if (show)
            {
                activeScreen = SCREEN_STATES.LOG_IN_SCREEN;
                logInScreen.SetActive(true);
                HeroController.StaticSelf.Controlled = true;
                HeroController.StaticSelf.ShowHide(true);
                HeroController.StaticSelf.SetToPosition(Main.WIDTH / 2f, -500f);
            }
            else
            {
                logInScreen.SetActive(false);
            }
        }

        private void ShowDesktopScreen(bool show)
        {
            if(show)
            {
                activeScreen = SCREEN_STATES.DESKTOP_SCREEN;
                DesktopScreen.SetActive(true);
                HeroController.StaticSelf.Controlled = true;
                HeroController.StaticSelf.ShowHide(true);
            }
        }

        void Update()
        {
            switch(activeScreen)
            {
                case SCREEN_STATES.LOG_IN_SCREEN:
                    checkLogInScreenInput();
                    break;
                default:
                    break;
            }

            if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Space))
            {
                switch(activeScreen)
                {
                    case SCREEN_STATES.DESKTOP_SCREEN:
                        switch (IconHandler.selectedBtn)
                        {
                            case BUTTONS.RECYBLE_BIN:
                                RecycleBinScreen.SetActive(true);
                                activeScreen = SCREEN_STATES.RECYCLE_BIN_SCREEN;
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.RECYCLE_BIN_SCREEN:
                        if (IconHandler.selectedBtn == BUTTONS.CLOSE)
                        {
                            RecycleBinScreen.SetActive(false);
                            activeScreen = SCREEN_STATES.DESKTOP_SCREEN;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void checkLogInScreenInput()
        {
            if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                if (inputText.text == logInPassword)
                {
                    ShowLogInScreen(false);
                    ShowDesktopScreen(true);
                }
                else
                    HintObj.SetActive(true);
            }
        }
    }
}