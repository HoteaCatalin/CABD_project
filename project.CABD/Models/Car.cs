//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project.CABD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public int An { get; set; }
        public int Pret { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
    }
}
