using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PissPuddleSpawner : MonoBehaviour
{



    [SerializeField]
    private Animator animator;

    Vector3 newPos;
    bool pissing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    bool posSet;
    public float fuck;
    // Update is called once per frame
    void Update()
    {
        pissing = animator.GetBool("Pissing");

        if (pissing)
        {
            //Debug.Log(f);
            setNewPos();
            if (Vector3.Distance(newPos, transform.position) <= fuck)
            {
                //new puddle
                f += Time.deltaTime;
                if (f >= 2)
                {
                    if (!spawned)
                    {
                      
                        Puddle();
                        PuddleScale = f - 2f;
                    }

                    PuddleScale += Time.deltaTime;
                    if (PuddleScale > 3) PuddleScale = 3;
                    if (g != null)
                    {

                        g.transform.localScale = new Vector3(PuddleScale, 0.05f, PuddleScale);
                    }

                }
            }
            else
            {
                spawned = false;
                PuddleScale = 0;
                f = 0;
                posSet = false;
              
                Debug.Log("Setting new pos");

            }
        }
        else
        {
            //        spawned = false;
        }
    }


    [SerializeField]
    GameObject piss;
    GameObject g;
    float PuddleScale;
    void Puddle()
    {
        g = Instantiate(piss, transform.position, Quaternion.identity);
        Debug.Log("New puddle");
        spawned = true;
    }
    bool spawned;
    float f;
    void setNewPos()
    {
        if (!posSet)
        {
            newPos = transform.position;
            posSet = true;
        }
    }
}
