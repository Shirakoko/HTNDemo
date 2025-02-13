using UnityEngine;

public class CatHTN : MonoBehaviour
{
    public int energy;
    public int full;
    public int mood;
    public bool masterBeside;

    private HTNPlanBuilder htnBuilder;

    private void Start()
    {
        // 注册世界状态
        HTNWorld.AddState("_energy", () => energy, value => energy = (int)value);
        HTNWorld.AddState("_mood", () => mood, value => mood = (int)value);
        HTNWorld.AddState("_full", () => full, value => full = (int)value);
        HTNWorld.AddState("_masterBeside", () => masterBeside, value => masterBeside = (bool)value);

        htnBuilder = new HTNPlanBuilder();

        // 构建猫猫的 HTN
        // htnBuilder.AddCompoundTask() // 生活（终极任务）
        //     .AddMethod(() => true) // 维持生命
        //         .AddCompoundTask() // 维持生命复合任务
        //             .AddMethod(() => true) // 进食
        //                 .AddPrimitiveTask(new P_Eat())  // 吃饭
        //                 .AddPrimitiveTask(new P_Drink()) // 喝水
        //                 .Back()
        //             .AddMethod(() => true) // 拉屎
        //                 .AddPrimitiveTask(new P_Poop()) // 拉屎
        //                 .Back()
        //             .Back()
        //         .Back()
        //     .AddMethod(() => true) // 运动
        //         .AddCompoundTask() // 运动复合任务
        //             .AddMethod(() => true) // 玩耍
        //                 .AddPrimitiveTask(new P_Parkour()) // 跑酷
        //                 .AddPrimitiveTask(new P_ChaseCock()) // 追蟑螂
        //                 .AddPrimitiveTask(new P_EatCock()) // 吃蟑螂
        //                 .Back()
        //             .AddMethod(() => true) // 拆家
        //                 .AddPrimitiveTask(new P_Destroy()) // 拆家
        //                 .Back()
        //             .Back()
        //         .Back()
        //     .AddMethod(() => true) // 叫唤
        //         .AddPrimitiveTask(new P_Meow(2.0f)) // 叫唤
        //         .Back()
        //     .AddMethod(() => HTNWorld.GetWorldState<bool>("_masterBeside")) // 撒娇
        //         .AddPrimitiveTask(new P_RubMaster()) // 蹭主人
        //         .AddPrimitiveTask(new P_Meow(2.0f)) // 叫唤
        //         .Back()
        //     .AddMethod(() => true) // 休息
        //         .AddPrimitiveTask(new P_Sleep()) // 睡觉
        //         .AddPrimitiveTask(new P_Idle()) // 发呆
        //         .Back()
        //     .AddMethod(() => true)
        //         .AddPrimitiveTask(new P_LickFur()) // 舔毛
        //         .Back()
        //     .End();
        
        htnBuilder.AddCompoundTask()
            .AddMethod(() => true)
                .AddPrimitiveTask(new P_TestTask(10.0f))
                .Back()
            .End();
        
        // InvokeRepeating("RunPlan", 0.0f, 0.0f);
    }

    void Update()
    {
        htnBuilder.RunPlan();
    }
}