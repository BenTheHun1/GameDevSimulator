using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int pts;
    public int ClickAmount;
    public int desiredPosition;
    public float camSpeed;
    public Text displayPts;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayPts.text = pts.ToString();
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), Time.deltaTime * camSpeed);
    }

    public void Click()
    {
        pts += ClickAmount;
    }

    public void MoveCam(string dir)
    {
        if (dir == "l")
        {
            desiredPosition = -230;
        }
        else if (dir == "m")
        {
            desiredPosition = 0;
        }
        else if (dir == "r")
        {
            desiredPosition = 230;
        }
    }

}
