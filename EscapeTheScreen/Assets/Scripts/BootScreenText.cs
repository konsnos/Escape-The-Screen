using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EscapeTheScreen
{
    public class BootScreenText : MonoBehaviour
    {
        [SerializeField]
        private string text;
        private int caretPos;
        private Text screenText;
        [SerializeField]
        private float textSpeed = 0.05f;
        private bool clickToContinue;

        // Use this for initialization
        private void Start()
        {
            clickToContinue = false;
            caretPos = 0;
            screenText = GetComponent<Text>();
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            StartCoroutine("typeText");
        }

        // Update is called once per frame
        private IEnumerator typeText()
        {
            while (caretPos < text.Length)
            {
                caretPos++;
                screenText.text = text.Substring(0, caretPos);
                yield return new WaitForSeconds(textSpeed);
            }
            clickToContinue = true;

            HeroController.StaticSelf.WalkAcrossScreen(200f,-380f);
        }

        private void Update()
        {
            if(clickToContinue)
            {
                var keyUpMouse0 = Input.GetKeyUp(KeyCode.Mouse0);
                var keyUpMouse1 = Input.GetKeyUp(KeyCode.Mouse1);
                var keyUpMouse2 = Input.GetKeyUp(KeyCode.Mouse2);
                if(keyUpMouse0 || keyUpMouse1 || keyUpMouse2)
                {
                    BootCompleted();
                }
            }
        }

        /// <summary>
        /// Hide boot. Show log in screen.
        /// </summary>
        private void BootCompleted()
        {
            HeroController.StaticSelf.ShowHide(false);
            //HeroController.Completed -= BootCompleted;
            Main.StaticSelf.HideBootScreen();
            Main.StaticSelf.ShowLogInScreen(true);
        }
    }
}