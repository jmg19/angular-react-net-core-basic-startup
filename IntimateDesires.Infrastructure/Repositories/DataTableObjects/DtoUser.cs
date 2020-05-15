using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories.DataTableObjects
{
    [Table("Users")]
    public class DtoUser
    {
        public DtoUser() { }

        public DtoUser(int ID, string UserName, string Hash, bool Active)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Hash = Hash;
            this.Active = Active;
        }

        [Key]
        public int ID { get; internal set; }
        public string UserName { get; internal set; }
        public string Hash { get; internal set; }
        public bool Active { get; internal set; }        
        
    }
}
