using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private Vector2 endingPosition;

    private void Start()
    {
        transform.localPosition = startingPosition;
    }

    public void RemoveObsticle()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(startingPosition, endingPosition));
    }

    public void MoveObsticleBack()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(endingPosition, startingPosition));
    }

    private IEnumerator MoveCoroutine(Vector2 start, Vector2 end)
    {
        float interpolation = 0f;
        while (interpolation < 1)
        {
            interpolation += 0.4f;
            transform.localPosition = Vector2.Lerp(start, end, interpolation);
            print("moving");
            yield return new WaitForSeconds(0.05f); // not that good but it is a okay for a start
        }
        yield break;
    }
}
