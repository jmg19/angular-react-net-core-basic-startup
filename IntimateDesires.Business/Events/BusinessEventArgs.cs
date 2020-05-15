using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business.Events
{
    public class BusinessEventArgs : EventArgs
    {
        private Type entityType;

        public BusinessEventArgs(Type type) {
            this.entityType = type;
        }
        public Type GetBusinessEntityType() {
            return entityType;
        }
    }
}
