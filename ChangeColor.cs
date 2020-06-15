using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChangeColor : MonoBehaviour
{
    [SerializeField]
    Color _colorToChange, _colorToReturn;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<Renderer>().material.color = _colorToChange;
        }
        else if (Input.GetMouseButton(1))
        {
            gameObject.GetComponent<Renderer>().material.color = _colorToReturn;
        }
    }
}
