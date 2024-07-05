using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _soundManager.SoundOn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _soundManager.SoundOff();
    }
}
