using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScroller : MonoBehaviour
{
    private static readonly int MainTex_ST = Shader.PropertyToID("_MainTex_ST");


    [SerializeField] private float _xSpeed = 0.5f;

    private SpriteRenderer _spriteRenderer;
    private MaterialPropertyBlock _mpb;
    private Vector2 _offset;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mpb = new MaterialPropertyBlock();
    }

    private void Update()
    {
        _offset.x += _xSpeed * Time.deltaTime;

        _spriteRenderer.GetPropertyBlock(_mpb);

        _mpb.SetVector(MainTex_ST, new Vector4(1, 1, _offset.x, _offset.y));

        _spriteRenderer.SetPropertyBlock(_mpb);
    }
}
