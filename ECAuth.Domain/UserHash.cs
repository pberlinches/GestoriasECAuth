using System;
using System.Collections.Generic;
using System.Text;

namespace ECAuth.Domain
{
    public class UserHash
    {

        public int id { get; set; }

        public string UserId { get; set; }
        public string Hash { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}