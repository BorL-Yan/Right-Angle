using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    
    public float _rotationSpeed = 100f;

    void Awaik()
    {

    }
    
    
    private Vector2 _positionMouse = Vector2.zero;
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     _positionMouse = ray.origin;
        //     
        //     
        // }

        if (Input.GetMouseButton(0))
        {
            // Получаем движение мыши по осям X и Y
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            // Вращаем куб вокруг оси Y (по горизонтали) и оси X (по вертикали)
            transform.Rotate(Vector3.up, -mouseX * _rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, -mouseY * _rotationSpeed * Time.deltaTime, Space.World);
                
        }
        //this.gameObject.transform.localRotation = Quaternion.Euler(0, 50, 0);
    }

    private Vector2 Distance(Vector2 point1, Vector2 point2) 
        => new Vector2( Mathf.Sqrt(Cube(point1.x-point2.x)), 
                        Mathf.Sqrt(Cube(point2.y - point1.y)));
    private float Cube(float value) => value * value; 
}
