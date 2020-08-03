using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.DataTableObjects
{
    [Table("Configurations")]
    public class DtoConfiguration
    {
        public DtoConfiguration()
        {

        }

        public DtoConfiguration(int ID, string Name, string Value)
        {
            this.ID = ID;
            this.Name = Name;
            this.Value = Value;
        }

        [Key]
        public int ID { get; internal set; }
        public string Name { get; internal set; }
        public string Value { get; internal set; }
    }
}
