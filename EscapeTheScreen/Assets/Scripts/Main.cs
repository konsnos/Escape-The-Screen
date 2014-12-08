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
        private GameObject RecycleBinWindow;
        [SerializeField]
        private GameObject UnsortedWindow;

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
            activeScreen = SCREEN_STATES.BOOT;
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
                activeScreen = SCREEN_STATES.LOG_IN;
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
                activeScreen = SCREEN_STATES.DESKTOP;
                DesktopScreen.SetActive(true);
                HeroController.StaticSelf.Controlled = true;
                HeroController.StaticSelf.ShowHide(true);
            }
        }

        void Update()
        {
            switch(activeScreen)
            {
                case SCREEN_STATES.LOG_IN:
                    checkLogInScreenInput();
                    break;
                default:
                    break;
            }

            if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Space))
            {
                switch(activeScreen)
                {
                    case SCREEN_STATES.DESKTOP:
                        switch (IconHandler.selectedBtn)
                        {
                            case BUTTONS.RECYBLE_BIN:
                                RecycleBinWindow.SetActive(true);
                                activeScreen = SCREEN_STATES.RECYCLE_BIN;
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.RECYCLE_BIN:
                        if (IconHandler.selectedBtn == BUTTONS.CLOSE)
                        {
                            RecycleBinWindow.SetActive(false);
                            activeScreen = SCREEN_STATES.DESKTOP;
                        }
                        break;
                    case SCREEN_STATES.UNSORTED:
                        switch(IconHandler.selectedBtn)
                        {
                            case BUTTONS.DRIVERS:
                                break;
                            case BUTTONS.COMPUTER_SCIENCE_DOC:
                                break;
                            case BUTTONS.ME_N_BABE:
                                break;
                            case BUTTONS.PACMAN_DOC:
                                break;
                            case BUTTONS.README_TXT:
                                break;
                            default:
                                break;
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