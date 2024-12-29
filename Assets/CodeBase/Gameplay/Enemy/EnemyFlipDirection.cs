using CodeBase.Logic;

namespace CodeBase.Enemy
{
    public class EnemyFlipDirection : FlipDirection
    {
        private EnemyMove _move;

        public void Construct(bool isLookLeft)
        {
            IsFacingRight = !isLookLeft;
        }
        private void Awake() => 
            _move = GetComponent<EnemyMove>();

        private void Update() => 
            CheckDirection(_move.HorizontalVelocity);
    }
}