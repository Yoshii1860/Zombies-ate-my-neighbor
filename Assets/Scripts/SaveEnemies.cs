using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEnemies : MonoBehaviour
{
    [SerializeField] public EnemyHealth[] enemyArray;
    [SerializeField] public string[] enemyStrings;
    int i = 0;

    void OnAwake() 
    {
        Debug.Log("SaveEnemies");
        foreach(EnemyHealth enemy in enemyArray)
        {
            string name = enemy.transform.gameObject.name + i.ToString();
            if(PlayerPrefs.GetString(name) == "true")
            {
                return;
            }
            else
            {
                enemyStrings[i] = name;
            }

            if(i != enemyArray.Length-1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    void Update() 
    {
        foreach(EnemyHealth enemy in enemyArray)
        {
            bool dead = enemy.IsDead();
            string name = enemy.transform.gameObject.name + i.ToString();
            if(PlayerPrefs.GetString(name) == "true")
            {
                return;
            }
            else if(dead)
            {
                enemyStrings[i] = "true";
            }

            if(i != enemyArray.Length-1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }
}
