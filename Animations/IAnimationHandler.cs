
public interface IAnimationHandler
{
    void PlayAnimation(string animationName);
    void StopAnimation();
    void ResetAnimation();
    void SetBool(string parameter, bool value);
    void SetInteger(string parameter, int value);
    void SetFloat(string parameter, float value);
    void SetTrigger(string parameter);
}