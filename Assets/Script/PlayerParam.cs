using UnityEngine;
[CreateAssetMenu(fileName = "PlayerParams", menuName = "ScriptableObject/Player Param", order = 1)]
public class PlayerParam: ScriptableObject
{
    [Header("水平方向参数")]
    public int maxSpeed;

}