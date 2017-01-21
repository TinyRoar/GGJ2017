using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepWater : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponent<Player>().TrySetValueToSkill<SlowerInDeepWater>(1);
    }
    void OnTriggerExit(Collider other)
    {
        other.transform.parent.GetComponent<Player>().TrySetValueToSkill<SlowerInDeepWater>(0);
    }

}
