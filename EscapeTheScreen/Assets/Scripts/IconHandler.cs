using UnityEngine;
using EscapeTheScreen;

namespace EscapeTheScreen
{
    public class IconHandler : MonoBehaviour
    {
        /// <summary>
        /// Reference to the image representing selection.
        /// </summary>
        [SerializeField]
        private GameObject selected;
        private SimpleRect selfRect;
        /// <summary>
        /// Button id.
        /// </summary>
        [SerializeField]
        private BUTTONS buttonId;

        [SerializeField]
        private SCREEN_STATES activeForWindow;
        /// <summary>
        /// Opens window.
        /// </summary>
        [SerializeField]
        private GameObject windowRef;
        public GameObject WindowRef => windowRef;

        public static BUTTONS selectedBtn;

        private void Start()
        {
            RectTransform selfT = GetComponent<RectTransform>();
            Vector3 pos = selfT.position;
            Rect rect = selfT.rect;
            selfRect = new SimpleRect(pos.x, pos.y, rect.width, rect.height);
        }

        /// <summary>
        /// Check if hero is over the icon.
        /// </summary>
        private void Update()
        {
            if (Main.ActiveScreen == activeForWindow)
            {
                bool isOver;

                if(buttonId == BUTTONS.CLOSE || buttonId == BUTTONS.HINT || buttonId == BUTTONS.PRINT_PICTURE)
                    isOver = Library.RectanglesCollide(HeroController.StaticSelf.GetRect(), selfRect);
                else
                    isOver = Library.HeroInsideRect(HeroController.StaticSelf.GetRect(), selfRect);

                if (isOver)
                {
                    if (!selected.activeSelf)
                    {
                        selected.SetActive(true);
                        selectedBtn = buttonId;
                    }
                }
                else
                {
                    if (selected.activeSelf)
                    {
                        selected.SetActive(false);
                        selectedBtn = BUTTONS.NONE;
                    }
                }
            }
            else if(selected.activeSelf)
            {
                selected.SetActive(false);
                selectedBtn = BUTTONS.NONE;
            }
        }
    }
}