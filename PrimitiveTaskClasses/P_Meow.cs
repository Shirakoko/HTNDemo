using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Meow : PrimitiveTask
{
    public P_Meow(float duration) : base(duration)
    {
    }

    public override string GetTaskName()
    {
        return "叫唤";
    }

    protected override bool MetCondition_OnRun()
    {
        int mood = HTNWorld.GetWorldState<int>("_mood");
        int full = HTNWorld.GetWorldState<int>("_full");
        return mood >= 8 && full >= 5; // 叫唤条件：心情值 >= 8 且饱腹值 >= 5
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        int mood = (int)worldState["_mood"];
        int full = (int)worldState["_full"];
        return mood >= 7 && full >= 5; // 规划时条件：心情值 >= 8 且饱腹值 >= 5
    }

    public override EStatus Operator()
    {
        // 叫唤任务的执行逻辑（例如动画、耗时等）
        if(_startTime < 0)
        {
            _startTime = Time.time;
            Debug.Log("开始叫唤...");
        }


        if(Time.time - _startTime >= this._duration)
        {
            Debug.Log($"叫唤完毕，耗时{this._duration}");
            _startTime = -1;
            return EStatus.Success;
        }
        return EStatus.Running;
    }

    protected override void Effect_OnRun()
    {
        int mood = HTNWorld.GetWorldState<int>("_mood");
        int full = HTNWorld.GetWorldState<int>("_full");

        HTNWorld.UpdateState("_mood", Math.Max(mood - 1, 0));
        HTNWorld.UpdateState("_full", Math.Max(full - 1, 0));
        HTNWorld.UpdateState("_masterBeside", true);
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int mood = (int)worldState["_mood"];
        int full = HTNWorld.GetWorldState<int>("_full");


        // 确保 _mood 不低于最小值 0
        worldState["_mood"] = Math.Max(mood - 1, 0);
        worldState["_full"] = Math.Max(full - 1, 0);
        worldState["_masterBeside"] = true;
    }
}