using System.Collections;
using System.Collections.Generic;
using RuntimeSets;
using UnityEngine;

public class LaserManager : MonoBehaviour {
    [SerializeField] private LaserRuntimeSet _laserRuntimeSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        _laserRuntimeSet.UpdateLasers();
    }
}
