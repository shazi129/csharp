using LitJsonEx;
using System.Collections.Generic;

//在售卖的英雄信息
public sealed class MLOnSaleHeroInfo : MLAIProtoBase
{
    public int iEntityID;  //英雄实体ID
    public int iConfigId;  //配置id, 用于查找信息
    public int iPos;       //列表位置
    public int iPrice;     //价格

    public override void WriteTo(JsonData data)
    {
        data["id"] = iConfigId;
        data["iEntityID"] = iEntityID;
        data["iPrice"] = iPrice;
        data["iPos"] = iPos;
    }
}