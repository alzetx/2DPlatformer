using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class SpriteScroller : MonoBehaviour, IGameTickable
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _xSpeed = 0.5f;

    private Material _mat;
    private Vector2 _offset;
    private void Awake()
    {
        _mat = Instantiate(_spriteRenderer.sharedMaterial);
        _spriteRenderer.material = _mat;
        _offset = _mat.mainTextureOffset;
    }

    public void Tick(float deltaTime)
    {
        _offset.x += _xSpeed * deltaTime;
        _mat.mainTextureOffset = _offset;
    }

    

}
