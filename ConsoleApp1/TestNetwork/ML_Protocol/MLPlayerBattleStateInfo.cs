
using LitJsonEx;
using System.Collections.Generic;

/**
 * �����Ϸ�е�״̬��Ϣ
 */

public sealed class MLPlayerBattleStateInfo : MLAIProtoBase
{
    public int iExp = 0;
    public int iLevel = 0;
    public int iMoney = 0;
    public int iRank = 0;
    public int iHP = 0;
    public int iFetter = 0;

    public override void WriteTo(JsonData data)
    {
        data["exp"] = iExp;
        data["level"] = iLevel;
        data["money"] = iMoney;
        data["rank"] = iRank;
        data["life"] = iHP;
        data["fetter"] = iFetter;
    }
}