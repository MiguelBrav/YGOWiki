namespace ServerYGO.Data.Entities
{
    public class TranslatedCardTypes
    {
        public int Id { get; set; }

        public int OriginalCardTypeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedName { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
