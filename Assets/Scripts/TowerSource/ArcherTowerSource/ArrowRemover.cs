using System.Collections.Generic;
using Pool;

namespace Towers.Source
{
    public class ArrowRemover : ObjectRemover
    {
        private Queue<Arrow> _arrows;

        public void Awake()
        {
            _arrows = new Queue<Arrow>();
        }

        public void SubscribeInstance(Arrow arrow)
        {
            _arrows.Enqueue(arrow);
            arrow.Fell += RemoveObject;
        }

        protected override void OnDisable()
        {
            for (int i = 0; i < _arrows.Count; i++)
                _arrows.Dequeue().Fell -= RemoveObject;
        }
    }
}
