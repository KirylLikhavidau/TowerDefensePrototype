using UnityEngine;
using Units.Enemy;
using States;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyUnit _enemyPrefab;
    [SerializeField] private Animator _animationTree;

    private bool _isDying;

    private void Start()
    {
        _isDying = false;
        ResetAnimations();
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.DownMotion, true);
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
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.Dead, false);
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.RightMotion, false);
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.LeftMotion, false);
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.DownMotion, false);
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.UpMotion, false);
    }

    private void PlayDeadAnimation()
    {
        _isDying = true;
        _animationTree.SetBool(AnimatorStates.EnemyAnimator.Dead, true);
    }

    private void PlayRightAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorStates.EnemyAnimator.RightMotion, true);
        }
    }

    private void PlayLeftAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorStates.EnemyAnimator.LeftMotion, true);
        }
    }

    private void PlayUpAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorStates.EnemyAnimator.UpMotion, true);
        }
    }

    private void PlayDownAnimation()
    {
        if (_isDying == false)
        {
            ResetAnimations();
            _animationTree.SetBool(AnimatorStates.EnemyAnimator.DownMotion, true);
        }
    }
}
