using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Connection")]
public class Spawn : ScriptableObject
{
    public static Spawn ActiveConnection { get; set; }
}
