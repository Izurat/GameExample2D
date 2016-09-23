using UnityEngine;
using System.Collections;

public class CharacterAnimationsManager : MonoBehaviour {
    public AnimationClip walkClip;
    public AnimationClip idleClip;
    private const string stepForwardAnimation = "KnightAnimationStepForward";
    private const string idleAnimation = "KnightAnimationIdle";

    private void setWalkAnimation()
    {
        Animation anim = GetComponent<Animation>();
        //anim[idleAnimation].normalizedTime = 0f;
        anim[idleAnimation].time = 0;
        anim.Sample();
        anim.Stop(idleAnimation);
        anim.Play(stepForwardAnimation);
    }

    private void setIdleAnimation()
    {
        Animation anim = GetComponent<Animation>();
        //anim[stepForwardAnimation].normalizedTime = 0f;
        anim[stepForwardAnimation].time = 0;
        anim.Sample();
        anim.Stop(stepForwardAnimation);
        anim.Play(idleAnimation);
        
    }
	void Start () 
    {
        Animation anim = GetComponent<Animation>();
        anim.AddClip(walkClip, stepForwardAnimation);
        anim.AddClip(idleClip, idleAnimation);
	}
	
	void Update () 
    {
        if (Input.GetKeyDown("e"))
        {
            setWalkAnimation();
        }

        if (Input.GetKeyDown("r"))
        {
            setIdleAnimation();
        }
	}
}
