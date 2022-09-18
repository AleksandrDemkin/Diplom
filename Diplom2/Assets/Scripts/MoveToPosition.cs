using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    SpriteRenderer renderer;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _duration;

    
    IEnumerator GenericAnimation(Vector3 targetPosition, Color targetColor, float
        duration)
    {
        Vector3 startPosition = transform.position;
        Color startColor = renderer.color;
        float progress = 0;
        while (progress <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            renderer.color = Color.Lerp(startColor, targetColor, progress);
            progress += Time.deltaTime / duration;
            yield return null;
        }
        
        transform.position = targetPosition;
        renderer.color = targetColor;
    }
}
