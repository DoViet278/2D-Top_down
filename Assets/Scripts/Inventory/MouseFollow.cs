using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse() 
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
