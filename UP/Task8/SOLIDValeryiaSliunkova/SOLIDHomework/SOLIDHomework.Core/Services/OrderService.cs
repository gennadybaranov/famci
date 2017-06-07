using SOLIDHomework.Core.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Services
{
    public class OrderService
    {
        private readonly IInventoryService _inventoryService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public OrderService(IInventoryService inventoryService, IUserService userService, ILogger loger)
        {
            _inventoryService = inventoryService;
            _userService = userService;
            _logger = loger;
        }

        private void NotifyCustomer(string username)
        {
            _logger.Write(string.Format("Email will be sent to {0}", username));
            string customerEmail = _userService.GetByUsername(username).Email;
            if (!String.IsNullOrEmpty(customerEmail))
            {
                try
                {
                    //construct the email message and send it, implementation ignored
                }
                catch (Exception)
                {
                    //log the emailing error, implementation ignored
                }
            }
        }
    }
}
