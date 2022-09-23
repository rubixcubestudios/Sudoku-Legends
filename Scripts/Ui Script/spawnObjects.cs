using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour
{
    public GameObject spawnPrefab;
    public float respawnTime = 1.0f;
    public Vector2 screenbound;
    public float MinScale;
    public float MaxScale;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(timer());
    }

    private void coinSpawn()
    {
        GameObject coinParent = Instantiate(spawnPrefab) as GameObject;
        coinParent.transform.position = new Vector2(Random.Range(-screenbound.x, screenbound.x), screenbound.y);
        coinParent.transform.Rotate(Vector3.forward, Random.Range(0, 360));
        coinParent.transform.SetParent(this.transform);
        float screenbound2 = Random.Range(MinScale, MaxScale);
        coinParent.transform.localScale = new Vector3(screenbound2, screenbound2, screenbound2);

    }

    IEnumerator timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            coinSpawn();
        }
    }
       
}
