using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObsticleBehaviour : MonoBehaviour
{
    //obsticle that is controlled by the obsticle gear
    private BoxCollider2D obsticleBoxCollider;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Vector3 endingPosition;

    private void Start()
    {
        transform.localPosition = startingPosition;
        obsticleBoxCollider = GetComponent<BoxCollider2D>();
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

    private IEnumerator MoveCoroutine(Vector3 start, Vector3 end)
    {
        float interpolation = 0f;
        MusicManager.Instance.PlayMusicClip(SoundData.ObsticleMoving);
        while (interpolation < 1)
        {
            interpolation += 0.1f;
            transform.localPosition = Vector3.Lerp(start, end, interpolation); //get the new position for every interpolation
            CheckSurroundingElement(); //remove any surrounding dragable elements 
            yield return new WaitForSeconds(0.2f); // not that good but it is a okay for a start
        }
        transform.localPosition = end;
        yield break;
    }

    private void CheckSurroundingElement()
    {
        float angle = 0f;
        float mindept = transform.position.z - 0.3f; //limiting the search scale so that it can detect only its layer
        float maxdept = transform.position.z + 0.3f;
        float reductionScale = 0.2f; // special value since the sprite is quite big. This value take into account that reduction in value
        var surroundingElement = Physics2D.OverlapBoxAll(transform.position, 
            obsticleBoxCollider.size * reductionScale, 
            angle ,
            LayerData.InnerGearLayer,
            mindept,
            maxdept
            );
        //Physics2D.over
        foreach(var collider in surroundingElement) //will remove any dragable objects nearby
        {
            var moveableComponent= collider.GetComponentInParent<IMoveable>();
            if(moveableComponent != null)
            {
                moveableComponent.RemoveItem();
            }
        }
    } 
}
