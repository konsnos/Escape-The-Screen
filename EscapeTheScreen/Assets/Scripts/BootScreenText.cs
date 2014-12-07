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
        private bool ready;
        public bool Ready { get { return ready; } }
        private Text screenText;
        [SerializeField]
        private float textSpeed = 0.05f;

        // Use this for initialization
        void Start()
        {
            ready = false;
            caretPos = 0;
            screenText = GetComponent<Text>();
            StartCoroutine("typeText");
        }

        // Update is called once per frame
        IEnumerator typeText()
        {
            while (caretPos < text.Length)
            {
                caretPos++;
                screenText.text = text.Substring(0, caretPos);
                yield return new WaitForSeconds(textSpeed);
            }
            ready = true;

            HeroController.Completed += BootCompleted;
            HeroController.StaticSelf.WalkAcrossScreen(200f,-380f);
        }

        /// <summary>
        /// Hide boot. Show log in screen.
        /// </summary>
        private void BootCompleted()
        {
            HeroController.StaticSelf.ShowHide(false);
            HeroController.Completed -= BootCompleted;
            Main.StaticSelf.HideBootScreen();
            Main.StaticSelf.ShowLogInScreen(true);
        }
    }
}