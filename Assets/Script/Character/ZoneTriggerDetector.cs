using UnityEngine;

public class ZoneTriggerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask = default;
    [Header("Event Emitter")]
    [SerializeField] private TriggerDetectEvent  _triggerDetectEvent  = default;

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger = " +other.gameObject);
        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            Debug.Log("enter trigger = " + other.gameObject);
            _triggerDetectEvent.RaiseEvent(true, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            _triggerDetectEvent.RaiseEvent(false, other.gameObject);
        }
    }
}
