using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using UnityEngine.UI;

public class Mower : MonoBehaviour
{
    private float speed = 0.02f;
    string filepath;
    string startsim;
    public List<Vector2> mowerMovement = new List<Vector2>();
    List<float> mowerRotation = new List<float>();
    AudioSource audioData;
    
   

    // Use this for initialization
    void Start()
    {
        filepath = Application.streamingAssetsPath + "/CSVoutputs.csv";
        StartCoroutine(ReadCSV(filepath));
        startsim = "F:\\new unity project 2\\assets\\simstart.bat";   

    }
    bool done = false;

    IEnumerator ReadCSV(string filepath)
    {
        using (StreamReader sr = new StreamReader(filepath))
        {


            string data = sr.ReadLine();
            sr.ReadLine();

            while ((data = sr.ReadLine()) != null)
            {
                data = sr.ReadLine();
                char[] comma = { ',' };
                string[] rows = data.Split(comma);
                Vector2 thisRowsData = new Vector2();
                float rotation = new float();
                thisRowsData.x = Convert.ToSingle(rows[5]);
                thisRowsData.y = Convert.ToSingle(rows[6]);
                rotation = Convert.ToSingle(rows[4]);

                //UnityEngine.Debug.Log("x values" + thisRowsData.x);
                //UnityEngine.Debug.Log("z values" + thisRowsData.y);
                //UnityEngine.Debug.Log("rotation values" + rotation);
                mowerMovement.Add(thisRowsData);
                mowerRotation.Add(rotation);

            }
        }
        yield return null;
        done = true;
       // UnityEngine.Debug.Log("Finshed reading car data");
    }

    public List<Vector2> GetMowerMovementData()
    {
        if (done)
        {
            foreach (var item in mowerMovement)
            {
               // UnityEngine.Debug.Log("counting movement data");
            }

        }
        return mowerMovement;
    }

    public List<float> GetMowerRotationData()
    {
        if (done)
        {
            foreach (var item in mowerRotation)
            {
               // UnityEngine.Debug.Log("counting rotation data");
            }

        }
        return mowerRotation;
    }


    private void Update()
    {
    }
    

    public void startsimulation()
    {

        var startInfo = new ProcessStartInfo();
        UnityEngine.Debug.Log("Directory = " + startInfo.WorkingDirectory);
        Process p = new Process();
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.FileName = startsim;
        p.Start();
        UnityEngine.Debug.Log("Simulation started.");
        while (!p.HasExited)
        {
            UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
            UnityEngine.Debug.Log(p.StandardError.ReadToEnd());
        }
        UnityEngine.Debug.Log("Simulation ended.");
    }


        
    
    //per 0.01s move robot position.
   public IEnumerator MowerMove(List<Vector2> mowerMovementlist)
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);

        int i = 0;

        while (i < mowerMovement.Count)
        {
            float angle = Mathf.Tan(mowerRotation.Count);
            transform.position = new Vector3(mowerMovement[i].x, 0, mowerMovement[i].y + angle);
           

            yield return new WaitForSeconds(speed);
            i++;
            //UnityEngine.Debug.Log("i is:" + i);

            if (i > mowerMovement.Count - 1)
            {
                audioData.Pause();
                yield break;
            }
        }
    }
}


    

