using UnityEngine;
using System.Collections;

public class PresentPrefabSpawner : MonoBehaviour {

    private float nextSpawn = 0;
    public Transform[] prefabToSpawn;
    //public float spawnRate = 1;
    //public float randomDelay = 1;
    public AnimationCurve spawnCurve;
    public float curveLenghtInSeconds = 30f;
    private float startTime;
    public float jitter = 0.25f;
    private float y;
    private Vector3 pos;
        
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
            y = Random.Range(-1, 3.5f);
            pos = new Vector3(transform.position.x, y, transform.position.z);
            transform.position = pos;
            Instantiate(prefabToSpawn[Random.Range(0, 4)], pos, Quaternion.identity);
            //Instantiate(prefabToSpawn[Random.Range(0,3)], transform.position, Quaternion.identity); //usa a posição do gameObject como parâmetro em transform.position
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
