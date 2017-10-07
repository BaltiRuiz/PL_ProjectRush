
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Character : MonoBehaviour {

    private AudioSource audioSource;

    private int noiseMaker;

    private GameObject noiseMakerInstance;

    public GameObject noiseMakerPrefab;

    private bool onSwitch;

    public AudioClip switchOnOffClip;

    private GameObject onSwitchInstance;

    private bool hasKey;

    public AudioClip keyPickupClip;

    private bool paused;

    private bool dead;

    public GameObject canvasPause;

    public GameObject canvasYouDied;

    public GameObject canvasYouWon;

    public GameObject canvasGroup;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        noiseMaker = 0;

        onSwitch = false;

        hasKey = false;

        paused = false;

        dead = false;

        this.GetComponent<ControllerTopZX>().enabled = true;
	}

    IEnumerator EndLevel()
    {
        while (true)
        {
            canvasYouWon.SetActive(true);

            this.GetComponent<ControllerTopZX>().enabled = false;

            yield return new WaitForSeconds(3.0f);

            SceneManager.LoadScene(0);
        }
    }

    IEnumerator Die()
    {
        while (true)
        {
            canvasYouDied.SetActive(true);

            this.GetComponent<ControllerTopZX>().enabled = false;

            yield return new WaitForSeconds(3.0f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
	
	void Update ()
    {
        if (!dead)
        {
            if (!paused)
            {
                if (this.GetComponent<AgentReactor>().getLife() == 0)
                {
                    StartCoroutine(Die());

                    dead = true;
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    paused = true;

                    canvasGroup.SetActive(true);

                    canvasPause.SetActive(true);

                    Time.timeScale = 0;

                    Settings.setPause(true);
                }

                if (onSwitch && Input.GetKeyDown(KeyCode.E))
                {
                    audioSource.PlayOneShot(switchOnOffClip, Settings.getSoundEffects());

                    onSwitchInstance.GetComponent<Switch>().spotLight1.enabled = !onSwitchInstance.GetComponent<Switch>().spotLight1.enabled;

                    onSwitchInstance.GetComponent<Switch>().spotLight2.enabled = !onSwitchInstance.GetComponent<Switch>().spotLight2.enabled;
                }

                if (noiseMaker == 1 && Input.GetKeyDown(KeyCode.Q))
                {
                    noiseMakerInstance = Instantiate(noiseMakerPrefab, transform.position + transform.forward, transform.rotation);

                    noiseMakerInstance.name = "NoiseMakerInstance";

                    noiseMaker = 2;
                }

                else if (noiseMaker == 2 && Input.GetKeyDown(KeyCode.Q))
                {
                    noiseMakerInstance.GetComponent<Action>().CurrentAction = (int)ActionIs.Ring;

                    noiseMaker = 0;
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    paused = false;

                    canvasGroup.SetActive(false);

                    foreach (Transform child in canvasGroup.transform)
                        child.gameObject.SetActive(false);

                    Time.timeScale = 1;

                    Settings.setPause(false);
                }
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Key")
        {
            audioSource.PlayOneShot(keyPickupClip, Settings.getSoundEffects());

            Destroy(collision.collider.gameObject);

            hasKey = true;
        }

        else if (collision.collider.gameObject.name == "WinZone" && !dead)
        {
            StartCoroutine(EndLevel());

            dead = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Switch"))
        {
            onSwitch = true;

            onSwitchInstance = other.gameObject;
        }

        else if (other.gameObject.name == "NoiseMaker")
        {
            Destroy(other.gameObject);

            noiseMaker = 1;
        }

        else if (other.gameObject.name == "Door" && hasKey)
        {
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Switch"))
        {
            onSwitch = false;

            onSwitchInstance = null;
        }
    }
}