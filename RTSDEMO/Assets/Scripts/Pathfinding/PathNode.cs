using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Pathfinding
{

    [System.Serializable]
    public class PathNode
    {

        private Grid<PathNode> grid;
        public Vector2Int position;

        [HideInInspector]
        public int gCost;
        [HideInInspector]
        public int hCost;
        [HideInInspector]
        public int fCost;

        [HideInInspector]
        public PathNode cameFromNode;
        //[HideInInspector]
        public RoadRuleTile roadRuleTile;

        public int key = 0;

        public float speed = 0;

        public bool buildable;

        public PathNode(Grid<PathNode> grid, Vector2Int position, Tilemap tileMap)
        {

            this.grid = grid;
            this.position = position;
            roadRuleTile = tileMap.GetTile((Vector3Int)position) as RoadRuleTile;

            if(roadRuleTile != null)
            {
                buildable = roadRuleTile.buildable;
                speed = roadRuleTile.speed;
                
            }

            key = Random.Range(1000000000, int.MaxValue);

        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public override string ToString()
        {
            return position.x + ":" + position.y + "\n" + key + "\n" + roadRuleTile;
        }

        public bool Walkable()
        {
            if(roadRuleTile != null)
                return speed != 0;

            return false;
        }

        

        public int GetCost()
        {

            if(roadRuleTile != null)
                return Mathf.RoundToInt((1 / (roadRuleTile.speed * 100)) * 100);

            return int.MaxValue;
        }


        //public void FindSettlement()
        //{

        //    bool isTyped = false;
        //    Settlement.SettlementType type = Settlement.SettlementType.Village;
        //    switch (roadRuleTile.theme)
        //    {
        //        default:
        //            isTyped = false;
        //            break;
        //        case RoadRuleTile.FieldTheme.City:
        //            isTyped = true;
        //            type = Settlement.SettlementType.City;
        //            break;
        //        case RoadRuleTile.FieldTheme.Castle:
        //            isTyped = true;
        //            type = Settlement.SettlementType.Castle;
        //            break;
        //        case RoadRuleTile.FieldTheme.Tower:
        //            isTyped = true;
        //            type = Settlement.SettlementType.Tower;
        //            break;
        //        case RoadRuleTile.FieldTheme.Village:
        //            isTyped = true;
        //            type = Settlement.SettlementType.Village;
        //            break;
        //        case RoadRuleTile.FieldTheme.Town:
        //            isTyped = true;
        //            type = Settlement.SettlementType.Town;
        //            break;



        //    }

        //    if(isTyped)
        //        settlementObject = SettlementObject.FindSettlementObjectAtPosition(position, type);
        //    else
        //        settlementObject = SettlementObject.FindSettlementObjectAtPosition(position);

        //}

        //public List<PathNode> GetAllSettlementNodes(ref List<PathNode> checkedNodes)
        //{
        //    if (settlementObject == null)
        //        return new List<PathNode>();

        //    List<PathNode> nodes = new List<PathNode>();

        //    nodes.Add(this);
        //    checkedNodes.Add(this);

        //    foreach(PathNode node in grid.GetNeighbourList(position))
        //    {

        //        if (node == null)
        //            continue;

        //        if (checkedNodes.Contains(node))
        //            continue;

        //        if(node.settlementObject == this.settlementObject)
        //        {

        //            if (!nodes.Contains(node))
        //            {
        //                nodes.Add(node);

        //                nodes.AddRange(node.GetAllSettlementNodes(ref checkedNodes));

        //            }

        //        }

        //    }

        //    return nodes;
        //}
    }


}