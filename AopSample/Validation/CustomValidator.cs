using AopSample.Helper;

namespace AopSample.Validation
{
    public abstract class CustomValidator<T> : ICustomValidator<T>, ICustomValidator
    {
        public void Validate(object instance, ActionType actionType) {
            Validate((T)instance, actionType);
        }

        public abstract void Validate(T instance, ActionType actionType);
    }
}