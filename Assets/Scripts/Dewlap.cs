using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dewlap : MonoBehaviour
{

    public GameObject dewlap;
    public Collider dewlapCollider;
    public int Energy;
    float timeLeft = 30;

    public float ZoomSpeed = 0.1f;        // The rate of change of the orthographic size in orthographic mode.

    // Use this for initialization
    void Start ()
    {
        Energy = 20;

	}

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("MiniGameMenu");
        }

        dewlap.transform.Translate((float)-0.01, (float)0.01, 0);

        // If there are two touches on the device...
        //if (Input.touchCount == 2)
        //{
        //    // Store both touches.
        //    Touch touchZero = Input.GetTouch(0);
        //    Touch touchOne = Input.GetTouch(1);

        //    // Find the position in the previous frame of each touch.
        //    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        //    // Find the magnitude of the vector (the distance) between the touches in each frame.
        //    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        //    // Find the difference in the distances between each frame.
        //    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


        //    dewlap.transform.Translate(deltaMagnitudeDiff * ZoomSpeed, deltaMagnitudeDiff + ZoomSpeed, 0);

        //}


        if (Input.GetMouseButtonDown(0))
        {
            Energy--;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider == dewlapCollider)
                    dewlap.transform.Translate((float)0.05, (float)-0.05, 0);
        }

        if (Energy <= 0)
        {
            SceneManager.LoadScene("MiniGameMenu");
        }

    }
    void OnGUI()
    {
        GUI.Label(new Rect(50, 150, 200, 50), "Energy: " + Energy);
        {
        }
        GUI.Label(new Rect(50, 100, 200, 50), "Timer: " + (int)timeLeft);
        {
        }
    }


}

