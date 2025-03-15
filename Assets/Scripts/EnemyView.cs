using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private Animator _animationTree;

    private void Start()
    {
        ResetAnimations();
    }

    private void Update()
    {
        if (_enemy.IsDead)
            _animationTree.SetBool("Dead", true);
    }

    private void ResetAnimations()
    {
        _animationTree.SetBool("Dead", false);
        _animationTree.SetBool("RightMotion", false);
        _animationTree.SetBool("LeftMotion", false);
        _animationTree.SetBool("DownMotion", true);
        _animationTree.SetBool("UpMotion", false);
    }
}