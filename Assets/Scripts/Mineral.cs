using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    private float Value=25.0f;
    private float Inventory=0f;
    private float MaxValue = 100f;

    public GameObject visual;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (visual != null)
        {
            Vector3 visualPosition = transform.position;

            // Move the visual element in a sine wave
            visualPosition.y += Mathf.Sin(timer) / 4;

            // set the modified position to the visual
            visual.transform.position = visualPosition;

            // rotate the visual for nice effect
            visual.transform.Rotate(visual.transform.up * 60 * Time.deltaTime);

            // sine wave requires a time variable, use this
            timer += 2.2f * Time.deltaTime;
        }
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
