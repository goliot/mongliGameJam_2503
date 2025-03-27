using UnityEngine;

public class CrossHair : MonoBehaviour
{
    Vector3 MousePosition = new Vector3();

    private void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        transform.position = MousePosition;
    }
}
