using System;
using System.Collections.Generic;
using System.Text;

namespace Education.DAL.Entities.Register
{
    public class Child : Entity
    {
        public string FullName { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }

        public Child() 
        {
            Marks = new List<Mark>();
        }
    }
}
