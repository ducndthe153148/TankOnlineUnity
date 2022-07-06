using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Bullet Bullet { get; set; }

    public int MaxRange { get; set; }

    private Boolean isGiveDamage = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //DestroyAfterRange();
    }

    //private void DestroyAfterRange()
    //{
    //    var currentPos = gameObject.transform.position;
    //    var initPos = Bullet.InitialPosition;
    //    switch (Bullet.Direction)
    //    {
    //        case Direction.Down:
    //            if (initPos.y - MaxRange >= currentPos.y)
    //            {
    //                Destroy(gameObject);
    //            }

    //            break;
    //        case Direction.Up:
    //            if (initPos.y + MaxRange <= currentPos.y)
    //            {
    //                Destroy(gameObject);
    //            }

    //            break;
    //        case Direction.Left:
    //            if (initPos.x - MaxRange >= currentPos.x)
    //            {
    //                Destroy(gameObject);
    //            }

    //            break;
    //        case Direction.Right:
    //            if (initPos.x + MaxRange <= currentPos.x)
    //            {
    //                Destroy(gameObject);
    //            }

    //            break;
    //        default:
    //            throw new ArgumentOutOfRangeException();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            TankController tc = collision.gameObject.GetComponent<TankController>();
            if (isGiveDamage)
            {
                tc.TakeDamage();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGiveDamage = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}