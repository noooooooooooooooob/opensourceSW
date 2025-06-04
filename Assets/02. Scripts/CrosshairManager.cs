using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    [Header("Crosshair Sprites")]
    public Sprite crosshair6;
    public Sprite crosshair7;
    public Sprite crosshair8;
    public Sprite crosshair9;
    public Sprite crosshair0;

    [Header("Hit Marker Sprites")]
    public Sprite normalHitMarker;
    public Sprite critHitMarker;

    private Image crosshairImage;
    private Image hitMarkerImage;
    private Camera mainCamera;

    void Start()
    {
        crosshairImage = transform.Find("CrosshairImage").GetComponent<Image>();
        hitMarkerImage = transform.Find("HitMarkerImage").GetComponent<Image>();

        hitMarkerImage.enabled = false;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;

        if (Input.GetKeyDown(KeyCode.Alpha6)) crosshairImage.sprite = crosshair6;
        if (Input.GetKeyDown(KeyCode.Alpha7)) crosshairImage.sprite = crosshair7;
        if (Input.GetKeyDown(KeyCode.Alpha8)) crosshairImage.sprite = crosshair8;
        if (Input.GetKeyDown(KeyCode.Alpha9)) crosshairImage.sprite = crosshair9;
        if (Input.GetKeyDown(KeyCode.Alpha0)) crosshairImage.sprite = crosshair0;
    }

    public void ShowHitMarker(bool isCritical)
    {
        hitMarkerImage.sprite = isCritical ? critHitMarker : normalHitMarker;
        hitMarkerImage.enabled = true;

        Invoke(nameof(HideHitMarker), 0.3f);
    }

    private void HideHitMarker()
    {
        hitMarkerImage.enabled = false;
    }
}
