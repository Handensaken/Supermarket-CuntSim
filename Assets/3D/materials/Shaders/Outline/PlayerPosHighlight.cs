using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosHighlight : MonoBehaviour
{
    [SerializeField]
    private Material mat;

    [SerializeField]
    [Tooltip("Default: 15")]
    private float _outLineCutoffRange = 15;

    // Start is called before the first frame update
    void Awake()
    {
        mat.SetFloat("_CutoffRange", _outLineCutoffRange);
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetVector("_PlayerPos", transform.position);
    }
}
