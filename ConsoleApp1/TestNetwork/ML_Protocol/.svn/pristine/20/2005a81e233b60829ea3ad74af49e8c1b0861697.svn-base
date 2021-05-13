
using LitJsonEx;
using System.Collections.Generic;

public sealed class MLHeroInfo : MLAIProtoBase
{
    public int iEntityID;  //英雄实体ID
    public int iConfigId;  //配置id, 用于查找信息
    public List<MLEquipInfo> vecEquipList = new List<MLEquipInfo>();  //查配置表的id

    //英雄所在的位置
    public int iPosX;
    public int iPosY;

    public override void WriteTo(JsonData data)
    {
        data["id"] = iConfigId;
        data["iEntityID"] = iEntityID;
        data["iPosX"] = iPosX;
        data["iPosY"] = iPosY;
        WriteListTo(data, "vecEquipList", vecEquipList);
    }
}