using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_Destroy : PrimitiveTask
{
    public P_Destroy(float duration) : base(duration)
    {
        this._task = Task.Destroy;
    }

    public override string GetTaskName()
    {
        return "拆家";
    }

    protected override bool MetCondition_OnRun()
    {
        int energy = HTNWorld.GetWorldState<int>("_energy");
        int mood = HTNWorld.GetWorldState<int>("_mood");
        bool masterBeside = HTNWorld.GetWorldState<bool>("_masterBeside");
        return energy >= 4 && mood <= 4 && !masterBeside; // 拆家条件：精力值 >= 4 且心情值 <= 4 且主人不在旁边
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        int energy = (int)worldState["_energy"];
        int mood = (int)worldState["_mood"];
        bool masterBeside = (bool)worldState["_masterBeside"];
        return energy >= 4 && mood <= 4 && !masterBeside; // 规划时条件：精力值 >= 4 且心情值 <= 4 且主人不在旁边
    }

    public override EStatus Operator()
    {
        // 拆家任务的执行逻辑（例如动画、耗时等）
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

        // 确保 _energy 不低于最小值 0
        HTNWorld.UpdateState("_energy", Math.Max(energy - 1, 0));
        HTNWorld.UpdateState("_masterBeside", true);
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int energy = HTNWorld.GetWorldState<int>("_energy");

        // 确保 _energy 不低于最小值 0
        worldState["_energy"] = Math.Max(energy - 1, 0);
        worldState["_masterBeside"] = true;
    }
}