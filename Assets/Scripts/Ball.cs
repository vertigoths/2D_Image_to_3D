using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private BlockController _blockController;

    public void OnCreate(BlockController blockController)
    {
        _blockController = blockController;
    }

    public void OnCall(Vector3 targetPos, float ballReachTime)
    {
        JumpToTarget(targetPos, ballReachTime);
    }
    
    private void JumpToTarget(Vector3 targetPos, float ballReachTime)
    {
        Vector3[] path =
        {
            targetPos, 
            new Vector3(transform.localPosition.x * Random.Range(0.85f, 1.15f), -3.5f, -0.4f)
        };
        
        transform.DOLocalPath(path, ballReachTime, PathType.CatmullRom);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PixelBlock>())
        {
            var indices = other.name.Split("-");
            var posX = int.Parse(indices[1]);
            var posY = int.Parse(indices[0]);

            if (_blockController.IsExistInMap(posY, posX) && !_blockController.GetDidFinish())
            {
                _blockController.RemoveFromMap(posX, posY);
            }
        }
    }
}
