using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Eat : PrimitiveTask
{
    public P_Eat(float duration) : base(duration)
    {
        this._task = Task.Eat;
    }

    public override string GetTaskName()
    {
        return "吃饭";
    }

    protected override bool MetCondition_OnRun()
    {
        int full = HTNWorld.GetWorldState<int>("_full");
        return full <= 8; // 吃饭条件：饱腹值 <= 8
    }

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState)
    {
        int full = (int)worldState["_full"];
        return full <= 8; // 规划时条件：饱腹值 <= 8
    }

    public override EStatus Operator()
    {
        // 吃饭任务的执行逻辑（例如动画、耗时等）
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
        int full = HTNWorld.GetWorldState<int>("_full");
        int mood = HTNWorld.GetWorldState<int>("_mood");

        // 确保 _full 和 _mood 不超过最大值 10
        HTNWorld.UpdateState("_full", Math.Min(full + 2, 10));
        HTNWorld.UpdateState("_mood", Math.Min(mood + 1, 10));
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        int full = (int)worldState["_full"];
        int mood = (int)worldState["_mood"];

        // 确保 _full 和 _mood 不超过最大值 10
        worldState["_full"] = Math.Min(full + 2, 10);
        worldState["_mood"] = Math.Min(mood + 1, 10);
    }
}