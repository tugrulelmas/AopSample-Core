using AopSample.Helper;

namespace AopSample.Validation
{
    public interface ICustomValidator
    {
        void Validate(object instance, ActionType actionType);
    }

    public interface ICustomValidator<in T>
    {
        void Validate(T instance, ActionType actionType);
    }
}