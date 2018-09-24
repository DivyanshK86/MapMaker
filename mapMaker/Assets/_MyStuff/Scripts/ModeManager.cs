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

    public GameObject buttonModeOptions;

    void Awake()
    {
        insatance = this;
    }

    public void _ChangeGameMode()
    {
        if (gameMode == GameMode.editMode)
            SetAccordingToMode(GameMode.viewMode);
        else if(gameMode == GameMode.viewMode)
            SetAccordingToMode(GameMode.editMode);
    }

    public void _TogglePlayMode()
    {
        if (gameMode != GameMode.playMode)
            SetAccordingToMode(GameMode.playMode);
        else
            SetAccordingToMode(GameMode.viewMode);
    }

    void editMode()
    {
        gameMode = GameMode.editMode;
        PlayModePanel.SetActive(false);
        //editGrid.SetActive(true);
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

        playModeImage.color = Color.white;
        EditAndViewModePanel.SetActive(true);
        gameModeIcon.sprite = icons[(int)gameMode];
    }

    void playMode()
    {
        foreach (Button btn in playModeButtons)
            btn.interactable = true;
        
        gameMode = GameMode.playMode;
        PlayModePanel.SetActive(true);
        playModeImage.color = Color.green;
        EditAndViewModePanel.SetActive(false);
        //editGrid.SetActive(false);
    }

    void SetAccordingToMode(GameMode mode)
    {
        foreach (Button btn in viewModeButtons)
            btn.interactable = false;
        foreach (Button btn in editModeButtons)
            btn.interactable = false;
        foreach (Button btn in playModeButtons)
            btn.interactable = false;
        foreach (Button btn in buttonModeButtons)
            btn.interactable = false;

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
    public void SetButtonReferenceMode(bool state)
    {
        if(state)
        {
            previousMode = gameMode;
            gameMode = GameMode.buttonMode;
            SetAccordingToMode(GameMode.buttonMode);
            foreach (Button btn in buttonModeButtons)
                btn.interactable = true;
        }
        else
        {
            gameMode = previousMode;
            SetAccordingToMode(gameMode);
        }

        buttonReference.drawingEnabled = state;
        buttonReference.refLinesHolder.gameObject.SetActive(state);
        buttonModeOptions.SetActive(state);
    }

    public void _DeleteNodes()
    {
        buttonReference.refBlockList.Clear();
        buttonReference.UpdateReferenceLines();
    }
}
