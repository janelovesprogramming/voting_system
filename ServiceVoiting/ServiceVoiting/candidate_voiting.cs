//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceVoiting
{
    using System;
    using System.Collections.Generic;
    
    public partial class candidate_voiting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public candidate_voiting()
        {
            this.bulletin = new HashSet<bulletin>();
        }
    
        public int id_candidate_voiting { get; set; }
        public int candidate_id { get; set; }
        public int voiting_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bulletin> bulletin { get; set; }
        public virtual candidate candidate { get; set; }
        public virtual voiting voiting { get; set; }
    }
}
