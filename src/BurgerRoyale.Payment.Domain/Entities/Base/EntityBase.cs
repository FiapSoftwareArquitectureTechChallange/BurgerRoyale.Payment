namespace BurgerRoyale.Payment.Domain.Entities.Base;

public class EntityBase
{
    public EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}