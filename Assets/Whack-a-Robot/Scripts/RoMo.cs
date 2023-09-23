using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class RoMo : MonoBehaviour
{
    // PUBLIC VARABLES
    // public Vector3 hiddenPosition = new(0, 0, 0);
    //public Vector3 visiblePosition = new(0, 3, 0);
    public float visibleHeight = 0f;
    public float hiddenHeight = -0.8f;
    public float hideTimer = 50f;
    public float speed = 1f;
    public bool isHit = false;
    public RoStates state;
    public ParticleSystem hitEffect;

    // PRIVATE VARIABLES

    Vector3 targetPosition;
    Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.speed = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        //RiseMole();
        // StartCoroutine(Peek());
        //  targetPosition = new Vector3(transform.localPosition.x, hiddenHeight, transform.localPosition.z);
        // transform.localPosition = targetPosition;
        //  RiseMole();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.state == RoStates.VISIBLE)

        {
            anim.SetBool("Roll_Anim", false);
            anim.SetBool("Open_Anim", true);

            targetPosition = new Vector3(transform.localPosition.x, visibleHeight, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed);
        }

        if (state == RoStates.HIDING)

        {
            anim.SetBool("Open_Anim", false);
            anim.SetBool("Roll_Anim", true);

            targetPosition = new Vector3(transform.localPosition.x, hiddenHeight, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed);

        }




    }


    public enum RoStates
    {
        HIDING,
        VISIBLE,
    }


    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("HammerHead"))
        {


            if (state == RoStates.VISIBLE)

            {
                onHit();
            }


        }

            


    }



    public void RiseMole()
    {
        // TODO: implement SFX here
     
        //
        this.state = RoStates.VISIBLE;
    }

    public void HideMole()
    {
        // TODO: implement SFX here
        
        //
        this.state = RoStates.HIDING;

    }

    void onHit()
    {
        Debug.Log("boop");
        hitEffect.Play();
        Destroy(gameObject);
    }



    // Coroutine for testing
    IEnumerator Peek()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        while (true)
        {
            yield return new WaitForSeconds(6);
            RiseMole();
            Debug.Log("Mole visible");
            yield return new WaitForSeconds(6);
            HideMole();
            Debug.Log("Mole hidden");
        }
    }
}

