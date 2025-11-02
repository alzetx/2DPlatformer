using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
public class DebugSystem : MonoBehaviour
{
#if UNITY_EDITOR

    [Inject, ShowInInspector, HideInEditorMode]
    private GameStateMachine _stateMachine;


#endif

}
