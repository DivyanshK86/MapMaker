using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {

    public enum GameMode {editMode = 0,viewMode = 1,playMode = 2}

    public static GameMode gameMode;

    public Sprite[] icons;
    public Image gameModeIcon;
    public Image playModeImage;

    public GameObject PlayModePanel;
    public GameObject EditAndViewModePanel;
    public Button[] viewModeButtons;
    public Button[] editModeButtons;

    public void _ChangeGameMode()
    {
        if (gameMode == GameMode.editMode)
        {
            viewMode();
        }
        else if(gameMode == GameMode.viewMode)
        {
            editMode();
        }

        gameModeIcon.sprite = icons[(int)gameMode];
    }

    public void _TogglePlayMode()
    {
        if (gameMode != GameMode.playMode)
        {
            playMode();
            playModeImage.color = Color.green;
            EditAndViewModePanel.SetActive(false);
        }
        else
        {
            viewMode();
            playModeImage.color = Color.white;
            EditAndViewModePanel.SetActive(true);
            gameModeIcon.sprite = icons[(int)gameMode];
        }
    }

    void editMode()
    {
        gameMode = GameMode.editMode;
        PlayModePanel.SetActive(false);

        foreach (Button btn in viewModeButtons)
            btn.interactable = false;
        foreach (Button btn in editModeButtons)
            btn.interactable = true;
    }

    void viewMode()
    {
        gameMode = GameMode.viewMode;
        PlayModePanel.SetActive(false);

        foreach (Button btn in viewModeButtons)
            btn.interactable = true;
        foreach (Button btn in editModeButtons)
            btn.interactable = false;
    }

    void playMode()
    {
        gameMode = GameMode.playMode;
        PlayModePanel.SetActive(true);
    }
}
