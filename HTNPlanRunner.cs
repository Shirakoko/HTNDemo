using UnityEngine;

public class HTNPlanRunner
{
    //当前运行状态
    private EStatus curState;
    //直接将规划器包含进来，方便重新规划
    private readonly HTNPlanner planner;
    //当前执行的原子任务
    private PrimitiveTask curTask;
    //标记「原子任务列表是否还有元素、能够继续」
    private bool canContinue;

    public HTNPlanRunner(HTNPlanner planner)
    {
        this.planner = planner;
        curState = EStatus.Failure;
    }
    
    public void RunPlan()
    {
        //如果当前运行状态是失败（一开始默认失败）
        if(curState == EStatus.Failure)
        {
            //就规划一次
            planner.Plan();
        } else if(curState == EStatus.Success)
        {
            //如果当前运行状态是成功，就表示当前任务完成了，让当前原子任务造成影响
            Debug.Log($"任务{curTask.GetTaskName()}执行成功，将对世界状态造成影响");
            curTask.Effect();
        }
        /*如果当前状态不是「正在执行/EStatus.Running」，就取出新一个原子任务作为当前任务
        无论失败还是成功，都要这么做。因为如果是失败，肯定在代码运行到这
        之前，已经进行了一次规划，理应获取新规划出的任务来运行；如果是因
        为成功，那也要取出新任务来运行*/
        if(curState != EStatus.Running)
        {
            //用Pop的返回结果判断规划器的FinalTasks是否为空
            canContinue = planner.FinalTasks.TryPop(out curTask);
        }

        /*如果canContinue为false（即curTask为null）也视作失败（其实是「全部完成」，但全部完成和失败是一样的，都要重新规划）。
        只有当canContinue && curTask.MetCondition()都满足时，才读取当前原子任务的运行状态，否则就失败。*/
        if (canContinue && curTask != null)
        {
            curState = curTask.MetCondition() ? curTask.Operator() : EStatus.Failure;
        } else {
            curState = EStatus.Failure;
        }
    }
}
