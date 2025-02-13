using System.Collections.Generic;
using UnityEngine;

public class P_TestTask : PrimitiveTask
{
    public P_TestTask(float duration)
    {
        this._duration = duration;
    }

    public override string GetTaskName()
    {
        return "测试任务";
    }

    protected override bool MetCondition_OnRun() => true;

    protected override bool MetCondition_OnPlan(Dictionary<string, object> worldState) => true;

    public override EStatus Operator()
    {
        // 测试任务的执行逻辑（例如动画、耗时等）
        if (_startTime < 0)
        {
            _startTime = Time.time;
            Debug.Log("开始执行测试任务... ");
        }

        if (Time.time - _startTime >= this._duration)
        {
            Debug.Log($"完成{this._duration}秒测试 ");
            // 重置开始时间
            _startTime = -1;
            return EStatus.Success;
        }
        return EStatus.Running; // 持续中
    }

    protected override void Effect_OnRun()
    {
        
    }

    protected override void Effect_OnPlan(Dictionary<string, object> worldState)
    {
        
    }
}