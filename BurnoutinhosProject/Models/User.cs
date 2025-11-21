using BurnoutinhosProject.Enums;

namespace BurnoutinhosProject.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public LanguageEnum PreferredLanguage { get; set; }

        public string ProfileImage { get; set; }
    }
}
