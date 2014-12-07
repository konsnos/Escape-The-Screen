﻿using UnityEngine;
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
        private float speed = 100f;
        private float speedOther;
        private bool controlled;
        private RectTransform selfT;
        #region LOOKING DIRECTIONS
        private Quaternion LOOKING_RIGHT_LEFT, LOOKING_UP, LOOKING_DOWN;
        private Vector3 SCALE_RIGHT_UP_DOWN, SCALE_LEFT;
        #endregion

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
        }

        // Use this for initialization
        void Start()
        {
            controlled = true;
            printedSprite = 0;
            currentSprite = 0;
            mouthTimePassed = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (controlled)
            {
                newPos = selfT.anchoredPosition;

                #region MOVEMENT
                /// Vertical movement
                if (Input.GetKey(KeyCode.W))
                {
                    selfT.localScale = SCALE_RIGHT_UP_DOWN;
                    selfT.rotation = LOOKING_UP;
                    newPos.y += Time.deltaTime * speed;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    selfT.localScale = SCALE_RIGHT_UP_DOWN;
                    selfT.rotation = LOOKING_DOWN;
                    newPos.y -= Time.deltaTime * speed;
                }
                /// Horizontal movement
                if (Input.GetKey(KeyCode.A))
                {
                    selfT.localScale = SCALE_LEFT;
                    selfT.rotation = LOOKING_RIGHT_LEFT;
                    newPos.x -= Time.deltaTime * speed;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    selfT.localScale = SCALE_RIGHT_UP_DOWN;
                    selfT.rotation = LOOKING_RIGHT_LEFT;
                    newPos.x += Time.deltaTime * speed;
                }

                #region KEEP INSIDE SCREEN
                if (newPos.x - selfT.rect.width / 2f < 0f)
                    newPos.x = selfT.rect.width / 2f;
                else if (newPos.x + selfT.rect.width / 2f > Main.WIDTH)
                    newPos.x = Main.WIDTH - selfT.rect.width / 2f;

                if (newPos.y + selfT.rect.height / 2f > 0f)
                    newPos.y = -selfT.rect.height / 2f;
                else if (newPos.y - selfT.rect.height / 2f < Main.HEIGHT)
                    newPos.y = Main.HEIGHT + selfT.rect.height / 2f;
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

        public void WalkAcrossScreen(float newSpeed, float height)
        {
            controlled = false;
            speedOther = newSpeed;
            Vector2 newPos = selfT.anchoredPosition;
            newPos.x = 0f - selfT.rect.width / 2f;
            newPos.y = height;
            selfT.anchoredPosition = newPos;
            selfT.localScale = SCALE_RIGHT_UP_DOWN;
            selfT.rotation = LOOKING_RIGHT_LEFT;
            //Debug.Log("Pacman pos: " + selfT.position + " local pos: " + selfT.anchoredPosition);
            //UnityEditor.EditorApplication.isPaused = true;
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

            controlled = true;
        }
    }
}