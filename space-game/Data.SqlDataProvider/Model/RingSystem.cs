//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.SqlDataProvider.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RingSystem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RingSystem()
        {
            this.CelestialObjects = new HashSet<CelestialObject>();
        }
    
        public int Id { get; set; }
        public double InnerRadius { get; set; }
        public double OuterRadius { get; set; }
        public int TextureGroupId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CelestialObject> CelestialObjects { get; set; }
        public virtual TextureGroup TextureGroup { get; set; }
    }
}