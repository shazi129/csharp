using LitJsonEx;
using ZGameChess;

public sealed class MLEquipEquipmentAction : MLAIProtoBase
{
    //英雄所在的位置
    public AreaType eAreaType = AreaType.None;
    public int iHeroPosX = 0;
    public int iHeroPosY = 0;

    //装备ID
    public int iEquipEntryID = 0;

    public override void ReadFrom(JsonData data)
    {
        int areaType = ValidField(data, "eAreaType", JsonType.Int) ? (int)data["eAreaType"] : 0;
        if (areaType < (int)AreaType.None || areaType > (int)AreaType.EnemyWait)
        {
            eAreaType = AreaType.None;
        }
        else
        {
            eAreaType = (AreaType)areaType;
        }

        iHeroPosX = ValidField(data, "iHeroPosX", JsonType.Int) ? (int)data["iHeroPosX"] : 0;
        iHeroPosY = ValidField(data, "iHeroPosY", JsonType.Int) ? (int)data["iHeroPosY"] : 0;
        iEquipEntryID = ValidField(data, "iEquipEntryID", JsonType.Int) ? (int)data["iEquipEntryID"] : 0;
    }

    public override void WriteTo(JsonData data)
    {
        data["eAreaType"] = (int)eAreaType;
        data["iHeroPosX"] = (int)iHeroPosX;
        data["iHeroPosY"] = (int)iHeroPosY;
        data["iEquipEntryID"] = (int)iEquipEntryID;
    }
}