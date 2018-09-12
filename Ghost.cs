using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Ghost : MonoBehaviour

{
    private float speed = 0.02f;
    string filepath;
    public List<Vector2> ghostMovement = new List<Vector2>();
    List<float> ghostRotation = new List<float>();
    
    



    // Use this for initialization
    void Start()
    {
        filepath = Application.streamingAssetsPath + "/ghost.csv";
        StartCoroutine(Readghost(filepath));
        

    }
    bool done = false;

    IEnumerator Readghost(string filepath)
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
                ghostMovement.Add(thisRowsData);
                ghostRotation.Add(rotation);

            }
        }
        yield return null;
        done = true;
        // UnityEngine.Debug.Log("Finshed reading car data");
    }

    public List<Vector2> GetGhostMovementData()
    {
        if (done)
        {
            foreach (var item in ghostMovement)
            {
                // UnityEngine.Debug.Log("counting movement data");
            }

        }
        return ghostMovement;
    }

    public List<float> GetGhostRotationData()
    {
        if (done)
        {
            foreach (var item in ghostRotation)
            {
                // UnityEngine.Debug.Log("counting rotation data");
            }

        }
        return ghostRotation;
    }


    private void Update()
    {
    }
    


    //per 0.01s move robot position.
    public IEnumerator GhostMove(List<Vector2> ghostMovementlist)
    {
       

        int i = 0;

        while (i < ghostMovement.Count)
        {
            float angle = Mathf.Tan(ghostRotation.Count);
            transform.position = new Vector3(ghostMovement[i].x, 0, ghostMovement[i].y + angle);


            yield return new WaitForSeconds(speed);
            i++;
            //UnityEngine.Debug.Log("i is:" + i);

            if (i > ghostMovement.Count - 1)
            {
                
                yield break;
            }
        }
    }
}
