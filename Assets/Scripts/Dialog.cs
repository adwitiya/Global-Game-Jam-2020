using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private int idx = -1;
    public List<Transform> dialogBoxes;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
            idx++;
    }

    private void FixedUpdate()
    {
        showCurrent();
    }

    public void game_continue(){
        idx++;
        showCurrent();
    }

    private void showCurrent()
    {
        for (int i=0; i<dialogBoxes.Count; i++)
        {
            if (i == idx)
                dialogBoxes[i].gameObject.SetActive(true);
            else
                dialogBoxes[i].gameObject.SetActive(false);
        }
    }
}
