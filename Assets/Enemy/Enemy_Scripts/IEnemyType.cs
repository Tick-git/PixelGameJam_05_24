internal interface IEnemyType
{
    public EnemyType GetEnemyType();
}

public enum EnemyType
{
    Normal,
    Fast
}