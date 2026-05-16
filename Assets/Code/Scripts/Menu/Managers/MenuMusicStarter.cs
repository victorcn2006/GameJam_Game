using UnityEngine;

public class MenuMusicStarter : MonoBehaviour
{
    [SerializeField] private AudioClip _menuMusic;

    void Start()
    {
        if (_menuMusic != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(_menuMusic);
        }
    }
}
