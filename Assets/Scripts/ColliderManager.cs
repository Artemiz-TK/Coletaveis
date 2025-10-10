using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pointsManager.points++;
            gameObject.SetActive(false);
        }
    }
}
