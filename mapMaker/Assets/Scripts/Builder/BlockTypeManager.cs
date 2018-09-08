using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypeManager : MonoBehaviour {

    public string blockType;

    void Start()
    {
        BlockMapper newBlock = new BlockMapper();
        newBlock.blockObject = this.gameObject;
        newBlock.position = new Vector2(transform.position.x, transform.position.y);
        DrawMap.instance.blocksMapper.Add(newBlock);
    }

    void OnDestroy()
    {
        BlockMapper thisBlockMap = DrawMap.instance.blocksMapper.Find(x => x.blockObject == this.gameObject);
        DrawMap.instance.blocksMapper.Remove(thisBlockMap);

        AutoSetBlock AutoSetBlock = GetComponent<AutoSetBlock>();
        AutoStickWall AutoStickWall = GetComponent<AutoStickWall>();

        if (AutoSetBlock != null)
        {
            AutoSetBlock.UpdateOthers();   
        }

    }
}
