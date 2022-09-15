using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Road Rule Tile", menuName = "TheMerchant/Tiles/Road Rule Tile")]
public class RoadRuleTile : RuleTile
{

    /*public enum FieldTheme
    {
        Plain,
        Forest,
        Mountain,
        Road,
        River,
        Sea,
        Bridge,
        Village,
        Town,
        Tower,
        Castle,
        City
    }

    public enum SettlementEntrance
    {
        None,
        CityEntrance,
        VillageEntrance,
        Castle,
        TowerEntrance,
        MineEntrance
    }*/

    public bool buildable;

    [Range(0,5)]
    public float speed = 1;
    public bool movable { get { return speed != 0; } }

    //public FieldTheme theme;
    public bool settlementTile;
    //public Atmosphere atmosphere;

    //public List<GameEvent> eventPool;

    //public bool gatherArea = false;

    //public double workTime = 100;

    //public double workCost = 20;

    //public List<RandomPoolItem> poolItems;

    public override string ToString()
    {
        return "Speed: " + speed.ToString() + ", Movable: " + movable.ToString();
    }

    //public Item PullItem()
    //{
    //    Item resultItem = new Item();

    //    foreach(RandomPoolItem poolItem in poolItems)
    //    {

    //        float rate = Random.Range(0, 1f);

    //        if (rate > poolItem.rate)
    //            continue;

    //        resultItem.Set(poolItem.Pull());

    //        break;

    //    }

    //    return resultItem;
    //}

    //public List<Item> PullItems()
    //{
    //    List<Item> resultItems = new List<Item>();

    //    foreach (RandomPoolItem poolItem in poolItems)
    //    {

    //        float rate = Random.Range(0, 1f);

    //        if (rate > poolItem.rate)
    //            continue;

    //        Item itemInstance = new Item();

    //        itemInstance.Set(poolItem.Pull());

    //        if (itemInstance.Amount() <= 0)
    //            continue;

    //        resultItems.Add(itemInstance);


    //    }

    //    return resultItems;
    //}

    //public bool CanGather()
    //{
    //    return gatherArea;
    //}



}
