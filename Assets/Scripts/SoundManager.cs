using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    //TODO: ������� ���, ����� ������������� ������� �� �����
    // ������� ����������� ��������� ����� �� ��������� ����� ��������� �����
    // ����������� ����������� �� ����. ������������� �������� �� ��������, ����� ������� [Inject] SoundManager _soundManager;

    
    [SerializeField] private AudioSource _musicSource, _soundSource;
    [SerializeField] private AudioClip _currentMusicClip;
    [SerializeField] private AudioClip[] _soundClips;


    private void Start() {
        _musicSource.clip = _currentMusicClip;
        _musicSource.volume = 0.8f;
        _musicSource.loop = true;
        _musicSource.Play();
    }


    public void PlaySound(int id) {
        _soundSource.clip = _soundClips[id];
        _soundSource.Play();
    }

}
