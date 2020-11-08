namespace BrewView.GraphQL
{
    public class Mutations
    {
        public static string CreateBrew =>
            @"mutation CreateAndReturnBrew($input: BrewInput) {create(brew: $input){gtin,productId}}";

        public static string ToggleFavorite =>
            @"mutation ToggleFavorite($productId : String){
  toggleFavorite(productId: $productId)
}";
    }
}