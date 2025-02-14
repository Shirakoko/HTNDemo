using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Parkour : PrimitiveTask
{
    public P_Parkour(float duration) : base(duration)
    {
        this._task = Task.Parkour;
    }
    
    public override string GetTaskName()
    {
        return "跑酷";
    }

    protected override bool MetCondition_OnRun()
    {
        int energy = HTNWorld.GetWorldState<int>("_energy");
        int mood = HTNWorld.GetWorldState<int>("_mood");
        return energy >= 5 && mood <= 7; // 跑酷条件：精力值 >= 5 且心情值 <= 7
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        int energy = (int)worldState["_energy"];
        int mood = (int)worldState["_mood"];
        return energy >= 5 && mood <= 7; // 规划时条件：精力值 >= 5 且心情值 <= 7
    }

    public override EStatus Operator()
    {
        // 跑酷任务的执行逻辑（例如动画、耗时等）
        if(_startTime < 0)
        {
            _startTime = Time.time;
            Debug.Log($"开始{GetTaskName()}...");
            CatHTN.Instance.ShowDialog($"开始{GetTaskName()}...");
        }

        if(Time.time - _startTime >= this._duration)
        {
            Debug.Log($"{GetTaskName()}完毕，耗时{this._duration}");
            CatHTN.Instance.ShowDialog($"{GetTaskName()}完毕，耗时{this._duration}");
            CatHTN.Instance.HideDialog();
            _startTime = -1;
            return EStatus.Success;
        }
        return EStatus.Running;
    }

    protected override void Effect_OnRun()
    {
        int energy = HTNWorld.GetWorldState<int>("_energy");
        int full = HTNWorld.GetWorldState<int>("_full");
        int mood = HTNWorld.GetWorldState<int>("_mood");

        // 确保 _energy 和 _full 不低于最小值 0，_mood 不超过最大值 10
        HTNWorld.UpdateState("_energy", Math.Max(energy - 2, 0));
        HTNWorld.UpdateState("_full", Math.Max(full - 3, 0));
        HTNWorld.UpdateState("_mood", Math.Min(mood + 2, 10));
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int energy = (int)worldState["_energy"];
        int full = (int)worldState["_full"];
        int mood = (int)worldState["_mood"];

        // 确保 _energy 和 _full 不低于最小值 0，_mood 不超过最大值 10
        worldState["_energy"] = Math.Max(energy - 2, 0);
        worldState["_full"] = Math.Max(full - 3, 0);
        worldState["_mood"] = Math.Min(mood + 2, 10);
    }
}