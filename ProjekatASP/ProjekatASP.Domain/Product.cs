﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Product : Entity
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int GenderId { get; set; }
        public bool Sale { get; set; }
        public bool InStock { get; set; }
        public string Material { get; set; }
        public string CountryOfOrigin { get; set; }


        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Price> Prices { get; set; }=new List<Price>();


        public virtual ICollection<Picture> Pictures { get; set; }=new List<Picture>();
        public virtual ICollection<ProductSize> ProductSizes { get; set; }=new List<ProductSize>();
        public virtual ICollection<CartItem> CartItems { get; set; }=new List<CartItem>();
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }=new List<ReceiptItem>();
    }
}
