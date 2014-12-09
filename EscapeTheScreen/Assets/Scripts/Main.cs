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
        [SerializeField]
        private GameObject DriversWindow;
        [SerializeField]
        private GameObject DriverInstalled;
        [SerializeField]
        private GameObject CompScienceWindow;
        [SerializeField]
        private GameObject Me_n_babeWindow;
        [SerializeField]
        private GameObject PacMan_Window;
        [SerializeField]
        private GameObject ReadMe_Window;
        [SerializeField]
        private GameObject Pass_Window;

        #endregion

        /// <summary>
        /// If true then printer is installed.
        /// </summary>
        [HideInInspector]
        public bool printerInstalled;

        #region LOG IN SCREEN VARS

        [SerializeField]
        private Text inputText;
        [SerializeField]
        private RectTransform enterBtn;
        [SerializeField]
        private GameObject LogInHintObj;
        [SerializeField]
        private string logInPassword = "yellow";
        [SerializeField]
        private string userPassword = "PICB";
        [SerializeField]
        private string[] hintPasswords = { "OMKO", "CAFS", "APAG", "URSC", "MMMM", "SSBP", "BPIC" };
        [SerializeField]
        private Text UserPassText;
        [SerializeField]
        private GameObject UserPassHint;

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
            printerInstalled = false;
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
                case SCREEN_STATES.USER_PASSWORD:
                    checkUserPassInput(false);
                    break;
                default:
                    break;
            }

            checkIconButtonClick();
        }

        private void checkIconButtonClick()
        {
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Space))
            {
                switch (activeScreen)
                {
                    case SCREEN_STATES.DESKTOP:
                        switch (IconHandler.selectedBtn)
                        {
                            case BUTTONS.RECYBLE_BIN:
                                activeScreen = SCREEN_STATES.RECYCLE_BIN;
                                RecycleBinWindow.SetActive(true);
                                break;
                            case BUTTONS.UNSORTED:
                                activeScreen = SCREEN_STATES.UNSORTED;
                                UnsortedWindow.SetActive(true);
                                break;
                            case BUTTONS.USER:
                                activeScreen = SCREEN_STATES.USER_PASSWORD;
                                Pass_Window.SetActive(true);
                                UserPassText.text = "";
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
                        switch (IconHandler.selectedBtn)
                        {
                            case BUTTONS.DRIVERS:
                                activeScreen = SCREEN_STATES.DRIVERS;
                                DriversWindow.SetActive(true);
                                break;
                            case BUTTONS.COMPUTER_SCIENCE_DOC:
                                activeScreen = SCREEN_STATES.COMP_SCIENCE;
                                CompScienceWindow.SetActive(true);
                                break;
                            case BUTTONS.ME_N_BABE:
                                activeScreen = SCREEN_STATES.ME_N_BABE;
                                Me_n_babeWindow.SetActive(true);
                                break;
                            case BUTTONS.PACMAN_DOC:
                                activeScreen = SCREEN_STATES.PACKMAN_DOC;
                                PacMan_Window.SetActive(true);
                                break;
                            case BUTTONS.README_TXT:
                                activeScreen = SCREEN_STATES.README_DOC;
                                ReadMe_Window.SetActive(true);
                                break;
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.DESKTOP;
                                UnsortedWindow.SetActive(false);
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.DRIVERS:
                        switch(IconHandler.selectedBtn)
                        {
                            case BUTTONS.DRIVER_CAMERA:
                                activeScreen = SCREEN_STATES.DRIVER_INSTALLED;
                                DriverInstalled.SetActive(true);
                                break;
                            case BUTTONS.DRIVER_GPU:
                                activeScreen = SCREEN_STATES.DRIVER_INSTALLED;
                                DriverInstalled.SetActive(true);
                                break;
                            case BUTTONS.DRIVER_PRINTER:
                                printerInstalled = true;
                                activeScreen = SCREEN_STATES.DRIVER_INSTALLED;
                                DriverInstalled.SetActive(true);
                                break;
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.UNSORTED;
                                DriversWindow.SetActive(false);
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.DRIVER_INSTALLED:
                        if(IconHandler.selectedBtn == BUTTONS.CLOSE)
                        {
                            activeScreen = SCREEN_STATES.DRIVERS;
                            DriverInstalled.SetActive(false);
                        }
                        break;
                    case SCREEN_STATES.COMP_SCIENCE:
                    case SCREEN_STATES.ME_N_BABE:
                    case SCREEN_STATES.PACKMAN_DOC:
                    case SCREEN_STATES.README_DOC:
                        if (IconHandler.selectedBtn == BUTTONS.CLOSE)
                        {
                            activeScreen = SCREEN_STATES.UNSORTED;
                            CompScienceWindow.SetActive(false);
                            Me_n_babeWindow.SetActive(false);
                            PacMan_Window.SetActive(false);
                            ReadMe_Window.SetActive(false);
                        }
                        break;
                    case SCREEN_STATES.USER_PASSWORD:
                        switch(IconHandler.selectedBtn)
                        {
                            case BUTTONS.OK:
                                checkUserPassInput(true);
                                break;
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.DESKTOP;
                                Pass_Window.SetActive(false);
                                break;
                            default:
                                break;
                        }
                        if(IconHandler.selectedBtn == BUTTONS.OK)
                        {
                            checkUserPassInput(true);
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
                    LogInHintObj.SetActive(true);
            }
        }

        private void checkUserPassInput(bool check)
        {
            UserPassText.text = UserPassText.text.ToUpper();

            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || check)
            {
                if (UserPassText.text == userPassword)
                {
                    UserPassHint.SetActive(false);
                    Debug.Log("Correct password");
                }
                else
                {
                    bool showHint = false;
                    for(byte i = 0;i<hintPasswords.Length;i++)
                    {
                        if (UserPassText.text == hintPasswords[i])
                        {
                            showHint = true;
                            break;
                        }
                    }

                    UserPassHint.SetActive(showHint);
                }
            }

            if (UserPassText.text.Length > 4)
                UserPassText.text = UserPassText.text.Substring(0, 4);
        }
    }
}