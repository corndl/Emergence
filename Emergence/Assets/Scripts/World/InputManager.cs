using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    #region Properties
    /// <summary>
    /// est le script du joueur (camera)
    /// </summary>
    [SerializeField]
    private CameraController _Player;
    /// <summary>
    /// script arme
    /// </summary>
    [SerializeField]
    private Arme _Arme1;
    /// <summary>
    /// script arme
    /// </summary>
    [SerializeField]
    private Arme _Arme2;
    #endregion

    #region API
    #endregion

    #region Unity
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _HandleKey();
    }
    #endregion

    #region Private

    void _HandleKey()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 axisInclinaison = new Vector2(Input.GetAxis("VerticalInclinaison"),-Input.GetAxis("HorizontalInclinaison"));
        Vector2 axisIncline = new Vector2(Input.GetAxis("VerticalInclinaisonAxis"),-Input.GetAxis("HorizontalInclinaisonAxis"));
        _Player.Move(axis);
        if(axisIncline.magnitude>0.1)
        _Player.Incline(45 * axisIncline);
        _Player.Rotate(axisInclinaison);
        if(Input.GetButton("Fire1")||Input.GetAxis("Fire1")>0)
            _Arme1.Attack();
        if(Input.GetButton("Fire2")||Input.GetAxis("Fire2")>0)
            _Arme2.Attack();

    }
    #endregion
}
