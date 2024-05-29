using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EswitchMerah : MonoBehaviour
{
    [SerializeField] PintuMerah _doorBehavior;

    [SerializeField] bool _isDoorOpenSwitch;
    [SerializeField] bool _isDoorCloseSwitch;

    float _switchSizeY;
    Vector3 _switchUpPos;
    Vector3 _switchDownPos;
    float _switchSpeed = 1f;
    float _switchDelay = 0.2f;
    bool _isPressingSwitch = false;
    bool _playerNearby = false;

    void Awake()
    {
        _switchSizeY = transform.localScale.y / 2;

        _switchUpPos = transform.position;
        _switchDownPos = new Vector3(transform.position.x, transform.position.y - _switchSizeY, transform.position.z);
    }

    void Update()
    {
        if (_isPressingSwitch)
        {
            MoveSwitchDown();
        }
        else
        {
            MoveSwitchUp();
        }

        if (_playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleSwitch();
        }
    }

    void MoveSwitchDown()
    {
        if (transform.position != _switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchDownPos, _switchSpeed * Time.deltaTime);
        }
    }

    void MoveSwitchUp()
    {
        if (transform.position != _switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchUpPos, _switchSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerNearby = false;
            StartCoroutine(SwitchUpDelay(_switchDelay));
        }
    }

    IEnumerator SwitchUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isPressingSwitch = false;
    }

    void ToggleSwitch()
    {
        _isPressingSwitch = !_isPressingSwitch;

        Debug.Log("Toggling switch. _isDoorOpenSwitch: " + _isDoorOpenSwitch + ", _isDoorCloseSwitch: " + _isDoorCloseSwitch + ", _doorBehavior._isDoorOpen: " + _doorBehavior._isDoorOpen);

        if (_isDoorOpenSwitch && !_doorBehavior._isDoorOpen)
        {
            _doorBehavior._isDoorOpen = true;
        }
        else if (_isDoorCloseSwitch && _doorBehavior._isDoorOpen)
        {
            _doorBehavior._isDoorOpen = false;
        }
    }
}
