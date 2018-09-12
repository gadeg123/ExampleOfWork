using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



public class controller : MonoBehaviour {

    public Mower mower;
    public MowerData md;
    public ProgressBar pb;
    public ProgressBarCircle circle;
    private SteamVR_TrackedObject trackedObj;
    // 2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    
    }

    private void Start()
    {
        Debug.Log(md.p.LookAheadDist);
        Debug.Log(md.p.Speed);
    }


    // Update is called once per frame
    void Update ()
    {       


         if (Controller.GetHairTriggerDown())
        {
            
            md.Write();
                
        }
        


        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));
           if (touchpad.x > 0.7f)
            {
                md.p.LookAheadDist++;
                pb.BarValue = md.p.LookAheadDist;

                if(md.p.LookAheadDist > 10)
                {
                    md.p.LookAheadDist = 10;
                }
           }

            else if (touchpad.x < -0.7f)
            {
                md.p.LookAheadDist--;
                pb.BarValue = md.p.LookAheadDist;

                if (md.p.LookAheadDist < 0)
                {
                    md.p.LookAheadDist = 0;
                }
            }

            if (touchpad.y > 0.7f)
            {
               md.p.Speed++;
                circle.BarValue = md.p.Speed;

                if (md.p.Speed > 10)
                {
                    md.p.Speed = 10;
                }
            }

            else if (touchpad.y < -0.7f)
           {
                md.p.Speed--;
                circle.BarValue = md.p.Speed;

                if (md.p.Speed < 0)
                {
                    md.p.Speed = 0;
                }
            }
        }



    }
}
