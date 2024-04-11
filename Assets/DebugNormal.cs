using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugNormal : MonoBehaviour
{
    public RawImage rawImage;
    public TMP_Text readout;

    void Update()
    {
        Camera camera = Camera.main;
        int x = (int)Input.mousePosition.x;
        int y = (int)Input.mousePosition.y;
        Vector3 screenPosition = new Vector3(x, y, -camera.transform.position.z);
        Texture texture = rawImage.texture;
        Color color = (texture as Texture2D).GetPixel(x, y);
        Vector3 normal = new Vector3(color.r, color.g, -color.b);
        readout.text = normal.ToString("F2");
        transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.rotation = Quaternion.LookRotation(normal);
    }
}
