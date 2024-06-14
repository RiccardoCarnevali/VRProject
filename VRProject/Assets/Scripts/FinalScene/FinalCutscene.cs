using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] private AutoSave _autoSave;
    [SerializeField] private AudioClip _sirenAudioClip;
    [SerializeField] private AudioClip _sheldonScooperAudioClip;
    [SerializeField] private Dialogue _firstDialogue;
    [SerializeField] private Dialogue _secondDialogue;
    [SerializeField] private SheldonScooper _sheldonScooper;
    [SerializeField] private GameObject _scoopingRoomCamera;
    [SerializeField] private RawImage _bloodSplatters;

    private bool _triggeredFinalScene = false;
    private AudioFade _audioFade;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioFade = GetComponent<AudioFade>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_triggeredFinalScene || !collider.TryGetComponent(out CharacterController controller)) { return; }
        collider.gameObject.SetActive(false);
        _scoopingRoomCamera.SetActive(true);
        _triggeredFinalScene = true;
        Settings.inCutscene = true;
        _autoSave.enabled = false;

        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Messenger<Action, float, float>.Broadcast(MessageEvents.FADE_IN, () => StartCoroutine(FinalRoomScene()), 4f, 2f);
    }


    private IEnumerator FinalRoomScene()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(_audioFade.FadeIn());
        _audioSource.Play();            // chiama sfx applausi
        yield return new WaitForSeconds(1f); // aspetta un altro po'
        DialogueManager.Instance().StartDialogue(_firstDialogue);
        yield return new WaitUntil(() => !Settings.dialogue);
        _audioSource.loop = false;
        _audioSource.Stop();
        yield return new WaitForSeconds(3f);
        DialogueManager.Instance().StartDialogue(_secondDialogue);
        yield return new WaitUntil(() => !Settings.dialogue);
        _audioSource.PlayOneShot(_sirenAudioClip);
        yield return new WaitForSeconds(_sirenAudioClip.length + 4f);
        _audioSource.PlayOneShot(_sheldonScooperAudioClip, 1f);
        _sheldonScooper.Scoop();
        yield return new WaitForSeconds(0.9f);
        _bloodSplatters.enabled = true;
        Messenger<Action, float, float>.Broadcast(MessageEvents.FADE_OUT, BackToMainMenu, 2f, 2f);

    }

    private void BackToMainMenu()
    {
        _bloodSplatters.enabled = false;
        Settings.inCutscene = false;
        SceneManager.LoadScene("MainMenu");
    }
}
