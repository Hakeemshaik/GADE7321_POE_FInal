using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class MouseMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Hide the default system cursor
        Cursor.visible = false;

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Set the position of the custom cursor to the mouse position
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0; // Ensure the cursor stays on the same plane
        transform.position = cursorPosition;
    }
}



