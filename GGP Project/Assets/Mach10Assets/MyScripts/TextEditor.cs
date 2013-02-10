using UnityEngine;
using System;
using System.Text;
using System.IO;

public class TextEditor : MonoBehaviour {
	string path;
	string fileName;

	// Use this for initialization
	void Start () {
		fileName = "/MYTXT.txt";
	}
	
	// Update is called once per frame
	void Update () {
		path = Application.dataPath + fileName;
		using(FileStream fs = File.Create(path)){
			AddText(fs, "My first text file!");
		}
	}
	
	private static void AddText(FileStream fs, string value){
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

}
