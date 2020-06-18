using UnityEngine;

//For the creation of the Laser
[RequireComponent(typeof(LineRenderer))]
public class Teleportation : MonoBehaviour
{
    [SerializeField]
    Transform _head, _cameraRig;

    [SerializeField]
    LineRenderer _laser;
    [SerializeField]
    int _laserSteps = 20;
    [SerializeField]
    float _laserSegmentDistance = 1f, _dropPerSegment = .1f;

    //Layer where we can teleport
    [SerializeField]
    int _targetLayer;

    private Vector3 _targetPos;
    private Vector3 _currentPos;

    bool _tpTarget;
    bool _tpCheck;

    private void Update()
    {
        if (_tpTarget == true)
        {
            RecordCurrentPos();
            StartDisplayRay();
        }
        else if (_tpTarget == false && _tpCheck)
        {
            ApplyTeleport();
            StopDisplayRay();
        }
    }

    //See TeleportInput for using
    public void StartTeleport()
    {
        _tpTarget = true;
        _tpCheck = false;
    }
    public void StopTeleport()
    {
        _tpTarget = false;
        _tpCheck = true;
    }


    private void RecordCurrentPos()
    {
        _currentPos = transform.position;
    }

    //Creation of the Ray
    private void StartDisplayRay()
    {
        _tpTarget = false;
        RaycastHit _hit;
        Vector3 _origine = _currentPos;
        _laser.SetPosition(0, _origine);

        for (int i = 0; i < _laserSteps - 1; i++)
        {
            Vector3 _offset = (transform.forward + (Vector3.down * _dropPerSegment * i)).normalized * _laserSegmentDistance;

            if (Physics.Raycast(_origine, _offset, out _hit, _laserSegmentDistance))
            {
                for (int j = i + 1; j < _laser.positionCount; j++)
                {
                    _laser.SetPosition(j, _hit.point);
                }

                //Check if the zone targeted is allowed
                if (_hit.transform.gameObject.layer == _targetLayer)
                {
                    //Before, Apply a material color in LineRenderer
                    _laser.startColor = _laser.endColor = Color.green;
                    _targetPos = _hit.point;
                    _tpTarget = true;
                    return;
                }
                else
                {
                    _laser.startColor = _laser.endColor = Color.red;
                    return;
                }
            }
            else
            {
                _laser.SetPosition(1 + i, _origine + _offset);
                _origine += _offset;
            }
        }
        _laser.startColor = _laser.endColor = Color.red;
    }

    private void ApplyTeleport()
    {
        _tpCheck = false;
        
        //Decommente this ligne and delete the next when using VR
        //Vector3 _offset = new Vector3(_targetPos.x - _head.transform.position.x, _targetPos.y - _cameraRig.position.y, _targetPos.z - _head.transform.position.z);
        Vector3 _offset = new Vector3(0,0,2);

        _head.position += _offset;
    }

    //Reset the Ray
    private void StopDisplayRay()
    {
        for (int i = 0; i < _laser.positionCount; i++)
        {
            _laser.SetPosition(i, Vector3.zero);
        }
    }
}