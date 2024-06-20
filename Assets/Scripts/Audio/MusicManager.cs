using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour 
{
    public static MusicManager Instance { get; private set; }
    
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip dungeonMusic;
    [SerializeField] private AudioClip exteriorMusic;
    [SerializeField] private AudioClip houseMusic;
    [SerializeField] private AudioClip preBossMusic;
    [SerializeField] private AudioClip bossMusic;
    
    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        ChangeScene.OnSceneChange += ChangeMusic;
        MainMenu.OnPLayMusic += PlayMusic;
    }
    
    private void OnDisable()
    {
        ChangeScene.OnSceneChange -= ChangeMusic;
        MainMenu.OnPLayMusic -= PlayMusic;
    }

    private void Start() 
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        PlayMusic(sceneIndex);
    }
    
    private void ChangeMusic(int actualSceneIndex, int nextSceneIndex)
    {
        AudioClip currentMusic = GetComponent<AudioSource>().clip;
        
        AudioClip nextMusic = nextSceneIndex switch
        {
            0 => menuMusic,
            1 => dungeonMusic,
            2 => dungeonMusic,
            3 => dungeonMusic,
            4 => exteriorMusic,
            5 => houseMusic,
            6 => preBossMusic,
            7 => bossMusic,
            _ => null
        };

        if (currentMusic != nextMusic)
        {
            StartCoroutine(FadeOutAndIn(nextMusic));
        }
    }
    
    private IEnumerator FadeOutAndIn(AudioClip nextMusic)
    {
        float t = 0f;
        AudioSource audioSource = GetComponent<AudioSource>();
        while (t < 1f)
        {
            t += Time.deltaTime;
            audioSource.volume = 1 - t;
            yield return null;
        }
        audioSource.clip = nextMusic;
        audioSource.Play();
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            audioSource.volume = t;
            yield return null;
        }
    }
    
    public void PlayMusic(int sceneIndex)
    {
        AudioClip music = sceneIndex switch
        {
            0 => menuMusic,
            1 => dungeonMusic,
            2 => dungeonMusic,
            3 => dungeonMusic,
            4 => exteriorMusic,
            5 => houseMusic,
            6 => preBossMusic,
            7 => bossMusic,
            _ => null
        };
        
        StartCoroutine(FadeOutAndIn(music));
    }
}
