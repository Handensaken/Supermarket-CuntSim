using UnityEngine;

public class MaxCamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private Vector3 _offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _offset = new Vector3(0, 50, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTransform != null)
        {
            transform.position = _playerTransform.position + _offset;
        }
    }
}
