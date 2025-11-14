using System;
using UnityEngine;

[Serializable]
public class CharacterView
{
    [SerializeField]
    private Animator _animator;
    private MoveAnimator _moveAnimator;
    public void OnStartGame(CharacterCore core)
    {
        _moveAnimator = new(_animator, core._moveComponent.IsMoving);
    }
    public void OnEnable()
    {
        _moveAnimator.Enable();
    }
    public void OnDisable()
    {
        _moveAnimator.Disable();
    }
}
