namespace SOLIDHomework.Core.Services
{
    public interface IInventoryService
    {
        void Reserve(string identifier, int quantity);
    }
}