using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Vacuum : MonoBehaviour {
    public Animator _animator;
    public bool isCurrentlyActive = false;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject vacuumBody;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update() {
        if (isCurrentlyActive) {
            player.GetComponent<Rigidbody>().AddForce((vacuumBody.transform.position - player.transform.position) * 3);
        }
    }
}
