using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement{
    public string mcharacter;
    public GameObject mPrefab;
}

public class LevelBuiilder : MonoBehaviour{
    public int mCurrentLevel;
    public List<LevelElement> mLevelElements;
    private Level mLevel;

    GameObject GetPrefab(char c){
        LevelElement levelElement = mLevelElements.Find(le => le.mcharacter == c.ToString());
        if(levelElement != null)
            return levelElement.mPrefab;
        else
            return null;
    }
    public void NextLevel(){
        mCurrentLevel++;
        if(mCurrentLevel >= GetComponent<Levels>().mLevels.Count){
            mCurrentLevel = 0;
        }
    }

    public void Build(){
        mLevel = GetComponent<Levels>().mLevels[mCurrentLevel];
        int startx = -mLevel.Width;
        int x = startx;
        int y = -mLevel.Height/2;
        foreach (var row in mLevel.mRows){
            foreach (var ch in row){
                Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);
                if(prefab){
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x,y,0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startx;
        }
    }
}
