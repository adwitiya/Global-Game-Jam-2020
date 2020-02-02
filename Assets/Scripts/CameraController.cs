using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTf;

    private Camera cam;

    public Shader greyscaleShader;

    public float cameraDistance = 10;


    void Awake()
    {
        //   GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
        cam = GetComponent<Camera>();
        cam.SetReplacementShader(greyscaleShader, "Queue");
    }

    void Start()
    {
        playerTf = GameObject.Find("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(playerTf.position.x, playerTf.position.y, -cameraDistance);
    }
}
