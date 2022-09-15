using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActivePathFixer : MonoBehaviour
{

    public Tile point,
        vertical,
        horizontal,
        leftToUp, leftToDown,
        rightToUp, rightToDown,
        fromRight, fromLeft, fromTop, fromBottom,
        toRight, toLeft, toTop, toBottom;



    public void FixPath(Tilemap tilemap, List<Vector2Int> path)
    {

        tilemap.ClearAllTiles();

        int order = 0;
        foreach(Vector2Int point in path)
        {

            if(point == path[0])
            {
                if(path.Count == 1)
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), this.point);
                    order += 1;
                    continue;
                }
                if(path[1] == new Vector2Int(point.x + 1, point.y))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), toRight);
                    order += 1;
                    continue;
                }
                if (path[1] == new Vector2Int(point.x, point.y + 1))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), toTop);
                    order += 1;
                    continue;
                }
                if (path[1] == new Vector2Int(point.x - 1, point.y))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), toLeft);
                    order += 1;
                    continue;
                }
                if (path[1] == new Vector2Int(point.x, point.y - 1))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), toBottom);
                    order += 1;
                    continue;
                }

            }
            else if(point == path[path.Count - 1])
            {

                if (path[path.Count - 2] == new Vector2Int(point.x + 1, point.y))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), fromRight);
                    order += 1;
                    continue;
                }
                if (path[path.Count - 2] == new Vector2Int(point.x, point.y + 1))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), fromTop);
                    order += 1;
                    continue;
                }
                if (path[path.Count - 2] == new Vector2Int(point.x - 1, point.y))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), fromLeft);
                    order += 1;
                    continue;
                }
                if (path[path.Count - 2] == new Vector2Int(point.x, point.y - 1))
                {
                    tilemap.SetTile(new Vector3Int(point.x, point.y, 0), fromBottom);
                    order += 1;
                    continue;
                }

            }
            else
            {

                // means comes from right
                if (path[order - 1].x > point.x)
                {
                    // means goes to left
                    if (path[order + 1].x < point.x)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), horizontal);
                        order += 1;
                        continue;
                    }
                    // means goes to up
                    if (path[order + 1].y > point.y)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), rightToUp);
                        order += 1;
                        continue;
                    }
                    // means goes to down
                    if (path[order + 1].y < point.y)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), rightToDown);
                        order += 1;
                        continue;
                    }

                }
                // means comes from left
                if (path[order - 1].x < point.x)
                {
                    // means goes to right
                    if (path[order + 1].x > point.x)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), horizontal);
                        order += 1;
                        continue;
                    }
                    // means goes to up
                    if (path[order + 1].y > point.y)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), leftToUp);
                        order += 1;
                        continue;
                    }
                    // means goes to down
                    if (path[order + 1].y < point.y)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), leftToDown);
                        order += 1;
                        continue;
                    }
                }
                // means comes from up
                if (path[order - 1].y > point.y)
                {
                    // means goes to right
                    if (path[order + 1].x > point.x)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), rightToUp);
                        order += 1;
                        continue;
                    }
                    // means goes to left
                    if (path[order + 1].x < point.x)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), leftToUp);
                        order += 1;
                        continue;
                    }
                    // means goes to down
                    if (path[order + 1].y < point.y)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), vertical);
                        order += 1;
                        continue;
                    }
                }
                // means comes from dowm
                if (path[order - 1].y < point.y)
                {
                    // means goes to right
                    if (path[order + 1].x > point.x)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), rightToDown);
                        order += 1;
                        continue;
                    }
                    // means goes to left
                    if (path[order + 1].x < point.x)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), leftToDown);
                        order += 1;
                        continue;
                    }
                    // means goes to up
                    if (path[order + 1].y > point.y)
                    {
                        tilemap.SetTile(new Vector3Int(point.x, point.y, 0), vertical);
                        order += 1;
                        continue;
                    }
                }

            }
        }


    }
        
    public void FixPath(Tilemap tilemap, List<Vector3> path)
    {
        List<Vector2Int> gridPath = new List<Vector2Int>();

        foreach(Vector3 pos in path)
        {
            //if (pos == path[0])
            //    continue;

            gridPath.Add((Vector2Int)tilemap.WorldToCell(pos));
        }

        FixPath(tilemap, gridPath);
    }

}
