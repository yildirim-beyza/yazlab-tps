using UnityEngine;

public class CoverController : MonoBehaviour
{
    public KeyCode key = KeyCode.LeftControl;

    // layer adları – istersen Inspector’dan değiştirirsin
    public string bulletLayerName = "Bullet";
    public string bulletPassLayerName = "BulletPass";
    public string coverLayerName = "Cover";

    public static bool PassThrough = false;   // mermiler duvardan geçsin mi?

    int bulletLayer, bulletPassLayer, coverLayer;

    void Awake()
    {
        bulletLayer = LayerMask.NameToLayer(bulletLayerName);
        bulletPassLayer = LayerMask.NameToLayer(bulletPassLayerName);
        coverLayer = LayerMask.NameToLayer(coverLayerName);

        if (bulletLayer < 0 || bulletPassLayer < 0 || coverLayer < 0)
            Debug.LogWarning("Layer isimlerini kontrol et: Bullet / BulletPass / Cover");

        // güvene almak için: global çarpışmayı da image/toggle et
        Physics.IgnoreLayerCollision(bulletPassLayer, coverLayer, true); // her zaman kapalı
        Physics.IgnoreLayerCollision(bulletLayer, coverLayer, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            PassThrough = !PassThrough;
            Debug.Log(PassThrough ? "PassThrough ON" : "PassThrough OFF");
        }
    }
}
