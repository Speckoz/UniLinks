using System.ComponentModel;

namespace UniLinks.Dependencies.Enums
{
	public enum WeekDaysEnum
	{
		[Description("Domingo")]
		Sunday = 1,

		[Description("Segunda-Feira")]
		Monday,

		[Description("Terça-Feira")]
		Tuesday,

		[Description("Quarta-Feira")]
		Wednesday,

		[Description("Quinta-Feira")]
		Thursday,

		[Description("Sexta-Feira")]
		Friday,

		[Description("Sábado")]
		Saturday,

		[Description("Segunda a Sexta")]
		AllValid
	}
}