using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
        [SerializeField]
        private GameObject Window_User;
        [SerializeField]
        private GameObject Window_Myself;
        [SerializeField]
        private GameObject Window_PrinterNotInstalled;
        [SerializeField]
        private GameObject Window_PrintPicture;

        #endregion

        /// <summary>
        /// If true then printer is installed.
        /// </summary>
        [HideInInspector]
        public bool printerInstalled;
        /// <summary>
        /// True when user has entered the correct password for the folder User.
        /// </summary>
        [HideInInspector]
        private bool passwordGiven;

        [SerializeField]
        private InputField logInInput;
        [SerializeField]
        private RectTransform enterBtn;
        [SerializeField]
        private GameObject LogInHintObj;
        [SerializeField]
        private string logInPassword = "YELLOW";
        [SerializeField]
        private string userPassword = "PICB";
        [SerializeField]
        private string[] hintPasswords = { "OMKO", "CAFS", "APAG", "URSC", "MMMM", "SSBP", "BPIC" };
        [SerializeField]
        private InputField UserPassInput;
        [SerializeField]
        private GameObject UserPassHint;
        [SerializeField]
        private AudioSource WonSnd;
        [SerializeField]
        private AudioSource OpenSmthSnd;

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
            passwordGiven = false;
            HeroController.StaticSelf.ShowHide(false);
            //ShowLogInScreen(true);
            //ShowDesktopScreen(true);
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
                    EventSystem.current.SetSelectedGameObject(logInInput.gameObject, null);
                    checkLogInScreenInput();
                    break;
                case SCREEN_STATES.USER_PASSWORD:
                    EventSystem.current.SetSelectedGameObject(UserPassInput.gameObject, null);
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
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.UNSORTED:
                                activeScreen = SCREEN_STATES.UNSORTED;
                                UnsortedWindow.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.USER:
                                if(passwordGiven)
                                {
                                    activeScreen = SCREEN_STATES.USER_FOLDER;
                                    Window_User.SetActive(true);
                                    OpenSmthSnd.Play();
                                }
                                else
                                {
                                    activeScreen = SCREEN_STATES.USER_PASSWORD;
                                    Pass_Window.SetActive(true);
                                    OpenSmthSnd.Play();
                                    UserPassInput.text = "";
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.RECYCLE_BIN:
                        if (IconHandler.selectedBtn == BUTTONS.CLOSE)
                        {
                            RecycleBinWindow.SetActive(false);
                            OpenSmthSnd.Play();
                            activeScreen = SCREEN_STATES.DESKTOP;
                        }
                        break;
                    case SCREEN_STATES.UNSORTED:
                        switch (IconHandler.selectedBtn)
                        {
                            case BUTTONS.DRIVERS:
                                activeScreen = SCREEN_STATES.DRIVERS;
                                DriversWindow.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.COMPUTER_SCIENCE_DOC:
                                activeScreen = SCREEN_STATES.COMP_SCIENCE;
                                CompScienceWindow.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.ME_N_BABE:
                                activeScreen = SCREEN_STATES.ME_N_BABE;
                                Me_n_babeWindow.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.PACMAN_DOC:
                                activeScreen = SCREEN_STATES.PACKMAN_DOC;
                                PacMan_Window.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.README_TXT:
                                activeScreen = SCREEN_STATES.README_DOC;
                                ReadMe_Window.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.DESKTOP;
                                UnsortedWindow.SetActive(false);
                                OpenSmthSnd.Play();
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
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.DRIVER_GPU:
                                activeScreen = SCREEN_STATES.DRIVER_INSTALLED;
                                DriverInstalled.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.DRIVER_PRINTER:
                                printerInstalled = true;
                                activeScreen = SCREEN_STATES.DRIVER_INSTALLED;
                                DriverInstalled.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.UNSORTED;
                                DriversWindow.SetActive(false);
                                OpenSmthSnd.Play();
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
                            OpenSmthSnd.Play();
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
                            OpenSmthSnd.Play();
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
                                OpenSmthSnd.Play();
                                break;
                            default:
                                break;
                        }
                        if(IconHandler.selectedBtn == BUTTONS.OK)
                        {
                            checkUserPassInput(true);
                        }
                        break;
                    case SCREEN_STATES.USER_FOLDER:
                        switch(IconHandler.selectedBtn)
                        {
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.DESKTOP;
                                Window_User.SetActive(false);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.MYSELF:
                                activeScreen = SCREEN_STATES.MYSELF_OPEN;
                                Window_Myself.SetActive(true);
                                OpenSmthSnd.Play();
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.MYSELF_OPEN:
                        switch(IconHandler.selectedBtn)
                        {
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.USER_FOLDER;
                                Window_Myself.SetActive(false);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.PRINT:
                                if (printerInstalled)
                                {
                                    activeScreen = SCREEN_STATES.PICTURE_OPEN;
                                    Window_PrintPicture.SetActive(true);
                                    OpenSmthSnd.Play();
                                }
                                else
                                {
                                    activeScreen = SCREEN_STATES.PRINTER_NOT_INSTALLED;
                                    Window_PrinterNotInstalled.SetActive(true);
                                    OpenSmthSnd.Play();
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case SCREEN_STATES.PRINTER_NOT_INSTALLED:
                        if(IconHandler.selectedBtn == BUTTONS.CLOSE)
                        {
                            activeScreen = SCREEN_STATES.MYSELF_OPEN;
                            Window_PrinterNotInstalled.SetActive(false);
                            OpenSmthSnd.Play();
                        }
                        break;
                    case SCREEN_STATES.PICTURE_OPEN:
                        switch(IconHandler.selectedBtn)
                        {
                            case BUTTONS.CLOSE:
                                activeScreen = SCREEN_STATES.MYSELF_OPEN;
                                Window_PrintPicture.SetActive(false);
                                OpenSmthSnd.Play();
                                break;
                            case BUTTONS.PRINT_PICTURE:
                                activeScreen = SCREEN_STATES.WON;
                                WonSnd.Play();
                                Invoke("openWonUrl", 3f);
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

        /// <summary>
        /// Opens won url! Congrats!
        /// </summary>
        private void openWonUrl()
        {
            Application.OpenURL("http://webexperiments.gr/escapethescreen/won.html");
        }

        private void checkLogInScreenInput()
        {
            //Debug.Log(logInInputText.text.ToUpper());
            //logInInputText.text = logInInputText.text.ToUpper();
            logInInput.text = logInInput.text.ToUpper();
            if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                if (logInInput.text == logInPassword)
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
            UserPassInput.text = UserPassInput.text.ToUpper();

            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || check)
            {
                if (UserPassInput.text == userPassword)
                {
                    UserPassHint.SetActive(false);

                    passwordGiven = true;
                    activeScreen = SCREEN_STATES.USER_FOLDER;
                    Pass_Window.SetActive(false);
                    Window_User.SetActive(true);
                    OpenSmthSnd.Play();
                }
                else
                {
                    bool showHint = false;
                    for(byte i = 0;i<hintPasswords.Length;i++)
                    {
                        if (UserPassInput.text == hintPasswords[i])
                        {
                            showHint = true;
                            break;
                        }
                    }

                    UserPassHint.SetActive(showHint);
                }
            }
        }
    }
}