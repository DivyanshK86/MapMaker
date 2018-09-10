using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {

    public enum GameMode {editMode = 0,viewMode = 1,playMode = 2}

    public static GameMode gameMode;

    public Sprite[] icons;
    public Image gameModeIcon;

    public GameObject PlayModePanel;
    public GameObject EditAndViewModePanel;
    public Button[] viewModeButtons;
    public Button[] editModeButtons;

    public void _ChangeGameMode()
    {
        if (gameMode == GameMode.editMode)
        {
            gameMode = GameMode.viewMode;
            PlayModePanel.SetActive(false);
            EditAndViewModePanel.SetActive(true);

            foreach (Button btn in viewModeButtons)
                btn.interactable = true;
            foreach (Button btn in editModeButtons)
                btn.interactable = false;
        }
        else if(gameMode == GameMode.viewMode)
        {
            gameMode = GameMode.playMode;
            PlayModePanel.SetActive(true);
            EditAndViewModePanel.SetActive(false);
        }
        else if(gameMode == GameMode.playMode)
        {
            gameMode = GameMode.editMode;
            PlayModePanel.SetActive(false);
            EditAndViewModePanel.SetActive(true);

            foreach (Button btn in viewModeButtons)
                btn.interactable = false;
            foreach (Button btn in editModeButtons)
                btn.interactable = true;
        }

        gameModeIcon.sprite = icons[(int)gameMode];
    }
}
