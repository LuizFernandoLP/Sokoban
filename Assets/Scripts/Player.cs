using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public bool Move(Vector2 direction){
        if (Mathf.Abs(direction.x) < 0.5){ //prevents from moving diagonally
            direction.x = 0;
        }
        else{
            direction.y = 0;
        }
        direction.Normalize(); //moves one unit at a time
        if (Blocked(transform.position, direction)){ //if path is blocked, don't move otherwise move
            return false;
        }
        else{
            transform.Translate(direction);
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction){
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        Debug.Log(walls.Length);
        foreach (var wall in walls){
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y){
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach(var box in boxes){
            if(box.transform.position.x == newPos.x && box.transform.position.y == newPos.y){
                Box box1 = box.GetComponent<Box>();
                if(box1 && box1.Move(direction)){
                    return false;
                }
                else{
                    return true;
                }
            }
        }
        return false;
    }
}