using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    public Transform connectedObject;

    public void OnTriggerEnter2D(Collider2D col)
    {
        connectedObject.gameObject.SetActive(false);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        connectedObject.gameObject.SetActive(true);
    }
}
