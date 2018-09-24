using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {

    public static ModeManager insatance;

    public enum GameMode {editMode = 0,viewMode = 1,playMode = 2, buttonMode = 3}

    public static GameMode gameMode,previousMode;

    public Sprite[] icons;
    public Image gameModeIcon;
    public Image playModeImage;

    public GameObject PlayModePanel;
    public GameObject EditAndViewModePanel;
    public GameObject editGrid;
    public Button[] playModeButtons;
    public Button[] viewModeButtons;
    public Button[] editModeButtons;
    public Button[] buttonModeButtons;


    void Awake()
    {
        insatance = this;
    }

    public void _ChangeGameMode()
    {
        if (gameMode == GameMode.editMode)
            viewMode();
        else if(gameMode == GameMode.viewMode)
            editMode();
    }

    public void _TogglePlayMode()
    {
        if (gameMode != GameMode.playMode)
            playMode();
        else
            viewMode();
    }

    void editMode()
    {
        gameMode = GameMode.editMode;
        PlayModePanel.SetActive(false);
        //editGrid.SetActive(true);

        foreach (Button btn in viewModeButtons)
            btn.interactable = false;
        foreach (Button btn in editModeButtons)
            btn.interactable = true;

        gameModeIcon.sprite = icons[(int)gameMode];
    }

    void viewMode()
    {
        gameMode = GameMode.viewMode;
        PlayModePanel.SetActive(false);
        //editGrid.SetActive(false);

        foreach (Button btn in viewModeButtons)
            btn.interactable = true;
        foreach (Button btn in editModeButtons)
            btn.interactable = false;

        playModeImage.color = Color.white;
        EditAndViewModePanel.SetActive(true);
        gameModeIcon.sprite = icons[(int)gameMode];
    }

    void playMode()
    {
        gameMode = GameMode.playMode;
        PlayModePanel.SetActive(true);
        playModeImage.color = Color.green;
        EditAndViewModePanel.SetActive(false);
        //editGrid.SetActive(false);
    }

    void SetAccordingToMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.editMode:
                editMode();
                break;
            case GameMode.viewMode:
                viewMode();
                break;
            case GameMode.playMode:
                playMode();
                break;
            case GameMode.buttonMode:
                break;
        }
    }

    [HideInInspector]
    public ButtonReference buttonReference = null;
    public void SetButtonReferenceMode(bool state, ButtonReference btnRef = null)
    {
        if(state)
        {
            previousMode = gameMode;
            gameMode = GameMode.buttonMode;
            buttonReference = btnRef;

        }
        else
        {
            gameMode = previousMode;
            SetAccordingToMode(gameMode);
        }

        buttonReference.drawingEnabled = state;
        buttonReference.refLinesHolder.gameObject.SetActive(state);
    }
}
