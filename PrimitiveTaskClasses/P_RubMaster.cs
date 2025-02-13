using System;
using System.Collections.Generic;
using UnityEngine;

public class P_RubMaster : PrimitiveTask
{
    public override string GetTaskName()
    {
        return "蹭主人";
    }

    protected override bool MetCondition_OnRun()
    {
        bool masterBeside = HTNWorld.GetWorldState<bool>("_masterBeside");
        return masterBeside; // 蹭主人条件：主人在旁边
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        bool masterBeside = (bool)worldState["_masterBeside"];
        return masterBeside; // 规划时条件：主人在旁边
    }

    public override EStatus Operator()
    {
        // 蹭主人任务的执行逻辑（例如动画、耗时等）
        Debug.Log("蹭主人");
        return EStatus.Success; // 假设直接成功
    }

    protected override void Effect_OnRun()
    {
        int mood = HTNWorld.GetWorldState<int>("_mood");

        // 确保 _mood 不超过最大值 10
        HTNWorld.UpdateState("_mood", Math.Max(mood - 2, 0));
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int mood = (int)worldState["_mood"];

        // 确保 _mood 不超过最大值 10
        worldState["_mood"] = Math.Max(mood - 2, 0);
    }
}