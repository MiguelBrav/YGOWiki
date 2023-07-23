namespace ServerYGO.Data.Entities
{
    public class TranslatedSpecialMonsterType
    {
        public int Id { get; set; }

        public int OriginalSpecialMonsterTypeId { get; set; }

        public string LanguageId { get; set; }

        public string TranslatedName { get; set; }

        public string TranslatedDescription { get; set; }

        public string ImageExample { get; set; }
    }
}
