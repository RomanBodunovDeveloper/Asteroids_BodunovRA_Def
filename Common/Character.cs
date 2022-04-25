public class Character : ObjectOnScene
{
    public float Health { get; private set; }

    public void InitCharacter(float health)
    {
        Health = health;
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DestroyObjectOnScene();
        }
    }
}
