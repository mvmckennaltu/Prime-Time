using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public int attackDamage = 1;
    private DoorBehavior doorBehavior;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Door"))
        {
            doorBehavior = collision.gameObject.GetComponent<DoorBehavior>();
            doorBehavior.Shoot();
            Destroy(gameObject);
        }
    }
    private void CheckBounds()
    {
        if (transform.position.x > 400 || transform.position.y > 400 || transform.position.z > 400)
        {
            Destroy(gameObject);
        }
    }
   
}
