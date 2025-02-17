using System.Collections.Generic;

public class P_TestTask : PrimitiveTask
{
    public P_TestTask(float duration) : base(duration)
    {
        this._task = Task.Test;
    }

    protected override bool MetCondition_OnRun() => true;

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState) => true;

    protected override void Effect_OnRun()
    {
        
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        
    }
}