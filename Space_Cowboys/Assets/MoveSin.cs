using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveSin : MonoBehaviour
{
    float sinCenterY;
    // Start is called before the first frame update
    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x);
        pos.y = sinCenterY + sin;

        transform.position = pos;
    }
}
