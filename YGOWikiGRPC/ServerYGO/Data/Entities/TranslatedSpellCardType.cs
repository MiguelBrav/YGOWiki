namespace ServerYGO.Data.Entities
{
    public class TranslatedSpellCardType
    {
        public int Id { get; set; }

        public int OriginalSpellCardTypeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedName { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
