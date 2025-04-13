using UnityEditorInternal;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private Animator _animationTree;

    private void OnEnable()
    {
        _enemyPrefab.Dying += PlayDeadAnimation;
    }

    private void OnDisable()
    {
        _enemyPrefab.Dying -= PlayDeadAnimation;
    }

    private void Start()
    {
        ResetAnimations();
    }

    private void ResetAnimations()
    {
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.Dead, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.RightMotion, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.LeftMotion, false);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.DownMotion, true);
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.UpMotion, false);
    }

    private void PlayDeadAnimation()
    {
        _animationTree.SetBool(AnimatorConstants.EnemyAnimator.States.Dead, true);
    }
}
