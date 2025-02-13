using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Drink : PrimitiveTask
{
    public override string GetTaskName()
    {
        return "喝水";
    }

    // 无条件
    protected override bool MetCondition_OnRun() => true; // 无条件

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState) => true; // 无条件

    public override EStatus Operator()
    {
        // 喝水任务的执行逻辑（例如动画、耗时等）
        Debug.Log("喝水");
        return EStatus.Success; // 假设直接成功
    }

    protected override void Effect_OnRun()
    {
        int full = HTNWorld.GetWorldState<int>("_full");
        int mood = HTNWorld.GetWorldState<int>("_mood");

        // 确保 _full 和 _mood 不超过最大值 10
        HTNWorld.UpdateState("_full", Math.Min(full + 1, 10));
        HTNWorld.UpdateState("_mood", Math.Min(mood + 1, 10));
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int full = (int)worldState["_full"];
        int mood = (int)worldState["_mood"];

        // 确保 _full 和 _mood 不超过最大值 10
        worldState["_full"] = Math.Min(full + 1, 10);
        worldState["_mood"] = Math.Min(mood + 1, 10);
    }
}