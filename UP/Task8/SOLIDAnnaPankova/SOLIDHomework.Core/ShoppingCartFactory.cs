using Ninject;
using SOLIDHomework.Core.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core
{
    public class ShoppingCartFactory
    {
        private IKernel kernel;
        public ShoppingCartFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }
        public ShoppingCart Create()
        {
            var cart = new ShoppingCart();
            kernel.Inject(cart);
            return cart;
        }
    }
}
