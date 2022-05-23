using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clientManager : MonoBehaviour
{
    public static clientManager instance;
    public bool decresingLight;
    float timer;

    public GameObject bloodSplater;

    public Image reportImage;
    public GameObject reportUI;

    private void Awake()
    {
        instance = this;
    }

    
    void Start()
    {
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            decreaseLight();
        }
        if (decresingLight)
        {
            timer += Time.deltaTime;
            if (timer <= 20)
            {
                RenderSettings.fogDensity = timer / 20;
            }
        }
    }

    // Update is called once per frame
    public void attack(int AttackerId , int secondPlayerID)
    {
        
    }

    public static void attack(GameObject attacker , GameObject second)
    {
        Vector3 move = second.transform.position - attacker.transform.position;
        attacker.GetComponent<CharacterController>().Move(new Vector3(move.x, attacker.transform.position.y, move.z));
        second.GetComponent<PlayerManagerAdd>().Die();
        clientManager.instance.addSplater(second);
        attacker.transform.LookAt(second.transform);

    }

    public void addSplater(GameObject second)
    {
        Instantiate(bloodSplater, second.transform.position  + Vector3.up *0.01f, Quaternion.Euler(Vector3.right *-90));
    }

    public void decreaseLight()
    {
        decresingLight = true;
    }

    public void report(GameObject reporter)
    {
        report(reporter.GetComponent<bodyManager>().materials[0].GetColor("Color_96ADA0CD"));

    }

    public void report(Color rebortedColor)
    {
        reportUI.SetActive(true);
        rebortedColor.a = 1;
        reportImage.color = rebortedColor;
    }
}
