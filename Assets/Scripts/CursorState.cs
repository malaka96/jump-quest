using UnityEngine;

public class CursorState : MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;
    }

}
