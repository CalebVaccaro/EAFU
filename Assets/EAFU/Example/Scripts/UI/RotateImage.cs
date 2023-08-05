using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private RectTransform imageTransform;
    [SerializeField] private float rotationSpeed = 50f;

    private void Update()
    {
        imageTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
