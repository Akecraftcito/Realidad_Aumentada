using UnityEngine;
using Vuforia;

public class AR_UIController : MonoBehaviour
{
    [SerializeField] private GameObject arInterface; // Canvas World Space
    [SerializeField] private float fadeDuration = 0.3f;
    
    private CanvasGroup canvasGroup;
    
    void Start() {
        canvasGroup = arInterface.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = arInterface.AddComponent<CanvasGroup>();
        arInterface.SetActive(false); // Ocultar al inicio
    }
    
    // 👉 Llamar desde TargetBehaviour cuando se detecta
    public void ShowUI() {
        arInterface.SetActive(true);
        StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));
    }
    public void ShowVideo() {
    VideoInterface.SetActive(true);
    StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));
    }
    
    public void HideUI() {
        StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, fadeDuration, () => {
            arInterface.SetActive(false);
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