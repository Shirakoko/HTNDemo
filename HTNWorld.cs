using System;
using System.Collections.Generic;


//世界状态只有一个即可，我们将其设为静态类
public static class HTNWorld
{
    //读 世界状态的字典
    private static readonly Dictionary<string, Func<object>> get_WorldState;
    //写 世界状态的字典
    private static readonly Dictionary<string, Action<object>> set_WorldState;
    
    static HTNWorld()
    {
        get_WorldState = new Dictionary<string, Func<object>>();
        set_WorldState = new Dictionary<string, Action<object>>();
    }
    //添加一个状态，需要传入状态名、读取函数和写入函数
    public static void AddState(string key, Func<object> getter, Action<object> setter)
    {
        get_WorldState[key] = getter;
        set_WorldState[key] = setter;
    }
    //根据状态名移除某个世界状态
    public static void RemoveState(string key)
    {
        get_WorldState.Remove(key);
        set_WorldState.Remove(key);
    }
    //修改某个状态的值
    public static void UpdateState(string key, object value)
    {
        //就是通过写入字典修改的
        set_WorldState[key].Invoke(value);
    }
    //读取某个状态的值，利用泛型，可以将获取的object转为指定的类型
    public static T GetWorldState<T>(string key) 
    {
        return (T)get_WorldState[key].Invoke();
    }
    //复制一份当前世界状态的值（这个主要是用在规划中）
    public static Dictionary<string, object> CopyWorldState()
    {
        var copy = new Dictionary<string, object>();
        foreach(var state in get_WorldState)
        {
            copy.Add(state.Key, state.Value.Invoke());
        }
        return copy;
    }
}
