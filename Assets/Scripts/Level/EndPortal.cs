using DG.Tweening;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeImage;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.TryGetComponent(out Player _))
            return;
        fadeImage.DOFade(1f, 0.8f);
    }
}