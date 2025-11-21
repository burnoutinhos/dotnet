using System.ComponentModel.DataAnnotations;


namespace BurnoutinhosProject.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "PT-BR")]
        PT_BR,
        
        [Display(Name = "EN-US")]
        EN_US,
        
        [Display(Name = "ES-ES")]
        ES_ES,
        [Display(Name = "FR-FR")]
        FR_FR,
        
        [Display(Name = "DE-DE")]
        DE_DE,
        
        [Display(Name = "IT-IT")]
        IT_IT,
        
        [Display(Name = "PT-PT")]
        PT_PT
    }
}
