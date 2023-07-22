namespace ServerYGO.Data.Entities
{
    public class TranslatedMonsterType
    {
        public int Id { get; set; }

        public int OriginalMonsterTypeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedName { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
