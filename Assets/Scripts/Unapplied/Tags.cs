using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理Tag，以免在写Tag时出错
/// </summary>
public class Tags : MonoBehaviour
{
    public const string player = "Player";
    public const string alarm = "AlarmLight";
    //警报声音所在Tag
    public const string siren = "Siren";
    //这个Tag的脚本会保存玩家最后出现位置，以便敌人可以追踪他
    public const string gameController = "GameController";
    //当警报灯亮起，关闭MainLight
    public const string mainLight = "MainLight";
    //当游戏开始和游戏结束，游戏界面会慢慢变黑
    public const string fader = "Fader";
    //滑动门
    public const string enemy = "Enemy";
}
