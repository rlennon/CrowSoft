using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowsoftmvc.Data;

namespace crowsoftmvc.Models
{
    public class Dummy
    {
        private DummyContext context;

        public int PersonID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
