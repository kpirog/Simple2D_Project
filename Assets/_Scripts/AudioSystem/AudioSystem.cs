using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    #region Global
    private static AudioSystem Instance;

    public static void PlaySFX_Global(AudioClip audioClip)
    {
        if (Instance == null)
            return;

        Instance.PlaySFX_Local(audioClip);
    }
    public static void PlayButtonSFX_Global()
    {
        if (Instance == null)
            return;

        Instance.PlaySFX_Local(Instance.buttonClip);
    }
    public static void SwitchMusic_Global(bool play)
    {
        if (Instance == null)
            return;

        if (!play)
        {
            Instance.musicSource.Pause();
        }
        else if (!Instance.musicSource.isPlaying)
        {
            Instance.musicSource.Play();
        }
    }

    #endregion

    #region Local

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip buttonClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void PlaySFX_Local(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    #endregion
}
