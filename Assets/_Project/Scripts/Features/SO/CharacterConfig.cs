using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/Character")]
public class CharacterConfig : ScriptableObject
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; } = 150f;
    [field: SerializeField]
    public float JumpForce { get; private set; } = 5f;
    [field: SerializeField]
    public int MaxHP { get; private set; } = 5;
}
