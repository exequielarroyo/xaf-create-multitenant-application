﻿using System.ComponentModel.DataAnnotations;

namespace OutlookInspired.Module.BusinessObjects{
    public class QuoteItem :BaseObject{
        public virtual Quote Quote { get; set; }
        public  virtual long? QuoteId { get; set; }
        public virtual Product Product { get; set; }
        public  virtual long? ProductId { get; set; }
        public  virtual int ProductUnits { get; set; }
        [DataType(DataType.Currency)]
        public  virtual decimal ProductPrice { get; set; }
        [DataType(DataType.Currency)]
        public  virtual decimal Discount { get; set; }
        [DataType(DataType.Currency)]
        public  virtual decimal Total { get; set; }
    }
}