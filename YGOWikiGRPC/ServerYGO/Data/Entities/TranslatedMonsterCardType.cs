namespace ServerYGO.Data.Entities
{
    public class TranslatedMonsterCardType
    {
        public int Id { get; set; }

        public int OriginalMonsterCardTypeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedName { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
