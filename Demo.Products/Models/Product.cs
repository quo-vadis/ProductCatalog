//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo.Products.Models
{
    using FileHelpers;
    using System;
    using System.Collections.Generic;

    [DelimitedRecord(",")]
    [IgnoreFirst()]
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<int> CategoryId { get; set; }

        [FieldHidden]
        public virtual Category Category { get; set; }
    }
}