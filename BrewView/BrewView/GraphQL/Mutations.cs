using System;
using System.Collections.Generic;
using System.Text;

namespace BrewView.GraphQL
{
    public class Mutations
    {
        public static string CreateBrew =>
            @"mutation CreateAndReturnBrew($input: BrewInput) {create(brew: $input){gtin,productId}}";

        public static string MakeFavorite =>
            @"mutation MakeFavorite($input: BrewInput) {makeFavorite(brew: $input){productId}}";
    }
}
