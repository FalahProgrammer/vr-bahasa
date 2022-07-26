using UnityEngine;

public class OnSceneChangeBlackPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup.alpha = 1;
    }
}
