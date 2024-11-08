using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed;
    public float Damage;


    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * MoveSpeed;
    }

    private void Update()
    {
        if (this.transform.position.x > 2)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }


}


