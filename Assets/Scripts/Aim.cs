using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class Aim : MonoBehaviour
{
    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition= Input.mousePosition;
    }
}
