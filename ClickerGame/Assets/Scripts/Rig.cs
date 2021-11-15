using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rig : MonoBehaviour
{
    Transform[] parts;

    // Start is called before the first frame update
    void Start()
    {
        parts = gameObject.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform part in parts)
        {
            switch (part.name)
            {
                case "Case":
                case "Rig":
                    break;
                case "HardDrive":

                    break;
                case "Motherboard":

                    break;
                case "RAM":

                    break;
                default:
                    Debug.Log("Unkown Part: " + part.name);
                    break;
            }
        }
    }
}
