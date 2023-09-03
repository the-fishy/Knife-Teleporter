using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Inputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new Inputs();
        inputs.UI.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = inputs.UI.Point.ReadValue<Vector2>();
        Debug.Log(mousePosition);
    }
}
