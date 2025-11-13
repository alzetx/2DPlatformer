using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageScroller : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed;

    [SerializeField]
    private float XDirection;

    [SerializeField]
    private float YDirection;

    [SerializeField]
    private RawImage _rawImage;

    Vector2 _direction = Vector2.zero;
    private Tweener _tweener;

    private void Awake()
    {
        _direction = new Vector2(XDirection, YDirection).normalized * _scrollSpeed;
        StartScrolling();
    }

    private void StartScrolling()
    {
        _tweener?.Kill();

        _tweener = DOTween.To(
            () => _rawImage.uvRect.position,
            pos => _rawImage.uvRect = new Rect(pos, _rawImage.uvRect.size),
            _rawImage.uvRect.position + _direction,
            1f) //duration
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
    private void OnDestroy()
    {
        _tweener?.Kill();
    }
}
