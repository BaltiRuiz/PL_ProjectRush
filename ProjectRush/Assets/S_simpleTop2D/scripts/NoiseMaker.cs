
using UnityEngine;

public class NoiseMaker : MonoBehaviour {

    private AudioSource audioSource;

    private bool ringing;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        ringing = false;
	}
	
	void Update ()
    {
        if (Settings.getPause() && audioSource.isPlaying)
        {
            audioSource.Pause();

            ringing = false;
        }

        else if (!Settings.getPause())
        {
            audioSource.volume = Settings.getSoundEffects();

            if (this.GetComponent<Action>().CurrentAction == (int)ActionIs.Ring && !ringing)
            {
                audioSource.Play();

                ringing = true;
            }

            if (this.GetComponent<AgentReactor>().getLife() <= 0)
            {
                this.GetComponent<Action>().CurrentAction = (int)ActionIs.Stop;

                this.gameObject.layer = 0;

                audioSource.Stop();

                ringing = false;
            }
        }
	}
}