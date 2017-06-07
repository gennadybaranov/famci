using System;

namespace SOLIDHomework.Core.Services
{
    public class InventoryService : IInventoryService
    {
        // that is Database-based service 
        public void Reserve(string identifier, int quantity)
        {

        }
    }

    public class TextBasedInventoryService : IInventoryService
    {
        public void Reserve(string identifier, int quantity)
        {
            throw new NotImplementedException();
        }
    }


    public interface IInventoryService
    {
        void Reserve(string identifier, int quantity);
    }
}