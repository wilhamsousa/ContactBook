using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using System.Text.Json.Serialization;

namespace Uex.ContactBook.Domain.Model.Base
{
    public abstract class BaseEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }

        [NotMapped, JsonIgnore]
        public bool Valid { get; protected set; }

        [NotMapped, JsonIgnore]
        public bool Invalid { get => !Valid; }

        public void SetId(Guid id) => Id = id;



        [NotMapped, JsonIgnore]
        public FluentValidation.Results.ValidationResult? ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }

        public string NotificationMessage()
        {
            if (ValidationResult != null && ValidationResult.Errors != null && ValidationResult.Errors.Any() )
                return ValidationResult.Errors.FirstOrDefault().ErrorMessage;

            return string.Empty;
        }

    }
}
