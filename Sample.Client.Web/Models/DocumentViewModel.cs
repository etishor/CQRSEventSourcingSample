using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample.ReadModel.Funds;

namespace Sample.Client.Web.Models
{
    public class DocumentViewModel
    {
        public Document Document { get; set; }
        public IEnumerable<ShareClass> AllShares { get; set; }
    }
}