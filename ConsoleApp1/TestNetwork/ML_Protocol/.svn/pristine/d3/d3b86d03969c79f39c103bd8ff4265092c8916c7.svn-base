using LitJsonEx;


public enum E_ML_ACTION_ID
{
    ACTION_NONE,
    #region 正常的ActionID

    Equip_Equipment_Action,

    #endregion
    ACTION_UNRECOGNIZED,
}

//AI 操作消息
public sealed class MLGameActionMsg : MLAIProtoBase
{
    public long lUin = 0;

    public E_ML_ACTION_ID eActionID = E_ML_ACTION_ID.ACTION_NONE;

    public JsonData actionData = null;

    public override void ReadFrom(JsonData data)
    {
        lUin = ReadInt(data, "lUin", 0);
        eActionID = (E_ML_ACTION_ID)ReadIntInRange(data, "eActionID", 0, (int)(E_ML_ACTION_ID.ACTION_UNRECOGNIZED - 1), 0);
        actionData = ValidField(data, "actionData", JsonType.Object) ? data["actionData"] : new JsonData();
    }

    public override void WriteTo(JsonData data)
    {
        data["lUin"] = lUin;
        data["eActionID"] = (int)eActionID;
        data["actionData"] = actionData;
    }
}
