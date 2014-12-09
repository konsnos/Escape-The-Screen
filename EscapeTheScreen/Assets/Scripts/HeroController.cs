using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EscapeTheScreen
{
    public class HeroController : MonoBehaviour
    {
        public static HeroController StaticSelf;
        /// <summary>
        /// Sprites of the hero to swap while moving.
        /// </summary>
        [SerializeField]
        private Sprite[] sprites;
        [SerializeField]
        private float mouthMoveTimeSpan = 0.1f;
        private float mouthTimePassed;
        /// <summary>
        /// Current sprite index.
        /// </summary>
        private byte currentSprite;
        /// <summary>
        /// Sprite currently visible.
        /// </summary>
        private byte printedSprite;
        private Image image;
        /// <summary>
        /// Movement speed.
        /// </summary>
        [SerializeField]
        private float speed = 150f;
        private float speedOther;
        private bool controlled;
        public bool Controlled
        {
            get { return controlled; }
            set { controlled = value; }
        }
        private RectTransform selfT;
        #region LOOKING DIRECTIONS
        private Quaternion LOOKING_RIGHT_LEFT, LOOKING_UP, LOOKING_DOWN;
        private Vector3 SCALE_RIGHT_UP_DOWN, SCALE_LEFT;
        #endregion
        private AudioSource moveSnd;
        private bool isMoving;

        Vector3 newPos;

        void Awake()
        {
            StaticSelf = this;
            image = GetComponent<Image>();
            selfT = GetComponent<RectTransform>();

            LOOKING_RIGHT_LEFT = Quaternion.identity;
            LOOKING_UP = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            LOOKING_DOWN = Quaternion.Euler(new Vector3(0f, 0f, -90f));

            SCALE_RIGHT_UP_DOWN = Vector3.one;
            SCALE_LEFT = new Vector3(-1f, 1f, 1f);

            moveSnd = GetComponent<AudioSource>();
        }

        // Use this for initialization
        void Start()
        {
            printedSprite = 0;
            currentSprite = 0;
            mouthTimePassed = 0f;
            isMoving = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (controlled)
            {
                newPos = selfT.anchoredPosition;

                bool newIsMoving = false;

                #region MOVEMENT
                /// Vertical movement
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    selfT.localScale = SCALE_RIGHT_UP_DOWN;
                    selfT.rotation = LOOKING_UP;
                    newPos.y += Time.deltaTime * speed;
                    newIsMoving = true;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    selfT.localScale = SCALE_RIGHT_UP_DOWN;
                    selfT.rotation = LOOKING_DOWN;
                    newPos.y -= Time.deltaTime * speed;
                    newIsMoving = true;
                }
                /// Horizontal movement
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    selfT.localScale = SCALE_LEFT;
                    selfT.rotation = LOOKING_RIGHT_LEFT;
                    newPos.x -= Time.deltaTime * speed;
                    newIsMoving = true;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    selfT.localScale = SCALE_RIGHT_UP_DOWN;
                    selfT.rotation = LOOKING_RIGHT_LEFT;
                    newPos.x += Time.deltaTime * speed;
                    newIsMoving = true;
                }

                if(isMoving != newIsMoving)
                {
                    isMoving = newIsMoving;
                    if(isMoving)
                    {
                        moveSnd.Play();
                    }
                    else
                    {
                        moveSnd.Stop();
                    }
                }

                #region KEEP INSIDE SCREEN
                if (newPos.x - selfT.rect.width / 2f < 0f)
                    newPos.x = selfT.rect.width / 2f;
                else if (newPos.x + selfT.rect.width / 2f > Main.WIDTH)
                    newPos.x = Main.WIDTH - selfT.rect.width / 2f;

                if (newPos.y + selfT.rect.height / 2f > 0f)
                    newPos.y = -selfT.rect.height / 2f;
                else if (newPos.y - selfT.rect.height / 2f < Main.HEIGHT + 40)
                    newPos.y = Main.HEIGHT + selfT.rect.height / 2f + 40;
                #endregion

                selfT.anchoredPosition = newPos;
                #endregion
            }

            #region PRINT SPRITE
            mouthTimePassed += Time.deltaTime;
            while(mouthTimePassed > mouthMoveTimeSpan)
            {
                mouthTimePassed -= mouthMoveTimeSpan;
                if (++currentSprite >= sprites.Length)
                    currentSprite = 0;
            }

            /// Update sprite for movement
            if(printedSprite != currentSprite)
            {
                printedSprite = currentSprite;
                image.sprite = sprites[printedSprite];
            }
            #endregion
        }

        /// <summary>
        /// Returns hero's rectangle.
        /// </summary>
        /// <returns></returns>
        public SimpleRect GetRect()
        {
            Vector3 pos = selfT.position;
            Rect rect = selfT.rect;
            return new SimpleRect(pos.x, pos.y, rect.width, rect.height);
        }

        public void ShowHide(bool show)
        {
            StopCoroutine("walkAcrossScreen");
            gameObject.SetActive(show);
        }

        public void SetInTheMiddle()
        {
            Vector2 newPos = selfT.anchoredPosition;
            newPos.x = Main.WIDTH / 2f;
            newPos.y = Main.HEIGHT / 2f;
            selfT.anchoredPosition = newPos;
        }

        /// <summary>
        /// Sets hero to the position given.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetToPosition(float width, float height)
        {
            Vector2 newPos = selfT.anchoredPosition;
            newPos.x = width;
            newPos.y = height;
            selfT.anchoredPosition = newPos;
        }

        public void WalkAcrossScreen(float newSpeed, float height)
        {
            ShowHide(true);
            Controlled = false;
            speedOther = newSpeed;
            Vector2 newPos = selfT.anchoredPosition;
            newPos.x = 0f - selfT.rect.width / 2f;
            newPos.y = height;
            selfT.anchoredPosition = newPos;
            selfT.localScale = SCALE_RIGHT_UP_DOWN;
            selfT.rotation = LOOKING_RIGHT_LEFT;
            StartCoroutine("walkAcrossScreen");
        }

        IEnumerator walkAcrossScreen()
        {
            while (selfT.anchoredPosition.x - selfT.rect.width / 2f < Main.WIDTH)
            {
                newPos = selfT.anchoredPosition;
                newPos.x += speedOther * Time.deltaTime;
                selfT.anchoredPosition = newPos;
                yield return null;
            }
        }
    }
}