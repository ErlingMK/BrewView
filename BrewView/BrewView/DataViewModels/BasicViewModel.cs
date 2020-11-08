using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BrewView.DataViewModels
{
    public class BasicViewModel
    {
        public BasicViewModel(string volume, string alcoholContent, string productLongName, string productId, string productShortName)
        {
            Volume = volume;
            AlcoholContent = alcoholContent;
            ProductLongName = productLongName;
            ProductId = productId;
            ProductShortName = productShortName;
        }

        public string ProductId { get; }
        public string ProductShortName { get; }
        public string ProductLongName { get; }
        public string Volume { get; }
        public string AlcoholContent { get; }
        public int Vintage { get; }
        public string AgeLimit { get; }
        public string PackagingMaterialId { get; }
        public string PackagingMaterial { get;  }
        public string VolumTypeId { get; }
        public string VolumType { get;  }
        public string CorkTypeId { get;  }
        public string CorkType { get;  }
        public int BottlePerSalesUnit { get;  }
        public string IntroductionDate { get; }
        public string ProductStatusSaleId { get; }
        public string ProductStatusSaleName { get; }
        public string ProductStatusSaleValidFrom { get; }
    }
}
