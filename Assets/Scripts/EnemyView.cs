using UnityEditorInternal;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private Animator _animationTree;

    private void OnEnable()
    {
        _enemyPrefab.Died += PlayDeadAnimation;
    }

    private void OnDisable()
    {
        _enemyPrefab.Died -= PlayDeadAnimation;
    }

    private void Start()
    {
        ResetAnimations();
    }

    private void ResetAnimations()
    {
        _animationTree.SetBool(EnemyAnimatorConstants.States.Dead, false);
        _animationTree.SetBool(EnemyAnimatorConstants.States.RightMotion, false);
        _animationTree.SetBool(EnemyAnimatorConstants.States.LeftMotion, false);
        _animationTree.SetBool(EnemyAnimatorConstants.States.DownMotion, true);
        _animationTree.SetBool(EnemyAnimatorConstants.States.UpMotion, false);
    }

    private void PlayDeadAnimation(Unit unit)
    {
        _animationTree.SetBool(EnemyAnimatorConstants.States.Dead, true);
    }
}
