using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


public class Hand : MonoBehaviour {

    [SerializeField]
    private GameObject handObj;
    private MeshRenderer handRenderer;


	// Use this for initialization
	void Start () {
        InteractionManager.InteractionSourceUpdated += InteractionSourceUpdated;
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
        InteractionManager.InteractionSourceLost += InteractionSourceLost;

        handRenderer = handObj.GetComponent<MeshRenderer>();
	}
	
    //手だして
    private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs eventArgs)
    {
        handRenderer.enabled = true;
        
        Vector3 position;
        if(eventArgs.state.sourcePose.TryGetPosition(out position))
        {
            handObj.transform.position = position;
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
        handRenderer.enabled = false;
    }




	// Update is called once per frame
	void Update () {
		
	}
}
