namespace UsersArbolusAPI.Options
{
    public record ArbolusApiOptions
    {
        public const string SectionName = "ArbolusApiConfig";
        public string ApiBaseUrl { get; set; } = string.Empty;
    }
}
