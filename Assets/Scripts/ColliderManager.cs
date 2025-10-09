using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PointsManager.instance.points++;
            gameObject.SetActive(false);
        }
    }
}
