using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class BlockMapper{

    public Vector2 position;
    public GameObject blockObject;
}

[System.Serializable]
public class MapAssets {

    public GameObject prefab;
    public Sprite icon;
}

public class DrawMap : MonoBehaviour {

    public static DrawMap instance;

    public List<MapAssets> mapAssets;
    public List<BlockMapper> blocksMapper;
    [HideInInspector]
    public Vector3[] directionList;
    public int selectedBlock = 1;
    public Image drawBlockIcon;

    float framesToSkip = 4;
    float skippedFrames;

    void Awake()
    {
        PopulateDirectionList();
        instance = this;
        blocksMapper = new List<BlockMapper>();
    }

    void Update()
    {
        if(ModeManager.gameMode == ModeManager.GameMode.editMode)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if(Input.GetMouseButton(0))
            {
                if (skippedFrames > 0)
                    skippedFrames--;
                else
                    DrawBlocks();
            }
            else
                skippedFrames = framesToSkip;

            if(Input.GetMouseButtonDown(0))
            {
                RotateWallStickers();
            }
        }
    }

    void DrawBlocks()
    {
        Vector3 touchpoint = CommonMethods.GetTouchPoint();
        Vector3 snappedTouchPoint = CommonMethods.GetSnappedPoint(touchpoint);
        BlockMapper block = CheckListForPosition(snappedTouchPoint);

        if(mapAssets[selectedBlock].prefab == null && block != null)
            Destroy(block.blockObject);
        else if (block == null && mapAssets[selectedBlock].prefab != null)
            Instantiate(mapAssets[selectedBlock].prefab, snappedTouchPoint, Quaternion.identity);
    }

    void RotateWallStickers()
    {
        Vector3 touchpoint = CommonMethods.GetTouchPoint();
        Vector3 snappedTouchPoint = CommonMethods.GetSnappedPoint(touchpoint);
        BlockMapper block = CheckListForPosition(snappedTouchPoint);

        if(block != null && mapAssets[selectedBlock].prefab != null)
        {
            if(block.blockObject.GetComponent<BlockTypeManager>().blockType == mapAssets[selectedBlock].prefab.GetComponent<BlockTypeManager>().blockType)
            {
                AutoStickWall autoStickWall = block.blockObject.GetComponent<AutoStickWall>();
                if (autoStickWall != null)
                    autoStickWall.SetSprites();
            }
        }
    }

/*    Vector3 GetTouchPoint()
    {
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPoint.z = 0;
        return touchPoint;
    }

    Vector3 GetSnappedTouchPoint(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
    }*/

    public BlockMapper CheckListForPosition(Vector3 position)
    {
        Vector2 pos = new Vector2(position.x, position.y);
        return blocksMapper.Find(x => x.position == pos);
    }
      
    void PopulateDirectionList()
    {
        directionList = new Vector3[4];
        directionList[0] = Vector3.up;
        directionList[1] = Vector3.right;
        directionList[2] = Vector3.down;
        directionList[3] = Vector3.left;
    }

    public void _ChangeDrawBlock()
    {
        selectedBlock++;
        if (selectedBlock >= mapAssets.Count)
            selectedBlock = 0;
        drawBlockIcon.sprite = mapAssets[selectedBlock].icon;
    }
}
