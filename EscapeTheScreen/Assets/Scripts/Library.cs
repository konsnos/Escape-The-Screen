using UnityEngine;

namespace EscapeTheScreen
{
    /// <summary>
    /// Contains public libraries.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Screens the game can be.
        /// </summary>
        public enum SCREEN_STATES { BOOT_SCREEN, LOG_IN_SCREEN, DESKTOP_SCREEN };

        /// <summary>
        /// Rectangles must be the position of the transform and the with/height of the rect.
        /// </summary>
        /// <returns></returns>
        public static bool RectanglesCollide(Vector4 rect1, Vector4 rect2)
        {
            if (rect1.x < rect2.x + rect2.z && rect1.x + rect1.z > rect2.x && rect1.y < rect2.y + rect2.w && rect1.y + rect1.w > rect2.y)
                return true;
            return false;
        }
    }
}