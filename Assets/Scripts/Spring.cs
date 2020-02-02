using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springStrength = 5;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag != "Foreground")
        {
            if (col.transform.name == "Door" && transform.name == "Spring_ignorore_door")
                return;
            Rigidbody2D colRb = col.transform.gameObject.GetComponent<Rigidbody2D>();
            if (transform.up == Vector3.up)
                colRb.velocity = new Vector2(colRb.velocity.x, 0);
            else if (transform.up == Vector3.right)
                colRb.velocity = new Vector2(0, colRb.velocity.y);
            colRb.AddForce(transform.up * springStrength * colRb.mass, ForceMode2D.Impulse);
        }
    }
}
