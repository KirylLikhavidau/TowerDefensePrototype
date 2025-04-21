using UnityEditorInternal;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Animator _animationTree;

    private bool _isDying;

    private void Start()
    {
        _isDying = false;
        ResetAnimations();
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.DownMotion, true);
    }

    private void OnEnable()
    {
        _enemyPrefab.Dying += PlayDeadAnimation;
        _enemyPrefab.RightMovement += PlayRightAnimation;
        _enemyPrefab.LeftMovement += PlayLeftAnimation;
        _enemyPrefab.DownMovement += PlayDownAnimation;
        _enemyPrefab.UpMovement += PlayUpAnimation;
    }

    private void OnDisable()
    {
        _enemyPrefab.Dying -= PlayDeadAnimation;
        _enemyPrefab.RightMovement -= PlayRightAnimation;
        _enemyPrefab.LeftMovement -= PlayLeftAnimation;
        _enemyPrefab.DownMovement -= PlayDownAnimation;
        _enemyPrefab.UpMovement -= PlayUpAnimation;
    }


    private void ResetAnimations()
    {
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.Dead, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.RightMotion, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.LeftMotion, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.DownMotion, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.UpMotion, false);
    }

    private void PlayDeadAnimation()
    {
        _isDying = true;
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.Dead, true);
    }

    private void PlayRightAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.RightMotion, true);
        }
    }

    private void PlayLeftAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.LeftMotion, true);
        }
    }

    private void PlayUpAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.UpMotion, true);
        }
    }

    private void PlayDownAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.DownMotion, true);
        }
    }
}
