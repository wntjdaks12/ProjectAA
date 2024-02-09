using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDataInfo : Data
{
    public List<DropGoodsInfo> DropGoodsInfos { get; set; }
    public List<DropItemInfo> DropItemInfos { get; set; }
}

public class DropGoodsInfo
{
    public int Id { get; set; }
    public int Probability { get; set; }
}

public class DropItemInfo
{
    public int Id { get; set; }
    public int Probability { get; set; }
}