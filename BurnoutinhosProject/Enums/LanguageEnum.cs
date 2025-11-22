using System.ComponentModel.DataAnnotations;


namespace BurnoutinhosProject.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "PT-BR")]
        PT_BR=0,
        
        [Display(Name = "EN-US")]
        EN_US=1,
        
        [Display(Name = "ES-ES")]
        ES_ES=2,
        [Display(Name = "FR-FR")]
        FR_FR=3,
        
        [Display(Name = "DE-DE")]
        DE_DE=4,
        
        [Display(Name = "IT-IT")]
        IT_IT=5,
        
        [Display(Name = "PT-PT")]
        PT_PT=6
    }
}
