using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _volumeChangeTime;

    private float _targetVolume;

    private event Action ChangedVolumeToZero;
    
    private void Awake()
    {
        _audioSource.volume = _minVolume;
        _targetVolume = _audioSource.volume;

        ChangedVolumeToZero += StopPlayed;
    }

    private void OnDisable()
    {
        ChangedVolumeToZero -= StopPlayed;
    }

    public void SoundOn()
    {
        _targetVolume = _maxVolume;
        _audioSource.Play();
        StartCoroutine(ChangeVolume());
    }
    
    public void SoundOff()
    {
        _targetVolume = _minVolume;
        StartCoroutine(ChangeVolume());
    }
    
    private IEnumerator ChangeVolume()
    {
        while (_audioSource.volume != _targetVolume)
        {
            _audioSource.volume = 
                Mathf.MoveTowards(_audioSource.volume, _targetVolume, Time.deltaTime/_volumeChangeTime );
            
            yield return null;
        }
        
        if(_audioSource.volume == 0)
            ChangedVolumeToZero?.Invoke();
    }

    private void StopPlayed() => _audioSource.Stop();
}