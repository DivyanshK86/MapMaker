using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetBlock : MonoBehaviour {

    public bool[] occludeDir;  //up,right,down,left
    public List<string> stickWith;

    void Start()
    {
        CheckUpdateAndUpdateOthers();
        SetSprites();
    }

    public void CheckUpdateAndUpdateOthers()
    {
        UpdateOthers();
        CheckUpdate();
    }

    public void UpdateOthers()
    {
        BlockMapper otherBlock = new BlockMapper();
        AutoSetBlock otherAutoSetBlock = new AutoSetBlock();
        AutoStickWall otherAutoStickWall = new AutoStickWall();

        foreach(Vector3 dir in DrawMap.instance.directionList)
        {
            otherBlock = DrawMap.instance.CheckListForPosition(transform.position + dir);
            if(otherBlock != null)
            {
                otherAutoSetBlock = otherBlock.blockObject.GetComponent<AutoSetBlock>();
                otherAutoStickWall = otherBlock.blockObject.GetComponent<AutoStickWall>();
                if (otherAutoSetBlock != null)
                    otherAutoSetBlock.CheckUpdate();
                if (otherAutoStickWall != null)
                    otherAutoStickWall.CheckUpdate();
            }
        }
    }

    public void CheckUpdate()
    {
        ResetOccludeArray();
        
        for(int i=0; i<DrawMap.instance.directionList.Length; i++)
            if(DrawMap.instance.CheckListForPosition(transform.position + DrawMap.instance.directionList[i]) != null)
            if(stickWith.Contains(DrawMap.instance.CheckListForPosition(transform.position + DrawMap.instance.directionList[i]).blockObject.GetComponent<BlockTypeManager>().blockType))
                occludeDir[i] = true;

        SetSprites();
    }
        
    public void SetSprites()
    {
        for(int i=0; i<4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).gameObject.SetActive(!occludeDir[i]);
        }
    }

    void ResetOccludeArray()
    {
        for (int i = 0; i < occludeDir.Length; i++)
            occludeDir[i] = false;
    }
}
