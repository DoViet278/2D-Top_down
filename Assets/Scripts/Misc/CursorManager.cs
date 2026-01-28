using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        Cursor.visible = false;

        if (Application.isPlaying)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    private void Update()
    {
        Vector2 cursorPos = Input.mousePosition;
        img.rectTransform.position = cursorPos;
    }
}
