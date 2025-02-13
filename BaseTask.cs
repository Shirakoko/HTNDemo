using System.Collections;
using System.Collections.Generic;

//用于描述运行结果的枚举（如果有看上一篇行为树的话，也可以直接用行为树的EStatus）
public enum EStatus
{
    Failure, Success, Running, 
}

public interface IBaseTask
{
    //判断是否满足条件
    bool MetCondition(Dictionary<string, object> worldState);
    //添加子任务
    void AddNextTask(IBaseTask nextTask);
}
