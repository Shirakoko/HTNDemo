using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Poop : PrimitiveTask
{
    protected override bool MetCondition_OnRun()
    {
        int full = HTNWorld.GetWorldState<int>("_full");
        return full >= 6; // 拉屎条件：饱腹值 >= 6
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        int full = (int)worldState["_full"];
        return full >= 6; // 规划时条件：饱腹值 >= 6
    }

    public override EStatus Operator()
    {
        // 拉屎任务的执行逻辑（例如动画、耗时等）
        Debug.Log("拉屎");
        return EStatus.Success; // 假设直接成功
    }

    protected override void Effect_OnRun()
    {
        int full = HTNWorld.GetWorldState<int>("_full");

        // 确保 _full 不低于最小值 0
        HTNWorld.UpdateState("_full", Math.Max(full - 2, 0));
        HTNWorld.UpdateState("_masterBeside", false);
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int full = (int)worldState["_full"];

        // 确保 _full 不低于最小值 0
        worldState["_full"] = Math.Max(full - 2, 0);
        worldState["_masterBeside"] = false;
    }
}