using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour, IAnimationHandler
{
    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            DebugUtils.LogError("Animator component is missing. AnimationHandler will not function correctly.");
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (!string.IsNullOrEmpty(animationName))
        {
            _animator.enabled = true;
            _animator.SetTrigger(animationName);
        }
        else
        {
            DebugUtils.LogWarning("Animation name is empty or null. PlayAnimation aborted.");
        }
    }

    public void StopAnimation()
    {
        _animator.enabled = false;
    }

    public void ResetAnimation()
    {
        // Reset animation by enabling the animator (default state handling is expected externally).
        _animator.enabled = true;
    }

    public void SetBool(string parameter, bool value)
    {
        if (AnimatorHasParameter(parameter))
        {
            _animator.SetBool(parameter, value);
        }
        else
        {
            DebugUtils.LogWarning($"Animator parameter '{parameter}' not found. SetBool aborted.");
        }
    }

    public void SetFloat(string parameter, float value)
    {
        if (AnimatorHasParameter(parameter))
        {
            _animator.SetFloat(parameter, value);
        }
        else
        {
            DebugUtils.LogWarning($"Animator parameter '{parameter}' not found. SetFloat aborted.");
        }
    }

    public void SetInteger(string parameter, int value)
    {
        if (AnimatorHasParameter(parameter))
        {
            _animator.SetInteger(parameter, value);
        }
        else
        {
            DebugUtils.LogWarning($"Animator parameter '{parameter}' not found. SetInteger aborted.");
        }
    }

    public void SetTrigger(string parameter)
    {
        if (AnimatorHasParameter(parameter))
        {
            _animator.SetTrigger(parameter);
        }
        else
        {
            DebugUtils.LogWarning($"Animator parameter '{parameter}' not found. SetTrigger aborted.");
        }
    }

    private bool AnimatorHasParameter(string parameter)
    {
        // Check if the parameter exists in the animator to avoid runtime errors.
        foreach (AnimatorControllerParameter param in _animator.parameters)
        {
            if (param.name == parameter)
            {
                return true;
            }
        }
        return false;
    }
}