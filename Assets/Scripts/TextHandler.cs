using CrazyMinnow.SALSA;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextHandler : MonoBehaviour {
    public string username, password, workspaceID, versionDate, url, messageText;
    public GameObject person;
    TextToSpeech _textToSpeech;

    void Start()
    {
        Credentials credentials = new Credentials(username, password, url);
        _textToSpeech = new TextToSpeech(credentials);
        person = transform.parent.gameObject;

    }

    private void OnFail(IBM.Watson.DeveloperCloud.Connection.RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleTextToSpeech.OnFail()", "Error received: {0}", error.ToString());
    }

    // Set voice type according to current scene/avatar
    public void Synthesize()
    {
        if (SceneManager.GetActiveScene().name == "Abigail")
        {
            _textToSpeech.Voice = VoiceType.en_US_Allison;
        }
        if (SceneManager.GetActiveScene().name == "Britney")
        {
            _textToSpeech.Voice = VoiceType.en_US_Lisa;
        }
        if (SceneManager.GetActiveScene().name == "Cindy")
        {
            _textToSpeech.Voice = VoiceType.en_GB_Kate;
        }
        _textToSpeech.ToSpeech(OnSynthesize, OnFail, messageText, false);
    }

    public void OnSynthesize(AudioClip clip, Dictionary<string, object> customData)
    {
        StartCoroutine(PlayClip(clip));
    }

    // Play audio clip on avatar and allow for mouth movement according to audio track
    public IEnumerator PlayClip(AudioClip clip)
    {
        GetComponent<SpeechHandler>().StopRecording();
        GetComponent<SpeechHandler>().Active = false;
        person.GetComponent<Salsa3D>().SetAudioClip(clip);
        person.GetComponent<Salsa3D>().Play();
        yield return new WaitUntil(() => !person.GetComponent<AudioSource>().isPlaying);
        GetComponent<SpeechHandler>().Active = true;
        GetComponent<SpeechHandler>().StartRecording();
    }
}
