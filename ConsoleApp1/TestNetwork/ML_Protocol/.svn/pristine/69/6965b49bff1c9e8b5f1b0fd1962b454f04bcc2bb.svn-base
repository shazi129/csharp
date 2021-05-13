using LitJsonEx;
using System.Collections.Generic;

public sealed class MLGameStatistics : MLAIProtoBase
{
    //未装备的装备信息
    public List<MLEquipInfo> vecNotEquipedEquipment = new List<MLEquipInfo>();

    //已装备的装备信息
    public List<MLEquipInfo> vecEquipedEquipment = new List<MLEquipInfo>();

    public override void WriteTo(JsonData data)
    {
        WriteListTo(data, "vecNotEquipedEquipment", vecNotEquipedEquipment);
        WriteListTo(data, "vecEquipedEquipment", vecEquipedEquipment);
    }
}