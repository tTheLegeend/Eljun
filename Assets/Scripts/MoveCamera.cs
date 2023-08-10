using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float scrollSpeed = 20; // Speed of camera
    void Update()
    {
        float mousePosX = Input.mousePosition.x;
        int scrollDistance = 50; // Distance for mouse to start scrolling

        if (mousePosX < scrollDistance)
        {
            if(Camera.main.transform.position.x > -1)
            {
                transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
            }
           
        }

        if (mousePosX >= Screen.width - scrollDistance)
        {
            if (Camera.main.transform.position.x < 15)
            {
                transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
            }
                
        }
    }
}
