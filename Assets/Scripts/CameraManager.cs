using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sensibilidade = 100f;
    [SerializeField] private float limiteVertical = 80f;

    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -5f);
    [SerializeField] private float smoothness = 10f;
    [SerializeField] private float rotacaoX = 0f;
    [SerializeField] private float rotacaoY = 0f;
    [SerializeField] private bool isMousePointing;

    void LateUpdate()
    {
        if (playerTransform == null) return;

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            isMousePointing = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isMousePointing = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isMousePointing)
        {
            Vector3 delta = Mouse.current.delta.ReadValue();
            rotacaoY += delta.x * sensibilidade * Time.deltaTime;
            rotacaoX -= delta.y * sensibilidade * Time.deltaTime;
            rotacaoX = Mathf.Clamp(rotacaoX, -limiteVertical, limiteVertical);
        }

        Quaternion finalRotate = Quaternion.Euler(rotacaoX, rotacaoY, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, rotacaoY, 0f);
        Vector3 pos = playerTransform.position + finalRotate * offset;

        transform.position = Vector3.Lerp(transform.position, pos, smoothness * Time.deltaTime);

        transform.LookAt(playerTransform.position + Vector3.up * 1.5f);
    }
}
