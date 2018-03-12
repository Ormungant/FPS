using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour {

    public Gradient gradient;
    float time;
    public float Lifetime;
    public float ScaleModifier;
    void Awake()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        time += Time.deltaTime;
        transform.GetComponent<MeshRenderer>().material.SetColor("_TintColor", gradient.Evaluate(time/Lifetime));
        transform.localScale += new Vector3(ScaleModifier/2, ScaleModifier/2, ScaleModifier);
        if(time/Lifetime >0.2)
        {
            Destroy(gameObject);
        }
    }

}
