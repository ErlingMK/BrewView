using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;

namespace BrewView.Models
{
    public class BrewInfoResponse
    {
        public BrewInfo CreateBrew { get; set; }
    }

    public class BrewInfo
    {
        public string ProductId { get; set; }
        public string Gtin { get; set; }
    }

}
