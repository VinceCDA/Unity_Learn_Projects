using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Vector3 _velocite;

    [SerializeField]
    private Vector3 _rotation;

    [SerializeField]
    private Vector3 _rotationCamera;


    private Rigidbody _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        EffectuerDeplacer();
        EffectuerTourner();
    }
    public void Deplacer(Vector3 velocite)
    {
        _velocite = velocite;
    }
    public void Tourner(Vector3 rotation)
    {
        _rotation = rotation;
    }
    public void TournerCamera(Vector3 rotationCamera)
    {
        _rotationCamera = rotationCamera;
    }
    private void EffectuerDeplacer()
    {
        if (_velocite != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocite * Time.deltaTime);
        }
    }
    private void EffectuerTourner()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
        _camera.transform.Rotate(-_rotationCamera);
    }
}
