﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewCakeMakerASP.NET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int ProductId { get; set; }
        [Required(ErrorMessage = " Không được để trống !")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Kí tự Lớn hơn 6 và nhỏ hơn 20 ")]

        public string Name { get; set; }
        [Required(ErrorMessage = " Không được để trống !")]

        public Nullable<decimal> Prince { get; set; }
        [Required(ErrorMessage = " Không được để trống !")]

        public string Image { get; set; }
        [Required(ErrorMessage = " Không được để trống !")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Kí tự Lớn hơn 10 và nhỏ hơn 200 ")]

        public string Description { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }

        public Nullable<int> TypeProductId { get; set; }
    
        public virtual NewTypeProduct NewTypeProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}