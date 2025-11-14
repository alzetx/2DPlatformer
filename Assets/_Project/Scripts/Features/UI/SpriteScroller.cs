using UnityEngine;

public class SpriteScroller : MonoBehaviour
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
    private void Update()
    {
        _offset.x += _xSpeed * Time.deltaTime;
        _mat.mainTextureOffset = _offset;
    }

}
