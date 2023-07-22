namespace ServerYGO.Data.Entities
{
    public class TranslatedRarityType
    {
        public int Id { get; set; }

        public int OriginalRarityTypeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
