using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStickWall : MonoBehaviour {

    public List<float> allAngles;
    public List<string> stickWith;
    int rotationIdx = 0;

    float[] angleList = {180,90,0,270};

    void Start()
    {
        CheckUpdate();
    }

    public void CheckUpdate()
    {
        allAngles = new List<float>();
        BlockMapper block = new BlockMapper();
        BlockTypeManager blockTypeManager;

        for(int i=0; i<DrawMap.instance.directionList.Length; i++)
        {
            block = DrawMap.instance.CheckListForPosition(transform.position + DrawMap.instance.directionList[i]);
            if(block != null)
            {
                blockTypeManager = block.blockObject.GetComponent<BlockTypeManager>();
                if(stickWith.Contains(blockTypeManager.blockType))
                    allAngles.Add(angleList[i]);
            }
        }
        SetSprites();
    }


    public void SetSprites()
    {
        if(allAngles.Count != 0)
        {
            if (rotationIdx >= allAngles.Count)
                rotationIdx = 0;
            
            transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(transform.GetChild(0).rotation.eulerAngles.x, transform.GetChild(0).rotation.eulerAngles.y, allAngles[rotationIdx]));
            rotationIdx++;
            if (rotationIdx >= allAngles.Count)
                rotationIdx = 0;
        }
        else
        {
            BlockMapper block = DrawMap.instance.CheckListForPosition(transform.position);
            Destroy(block.blockObject);
            DrawMap.instance.blocksMapper.Remove(block);
        }


    }
}
