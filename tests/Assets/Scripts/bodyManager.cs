using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyManager : MonoBehaviour
{
    public GameObject head;
    public GameObject buttonPart;
    public GameObject bloodPlane;
    public GameObject cam;
    public GameObject orientation;

    public Material[] materials = new Material[2];

    public Color mainColor;

    private void Awake()
    {
        materials[0] = head.GetComponent<SkinnedMeshRenderer>().materials[0];
        materials[1] = buttonPart.GetComponent<SkinnedMeshRenderer>().materials[0];
        mainColor = materials[0].GetColor("Color_96ADA0CD");
    }

    public void SetColor(string id, Color color)
    {
        foreach (Material material in materials)
        {
            material.SetColor(id, color);
        }
    }

    public void setBoundBodyColor( Color color)
    {
        foreach (Material material in materials)
        {
            material.SetColor("Color_282E291D", color);
        }
    }

    public void setMainBodyColor(Color color)
    {
        mainColor = color;
        foreach (Material material in materials)
        {
            material.SetColor("Color_96ADA0CD", color);
        }
    }
}
