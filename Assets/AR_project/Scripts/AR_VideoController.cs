using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro; // <-- Se agregó esta línea para solucionar el error

public class AR_VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button playButton, pauseButton;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI timeText;
    
    void Start() {
        // Configurar eventos de botones
        playButton.onClick.AddListener(PlayVideo);
        pauseButton.onClick.AddListener(PauseVideo);
        
        // Actualizar UI periódicamente
        InvokeRepeating(nameof(UpdateUI), 0f, 0.1f);
    }
    
    public void PlayVideo() {
        if (videoPlayer != null && !videoPlayer.isPlaying) {
            videoPlayer.Play();
            UpdateButtonState();
        }
    }
    
    public void PauseVideo() {
        if (videoPlayer != null && videoPlayer.isPlaying) {
            videoPlayer.Pause();
            UpdateButtonState();
        }
    }
    
    private void UpdateUI() {
        if (videoPlayer == null || !videoPlayer.isPrepared) return;
        
        // Actualizar barra de progreso
        if (videoPlayer.length > 0) {
            progressSlider.value = (float)(videoPlayer.time / videoPlayer.length);
        }
        
        // Formatear tiempo: MM:SS
        string current = System.TimeSpan.FromSeconds(videoPlayer.time).ToString(@"mm\:ss");
        string total = System.TimeSpan.FromSeconds(videoPlayer.length).ToString(@"mm\:ss");
        timeText.text = $"{current} / {total}";
    }
    
    private void UpdateButtonState() {
        playButton.gameObject.SetActive(!videoPlayer.isPlaying);
        pauseButton.gameObject.SetActive(videoPlayer.isPlaying);
    }
    
    // Liberar recursos al destruir
    void OnDestroy() {
        if (videoPlayer != null) videoPlayer.Stop();
        CancelInvoke(nameof(UpdateUI));
    }
}