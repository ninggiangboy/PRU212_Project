using UnityEngine;

namespace UI.Scripts.Sound
{
    public class GameplaySound : MonoBehaviour
    {
        [Header("----- Audio Source -------")]
        public AudioSource musicSource;
        public AudioSource SFXSource;

        [Header("----- Audio Clip -------")]
        public AudioClip[] background;

        public AudioClip AsaultRifles;
        public AudioClip Miniguns_loop;
        public AudioClip Pistol;
        public AudioClip Shotguns;
        public AudioClip SniperRifles;
        public AudioClip ZExtrem;


        // Start is called before the first frame update
        void Start()
        {
            System.Random rand = new System.Random();
            musicSource.clip = background[rand.Next(background.Length)];
            musicSource.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySFX(AudioClip clip)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
