using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    #region Variables globales
    [SerializeField]
    private float _vitesse = 3.0f;

    [SerializeField]
    private float _sensibiliteSourisX = 3.0f;

    [SerializeField]
    private float _sensibiliteSourisY = 3.0f;

    [SerializeField]
    private float _puissanceJetpack = 1000.0f;

    private PlayerMotor _motor;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculVelocite();
        CalculRotationHorizontal();
        CalculRotationVertical();
        CalculForceJetPack();
    }

    // Calcul la vélocité du joueur
    private void CalculVelocite()
    {
        
        float xDeplacement = Input.GetAxisRaw("Horizontal");
        float yDeplacement = Input.GetAxisRaw("Vertical");

        Vector3 deplacementHorizontal = transform.right * xDeplacement;
        Vector3 deplacementVertical = transform.forward * yDeplacement;

        Vector3 velocite = (deplacementVertical + deplacementHorizontal).normalized * _vitesse;
        _motor.Deplacer(velocite);

    }
    // Calcul rotation horizontal du joueur
    private void CalculRotationHorizontal()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
       

        Vector3 rotation = new Vector3(0, yRotation, 0) * _sensibiliteSourisX;

        _motor.Tourner(rotation);
    }
    // Calcul rotation vertical de la camera
    private void CalculRotationVertical()
    {
        
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = new Vector3(xRotation, 0, 0) * _sensibiliteSourisY;

        _motor.TournerCamera(rotation);
    }
    // Calcul de la force du Jetpack
    private void CalculForceJetPack()
    {
        Vector3 forceJetpack = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            forceJetpack = Vector3.up * _puissanceJetpack;
        }
        _motor.UtiliserJetpack(forceJetpack);
    }
}
