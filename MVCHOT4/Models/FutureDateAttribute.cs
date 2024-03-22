using System.ComponentModel.DataAnnotations;

namespace MVCHOT4.Models
{
	public class FutureDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			DateTime dateTime = (DateTime)value;
			if (dateTime < DateTime.Now)
			{
				return new ValidationResult(ErrorMessage);
			}
			return ValidationResult.Success;
		}
	}
}
