using UnityEngine;
[CreateAssetMenu(fileName = "PlayerParams", menuName = "ScriptableObject/Player Param", order = 1)]
public class PlayerParam : ScriptableObject
{
    [Header("水平方向参数")]
    public int maxMoveSpeed;

    [Header("垂直方向参数")]
    public float jumpMax;//最大跳跃高度
    public float jumpMin;//最小跳跃高度
    public float jumpSpeed;

    [Header("攀爬参数")]
    public float climbSpeed;//下滑速度同时也是攀爬速度
}