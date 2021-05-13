using LitJsonEx;

public class MLResetEnvMsg : MLAIProtoBase
{
    public long lUin = 0;

    public override void WriteTo(JsonData data)
    {
        data["lUin"] = lUin;
    }

    public override void ReadFrom(JsonData data)
    {
        lUin = ReadInt(data, "lUin", 0);
    }
}