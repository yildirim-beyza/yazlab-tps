using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;                         // Ýstersen görünür býrakabilirsin
        Cursor.lockState = CursorLockMode.Confined;     // Ýmleci oyun penceresine kilitle
    }

    void Update()
    {
        // Canvas: Screen Space - Overlay ise bu yeterli
        transform.position = Input.mousePosition;
    }
}
