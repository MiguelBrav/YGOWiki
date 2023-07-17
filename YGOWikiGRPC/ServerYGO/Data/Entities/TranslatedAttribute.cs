namespace ServerYGO.Data.Entities
{
    public class TranslatedAttribute
    {
        public int Id { get; set; }

        public int OriginalAttributeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedName { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
