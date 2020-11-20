using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    private float Value=25.0f;
    private float Inventory=0f;
    private float MaxValue = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpMineral()
    {
        if (Inventory < MaxValue)
        {
            Inventory += Value;
        }
    }

    public float GetInventory()
    {
        return Inventory;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Inventory", Inventory);
    }

    public void FetchInventory()
    {
        Inventory = PlayerPrefs.GetFloat("Inventory");
    }
}
