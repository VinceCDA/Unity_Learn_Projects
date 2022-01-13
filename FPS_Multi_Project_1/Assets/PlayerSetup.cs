using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] _componentsToDisable;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < _componentsToDisable.Length; i++)
            {
                _componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            _camera = Camera.main;
            if (_camera != null)
            {
                _camera.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        if (_camera != null)
        {
            _camera.gameObject.SetActive(true);
        }
    }
}
