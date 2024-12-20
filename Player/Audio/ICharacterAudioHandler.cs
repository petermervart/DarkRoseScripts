using UnityEngine;

public interface ICharacterAudioHandler
{
    public void OnHitGround();

    public void OnHit(bool hitSomething);

    public void OnGotHurt();

    public void OnFootSteps(ESurfaceType groundMaterial);
}
