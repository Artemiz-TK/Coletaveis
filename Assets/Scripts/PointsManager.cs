using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;
    [SerializeField] private TextMeshProUGUI Text;
    public int points;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Text.text = $"Pontos: {points}";
    }
}
