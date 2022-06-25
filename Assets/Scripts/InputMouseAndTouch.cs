using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseAndTouch : MonoBehaviour
{
    [SerializeField] private PaintManager paintManager;



    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            paintManager.Draw(Input.mousePosition.x, Input.mousePosition.y);
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            paintManager.DrawStop();
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
            {
                paintManager.Draw(touch.position.x, touch.position.y);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                paintManager.DrawStop();
            }
        }
    }
}
