using KC.RTS.Buildings;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{


    public Pathfinding.Pathfinding pathfinding;
    public Vector3 originPosition;
    public Vector2Int upperLimits;
    public Vector2Int lowerLimits;

    private void Awake()
    {

        Tilemap roadTileMap = GameObject.Find("RoadMap").GetComponent<Tilemap>();
        
        pathfinding = new Pathfinding.Pathfinding(upperLimits.x - lowerLimits.x, upperLimits.y - lowerLimits.y, GetComponent<Grid>().cellSize.x, lowerLimits, originPosition, roadTileMap);

    }


    public List<Vector2Int> GetPath(Vector2Int startPos, Vector2Int endPos)
    {

        List<Pathfinding.PathNode> path = pathfinding.FindPath(startPos, endPos);

        if(path == null)
        {

            //GameSystems.PrintUI("No Route!");

            return null;
        }

        List<Vector2Int> positions = new List<Vector2Int>();


        foreach(Pathfinding.PathNode node in path)
        {

            positions.Add(node.position);

        }


        return positions;
    }
    public List<Vector2> GetPath(Vector3 startPos, Vector3 endPos)
    {

        Grid grid = GetComponent<Grid>();

        Vector2Int startp = (Vector2Int)grid.WorldToCell(startPos);
        Vector2Int endp = (Vector2Int)grid.WorldToCell(endPos);

        List<Pathfinding.PathNode> path = pathfinding.FindPath(startp, endp);

        if (path == null)
        {

            //GameSystems.PrintUI("No Route!");

            return null;
        }

        List<Vector2Int> positions = new List<Vector2Int>();


        foreach (Pathfinding.PathNode node in path)
        {

            positions.Add(node.position);

        }

        List<Vector2> returnList = positions.ConvertAll<Vector2>((x)=>((Vector2)grid.CellToWorld((Vector3Int)x) + new Vector2(0.5f, 0.5f)));

        return returnList;
    }

    public Vector2Int RandomPosition()
    {
        return new Vector2Int(Random.Range(lowerLimits.x, upperLimits.x), Random.Range(lowerLimits.y, upperLimits.y));
    }


    public List<PathNode> GetBuildingArea(Vector3 p_position, Building building)
    {
        Grid grid = GetComponent<Grid>();

        Vector2Int position = (Vector2Int)grid.WorldToCell(p_position);
        List<PathNode> area = new List<PathNode>();


        for(int x = 0; x < building.size.x; ++x)
        {
            for (int y = 0; y < building.size.y; ++y)
            {

                Vector2Int pos = position + new Vector2Int(x, y);
                
                PathNode node = pathfinding.GetGrid().GetValue(pos);
                area.Add(node);
            }

        }

        return area;
    }

    public void SetBuildingAreaUnable(Vector3 p_position, Building building)
    {
        Grid grid = GetComponent<Grid>();

        Vector2Int position = (Vector2Int)grid.WorldToCell(p_position);


        for(int x = 0; x < building.size.x; ++x)
        {
            for (int y = 0; y < building.size.y; ++y)
            {

                Vector2Int pos = position + new Vector2Int(x, y);

                Grid<PathNode> pathGrid = pathfinding.GetGrid();
                
                PathNode node = pathGrid.GetValue(pos);

                node.speed = 0;
                node.buildable = false;

                pathGrid.SetValue(pos, node);
                pathfinding.SetGrid(pathGrid);

            }

        }

    }

    //public Vector2Int RandomSettlement()
    //{
    //    Vector2Int settlementGridPos = new Vector2Int();

    //    List<SettlementObject> settlements = new List<SettlementObject>(FindObjectsOfType<SettlementObject>());

    //    SettlementObject randomSettlement = settlements[Random.Range(0, settlements.Count - 1)];

    //    settlementGridPos = (Vector2Int)FindObjectOfType<Grid>().WorldToCell(randomSettlement.transform.position);

    //    return settlementGridPos;
    //}

    //public Vector2Int RandomCloseSettlement(Vector2Int currentPosition)
    //{
    //    Vector2Int settlementGridPos = new Vector2Int();

    //    List<SettlementObject> settlements = new List<SettlementObject>(FindObjectsOfType<SettlementObject>());

    //    settlements.Sort(delegate(SettlementObject A, SettlementObject B)
    //    {
    //        int distanceA = Mathf.RoundToInt(Vector2Int.Distance(currentPosition, A.position));
    //        int distanceB = Mathf.RoundToInt(Vector2Int.Distance(currentPosition, B.Position));

    //        return distanceA.CompareTo(distanceB);
    //    });

    //    //settlements.Reverse();

    //    string closestSetlements = "";

    //    for(int i = 0; i < 5; ++i)
    //    {
    //        closestSetlements += settlements[i].settlement.name + ",";
    //    }

    //    closestSetlements = closestSetlements.Substring(0, closestSetlements.Length - 1);

    //    //Debug.Log(closestSetlements);

    //    SettlementObject randomSettlement = settlements[Random.Range(0, 5)];

    //    settlementGridPos = (Vector2Int)FindObjectOfType<Grid>().WorldToCell(randomSettlement.transform.position);

    //    return settlementGridPos;
    //}


}
