using LitJsonEx;

public sealed class MLEquipInfo : MLAIProtoBase
{
    public int iEntityID;  //ʵ���id
    public int iTableID;  //�����ñ��id

    public override void WriteTo(JsonData data)
    {
        data["iEntityID"] = iEntityID;
        data["iTableID"] = iTableID;
    }
}