using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class AppController : MonoBehaviour
{

    public Camera FirstPersonCamera;
    public GameObject platform;


    private bool mIsQuitting = false;
    //private bool pIsSet = false;
    private const float mModelRotation = 180.0f;
    private int n_hit = 0;
 
    public GameObject disk0;
    public GameObject disk1;
    public GameObject disk2;

    Stack<int> pile0 = new Stack<int>();
    Stack<int> pile1 = new Stack<int>();
    Stack<int> pile2 = new Stack<int>();
    Stack<int> DiskOnHand = new Stack<int>();
    private GameObject[] Platform = new GameObject[3];
    private GameObject[] disk = new GameObject[3];
    private GameObject[] Disk = new GameObject[3];
    private Pose[] pPlat = new Pose[3];
    private Pose[] pDisk = new Pose[3];
    private Anchor[] anchors = new Anchor[3];
   
    private Pose posA;
    private Pose posB;
    private Anchor ancA;
    private Anchor ancB;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Platform[i] = GameObject.Find("platform");
        }
        disk[0] = disk0;
        disk[1] = disk1;
        disk[2] = disk2;
        pile0.Push(0);
        pile0.Push(1);
        pile0.Push(2);
        OnCheckDevice();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateApplicationLifecycle();
        
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
       

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("The light shoots on the back of the plane.");
            }
            else
            {
                if ((n_hit >= 0) && (n_hit < 3)){
                    pPlat[n_hit] = hit.Pose;
                    Platform[n_hit] = Instantiate(platform, pPlat[n_hit].position, pPlat[n_hit].rotation);
                    Platform[n_hit].transform.Rotate(0, mModelRotation, 0, Space.Self);
                    anchors[n_hit] = hit.Trackable.CreateAnchor(hit.Pose);
                    Platform[n_hit].transform.parent = anchors[n_hit].transform;
                }
                else if (n_hit == 3)
                {
                    float yy = Platform[0].GetComponent<Transform>().position.y + (float)0.01;
                    for (int j = 0; j < 3; j++)
                    {
                        yy = yy + (float)0.02;
                        pDisk[j].position = new Vector3(Platform[0].GetComponent<Transform>().position.x, yy, Platform[0].GetComponent<Transform>().position.z);
                        Disk[j] = Instantiate(disk[j], pDisk[j].position, pDisk[j].rotation);
                        Disk[j].transform.Rotate(0, mModelRotation, 0, Space.Self);
                        Disk[j].transform.parent = anchors[0].transform;
                    }
                    //pIsSet = true;
                }
                if (n_hit == 4)
                {
                    GameObject.Destroy(Disk[2], 0);
                    float yy = Platform[1].GetComponent<Transform>().position.y + (float)0.03;
                    pDisk[2].position = new Vector3(Platform[1].GetComponent<Transform>().position.x, yy, Platform[1].GetComponent<Transform>().position.z);
                    Disk[2] = Instantiate(disk[2], pDisk[2].position, pDisk[2].rotation);
                    Disk[2].transform.Rotate(0, mModelRotation, 0, Space.Self);
                    Disk[2].transform.parent = anchors[1].transform;
                }

                if (n_hit == 5)
                {
                    GameObject.Destroy(Disk[1], 0);
                    float yy = Platform[1].GetComponent<Transform>().position.y + (float)0.05;
                    pDisk[1].position = new Vector3(Platform[1].GetComponent<Transform>().position.x, yy, Platform[1].GetComponent<Transform>().position.z);
                    Disk[1] = Instantiate(disk[1], pDisk[1].position, pDisk[1].rotation);
                    Disk[1].transform.Rotate(0, mModelRotation, 0, Space.Self);
                    Disk[1].transform.parent = anchors[1].transform;
                }

                if (n_hit == 6)
                {
                    GameObject.Destroy(Disk[0], 0);
                    float yy = Platform[2].GetComponent<Transform>().position.y + (float)0.03;
                    pDisk[0].position = new Vector3(Platform[2].GetComponent<Transform>().position.x, yy, Platform[2].GetComponent<Transform>().position.z);
                    Disk[0] = Instantiate(disk[0], pDisk[0].position, pDisk[0].rotation);
                    Disk[0].transform.Rotate(0, mModelRotation, 0, Space.Self);
                    Disk[0].transform.parent = anchors[2].transform;
                }

                if (n_hit == 7)
                {
                    GameObject.Destroy(Disk[1], 0);
                    float yy = Platform[2].GetComponent<Transform>().position.y + (float)0.05;
                    pDisk[1].position = new Vector3(Platform[2].GetComponent<Transform>().position.x, yy, Platform[2].GetComponent<Transform>().position.z);
                    Disk[1] = Instantiate(disk[1], pDisk[1].position, pDisk[1].rotation);
                    Disk[1].transform.Rotate(0, mModelRotation, 0, Space.Self);
                    Disk[1].transform.parent = anchors[1].transform;
                }

                if (n_hit == 8)
                {
                    GameObject.Destroy(Disk[2], 0);
                    float yy = Platform[2].GetComponent<Transform>().position.y + (float)0.07;
                    pDisk[2].position = new Vector3(Platform[2].GetComponent<Transform>().position.x, yy, Platform[2].GetComponent<Transform>().position.z);
                    Disk[2] = Instantiate(disk[2], pDisk[2].position, pDisk[2].rotation);
                    Disk[2].transform.Rotate(0, mModelRotation, 0, Space.Self);
                    Disk[2].transform.parent = anchors[2].transform;
                }

                n_hit = n_hit + 1;
            }      
        }

        //if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit) && pIsSet)
        //{
        //    if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
        //    {
        //        Debug.Log("The light shoots on the back of the plane.");
        //    }
        //    else
        //    {
        //        int n_platform = -1;
        //        for (int i = 0; i < 3; i++)
        //        {
        //            if (anchors[i] == hit.Trackable.CreateAnchor(hit.Pose)) { n_platform = i; };
        //        }
        //        if(DiskOnHand == null)
        //        {
        //            int n_disk = -1;
        //            if (n_platform == 0) { n_disk = pile0.Pop(); }
        //            else if (n_platform == 1) { n_disk = pile1.Pop(); }
        //            else if (n_platform == 2) { n_disk = pile2.Pop(); };
        //            DiskOnHand.Push(n_disk);
        //            GameObject.Destroy(Disk[n_disk]);
        //        }
        //        else
        //        {
        //            int n_disk = DiskOnHand.Pop();


        //            int c_disk = 0;
        //            if (n_platform == 0) { c_disk = pile0.Count; }
        //            else if (n_platform == 1) { c_disk = pile1.Count; }
        //            else if (n_platform == 2) { c_disk = pile2.Count; };

        //            float yy = Platform[n_platform].GetComponent<Transform>().position.y + (float)0.03;
        //            yy = yy + (float)0.02 * c_disk;
        //            pDisk[n_disk].position = new Vector3(Platform[n_platform].GetComponent<Transform>().position.x, yy, Platform[n_platform].GetComponent<Transform>().position.z);
        //            Disk[n_disk] = Instantiate(disk[n_disk], pDisk[n_disk].position, pDisk[n_disk].rotation);
        //            Disk[n_disk].transform.Rotate(0, mModelRotation, 0, Space.Self);
        //            Disk[n_disk].transform.parent = anchors[n_disk].transform;

        //            if (n_platform == 0) { pile0.Push(n_disk); }
        //            else if (n_platform == 1) { pile1.Push(n_disk); }
        //            else if (n_platform == 2) { pile2.Push(n_disk); };


        //        }
        //    }
        //}

      
    }
    /// <summary>
    /// Check equipement
    /// </summary>
    private void OnCheckDevice()
    {
        if (Session.Status == SessionStatus.ErrorSessionConfigurationNotSupported)
        {
            ShowAndroidToastMessage("ARCore is not supported in this device or config faut.");
            mIsQuitting = true;
            Invoke("DoQuit", 0.5f);
        }
        else if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            ShowAndroidToastMessage("You need to authorize the camera for this app.");
            mIsQuitting = true;
            Invoke("DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            ShowAndroidToastMessage("ARCore error, please reboot the app.");
            mIsQuitting = true;
            Invoke("DoQuit", 0.5f);
        }
    }
    /// <summary>
    /// Manage the life circle of the app
    /// </summary>
    private void UpdateApplicationLifecycle()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (mIsQuitting)
        {
            return;
        }
    }
    /// <summary>
    /// Quit
    /// </summary>
    private void DoQuit()
    {
        Application.Quit();
    }
    /// <summary>
    /// Show messages
    /// </summary>
    /// <param name="message">The message to show</param>
    private void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}