using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject wolfPrefab;
    public GameObject rabbitPrefab;
    public GameObject deerPrefab;
    public int wolfAmount = 5;
    public int rabbitAmount = 5;
    public int deerGroupsAmount = 3;
    public static int deerGroupsAmountG;
    public static List<GameObject[]> deerGroups = new List<GameObject[]>();
    private void Start()
    {
        spawnWolfs();
        spawnRabbits();
        spawnDeers();
    }

    void spawnWolfs()
    {
        for (int i = 0; i < wolfAmount; i++)
        {
            int x = Random.Range(-50,50);
            int y = Random.Range(-30,30);
            GameObject wolf = Instantiate(wolfPrefab,new Vector2(x,y), Quaternion.LookRotation(new Vector3()));
        }
    }

    void spawnRabbits()
    {
        for (int i = 0; i < rabbitAmount; i++)
        {
            int x = Random.Range(-50,50);
            int y = Random.Range(-30,30);
            GameObject rabbit = Instantiate(rabbitPrefab,new Vector2(x,y), Quaternion.LookRotation(new Vector3()));
        }
    }
    
    void spawnDeers()
    {
        deerGroups.Clear();
        for (int i = 0; i < deerGroupsAmount; i++)
        {
            int x = Random.Range(-48,48);
            int y = Random.Range(-30,30);
            GameObject[] group = new GameObject[3];
            GameObject deer1 = Instantiate(deerPrefab,new Vector2(x+3,y), Quaternion.LookRotation(new Vector3()));
            GameObject deer2 = Instantiate(deerPrefab,new Vector2(x,y), Quaternion.LookRotation(new Vector3()));
            GameObject deer3 = Instantiate(deerPrefab,new Vector2(x-3,y), Quaternion.LookRotation(new Vector3()));
            group[0] = deer1;
            group[1] = deer2;
            group[2] = deer3;
            deerGroups.Add(group);
        }

        deerGroupsAmountG = deerGroups.Count;
    }
}
