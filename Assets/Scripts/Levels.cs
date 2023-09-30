using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level{
    public List<string> mRows = new List<string>();

    public int Height {get{return mRows.Count;}}
    public int Width{
        get{
            int maxLength = 0;
            foreach (var r in mRows){
                if (r.Length > maxLength)
                    maxLength = r.Length;
            }
            return maxLength;
        }
    }
}


public class Levels : MonoBehaviour{
    public string filename;
    public List<Level> mLevels;

    void Awake(){
        TextAsset textAsset = (TextAsset) Resources.Load(filename);
        if(!textAsset){
            Debug.Log("Levels: " + filename + ".txt does not exist!");
            return;
        }
        else{
            Debug.Log("Levels imported");
        }
        string completeText = textAsset.text;
        string[] lines;
        lines = completeText.Split(new string[] {"\n"}, System.StringSplitOptions.None);
        mLevels.Add(new Level());
        for( long i = 0; i < lines.LongLength; i++){
            string line = lines[i];
            if(line.StartsWith(";")){
                Debug.Log("New Level Added");
                mLevels.Add(new Level());
                continue;
            }
            mLevels[mLevels.Count-1].mRows.Add(line);
        }
    }
}
