
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject canvasMainMenu;

    public GameObject canvasSettings;

    public GameObject canvasCredits;

    public Slider musicVolumeSlider;

    public Toggle soundEffectsToggle;

    public Dropdown difficultyDropdown;

    void Start()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().musicVolumeSlider.value = Settings.getMusicVolume();

        if (Settings.getSoundEffects() == 1)
            GameObject.Find("MenuManager").GetComponent<MenuManager>().soundEffectsToggle.isOn = true;

        else
            GameObject.Find("MenuManager").GetComponent<MenuManager>().soundEffectsToggle.isOn = false;
    }

    public void goToScene(string level)
    {
        Settings.setPause(false);

        Time.timeScale = 1;

        SceneManager.LoadScene(level);
    }

    public void exitButtonAction()
    {
        Application.Quit();
    }

    public void goToMainMenu()
    {
        canvasMainMenu.SetActive(true);

        canvasSettings.SetActive(false);

        canvasCredits.SetActive(false);
    }

    public void goToSettings()
    {
        canvasMainMenu.SetActive(false);

        canvasSettings.SetActive(true);
    }

    public void goToCredits()
    {
        canvasMainMenu.SetActive(false);

        canvasCredits.SetActive(true);
    }

    public void setMusicVolume()
    {
        Settings.setMusicVolume(musicVolumeSlider.value);
    }

    public void setSoundEffects()
    {
        if (soundEffectsToggle.isOn)
            Settings.setSoundEffects(1);

        else
            Settings.setSoundEffects(0);
    }

    public void setDifficulty()
    {
        Settings.setDifficulty(difficultyDropdown.value);
    }
}