using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{

    public List<Transform> wires;
    public List<Transform> brokenSounds;
    public List<Transform> fixedSounds;

    public Material grayscale;

    private void Awake()
    {
        foreach (Transform t in wires)
            t.gameObject.SetActive(false);
        foreach (Transform t in brokenSounds)
            t.gameObject.SetActive(false);
        foreach (Transform t in fixedSounds)
            t.gameObject.SetActive(false);
    }

    public void setWires(int collected)
    {
        if (collected > wires.Count)
            collected = wires.Count;

        for (int i = 0; i < collected; i++)
            wires[i].gameObject.SetActive(true);

        for (int i = collected; i < wires.Count; i++)
            wires[i].gameObject.SetActive(false);
    }

    public void setBrokenSounds(int collected)
    {
        if (collected > brokenSounds.Count)
            collected = brokenSounds.Count;

        for (int i = 0; i < collected; i++)
            brokenSounds[i].gameObject.SetActive(true);

        for (int i = collected; i < brokenSounds.Count; i++)
            brokenSounds[i].gameObject.SetActive(false);
    }

    public void setFixedSounds(int collected)
    {
        if (collected > fixedSounds.Count)
            collected = fixedSounds.Count;

        for (int i = 0; i < collected; i++)
            fixedSounds[i].gameObject.SetActive(true);

        for (int i = collected; i < fixedSounds.Count; i++)
            fixedSounds[i].gameObject.SetActive(false);
    }

    public void SetGrayscale(bool on)
    {
        if (on)
        {
            foreach (Transform t in wires)
                t.GetComponent<Image>().material = grayscale;
            foreach (Transform t in brokenSounds)
                t.GetComponent<Image>().material = grayscale;
            foreach (Transform t in fixedSounds)
                t.GetComponent<Image>().material = grayscale;
        }
        else
        {
            foreach (Transform t in wires)
                t.GetComponent<Image>().material = null;
            foreach (Transform t in brokenSounds)
                t.GetComponent<Image>().material = null;
            foreach (Transform t in fixedSounds)
                t.GetComponent<Image>().material = null;
        }
    }

}
