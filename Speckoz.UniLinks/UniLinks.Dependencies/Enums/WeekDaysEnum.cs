using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniLinks.Dependencies.Enums
{
    public enum WeekDaysEnum
    {
        [Display(Name = "Domingo")]
        Sunday = 1,

        [Display(Name = "Segunda-Feira")]
        Monday,

        [Display(Name = "Terça-Feira")]
        Tuesday,

        [Display(Name = "Quarta-Feira")]
        Wednesday,

        [Display(Name = "Quinta-Feira")]
        Thursday,

        [Display(Name = "Sexta-Feira")]
        Friday,

        [Display(Name = "Sabado")]
        Saturday,

        [Display(Name = "Segunda a Sexta")]
        AllValid
    }
}