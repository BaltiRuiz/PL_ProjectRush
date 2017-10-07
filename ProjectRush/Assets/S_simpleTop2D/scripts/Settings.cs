
public static class Settings {

    // Default settings

    private static float musicVolume = 1.0f;

    private static int soundEffects = 1;

    private static int difficulty = 1;

    private static bool pause = false;

    public static float getMusicVolume()
    {
        return musicVolume;
    }

    public static int getSoundEffects()
    {
        return soundEffects;
    }

    public static int getDifficulty()
    {
        return difficulty;
    }

    public static bool getPause()
    {
        return pause;
    }

    public static void setMusicVolume(float new_music_volume)
    {
        musicVolume = new_music_volume;
    }

    public static void setSoundEffects(int new_sound_effect)
    {
        soundEffects = new_sound_effect;
    }

    public static void setDifficulty(int new_difficulty)
    {
        difficulty = new_difficulty;
    }

    public static void setPause(bool new_pause)
    {
        pause = new_pause;
    }
}