using UnityEngine;

namespace KC.RTS.InputManager
{
    public static class MultiSelect
    {
        private static Texture2D _whiteTexture;

        public static Texture2D WhiteTexture
        {
            get
            {
                if (_whiteTexture == null)
                {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }
                return _whiteTexture;
            }
        }

        public static void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
        }

        public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
        {
            //Top
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            //Bottom
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
            //Left
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            //Right
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        }

        public static Rect GetScreenRect(Vector2 screenPos1, Vector2 screenPos2)
        {
            // from bottom right to top left
            screenPos1.y = Screen.height - screenPos1.y;
            screenPos2.y = Screen.height - screenPos2.y;

            //corners
            Vector2 bR = Vector2.Max(screenPos1, screenPos2);
            Vector2 tl = Vector2.Min(screenPos1, screenPos2);

            // create rectangle
            return Rect.MinMaxRect(tl.x, tl.y, bR.x, bR.y);
        }

        public static Bounds GetVPBounds(Camera cam, Vector2 screenPos1, Vector2 screenPos2)
        {
            Vector2 pos1 = cam.ScreenToViewportPoint(screenPos1);
            Vector2 pos2 = cam.ScreenToViewportPoint(screenPos2);

            Vector2 min = Vector2.Min(pos1, pos2);
            Vector2 max = Vector2.Max(pos1, pos2);

            //min.z = cam.nearClipPlane;
            //max.z = cam.farClipPlane;

            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);

            return bounds;
        }
    }
}
