using UnityEngine;
using UnityEngine.EventSystems;

public class Model3D_Interaction : MonoBehaviour, 
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float minScale = 0.5f, maxScale = 2f;
    
    private bool isDragging = false;
    private Vector3 originalScale;
    
    void Start() {
        originalScale = transform.localScale;
        // Requerir EventSystem en escena
    }
    
    public void OnPointerDown(PointerEventData eventData) {
        isDragging = true;
    }
    
    public void OnDrag(PointerEventData eventData) {
        if (!isDragging) return;
        
        // Rotación horizontal con arrastre
        float rotationX = eventData.delta.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, -rotationX, Space.World);
        
        // Escalar con pinza (simulado con scroll)
        if (Input.mouseScrollDelta.y != 0) {
            float newScale = originalScale.x + Input.mouseScrollDelta.y * 0.1f;
            newScale = Mathf.Clamp(newScale, minScale, maxScale);
            transform.localScale = Vector3.one * newScale;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData) {
        isDragging = false;

    }
}