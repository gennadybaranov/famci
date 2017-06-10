using System.Configuration;

namespace SOLIDHomework.Core.TaxCalculator
{
    public class DefaultTaxCalculator : ITaxCalculator
    {
        private readonly decimal taxAmnt;

        public DefaultTaxCalculator()
        {
            taxAmnt = 1;
            decimal.TryParse(ConfigurationManager.AppSettings["taxAmntDefault"], out taxAmnt);
        }

        public decimal CalculateTax(decimal sum)
        {
            return sum * taxAmnt;
        }
    }
}