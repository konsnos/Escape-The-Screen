using UnityEngine;
using System.Collections;

namespace EscapeTheScreen
{
    public class BootScreenText : MonoBehaviour
    {
        [SerializeField]
        private string text;
        private int caretPos;
        private bool ready;
        public bool Ready { get { return ready; } }

        // Use this for initialization
        void Start()
        {
            ready = false;
            caretPos = 0;
            StartCoroutine("typeText");
        }

        // Update is called once per frame
        IEnumerator typeText()
        {
            yield return null;
        }
    }
}