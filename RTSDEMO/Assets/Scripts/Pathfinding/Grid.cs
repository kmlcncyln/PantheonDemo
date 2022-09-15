using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;


namespace Pathfinding
{

    public class Grid<TGridObject>
    {

        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
        public class OnGridValueChangedEventArgs
        {
            public Vector2Int position;
        }


        private int width;
        private int height;
        private float cellSize;
        private Vector2Int lowerLimit;
        private Vector3 originPosition;

        private TGridObject[,] gridArray;
        //private TextMesh[,] debugTextArray;

        public Grid(int width, int height, float cellSize, Vector2Int lowerLimit, Vector3 originPosition, Tilemap roadTileMap, System.Func<Grid<TGridObject>, Vector2Int, Tilemap, TGridObject> createObject)
        {

            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.lowerLimit = lowerLimit;

            gridArray = new TGridObject[width, height];
            //debugTextArray = new TextMesh[width, height];

            for (int x = lowerLimit.x; (x - lowerLimit.x) < gridArray.GetLength(0); ++x)
            {


                for (int y = lowerLimit.y; (y - lowerLimit.y) < gridArray.GetLength(1); ++y)
                {

                    //Debug.Log(y + "->" + (y - lowerLimit.y) + " / " + gridArray.GetLength(1));
                    gridArray[x - lowerLimit.x,y - lowerLimit.y] = createObject(this, new Vector2Int(x, y), roadTileMap);
                    //debugTextArray[x - lowerLimit.x, y - lowerLimit.y] = CreateWorldText(gridArray[x - lowerLimit.x, y - lowerLimit.y].ToString(), null, GetWorldPosition(x, y) + (new Vector3(cellSize, cellSize, 0) * .5f), 32, Color.white, TextAnchor.MiddleCenter);

                    //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 5);
                    //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 5);

                }


            }

            //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
            //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);


        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize;
        }

        private Vector3 GetWorldPosition(Vector2Int position)
        {
            return new Vector3(position.x, position.y) * cellSize + originPosition;
        }

        public Vector2Int GetXY(Vector3 worldPosition)
        {
            Vector2Int returnValue = new Vector2Int();

            returnValue.x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            returnValue.y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

            return returnValue;
        }

        public void SetValue(Vector2Int position, TGridObject value)
        {

            if (position.x < lowerLimit.x || position.y < lowerLimit.y || position.x >= width - lowerLimit.x || position.y >= height - lowerLimit.y)
                return;

            gridArray[position.x - lowerLimit.x, position.y - lowerLimit.y] = value;
            //debugTextArray[position.x, position.y].text = value.ToString();

            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { position = position });

        }

        public void SetValue(Vector3 position, TGridObject value)
        {

            Vector2Int gridPosition = GetXY(position);

            SetValue(gridPosition, value);

        }

        public TGridObject GetValue(Vector2Int position)
        {

            if (position.x < lowerLimit.x || position.y < lowerLimit.y || position.x >= width + lowerLimit.x || position.y >= height + lowerLimit.y)
                return default;

            //Debug.Log((position.x - lowerLimit.x).ToString() + ":" + (position.y - lowerLimit.y).ToString() );

            return gridArray[position.x - lowerLimit.x, position.y - lowerLimit.y];
        }

        public TGridObject GetValue(Vector3 position)
        {

            Vector2Int gridPosition = GetXY(position);

            return GetValue(gridPosition);

        }

        public Vector2Int GetLowerLimits()
        {
            return lowerLimit;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public List<TGridObject> GetNeighbourList(Vector2Int position)
        {

            List<TGridObject> neighbourList = new List<TGridObject>();

            Vector2Int arrayPosition = new Vector2Int(position.x - lowerLimit.x, position.y - lowerLimit.y);

            if (arrayPosition.x - 1 >= lowerLimit.x) neighbourList.Add(GetValue(new Vector2Int(position.x - 1, position.y)));
            if (arrayPosition.x + 1 < width - lowerLimit.x) neighbourList.Add(GetValue(new Vector2Int(position.x + 1, position.y)));
            if (arrayPosition.y - 1 >= lowerLimit.y) neighbourList.Add(GetValue(new Vector2Int(position.x, position.y - 1)));
            if (arrayPosition.y + 1 < height - lowerLimit.y) neighbourList.Add(GetValue(new Vector2Int(position.x, position.y + 1)));

            return neighbourList;
        }

        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 32, Color color = default, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            if (color == null)
                color = Color.white;

            GameObject gameObject = new GameObject("World Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = .05f;
            textMesh.color = color;
            textMesh.anchor = textAnchor;

            gameObject.GetComponent<MeshRenderer>().sortingOrder = 50;

            return textMesh;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;
            return vec;
        }

    }


}
