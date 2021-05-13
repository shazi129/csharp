using LitJsonEx;
using System.Collections.Generic;

public sealed class MLAvailableEquipList : MLAIProtoBase
{
    public List<MLEquipInfo> EquipList = new List<MLEquipInfo>();

    public override void WriteTo(JsonData data)
    {
        WriteListTo(data, "EquipList", EquipList);
    }
}