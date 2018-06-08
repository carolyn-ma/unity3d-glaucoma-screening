using UnityEngine;
using System.IO;

public class BlindSpotMapper : MonoBehaviour
{

    float rotateBy = 0.25f;
    float spotAng = 0.25f;
    public Light spotL;
    public Vector3 rotationAng;
    public string lr;
    public float temp;

    void Start()
    {
        spotL = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * rotateBy);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotateBy);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.left * rotateBy);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right * rotateBy);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            spotL.spotAngle += spotAng;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (spotL.spotAngle > 0)
            {
                spotL.spotAngle -= spotAng;
            }
        }
        
    }

    void OnApplicationQuit()
    {
       
        if (transform.localEulerAngles.y < 180) {
            lr = "right";
            temp = transform.localEulerAngles.y;
        }
        if (transform.localEulerAngles.y > 180)
        {
            lr = "left";
            temp = 360 - transform.localEulerAngles.y;
        }

       // File.AppendAllText("C:/Users/llim_000/Dropbox/Oculus work/CarolynMa/VF Project/VF Project/BlindSpotOutput.txt", System.String.Format("This patient's "+lr+" eye blind spot, tested at " + System.DateTime.Now + " is at " + temp + " degrees temporally.\n\n"));

    }


}