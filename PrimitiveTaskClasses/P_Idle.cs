using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Idle : PrimitiveTask
{
    public P_Idle(float duration) : base(duration)
    {
    }

    public override string GetTaskName()
    {
        return "发呆";
    }

    protected override bool MetCondition_OnRun()
    {
        // 无条件执行
        return true;
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        // 无条件执行
        return true;
    }

    public override EStatus Operator()
    {
        // 发呆任务的执行逻辑（例如动画、耗时等）
        if(_startTime < 0)
        {
            _startTime = Time.time;
            Debug.Log("开始发呆...");
        }


        if(Time.time - _startTime >= this._duration)
        {
            Debug.Log($"发呆完毕，耗时{this._duration}");
            _startTime = -1;
            return EStatus.Success;
        }
        return EStatus.Running;
    }

    protected override void Effect_OnRun()
    {
        int mood = HTNWorld.GetWorldState<int>("_mood");

        // 确保 _mood 不超过最大值 10
        HTNWorld.UpdateState("_mood", Math.Min(mood + 1, 10));
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int mood = (int)worldState["_mood"];

        // 确保 _mood 不超过最大值 10
        worldState["_mood"] = Math.Min(mood + 1, 10);
    }
}