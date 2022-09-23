using System;
using UnityEngine;
using UnityEngine.UI;

public class rotatetotate : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;
    public float rotateSpeed = 200;
    private float randomDir;

    // Use this for initialization
    void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        randomDir = UnityEngine.Random.Range(-rotateSpeed, rotateSpeed);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float currentSpeed = randomDir * Time.deltaTime;
        rectComponent.Rotate(0f, 0f, currentSpeed);
    }
}