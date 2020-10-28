using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLightManager : MonoBehaviour
{
    public Camera cam;
    public GameObject[] lightsPrefabs;
    float height;
    public float gap;
    void Start()
    {
        if(!GameManager.manager.bgLightsMade)
        {
            height = 2f * cam.orthographicSize;
            int numLines = Mathf.CeilToInt((height + 3) / ((4.5f + (3 * gap))));
            int extra = numLines % lightsPrefabs.Length;
            int colorCount = numLines / lightsPrefabs.Length;
            int[] numToInstantiate = new int[lightsPrefabs.Length];
            for (int i = 0; i < numToInstantiate.Length; i++)
            {
                numToInstantiate[i] = colorCount;
            }
            for (int i = 0; i < extra; i++)
            {
                numToInstantiate[((numToInstantiate.Length / 2) - (extra / 2)) + i]++;
            }
            Vector3 pos = new Vector3(0, cam.orthographicSize + 1.5f);
            float posMod = 4.5f;
            float totalAddedSpace = 0.0f;
            List<BGLight> lights = new List<BGLight>();
            for (int i = 0; i < numToInstantiate.Length; i++)
            {
                posMod += gap * i;
                for (int j = 0; j < numToInstantiate[i]; j++)
                {
                    totalAddedSpace += gap * i;
                    GameObject obj = GameObject.Instantiate(lightsPrefabs[i], pos, Quaternion.identity);
                    lights.Add(obj.GetComponent<BGLight>());
                    pos.y -= posMod;
                }

            }
            foreach (BGLight light in lights)
            {
                light.top = cam.orthographicSize + 1.5f + totalAddedSpace;
                light.bottom = -cam.orthographicSize - 1.5f - totalAddedSpace;
            }
            GameManager.manager.bgLightsMade = true;
            Debug.Log("done");
        }
    }
}
