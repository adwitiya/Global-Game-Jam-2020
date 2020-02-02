using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform target;
    public float proximityThreshold = 1.5f;

    private void Update()
    {
        
        if (Vector2.Distance(transform.position, target.position) < proximityThreshold)
        {
            transform.position = target.position;
            target.gameObject.SetActive(true);
            Rigidbody2D rb = transform.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            transform.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
