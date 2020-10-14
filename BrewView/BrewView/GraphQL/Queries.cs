namespace BrewView.GraphQL
{
    public class Queries
    {
        public const string BrewByGtin = @"query ($gtin: String){
  alcoholicEntity(gtin: $gtin){
    basic{
      productLongName
      alcoholContent
      volume
    }
    description{
      characteristics{
        colour
        odour
        taste
      }
      sweetness
      fullness
    }
  }
}";
    }
}