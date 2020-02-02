using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    private int collected = 0;

    public Transform door;

    public List<Transform> glichCollections;


    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.transform.tag == "Liftable")
        {
            glichCollections[collected].gameObject.SetActive(false);
            collected += 1;
            col.transform.gameObject.SetActive(false);

            if (collected == 3)
                door.gameObject.SetActive(true);
        }
    }
}
