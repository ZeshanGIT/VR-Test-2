using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickDetect : MonoBehaviour
{
    public float speed = 1f, rotSpeed = 20;
    bool zoom = false, rotate = false;
    bool ready = true;
    bool changeScene = false;
    public string scne;
    public Transform looktAtFirst, vashal, bedroom, hall;
    private void OnMouseDown()
    {
        Debug.Log(name);
        Camera.main.transform.LookAt(transform);
        zoom = true;
    }

    public float smooth = 1f;
    float initDir;
    private Quaternion targetRotation;

    private void Start()
    {
        if(vashal != null && bedroom != null && hall != null)
        switch(StaticClass.lastVisit)
        {
                case StaticClass.room.HALL:
                    looktAtFirst = hall;
                    break;

                case StaticClass.room.VASHAL:
                    looktAtFirst = vashal;
                    break;

                case StaticClass.room.BEDROOM:
                    looktAtFirst = bedroom;
                    break;
        }
        Camera.main.transform.LookAt(looktAtFirst);
        Camera.main.fieldOfView = 0;
        initDir = Camera.main.transform.rotation.y;

        StaticClass.lastVisit = SceneManager.GetActiveScene().name == "bedroom" ?StaticClass.room.BEDROOM: StaticClass.room.HALL;
    }

    private void Update()
    {
        if (ready)
        {
            if (Camera.main.fieldOfView <= 60)
            {
                Camera.main.fieldOfView += speed;
                speed += (Camera.main.fieldOfView <= 30 ? 1 : -1) * speed / 10;
            }
            else
            {
                rotate = true;
            }
            if (rotate)
            {
                Camera.main.transform.Rotate(0, Time.deltaTime * rotSpeed, 0, Space.Self);
                rotSpeed += ((Camera.main.transform.rotation.y - initDir) >= 0.6 ? -1: 1) * rotSpeed / 10;
                
                //Debug.Log("Init Dir : " + initDir);
                //Debug.Log("Rotation : " + Camera.main.transform.rotation.y);
                //Debug.Log("Diff : " + (Camera.main.transform.rotation.y - initDir));
                if ((Camera.main.transform.rotation.y - initDir) >= 1.2) {
                    rotate = false;
                    ready = false;
                }
            }
        }
        if (zoom)
        {
            if (Camera.main.fieldOfView >= 5)
            {
                Camera.main.fieldOfView -= speed;
                speed += speed/10;
            }
            else
            {
                zoom = false;
                changeScene = true;
            }
        }
        else if (changeScene)
        {
            Debug.Log("Load scene !!!");
            SceneManager.LoadScene(scne);
        }
    }
}
