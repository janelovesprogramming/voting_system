
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace VOTING_SYSTEM_SERVER
{

using System;
    using System.Collections.Generic;
    
public partial class Bulletin
{

    public int ID_Bulletin { get; set; }

    public int Candidate_Voting_ID { get; set; }

    public int Vote { get; set; }

    public int Users_ID { get; set; }



    public virtual Candidate_Voting Candidate_Voting { get; set; }

    public virtual User User { get; set; }

}

}
