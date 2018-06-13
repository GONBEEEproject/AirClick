using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureMic : MonoBehaviour {

    private AudioSource AS;

    [SerializeField]
    private GameObject handObj;
    private MeshRenderer handRenderer;

    private bool isHand=false;

	// Use this for initialization
	void Start () {
        AS = GetComponent<AudioSource>();
        AS.clip = Microphone.Start(null, true, 999, 44100);
        AS.loop = true;

        while (!(Microphone.GetPosition("") > 0)) { }
        AS.Play();

        handRenderer = GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        float vol = GetAverageVolume();
        //Debug.Log(vol);

        if (vol > 0.05)
        {
            isHand=!isHand;
            handRenderer.enabled = isHand;
        }

	}

    float GetAverageVolume()
    {
        float[] data = new float[256];
        float a = 0;
        AS.GetOutputData(data, 0);
        foreach(float s in data)
        {
            a += Mathf.Abs(s);
        }

        return a / 256.0f;
    }


}
