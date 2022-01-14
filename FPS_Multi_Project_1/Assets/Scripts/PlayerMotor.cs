using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerMotor : MonoBehaviour
{
    #region Variables globales
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _angleCameraCourante = 0.0f;
    [SerializeField]
    private Vector3 _velocite;

    [SerializeField]
    private Vector3 _rotation;
    [SerializeField]
    private float _limiteRotationCamera = 85.0f;

    [SerializeField]
    private Vector3 _rotationCamera;

    [SerializeField]
    private Vector3 _velociteJetpack;

    [Header("Joint Option")]
    [SerializeField]
    private float _jointYDriveSpring = 20.0f;

    [SerializeField]
    private float _jointYDriveMax = 60.0f;

    private Rigidbody _rigidbody;
    private ConfigurableJoint _joint;
    private JointDrive _yJointDrive;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _joint = GetComponent<ConfigurableJoint>();
        _yJointDrive = _joint.yDrive;
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


    #region Recuperation des valeurs de PlayerController
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
    public void UtiliserJetpack(Vector3 forceJetpack)
    {
        _velociteJetpack = forceJetpack;
    }
    #endregion


    // Deplacement effectif du joueur
    private void EffectuerDeplacer()
    {
        if (_velocite != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocite * Time.deltaTime);
        }
        if (_velociteJetpack != Vector3.zero)
        {
            _rigidbody.AddForce(_velociteJetpack * Time.deltaTime, ForceMode.Acceleration);
            _yJointDrive.positionSpring = 0.0f;
            _joint.yDrive = _yJointDrive;
        }
        else
        {
            _yJointDrive.positionSpring = _jointYDriveSpring;
            _joint.yDrive = _yJointDrive;
        }
    }
    // Rotation effective du joueur
    private void EffectuerTourner()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
        RestreindreAngleCamera();
        _camera.transform.localEulerAngles = -_rotationCamera;

    }
    //Restriction de l'angle vertical du joueur
    private void RestreindreAngleCamera()
    {
        _angleCameraCourante += _rotationCamera.x;
        _angleCameraCourante = Mathf.Clamp(_angleCameraCourante, -_limiteRotationCamera, _limiteRotationCamera);
        _rotationCamera.x = _angleCameraCourante;
    }
}
