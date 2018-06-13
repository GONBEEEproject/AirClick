using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;



public class HandManager : MonoBehaviour {

    [SerializeField]
    private GameObject handObj;
    //private MeshRenderer handRenderer;
    private bool isHand = false;

    [SerializeField]
    private GameObject bulletMother;
    private bool isShot = false;

    [SerializeField]
    private GameObject bulletPrefab;

    private GazeManager GM;

    [SerializeField]
    private AudioSource AS;

    [SerializeField]
    private AudioSource SEAS;


    [SerializeField]
    private TrailRenderer[] renderers;


    // Use this for initialization
    void Start()
    {
        InteractionManager.InteractionSourceUpdated += InteractionSourceUpdated;
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
        InteractionManager.InteractionSourceLost += InteractionSourceLost;

        //handRenderer = handObj.GetComponent<MeshRenderer>();

        GM = GetComponent<GazeManager>();

        //AS = GetComponent<AudioSource>();
        AS.clip = Microphone.Start(null, true, 999, 44100);
        AS.loop = true;

        while (!(Microphone.GetPosition("") > 0)) { }
        AS.Play();
    }

    //手だして
    private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs eventArgs)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        //handRenderer.enabled = true;
        handObj.SetActive(true);

        Vector3 position;
        if (eventArgs.state.sourcePose.TryGetPosition(out position))
        {
            handObj.transform.position = position;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }
    }

    //指さげて
    private void InteractionSourcePressed(InteractionSourcePressedEventArgs eventArgs)
    {

    }

    //指あげて
    private void InteractionSourceReleased(InteractionSourceReleasedEventArgs eventArgs)
    {

    }

    //手ださないで
    private void InteractionSourceLost(InteractionSourceLostEventArgs eventArgs)
    {
        //handRenderer.enabled = false;
        handObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float vol = GetAverageVolume();
        //Debug.Log(vol);

        if (vol > 0.08)
        {
            //isHand = !isHand;
            //handRenderer.enabled = isHand;
            //handObj.SetActive(isHand);

            if (!isShot)
            {
                Shoot();
            }
            else
            {
                Reload();
            }
        }

    }

    void Shoot()
    {
        isShot = true;
        Debug.Log("Shot!");

        Instantiate(bulletPrefab, handObj.transform.position,Quaternion.LookRotation(GM.GetRayPos()));

        bulletMother.SetActive(false);

    }

    void Reload()
    {
        isShot = false;
        Debug.Log("Reloaded!");

        bulletMother.SetActive(true);

        SEAS.PlayOneShot(SEAS.clip);

    }

    float GetAverageVolume()
    {
        float[] data = new float[256];
        float a = 0;
        AS.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }

        return a / 256.0f;
    }
}
