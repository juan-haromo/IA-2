using System.Collections.Generic;

public class BlackBoard
{
    private Dictionary<string, object> data;

    public BlackBoard()
    {
        data = new Dictionary<string, object>();
    }

    public void SetValue<T>(string key, T value)
    {
        if(!data.TryAdd(key, value))
        {
            data[key] = value;   
        }
    }

    
    public T GetValue<T>(string key)
    {
        if (data.ContainsKey(key) && (T)data[key] != null)
        { 
            return (T)data[key];
        }
        return default;
    }
}
