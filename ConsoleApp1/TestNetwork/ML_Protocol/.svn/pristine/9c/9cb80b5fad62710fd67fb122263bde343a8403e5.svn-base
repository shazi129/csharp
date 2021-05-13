using LitJsonEx;
using System;
using System.Collections.Generic;

public class MLAIProtoBase
{
    public virtual void WriteTo(JsonData data) 
    {
        throw new NotImplementedException(); 
    }

    public virtual void ReadFrom(JsonData data)
    {
        throw new NotImplementedException();
    }

    public static bool ValidField(JsonData data, string key, JsonType jsonType)
    {
        if (data.ContainsKey(key) && data[key].GetJsonType() == jsonType)
        {
            return true;
        }
        return false;
    }

    public static long ReadLong(JsonData data, string key, long defaultValue)
    {
        return ValidField(data, key, JsonType.Long) ? (long)data[key] : defaultValue;
    }

    public static int ReadInt(JsonData data, string key, int defaultValue)
    {
        return ValidField(data, key, JsonType.Int) ? (int)data[key] : defaultValue;
    }

    public static int ReadIntInRange(JsonData data, string key, int min, int max, int defaultValue)
    {
        int value = ReadInt(data, key, defaultValue);
        if (value < min || value > max)
        {
            value = defaultValue;
        }
        return value;
    }

    public static JsonData ReadObject(JsonData data, string key, JsonData defaultValue)
    {
        if (ValidField(data, key, JsonType.Object))
        {
            return data[key];
        }
        else if (defaultValue != null)
        {
            return defaultValue;
        }
        else
        {
            defaultValue = new JsonData();
            defaultValue.SetJsonType(JsonType.Object);
            return defaultValue;
        }
    }


    public static void WriteListTo<T>(JsonData data, string name, List<T> list) where T : MLAIProtoBase
    {
        JsonData listData = new JsonData();
        listData.SetJsonType(JsonType.Array);
        for (int i = 0; i < list.Count; i++)
        {
            JsonData item = new JsonData();
            list[i].WriteTo(item);
            listData.Add(item);
        }

        data[name] = listData;
    }

    public static void WriteStruct(JsonData data, string name, MLAIProtoBase st)
    {
        JsonData stData = new JsonData();
        st.WriteTo(stData);
        data[name] = stData;
    }
}