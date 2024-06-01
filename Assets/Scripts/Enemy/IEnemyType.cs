internal interface IEnemyType
{
    public EnemyType GetEnemyType();
}

public enum EnemyType
{
    NormalEnemy,
    FastEnemy
}