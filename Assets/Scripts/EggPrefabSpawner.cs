using UnityEngine;
using System.Collections;

public class EggPrefabSpawner : MonoBehaviour {

    private float nextSpawn = 0;
    public Transform[] prefabToSpawn;
    //public float spawnRate = 1;
    //public float randomDelay = 1;
    public AnimationCurve spawnCurve;
    public float curveLenghtInSeconds = 30f;
    private float startTime;
    public float jitter = 0.25f;
        
    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            Instantiate(prefabToSpawn[Random.Range(0,3)], transform.position, Quaternion.identity);
            //nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);

            float curvePos = (Time.time - startTime) / curveLenghtInSeconds;
            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;
            }

            nextSpawn = Time.time + spawnCurve.Evaluate(curvePos) + Random.Range(-jitter, jitter);

        }

    }
}
