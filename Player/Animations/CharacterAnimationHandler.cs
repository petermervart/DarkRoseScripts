using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimationHandler : AnimationHandler, ICharacterAnimationHandler
{
    [SerializeField]
    private int _attackAnimationsAmount; // for random animation... its not handled very efficiently there might be a better solution but not found yet with usage of animator

    public event Action OnAttack;

    public void OnChangedRunning(bool IsRunning)
    {
        _animator.SetBool("isrunning", IsRunning);
    }

    public void OnChangedMoving(bool IsMoving)
    {
        _animator.SetBool("ismoving", IsMoving);
    }

    public void OnChangedAttack(bool IsAttacking)
    {
        _animator.SetBool("isattacking", IsAttacking);

        if (IsAttacking)
        {
            int randomAttackAnimation = Random.Range(0, _attackAnimationsAmount);

            _animator.SetInteger("whichattack", randomAttackAnimation);
        }
    }

    public void Attack() // this is called at certain frame of the animation
    {
        OnAttack?.Invoke();
    }

    public void EndAttackAnimation()
    {
        int randomAttackAnimation = Random.Range(0, _attackAnimationsAmount);

        _animator.SetInteger("whichattack", randomAttackAnimation);
    }
}
