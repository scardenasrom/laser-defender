using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

    public AudioClip startMenuClip;
    public AudioClip gameClip;
    public AudioClip endScreenClip;

    private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            music.clip = ChooseAudioClipBasedOnLevelIndex(currentSceneIndex);
            music.volume = ChooseVolumeBasedOnLevelIndex(currentSceneIndex);
            music.Play();
		}
	}

    AudioClip ChooseAudioClipBasedOnLevelIndex(int levelIndex) {
        switch (levelIndex) {
            case 0:
                return startMenuClip;
            case 1:
                return gameClip;
            case 2:
            default:
                return endScreenClip;
        }
    }

    float ChooseVolumeBasedOnLevelIndex(int levelIndex) {
        switch (levelIndex) {
            case 0:
                return 0.1f;
            case 1:
                return 0.05f;
            case 2:
            default:
                return 0.1f;
        }
    }

    void OnLevelWasLoaded(int level) {
        music.Stop();
        music.clip = ChooseAudioClipBasedOnLevelIndex(level);
        music.volume = ChooseVolumeBasedOnLevelIndex(level);
        music.Play();
    }

}
