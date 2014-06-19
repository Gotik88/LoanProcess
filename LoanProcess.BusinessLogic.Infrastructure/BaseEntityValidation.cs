// ============================================================================
// <copyright file="BaseEntityValidation.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using LoanProcess.BusinessLogic.BusinessRules;

    public abstract partial class BaseEntity
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();

        private IList<ValidationError> _validationErrors;

        private IEnumerable<PropertyInfo> _dataProperties;

        private IEnumerable<PropertyInfo> DataProperties
        {
            get
            {
                return this._dataProperties ?? (this._dataProperties = from p in this.GetType().GetProperties()
                                                                       where (p.GetCustomAttributes(typeof(AssociationAttribute), true).Any() ||
                                                                       p.GetCustomAttributes(typeof(DataMemberAttribute), true).Any() ||
                                                                       p.GetCustomAttributes(typeof(ValidationAttribute), true).Any())
                                                                       select p);
            }
        }

        private new IEnumerable<ValidationError> ValidationErrors
        {
            get
            {
                return _validationErrors ?? new List<ValidationError>();
            }
        }

        protected virtual IEnumerable<ValidationError> Validate()
        {
            ClearValidationErrors();
            ValidateBusinessRules();
            ValidateAllProperties();

            return ValidationErrors;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        private void ValidateBusinessRules()
        {
            _brokenRules.ForEach(rule =>
            {
                if (_validationErrors.All(x => x.PropertyName != rule.Id.ToString()))
                {
                    _validationErrors.Add(new ValidationError(rule.Id.ToString(), rule.Rule));
                }
            });
        }

        private bool ValidateAllProperties()
        {
            foreach (var property in this.DataProperties)
            {
                if (_validationErrors.All(x => x.PropertyName != property.Name))
                {
                    ////ValidateProperty(property.Name, property.GetValue(this, new object[0]));
                }
            }

            ValidateEntityWithAnnotation();

            return !ValidationErrors.Any();
        }

        private void ClearValidationErrors()
        {
            if (_validationErrors == null)
            {
                _validationErrors = new List<ValidationError>();
            }

            _validationErrors.Clear();
        }

        private void ValidateEntityWithAnnotation()
        {
            var validationAttributes = GetType().GetCustomAttributes(typeof(ValidationAttribute), false).OfType<ValidationAttribute>();
            var validationContext = new ValidationContext(this);

            foreach (var validationAttribute in validationAttributes)
            {
                var validationResult = validationAttribute.GetValidationResult(this, validationContext);

                if (validationResult != null && validationResult != ValidationResult.Success)
                {
                    validationResult.MemberNames.ToList().ForEach(memberName =>
                    {
                        if (_validationErrors.All(x => x.PropertyName != memberName))
                        {
                            _validationErrors.Add(new ValidationError(validationResult.ErrorMessage, memberName));
                        }
                    });
                }
            }
        }
    }
}
