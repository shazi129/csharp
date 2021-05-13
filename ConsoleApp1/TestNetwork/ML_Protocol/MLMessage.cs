
using LitJsonEx;
using System.Collections.Generic;

public enum E_ML_MSG_ID
{
    E_NONE,
    E_ENV_RESET,
    E_START_GAME,
    E_STEP_ACTION,
    E_STEP_OBSERVATION,

    TypeCount
}


public class MLMsgHead : MLAIProtoBase
{
    public long lUin = 0;
    public long lSeq = 0;
    public E_ML_MSG_ID eMsgID = E_ML_MSG_ID.E_NONE;

    public override void ReadFrom(JsonData data)
    {
        lUin = ReadLong(data, "lUin", 0);
        lSeq = ReadLong(data, "lSeq", 0);
        eMsgID = (E_ML_MSG_ID)ReadIntInRange(data, "eMsgID", 0, (int)E_ML_MSG_ID.TypeCount - 1, (int)E_ML_MSG_ID.E_NONE);
    }

    public override void WriteTo(JsonData data)
    {
        data["lUin"] = lUin;
        data["lSeq"] = lSeq;
        data["eMsgID"] = (int)eMsgID;
    }
}


public sealed class MLMessage : MLAIProtoBase
{
    public MLMsgHead stMsgHead = new MLMsgHead();
    public JsonData msgContent = null;

    public MLMessage()
    {
        msgContent = new JsonData();
        msgContent.SetJsonType(JsonType.Object);
    }

    public override void ReadFrom(JsonData data)
    {
        stMsgHead.ReadFrom(data["stMsgHead"]);
        msgContent = ReadObject(data, "msgContent", null);
    }

    public override void WriteTo(JsonData data)
    {
        WriteStruct(data, "stMsgHead", stMsgHead);
        data["msgContent"] = msgContent;
    }
}