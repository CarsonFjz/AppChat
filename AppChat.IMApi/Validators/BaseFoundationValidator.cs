using FluentValidation;

namespace AppChat.IMApi.Validators.Home
{
    public abstract class BaseFoundationValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseFoundationValidator()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
    }
}