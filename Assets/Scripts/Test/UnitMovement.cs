using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    #region Variables
    // Add your variables here

    [SerializeField] private GameObject _currentTarget;
    [SerializeField] private float _YOffset = 0.5f;

    [SerializeField] private Transform[] _waypoints;

    private Dictionary<GameObject, int> _unitPositions = new();
    private int[] _unitOnEachPoint;

    #endregion

    #region MonoBehaviour Lifecycle Methods

    // Called when the script is initialized
    private void Awake()
    {
        InitializeUnitOnPoints();

        if (_currentTarget)
        {
            _unitPositions[_currentTarget] = 0;
        }
    }

    private void InitializeUnitOnPoints()
    {
        _unitOnEachPoint = new int[_waypoints.Length];

        for (int i = 0; i < _unitOnEachPoint.Length; i++)
        {
            _unitOnEachPoint[i] = 0;
        }

        _unitOnEachPoint[0] = 3;
    }

    // Called when the script is initialized
    private void Start()
    {

    }

    // Called when the object is enabled
    private void OnEnable()
    {

    }

    // Called when the object is disabled
    private void OnDisable()
    {

    }

    // Called every frame
    private void Update()
    {

    }

    // Called on every physics update (Fixed timestep)
    private void FixedUpdate()
    {

    }

    // Called after all Update methods have been called
    private void LateUpdate()
    {

    }

    #endregion

    #region Collision Methods

    // Called when the collider enters another collider
    private void OnCollisionEnter(Collision collision)
    {

    }

    // Called when the collider stays in contact with another collider
    private void OnCollisionStay(Collision collision)
    {

    }

    // Called when the collider exits another collider
    private void OnCollisionExit(Collision collision)
    {

    }

    // Called when a trigger collider enters another collider
    private void OnTriggerEnter(Collider other)
    {

    }

    // Called when a trigger collider stays in contact with another collider
    private void OnTriggerStay(Collider other)
    {

    }

    // Called when a trigger collider exits another collider
    private void OnTriggerExit(Collider other)
    {

    }
    #endregion

    #region Custom Methods

    public void SetTarget(GameObject gameObject)
    {
        _currentTarget = gameObject;

        if (!_unitPositions.ContainsKey(gameObject))
        {
            _unitPositions[gameObject] = 0;
        }
    }

    public void MoveToPoint(int index)
    {
        if (index < 0)
        {
            Debug.LogError("Invalid index");
            return;
        }

        if (index > _waypoints.Length - 1)
        {
            index = 0;
        }

        _currentTarget.transform.position = _waypoints[index].position;

        // set the parent of the current target to the waypoint
        _currentTarget.transform.parent = _waypoints[index];

        _unitPositions[_currentTarget] = index;

        if (_unitOnEachPoint[index] > 0)
        {
            // apply an offset to the y position, so all of the sprites are visible
            _currentTarget.transform.position += new Vector3(0, _YOffset * _unitOnEachPoint[index], 0);
        }

        // add 1 unit to the current point
        _unitOnEachPoint[index]++;

        int previousIndex = 0;

        // remove 1 unit from the previous point
        if (index == 0)
        {
            previousIndex = _waypoints.Length - 1;
        }
        else
        {
            previousIndex = index - 1;
        }

        _unitOnEachPoint[previousIndex]--;

        // adjust positions of the other units

        int count = 0;

        foreach (Transform transform in _waypoints[previousIndex])
        {
            transform.position = _waypoints[previousIndex].position;
            transform.position += new Vector3(0f, _YOffset * count, 0f);
            count++;
        }
    }

    public void MoveToNextPoint()
    {
        MoveToPoint(_unitPositions[_currentTarget] + 1);
    }

    public void MoveToPreviousPoint()
    {
        MoveToPoint(_unitPositions[_currentTarget] - 1);
    }

    #endregion
}
