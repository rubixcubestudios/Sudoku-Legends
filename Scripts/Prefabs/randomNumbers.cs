using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class randomNumbers : MonoBehaviour
{
    public float seconds = 2;
    public TMP_Text GridText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        GridText.text = Random.Range(1,9).ToString();
        StartCoroutine(Wait2Seconds());
    }

    IEnumerator Wait2Seconds()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
