using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    Gun[] guns;

    float moveSpeed = 3;
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;

    GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
        guns = transform.GetComponentsInChildren<Gun>();
        foreach(Gun gun in guns)
        {
            gun.isActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if(shoot)
        {
            shoot = false;
            foreach(Gun gun in guns)
            {
                gun.Shoot();
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 3;
        }
        Vector2 move = Vector2.zero;
        if (moveUp)
        {
            move.y += moveAmount;
        }
        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;
        if (pos.x <= 1.5f)
        {
            pos.x = 1.5f;
        }
        if (pos.x >= 16f)
        {
            pos.x = 16;
        }
        if (pos.y <= 1)
        {
            pos.y = 1;
        }
        if (pos.y >= 9)
        {
            pos.y = 9;
        }

        transform.position = pos;
    }



    void ActivateShield()
    {
        shield.SetActive(true);
    }
        void DeactivateShield()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            if(HasShield())
            {
                DeactivateShield();
            }
            else
            {
                Destroy(gameObject);
            }
            Destroy(destructable.gameObject);
        }
        
        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if(powerUp)
        {
            if(powerUp.activateShield)
            {
                ActivateShield();
            }
            Destroy(powerUp.gameObject);
        }
    }
}
