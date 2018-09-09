using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockMapper{

    public Vector2 position;
    public GameObject blockObject;
}

public class DrawMap : MonoBehaviour {

    public static DrawMap instance;

    public List<GameObject> mapAssets;
    public List<BlockMapper> blocksMapper;
    [HideInInspector]
    public Vector3[] directionList;
    public int selectedBlock;

    void Awake()
    {
        PopulateDirectionList();
        instance = this;
        blocksMapper = new List<BlockMapper>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            DrawBlocks();
        }

        if(Input.GetMouseButtonDown(1))
        {
            selectedBlock++;
            if (selectedBlock >= mapAssets.Count)
                selectedBlock = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            RotateWallStickers();
        }
    }

    void DrawBlocks()
    {
        Vector3 touchpoint = GetTouchPoint();
        Vector3 snappedTouchPoint = GetSnappedTouchPoint(touchpoint);
        BlockMapper block = CheckListForPosition(snappedTouchPoint);

        if(mapAssets[selectedBlock] == null && block != null)
            Destroy(block.blockObject);
        else if (block == null && mapAssets[selectedBlock] != null)
            Instantiate(mapAssets[selectedBlock], snappedTouchPoint, Quaternion.identity);
    }

    void RotateWallStickers()
    {
        Vector3 touchpoint = GetTouchPoint();
        Vector3 snappedTouchPoint = GetSnappedTouchPoint(touchpoint);
        BlockMapper block = CheckListForPosition(snappedTouchPoint);

        if(block != null && mapAssets[selectedBlock] != null)
        {
            if(block.blockObject.GetComponent<BlockTypeManager>().blockType == mapAssets[selectedBlock].GetComponent<BlockTypeManager>().blockType)
            {
                AutoStickWall autoStickWall = block.blockObject.GetComponent<AutoStickWall>();
                if (autoStickWall != null)
                    autoStickWall.SetSprites();
            }
        }
    }

    Vector3 GetTouchPoint()
    {
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPoint.z = 0;
        return touchPoint;
    }

    Vector3 GetSnappedTouchPoint(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
    }

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
}
