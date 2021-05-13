using LitJsonEx;
using System.Collections.Generic;

public enum EPlayerMoveAction
{
    None,
    Pickup_Items
}

public class ActionResponse : MLAIProtoBase
{
    public string sResponseName;
    public int iResponseResult;

    public override void WriteTo(JsonData data)
    {
        data["sResponseName"] = sResponseName;
        data["iResponseResult"] = iResponseResult;
    }

}

public class MLObservationMsg : MLAIProtoBase
{
    //用户uin
    public long lUin = 0;

    //上次操作的结果
    public List<ActionResponse> vecActionResponse = new List<ActionResponse>();

    //可使用的装备列表
    public MLAvailableEquipList AvailableEquipList = new MLAvailableEquipList();

    //小小英雄当前移动目的
    public EPlayerMoveAction ePlayerMoveAction = EPlayerMoveAction.None;

    public override void WriteTo(JsonData data)
    {
        data["lUin"] = lUin;
        WriteListTo(data, "vecActionResponse", vecActionResponse);
        data["ePlayerMoveAction"] = (int)ePlayerMoveAction;
        WriteStruct(data, "vecAvailableEquipList", AvailableEquipList);
    }
}