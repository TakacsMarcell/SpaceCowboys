using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    bool IsOnScreen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= 17.3f)
        {
            IsOnScreen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && IsOnScreen == true)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
    }
}
