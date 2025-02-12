using System;
using System.Collections.Generic;

public class Method : IBaseTask
{
    //子任务列表，可以是复合任务，也可以是原点任务
    public List<IBaseTask> SubTask { get; private set; }
    //方法的前提条件
    private readonly Func<bool> condition;

    public Method(Func<bool> condition)
    {
        SubTask = new List<IBaseTask>();
        this.condition = condition;
    }
    //方法条件满足的判断=方法本身前提条件满足+所有子任务条件满足
    public bool MetCondition(Dictionary<string, object> worldState = null)
    {
        /*
        再复制一遍世界状态，用于追踪每个子任务的Effect。方法有多个子任务，
        只要其中一个不满足条件，那整个方法不满足条件，之前子任务进行Effect也不算数
        因此用tpWorld记录，待验证了方法满足条件后（所有子任务均满足条件），再复制回worldState
        */
        var tpWorld = new Dictionary<string, object>(worldState);
        if (condition())//方法自身的前提条件是否满足
        {
            for (int i = 0; i < SubTask.Count; ++i)
            {
                //一旦有一个子任务的条件不满足，这个方法就不满足了
                if(!SubTask[i].MetCondition(tpWorld))
                {
                    return false;
                }
            }
            //最终满足条件后，再将各Effect导致的新世界状态（tpWorld）给worldState
            worldState = tpWorld;
            return true;//如果子任务全都满足了，那就成了！
        }
        return false;
    }
    //添加子任务
    public void AddNextTask(IBaseTask nextTask)
    {
        SubTask.Add(nextTask);
    }
}
