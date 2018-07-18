using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour {

    public GameObject sphereGo;
    public Material mt;
    public AudioClip[] audioClip;

    private bool lockOne = true;
	
	void Start () {

        // Debug.Log (sphereGo.GetComponent<MeshRenderer> ().material);
        // mt = sphereGo.GetComponent<MeshRenderer> ().material;
        // mt.SetFloat ("_alphaIntensity",2);
       
      

    }
	

	void Update () {

        if (Input.GetKeyDown (KeyCode.Alpha1)) {

            OnUnfrozen ();
        }

        if (Input.GetKeyDown (KeyCode.Alpha2)) {

            OnFrozen ();
        }

    }


    void OnFrozen () {

        StopAllCoroutines ();
        StartCoroutine (RunAction (true));
        if (mt.GetFloat ("_alphaIntensity") < 10) {
            this.gameObject.GetComponent<AudioSource> ().PlayOneShot (audioClip[0]);
        }

    }

    void OnUnfrozen () {

        StopAllCoroutines ();
        StartCoroutine (RunAction (false));
       
      
    }

    IEnumerator RunAction (bool status) {

        lockOne = true;
        float valueAlphaIntensity = mt.GetFloat ("_alphaIntensity");
        float valueFresnelPower = mt.GetFloat ("_fresnelPower");

        float addValueAlphaIntensity = 0.14f;
        float addValueFresnelPower = 0.035f;

        while (lockOne) {                    

            for (int i = 0 ; i < 120 ; i++) {

                if (status) {

                    valueAlphaIntensity += addValueAlphaIntensity;

                    if (i > 10) { 
                    valueFresnelPower -= addValueFresnelPower;
                    }
                }

                else {

                    valueFresnelPower += addValueFresnelPower;

                    if (i > 15) {
                                            
                        valueAlphaIntensity -= addValueAlphaIntensity;
                    }

                }

                mt.SetFloat ("_alphaIntensity",Mathf.Clamp (valueAlphaIntensity,0,10));
                mt.SetFloat ("_fresnelPower",Mathf.Clamp (valueFresnelPower,1.5f,4));

                yield return new WaitForSeconds (0.02f);
                


            }
            lockOne = false;
            Debug.Log ("Open Lock");
        }


        yield return 0;
    }



}
