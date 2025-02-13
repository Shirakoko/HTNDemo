using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class PrimitiveTask : IBaseTask
{
    protected float _startTime = -1f;
    protected float _duration = 0.0f;

    public abstract string GetTaskName();
    //原子任务不可以再分解为子任务，所以AddNextTask方法不必实现
    void IBaseTask.AddNextTask(IBaseTask nextTask)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 执行前判断条件是否满足，传入null时直接修改HTNWorld
    /// </summary>
    /// <param name="worldState">用于plan的世界状态副本</param>
    public bool MetCondition(Dictionary<string, object> worldState = null)
    {
        if(worldState == null)//实际运行时
        {
            return MetCondition_OnRun();
        }
        else//模拟规划时，若能满足条件就直接进行Effect
        {
            if(MetCondition_OnPlan(worldState))
            {
                Effect_OnPlan(worldState);
                return true;
            }
            return false;
        }
    }
    protected virtual bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        return true;
    }
    protected virtual bool MetCondition_OnRun()
    {
        return true;
    }
    
    //任务的具体运行逻辑，交给具体类实现
    public abstract EStatus Operator();

    /// <summary>
    /// 执行成功后的影响，传入null时直接修改HTNWorld
    /// </summary>
    /// <param name="worldState">用于plan的世界状态副本</param>
    public void Effect(Dictionary<string, object> worldState = null)
    {
        Effect_OnRun();
    }
    protected virtual void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        ;
    }
    protected virtual void Effect_OnRun()
    {
        ;
    }
}
