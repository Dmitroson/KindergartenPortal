using System;
using System.Collections.Generic;
using System.Text;

namespace Education.DAL.Entities.Register
{
    public class Mark : Entity
    {
        public string Value { get; set; }
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }
        public DateTime Date { get; set; }

        public Mark() { }
    }
}
