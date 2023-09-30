using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public LevelBuiilder mLevelBuilder;
    public GameObject mNextButton;
    private bool mReadyForInput;
    private Player mPlayer;

    [SerializeField] public Sprite[] crossSprites;
    [SerializeField] public TMP_Text tMP_Text;
    [SerializeField] public TMP_Text sokoban;
    [SerializeField] public GameObject Play;
    [SerializeField] public GameObject Menu;


    void Start(){
        mNextButton.SetActive(false);
        tMP_Text.enabled = false;
        ResetScene();
    }

    void Update(){
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        float randomNumber = Random.Range(0, 5);
        foreach(var cr in crosses){
            if (randomNumber == 0)
                cr.gameObject.GetComponent<SpriteRenderer>().sprite = crossSprites[0];
            else if (randomNumber == 1)
                cr.gameObject.GetComponent<SpriteRenderer>().sprite = crossSprites[1];
            else if (randomNumber == 2)
                cr.gameObject.GetComponent<SpriteRenderer>().sprite = crossSprites[2];
            else if (randomNumber == 3)
                cr.gameObject.GetComponent<SpriteRenderer>().sprite = crossSprites[3];
            else if (randomNumber == 4)
                cr.gameObject.GetComponent<SpriteRenderer>().sprite = crossSprites[4];
            }
        


        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if(moveInput.sqrMagnitude > 0.5){ //movement is discrete -> player can't keep moving by holding key.
            if(mReadyForInput){
                mReadyForInput = false;
                mPlayer.Move(moveInput);
                mNextButton.SetActive(IsLevelComplete());
                if(IsLevelComplete()){ tMP_Text.enabled = true;}
            }
        }
        else{
            mReadyForInput = true;
        }
    }

    public void NextLevel(){
        mNextButton.SetActive(false);
        tMP_Text.enabled = false;
        mLevelBuilder.NextLevel();
        StartCoroutine(ResetSceneASync());
    }

    public void ResetScene(){
        mNextButton.SetActive(false);
        tMP_Text.enabled = false;
        StartCoroutine(ResetSceneASync());
    }

    bool IsLevelComplete(){
        Box[] boxes = FindObjectsOfType<Box>();
        foreach(var box in boxes){
            if(!box.mOnCross)
                return false;
        }
        return true;
    }

    IEnumerator ResetSceneASync() {
        if (SceneManager.sceneCount > 1) {
            AsyncOperation asyncUn = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUn.isDone) {
                yield return null;
                Debug.Log("Unloading");
            }
            Debug.Log("Unloading done.");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
            Debug.Log("Loading");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        mLevelBuilder.Build();
        mPlayer = FindObjectOfType<Player>();
        Debug.Log("Level loaded.");
    }

    public void play(){
        Play.SetActive(false);
        Menu.SetActive(false);
        sokoban.enabled = false;
    }





}
