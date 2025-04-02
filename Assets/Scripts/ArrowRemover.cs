public class ArrowRemover : ObjectRemover
{
    private Arrow _arrow;

    private void Awake()
    {
        _arrow = (Arrow)_objectPrefab;
    }

    protected override void OnEnable()
    {
        
    }

    protected override void OnDisable()
    {
        
    }
}
