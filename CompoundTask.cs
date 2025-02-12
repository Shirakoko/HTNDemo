using System.Collections.Generic;

public class CompoundTask : IBaseTask
{
    //选中的方法
    public Method ValidMethod { get; private set; }
    //子任务（方法）列表
    private readonly List<Method> methods;
    
    public CompoundTask()
    {
        methods = new List<Method>();
    }
    
    public void AddNextTask(IBaseTask nextTask)
    {
        //要判断添加进来的是不是方法类，是的话才添加
        if (nextTask is Method m)
        {
            methods.Add(m);
        }
    }
    
    public bool MetCondition(Dictionary<string, object> worldState)
    {
        for (int i = 0; i < methods.Count; ++i)
        {
            //只要有一个方法满足前提条件就可以
            if(methods[i].MetCondition(worldState))
            {
                //记录下这个满足的方法
                ValidMethod = methods[i];
                return true;
            }
        }
        return false;
    }
}
