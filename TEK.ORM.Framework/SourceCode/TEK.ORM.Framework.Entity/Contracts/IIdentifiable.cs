namespace Eyefinity.Tax.Entity.Contracts
{
    public interface IIdentifiable<TId>
    {
        TId Id { get; set; }
    }
}