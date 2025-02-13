using System.Collections.Generic;

public class CompoundTask : IBaseTask
{
    // 选中的方法
    public Method ValidMethod { get; private set; }
    // 子任务（方法）列表
    private readonly List<Method> methods;

    public CompoundTask()
    {
        methods = new List<Method>();
    }

    public void AddNextTask(IBaseTask nextTask)
    {
        // 要判断添加进来的是不是方法类，是的话才添加
        if (nextTask is Method m)
        {
            methods.Add(m);
        }
    }

    public bool MetCondition(Dictionary<string, object> worldState)
    {
        // 收集所有满足条件的方法
        var validMethods = new List<Method>();
        for (int i = 0; i < methods.Count; ++i)
        {
            if (methods[i].MetCondition(worldState))
            {
                validMethods.Add(methods[i]);
            }
        }

        // 如果有满足条件的方法，则随机选择一个
        if (validMethods.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, validMethods.Count);
            ValidMethod = validMethods[randomIndex];
            return true;
        }

        // 如果没有满足条件的方法，则返回 false
        return false;
    }
}
