using UnityEngine;
using Vuforia;

public class AR_UIController : MonoBehaviour
{
    [SerializeField] private GameObject arInterface; // Canvas World Space
    [SerializeField] private GameObject VideoInterface;
    [SerializeField] private float fadeDuration = 0.3f;

    private CanvasGroup canvasGroup;

    void Start() {
        if (arInterface == null) return;

        canvasGroup = arInterface.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = arInterface.AddComponent<CanvasGroup>();
        arInterface.SetActive(false); // Ocultar al inicio

        if (VideoInterface != null) {
            VideoInterface.SetActive(false);
        }
    }

    // 👉 Llamar desde TargetBehaviour cuando se detecta
    public void ShowUI() {
        if (arInterface == null) return;
        arInterface.SetActive(true);
        if (canvasGroup != null) StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));
    }

    public void ShowVideo() {
        if (VideoInterface != null) {
            VideoInterface.SetActive(true);
        }

        if (canvasGroup != null) {
            StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));
        }
    }

    public void HideUI() {
        if (canvasGroup == null) return;
        StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, fadeDuration, () => {
            if (arInterface != null) arInterface.SetActive(false);
        }));
    }

    private System.Collections.IEnumerator FadeCanvasGroup(
        CanvasGroup group, float start, float end, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;
        while (elapsed < duration) {
            group.alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        group.alpha = end;
        onComplete?.Invoke();
    }
}