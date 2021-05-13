using LitJsonEx;
using System.Collections.Generic;


public enum E_MLAI_STATE
{
    E_PLAYING, 
    E_TAINNING
}

public class MLAIPlayerInfo : MLAIProtoBase
{
    public long lUin = 0;
    public E_MLAI_STATE eState = E_MLAI_STATE.E_PLAYING;
    public int iLevel = 0;

    public override void WriteTo(JsonData data)
    {
        data["lUin"] = lUin;
        data["eState"] = (int)eState;
        data["iLevel"] = iLevel;
    }
}

public class MLStartGameReq : MLAIProtoBase
{
    public int iID = 0;

    public List<MLAIPlayerInfo> vecPlayerInfo = new List<MLAIPlayerInfo>();

    public override void WriteTo(JsonData data)
    {
        data["ID"] = iID;
        WriteListTo(data, "vecPlayerInfo", vecPlayerInfo);
    }
}