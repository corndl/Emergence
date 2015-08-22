using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    #region Properties
    /// <summary>
    /// est le script du joueur (camera)
    /// </summary>
    [SerializeField] private CameraController _Player;
    /// <summary>
    /// script arme
    /// </summary>
    [SerializeField] private Arme _Arme;
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
        _Player.Move(axis);
        _Player.Rotate(axisInclinaison);
        if(Input.GetButtonDown("Fire1"))
            _Arme.Attack();
    }
    #endregion
}
