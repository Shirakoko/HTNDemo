using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatHTN : MonoBehaviour
{
    private static CatHTN _instance;
    public static CatHTN Instance => _instance;

    #region 猫猫的状态
    public int energy;
    public int full;
    public int mood;
    public bool masterBeside;
    #endregion

    #region
    private GameObject panelDialogGo;
    private Text textDialog;
    #endregion

    // 存储任务类型和对应位置的字典
    private Dictionary<Task, Vector3> _taskPositions = new Dictionary<Task, Vector3>();

    private HTNPlanBuilder htnBuilder;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        panelDialogGo = transform.Find("Canvas").Find("Panel_Dialog").gameObject;
        textDialog = panelDialogGo.transform.Find("Text_Dialog").GetComponent<Text>();

        panelDialogGo.SetActive(false);
    }

    private void Start()
    {
        // 查找 Positions 空物体
        GameObject positionsParent = GameObject.Find("Positions");
        // 遍历 Positions 下的所有子物体
        foreach (Transform child in positionsParent.transform)
        {
            // 尝试将子物体名称解析为 Task 枚举
            if (Enum.TryParse(child.name, out Task task))
            {
                _taskPositions[task] = child.position;
            }
        }

        // 注册世界状态
        HTNWorld.AddState("_energy", () => energy, value => energy = (int)value);
        HTNWorld.AddState("_mood", () => mood, value => mood = (int)value);
        HTNWorld.AddState("_full", () => full, value => full = (int)value);
        HTNWorld.AddState("_masterBeside", () => masterBeside, value => masterBeside = (bool)value);

        htnBuilder = new HTNPlanBuilder();

        // 构建猫猫的 HTN 网络结构
        htnBuilder.AddCompoundTask() // 生活（终极任务）
            .AddMethod(() => true) // 维持生命
                .AddCompoundTask() // 维持生命复合任务
                    .AddMethod(() => true) // 进食
                        .AddPrimitiveTask(new P_Eat(3.0f,TaskStart))  // 吃饭
                        .AddPrimitiveTask(new P_Drink(2.0f, TaskStart)) // 喝水
                        .Back()
                    .AddMethod(() => true) // 拉屎
                        .AddPrimitiveTask(new P_Poop(5.0f, TaskStart)) // 拉屎
                        .Back()
                    .Back()
                .Back()
            .AddMethod(() => true) // 运动
                .AddCompoundTask() // 运动复合任务
                    .AddMethod(() => true) // 玩耍
                        .AddPrimitiveTask(new P_Parkour(6.0f, TaskStart)) // 跑酷
                        .AddPrimitiveTask(new P_ChaseCock(4.0f, TaskStart)) // 追蟑螂
                        .AddPrimitiveTask(new P_EatCock(1.0f, TaskStart)) // 吃蟑螂
                        .Back()
                    .AddMethod(() => true) // 拆家
                        .AddPrimitiveTask(new P_Destroy(3.0f, TaskStart)) // 拆家
                        .Back()
                    .Back()
                .Back()
            .AddMethod(() => true) // 叫唤
                .AddPrimitiveTask(new P_Meow(2.0f, TaskStart)) // 叫唤
                .Back()
            .AddMethod(() => HTNWorld.GetWorldState<bool>("_masterBeside")) // 撒娇
                .AddPrimitiveTask(new P_RubMaster(4.0f, TaskStart)) // 蹭主人
                .AddPrimitiveTask(new P_Meow(2.0f, TaskStart)) // 叫唤
                .Back()
            .AddMethod(() => true) // 休息
                .AddPrimitiveTask(new P_Sleep(7.0f, TaskStart)) // 睡觉
                .AddPrimitiveTask(new P_Idle(2.5f, TaskStart)) // 发呆
                .Back()
            .AddMethod(() => true)
                .AddPrimitiveTask(new P_LickFur(3.5f, TaskStart)) // 舔毛
                .Back()
            .End();
    }

    void Update()
    {
        // 循环执行计划
        htnBuilder.RunPlan();
    }

    public void ShowDialog(string text)
    {
        this.textDialog.text = text;
        this.panelDialogGo.SetActive(true);
    }

    public void HideDialog()
    {
        this.textDialog.text = "";
        this.panelDialogGo.SetActive(false);
    }

    public void TaskStart(Task task)
    {
        if (_taskPositions.TryGetValue(task, out Vector3 position))
        {
            // 将当前游戏物体瞬移到指定位置
            this.transform.position = position;
            // 后续可改成缓慢移动
        }
    }
}