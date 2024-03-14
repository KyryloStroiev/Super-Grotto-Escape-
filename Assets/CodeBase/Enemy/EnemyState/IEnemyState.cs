namespace CodeBase.Enemy.EnemyState
{
    public interface IEnemyState
    {
        void Enable();
        void Disable();
        
        bool IsEnable { get; set; }
    }
}