using LitJsonEx;

public sealed class MLEquipInfo : MLAIProtoBase
{
    public int iEntityID;  //实体的id
    public int iTableID;  //查配置表的id

    public override void WriteTo(JsonData data)
    {
        data["iEntityID"] = iEntityID;
        data["iTableID"] = iTableID;
    }
}