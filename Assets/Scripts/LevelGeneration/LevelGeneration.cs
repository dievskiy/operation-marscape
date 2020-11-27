using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public GameObject[] prefabs;

    private GameObject[] level;

    // Start is called before the first frame update
    void Start()
    {
        level = new GameObject[] { prefabs[0], prefabs[0], prefabs[1], prefabs[5], prefabs[6], prefabs[0], prefabs[2], prefabs[3], prefabs[4] };

        Vector3 pos = Vector3.zero;
        pos.x -= 25;

        foreach (GameObject prefab in level)
        {
            Instantiate(prefab, pos, Quaternion.identity);
            pos.x += 25;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
