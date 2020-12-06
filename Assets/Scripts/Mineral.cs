using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    //Variables for congiguring mineral
    private float Value=25.0f;
    private float Inventory=0f;
    private float MaxValue = 100f;

    public GameObject visual;
    private float timer;

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

    //If there inventory isn't full, value is added to it
    //Also adds one to mineralCount which is used for displaying score
    public void PickUpMineral()
    {
        if (Inventory < MaxValue)
        {
            Inventory += Value;
            GameController.current.mineralCount++;
            GameController.current.totalCollectedMinerals++;
        }

    }

    //Drops mineral if player collides with enemy or enemybullet
    public void DropMineral() {
        if (Inventory > 0)
        {
            Inventory -= Value;
            if (GameController.current.mineralCount > 0)
            {
                GameController.current.mineralCount--;
            }
        }
    }

    //Returns inventory
    public float GetInventory()
    {
        return Inventory;
    }
}
