using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Meow : PrimitiveTask
{
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
        return mood >= 8 && full >= 5; // 规划时条件：心情值 >= 8 且饱腹值 >= 5
    }

    public override EStatus Operator()
    {
        // 叫唤任务的执行逻辑（例如动画、耗时等）
        Debug.Log("叫唤");
        return EStatus.Success; // 假设直接成功
    }

    protected override void Effect_OnRun()
    {
        int mood = HTNWorld.GetWorldState<int>("_mood");
        int full = HTNWorld.GetWorldState<int>("_full");

        HTNWorld.UpdateState("_mood", Math.Max(mood - 1, 0));
        HTNWorld.UpdateState("_full", Math.Max(full - 1, 0));
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int mood = (int)worldState["_mood"];
        int full = HTNWorld.GetWorldState<int>("_full");


        // 确保 _mood 不低于最小值 0
        worldState["_mood"] = Math.Max(mood - 1, 0);
        HTNWorld.UpdateState("_full", Math.Max(full - 1, 0));
    }
}