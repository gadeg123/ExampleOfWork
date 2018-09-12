using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class MowerData : MonoBehaviour {

    public List<Parameters> database = new List<Parameters>();
    public Parameters p;
    string path;


    private void Awake()
    {
        path = Application.dataPath + "/config.json";
        p = new Parameters();
        Read();
    }

    public void Start()
    {
      //   path = Application.dataPath + "/config.json";
      //  p = new Parameters();
        //Read();
        //Write();

    }

   


        public void Write()
        {
        //    JObject jsonVariable = new JObject();


        //    jsonVariable.Add("lookAheadDist", Look_ahead_dist);
        //    jsonVariable.Add("speed", Speed_ref);

        string json = File.ReadAllText(path);
        json = Reverse(json);
        Debug.Log(json);
        File.WriteAllText(path, json);


        //    File.WriteAllText(path, jsonVariable.ToString());

    }





    public void Read()
    {
        string json = File.ReadAllText(path);
         json = Convert(json);
        Debug.Log(json);
        JsonConvert.PopulateObject(json, p);

        //string path = Application.dataPath + "/config.json";
        //string jstring = File.ReadAllText(path);

        //JObject parameterjson = JObject.Parse(jstring);

        //database.Add(new Parameters((int)parameterjson["parameters"]["{fmu3}.Steering_control.look_ahead_dist"], (int)parameterjson["parameters"]["{fmu3}.Steering_control.speed_ref"]));

       // pb.BarValue = database[0].Look_ahead_dist;
        //circle.BarValue = database[0].Speed_ref;

    }

     void Update()
    {
        
    }

    string Convert(string json)
     {
      json = json.Substring(920);
       json = json.Remove(json.Length - 514);
       json =json.Replace("{fmu3}.Steering_control.look_ahead_dist", "LookAheadDist");
       json = json.Replace("{fmu3}.Steering_control.speed_ref", "Speed");
        json = json.Insert(0, "{");
        //json = json.Insert(json.Length, "}");
       return json;
     }

    string Reverse(string jj)
    {
        string json = "";
        json = jj.Substring(0, 920);
        string s = JsonConvert.SerializeObject(p);
        s = s.Substring(1, s.Length - 1);
        json += s;
        json += jj.Substring(jj.Length - 514);
        json = json.Replace("LookAheadDist", "{fmu3}.Steering_control.look_ahead_dist");
        json = json.Replace("Speed", "{fmu3}.Steering_control.speed_ref");
        return json;

    }


}

public class Parameters

{
    public int LookAheadDist { get; set; }
    public int Speed { get; set; }
}

