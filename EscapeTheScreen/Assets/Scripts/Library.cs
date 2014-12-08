using UnityEngine;

namespace EscapeTheScreen
{
    /// <summary>
    /// Contains public libraries.
    /// </summary>
    public class Library
    {
        public static bool HeroInsideRect(SimpleRect hero, SimpleRect rect)
        {
            Vector2 centerPoint = new Vector2(hero.x + hero.width / 2f, hero.y + hero.height / 2f);

            if (centerPoint.x > rect.x && centerPoint.x < rect.x + rect.width && centerPoint.y > rect.y && centerPoint.y < rect.y + rect.height)
                return true;
            return false;
        }

        /// <summary>
        /// Rectangles must be the position of the transform and the with/height of the rect.
        /// </summary>
        /// <returns></returns>
        public static bool RectanglesCollide(SimpleRect rect1, SimpleRect rect2)
        {
            if (rect1.x < rect2.x + rect2.width && rect1.x + rect1.width > rect2.x && rect1.y < rect2.y + rect2.height && rect1.y + rect1.height > rect2.y)
                return true;
            return false;
        }
    }
    
    /// <summary>
    /// Screens the game can be.
    /// </summary>
    public enum SCREEN_STATES { BOOT, LOG_IN, DESKTOP, RECYCLE_BIN, UNSORTED, DRIVERS, COMP_SCIENCE, ME_N_BABE, PACKMAN_DOC, README_DOC };

    /// <summary>
    /// Buttons ids.
    /// </summary>
    public enum BUTTONS { NONE, RECYBLE_BIN, USER, UNSORTED, CLOSE, DRIVERS, COMPUTER_SCIENCE_DOC, ME_N_BABE, PACMAN_DOC, README_TXT };

    public struct SimpleRect
    {
        public SimpleRect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public float x, y, width, height;
    }
}