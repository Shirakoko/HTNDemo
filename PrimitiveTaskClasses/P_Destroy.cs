using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Destroy : PrimitiveTask
{
    protected override bool MetCondition_OnRun()
    {
        int energy = HTNWorld.GetWorldState<int>("_energy");
        bool masterBeside = HTNWorld.GetWorldState<bool>("_masterBeside");
        return energy >= 5 && !masterBeside; // 拆家条件：精力值 >= 5 且主人不在旁边
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        int energy = (int)worldState["_energy"];
        bool masterBeside = (bool)worldState["_masterBeside"];
        return energy >= 5 && !masterBeside; // 规划时条件：精力值 >= 5 且主人不在旁边
    }

    public override EStatus Operator()
    {
        // 拆家任务的执行逻辑（例如动画、耗时等）
        Debug.Log("拆家");
        return EStatus.Success; // 假设直接成功
    }

    protected override void Effect_OnRun()
    {
        int mood = HTNWorld.GetWorldState<int>("_mood");

        // 确保 _mood 不低于最小值 0
        HTNWorld.UpdateState("_mood", Math.Max(mood - 2, 0));
        HTNWorld.UpdateState("_masterBeside", true);
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int mood = (int)worldState["_mood"];

        // 确保 _mood 不低于最小值 0
        worldState["_mood"] = Math.Max(mood - 2, 0);
        worldState["_masterBeside"] = true;
    }
}