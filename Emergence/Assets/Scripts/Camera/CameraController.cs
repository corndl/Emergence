using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEditor;

public class CameraController : MonoBehaviour {

    #region Properties
    /// <summary>
    /// gameObject contenant la camera
    /// </summary>
    [SerializeField] 
    GameObject _camera;
    /// <summary>
    /// hauteur de la camera par rapport au sol
    /// </summary>
    [SerializeField]
    float _hauteur = 20f;
    /// <summary>
    /// position actuel dans le plan 2D de la cible de la camera
    /// </summary>
    [SerializeField]
    Vector2 _position = new Vector2(25f,25f);
    /// <summary>
    /// position minimal de la camera dans l'espace
    /// </summary>
    [SerializeField]
    Vector2 _mapMin = new Vector2(10f,10f);
    /// <summary>
    /// position minimal de la camera dans l'espace
    /// </summary>
    [SerializeField]
    Vector2 _mapMax = new Vector2(50f, 50f);
    /// <summary>
    /// angle d'inclinaison maximal [x,z]
    /// </summary>
    [SerializeField]
    Vector2 _maxInclinaison = new Vector2(45f,45f);
    /// <summary>
    /// inclinaison de la camera [x,z]
    /// </summary>
    [SerializeField]
    Vector2 _inclinaison = new Vector2(-20f, 0f);

    #endregion

    #region API

    /// <summary>
    /// Déplace la camera à la position
    /// </summary>
    /// <param name="newPosition">[x,z]</param>
    public void SetPosition(Vector2 newPosition)
    {
        Vector2 tempPosition = 
            new Vector2(
                Mathf.Clamp(newPosition.x, _mapMin.x, _mapMax.x),
                Mathf.Clamp(newPosition.y, _mapMin.y, _mapMax.y));
        _position = tempPosition;
    }

    /// <summary>
    /// Déplace la camera de [x,z]
    /// </summary>
    /// <param name="offset">[x,z]</param>
    public void Move(Vector2 offset)
    {
        SetPosition(_position + offset);
    }

    /// <summary>
    /// Incline la camera selon les angle [x,z]
    /// </summary>
    /// <param name="newInclinaison">[x°,z°]</param>
    public void Incline(Vector2 newInclinaison)
    {
        Vector2 tempInclinaison =
            new Vector2(
                Mathf.Clamp(newInclinaison.x, -_maxInclinaison.x, _maxInclinaison.x),
                Mathf.Clamp(newInclinaison.y, -_maxInclinaison.y, _maxInclinaison.y));
        _inclinaison = tempInclinaison;
    }
    #endregion

    #region Unity
    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _Rotate();
	    _Move();
	}
    #endregion

    #region Private
    const float HIGH = 100f;


    /// <summary>
    /// place la camera dans la bonne position
    /// </summary>
    void _Move()
    {
        _camera.transform.localPosition = new Vector3(0,_hauteur,0);
        Vector3 repere = new Vector3(_position.x,HIGH,_position.y);
        Ray ray = new Ray(repere,Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = hit.point;
            transform.position = target;
        }
        else
        {
            Debug.Log("La camera ne peut pas se placer correctement.");
            Debug.Log("Aucun collider dans son champ de vision.");
        }
        
    }
    /// <summary>
    /// oriente la camera suivant l'inclinaison _inclinaison
    /// </summary>
    void _Rotate()
    {
        transform.rotation = Quaternion.Euler(_inclinaison.x, 0, _inclinaison.y);
    }

    #endregion
}
