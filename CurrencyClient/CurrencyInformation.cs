using System;

namespace Currency.Models
{
    // Currency info i.e. Name, Rate Currency, Date ExChange
    public class CurrencyInformation
    {
        
        public String Currencyname
        {
            get;
            set;
        }

       
        public double CurrencyRate
        {
            get;
            set;
        }

        public DateTime RateDate
        {
            get;
            set;
        }

       
        public bool CurrencyWarning { get; set; }
    }
}
