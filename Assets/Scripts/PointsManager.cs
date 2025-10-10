using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    public int points;

    void Update()
    {
        Text.text = $"Pontos: {points}";
    }
}
