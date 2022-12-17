using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEnemies : MonoBehaviour
{
    [SerializeField] public EnemyHealth[] enemyArray;
    [SerializeField] public string[] enemyStrings;
    int i = 0;

    void Start() 
    {
        Debug.Log("SaveEnemies");
        foreach(EnemyHealth enemy in enemyArray)
        {
            string name = enemy.transform.gameObject.name + i.ToString();
            if(enemyStrings[i] == "null")
            {
                enemyStrings[i] = name;
            }
            i++;
            if(i == enemyArray.Length)
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
            if(dead && PlayerPrefs.GetString(name) == name)
            {
                enemyStrings[i] = "true";
            }
            i++;
            if(i == enemyArray.Length)
            {
                i = 0;
            }
        }
    }
}
