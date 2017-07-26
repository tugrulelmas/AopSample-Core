using AopSample.Entities;
using AopSample.Helper;
using System;

namespace AopSample.Validation
{
    public class UserValidator : CustomValidator<User>
    {
        public override void Validate(User instance, ActionType actionType) {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            if (actionType == ActionType.Add) {
                if (string.IsNullOrEmpty(instance.Name))
                    throw new Exception("UserNameIsEmpty");

                if (string.IsNullOrEmpty(instance.Email))
                    throw new Exception("UserEmailIsEmpty");
            } else {
                if (instance.Id == Guid.Empty)
                    throw new Exception("UserIdIsEmpty");
            }
        }
    }
}