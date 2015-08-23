using UnityEngine;
using System.Collections;

public class Arme : MonoBehaviour {


    #region Class

    private class TransformVector
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public TransformVector()
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }

        public TransformVector(Vector3 position,Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Transform GetTransform(Transform trans)
        {
            
            trans.position = Position;
            trans.rotation = Rotation;
            return trans;
        }

        public TransformVector Add(TransformVector transformVector)
        {
            return new TransformVector(Position + transformVector.Position, Rotation * transformVector.Rotation);

        }
        public TransformVector Sub(TransformVector transformVector)
        {
            return new TransformVector(Position - transformVector.Position, Rotation * Quaternion.Inverse(transformVector.Rotation));
        }

        public TransformVector Mult(float coeff)
        {
            Position *= coeff;
            Rotation.x *= coeff;
            Rotation.y *= coeff;
            Rotation.z *= coeff;
            Rotation.w *= coeff;
            return this;
        }
    }

    private class TransformCurve
    {
        AnimationCurve _posX;
        AnimationCurve _posY;
        AnimationCurve _posZ;

        AnimationCurve _rotX;
        AnimationCurve _rotY;
        AnimationCurve _rotZ;
        AnimationCurve _rotW;

        private bool _isLocal;

        public TransformCurve(TransformVector initialTransform, float initialTime, TransformVector outTangent, TransformVector destinationTransform, float destinationTime, TransformVector inTangent,bool isLocal = true)
        {
            _isLocal = isLocal;
            Keyframe initPosXKey = new Keyframe(initialTime, initialTransform.Position.x, 0, outTangent.Position.x);
            Keyframe initPosYKey = new Keyframe(initialTime, initialTransform.Position.y, 0, outTangent.Position.y);
            Keyframe initPosZKey = new Keyframe(initialTime, initialTransform.Position.z, 0, outTangent.Position.z);

            Keyframe destPosXKey = new Keyframe(destinationTime, destinationTransform.Position.x, inTangent.Position.x, 0);
            Keyframe destPosYKey = new Keyframe(destinationTime, destinationTransform.Position.y, inTangent.Position.y, 0);
            Keyframe destPosZKey = new Keyframe(destinationTime, destinationTransform.Position.z, inTangent.Position.z, 0);

            _posX = new AnimationCurve();
            _posY = new AnimationCurve();
            _posZ = new AnimationCurve();

            _posX.AddKey(initPosXKey);
            _posX.AddKey(destPosXKey);
            _posY.AddKey(initPosYKey);
            _posY.AddKey(destPosYKey);
            _posZ.AddKey(initPosZKey);
            _posZ.AddKey(destPosZKey);

            Keyframe initRotXKey = new Keyframe(initialTime, initialTransform.Rotation.x, 0, outTangent.Rotation.x);
            Keyframe initRotYKey = new Keyframe(initialTime, initialTransform.Rotation.y, 0, outTangent.Rotation.y);
            Keyframe initRotZKey = new Keyframe(initialTime, initialTransform.Rotation.z, 0, outTangent.Rotation.z);
            Keyframe initRotWKey = new Keyframe(initialTime, initialTransform.Rotation.w, 0, outTangent.Rotation.w);

            Keyframe destRotXKey = new Keyframe(destinationTime, destinationTransform.Rotation.x, inTangent.Rotation.x, 0);
            Keyframe destRotYKey = new Keyframe(destinationTime, destinationTransform.Rotation.y, inTangent.Rotation.y, 0);
            Keyframe destRotZKey = new Keyframe(destinationTime, destinationTransform.Rotation.z, inTangent.Rotation.z, 0);
            Keyframe destRotWKey = new Keyframe(destinationTime, destinationTransform.Rotation.w, inTangent.Rotation.w, 0);

            _rotX = new AnimationCurve();
            _rotY = new AnimationCurve();
            _rotZ = new AnimationCurve();
            _rotW = new AnimationCurve();

            _rotX.AddKey(initRotXKey);
            _rotX.AddKey(destRotXKey);
            _rotY.AddKey(initRotYKey);
            _rotY.AddKey(destRotYKey);
            _rotZ.AddKey(initRotZKey);
            _rotZ.AddKey(destRotZKey);
            _rotW.AddKey(initRotWKey);
            _rotW.AddKey(destRotWKey);

        }

        public void SetTransform(Transform trans, float time)
        {
            if (_isLocal)
            {
                trans.localPosition = new Vector3(_posX.Evaluate(time), _posY.Evaluate(time), _posZ.Evaluate(time));
                trans.localRotation = new Quaternion(_rotX.Evaluate(time), _rotY.Evaluate(time), _rotZ.Evaluate(time),
                    _rotW.Evaluate(time));
            }
            else
            {
                trans.position = new Vector3(_posX.Evaluate(time), _posY.Evaluate(time), _posZ.Evaluate(time));
                trans.rotation = new Quaternion(_rotX.Evaluate(time), _rotY.Evaluate(time), _rotZ.Evaluate(time),
                    _rotW.Evaluate(time));
            }
        }
        public TransformVector GetTransform(float time)
        {
            TransformVector trans = new TransformVector(
                new Vector3(_posX.Evaluate(time), _posY.Evaluate(time), _posZ.Evaluate(time)),
                new Quaternion(_rotX.Evaluate(time), _rotY.Evaluate(time), _rotZ.Evaluate(time), _rotW.Evaluate(time)));
            return trans;
        }

        public TransformVector GetSpeed(float time)
        {
            TransformVector precVect = GetTransform(Time.deltaTime);
            TransformVector vect = GetTransform(time);

            return vect.Sub(precVect);
        }
    }
    #endregion

    #region Properties
    /// <summary>
    /// transform de la cible où frapper [armePivot]
    /// </summary>
    [SerializeField]
    Transform _normalTarget;
    /// <summary>
    /// transform de la cible où frapper [armePivot]
    /// </summary>
    [SerializeField]
    protected float _timeAttack = 1;
    /// <summary>
    /// temps après avoir toucher le sol avant de pouvoir réattaquer
    /// </summary>
    [SerializeField] 
    protected float _cooldown = 0.2f;

    [SerializeField] protected GameObject _particulePrefab;

    [SerializeField] protected Color CibleColor=Color.green;

    [SerializeField]
    protected AudioClip _sonKill;

    [SerializeField]
    protected AudioClip _sonNoKill;

    #endregion

    #region API
    /// <summary>
    /// déclanche l'animation d'attaque.
    /// </summary>
    public virtual void Attack()
    {
        if (_isAttacking || _lastAttackTime + _cooldown >Time.time)
            return;

        Debug.Log("LastAttack : " + _lastAttackTime);

        foreach (var colli in _colliders)
        {
            colli.enabled = true;
        }
        TransformVector init = new TransformVector(transform.position,transform.rotation);
        TransformVector dest = new TransformVector(_normalTarget.position, _normalTarget.rotation);
        _transformCurve = new TransformCurve(init, Time.time, dest.Sub(init).Mult(-1).Sub(_transformCurve.GetSpeed(Time.time)), dest, Time.time + _timeAttack, dest.Sub(init).Mult(10),false);
        _isAttacking = true;
        TempCible = Instantiate(_normalTarget.gameObject);
        TempCible.transform.position = _normalTarget.position;
        TempCible.transform.rotation = _normalTarget.rotation;
        TempCible.GetComponentInChildren<SpriteRenderer>().color = CibleColor; 
        TempCible.transform.SetParent(null,true);
        Destroy(TempCible,_timeAttack);
    }


    
    #endregion

    #region Unity
    // Use this for initialization
    protected virtual void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _colliders = GetComponentsInChildren<Collider>();
        _zeroTransformVector = new TransformVector();
        _initialTransformVector = new TransformVector(transform.localPosition,transform.localRotation);
        _transformCurve = new TransformCurve(_initialTransformVector, Time.time, _zeroTransformVector, _initialTransformVector, Time.time, _zeroTransformVector);
        BugManager = FindObjectOfType<BugManager>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _transformCurve.SetTransform(transform,Time.time);
        
    }

    #endregion

    #region Private

    private Collider[] _colliders;
    private static TransformVector _zeroTransformVector = new TransformVector();
    private TransformVector _initialTransformVector;
    private TransformCurve _transformCurve;
    protected bool _isAttacking = false;
    protected float _lastAttackTime = 0;
    protected GameObject TempCible;
    protected BugManager BugManager;
    protected AudioSource _AudioSource;
    protected int _nbKill = 0;
    protected virtual void _StopAttack()
    {
        if (_isAttacking)
        {
            foreach (var colli in _colliders)
            {
                colli.enabled = false;
            }
            _transformCurve = new TransformCurve(new TransformVector(transform.localPosition, transform.localRotation),
                Time.time, _zeroTransformVector, _initialTransformVector, Time.time + _timeAttack, _zeroTransformVector);
            _lastAttackTime = Time.time;
        }
        _isAttacking = false;
        _nbKill = 0;
    }
    #endregion
}
