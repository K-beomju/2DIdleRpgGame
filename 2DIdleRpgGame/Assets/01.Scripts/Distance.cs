using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour

{
     public GameObject Player;
    public GameObject Enemy;
    private float Dist;

    // Update is called once per frame
    void Update () {
        Dist = Vector3.Distance(Player.transform.position, Enemy.transform.position);
    }

    void LateUpdate()
    {
        print("Dist : " + Dist);
    }
}



