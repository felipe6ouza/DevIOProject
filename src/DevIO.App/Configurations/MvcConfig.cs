using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.App.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor fornecido é inválido para este campo");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Esse campo deve ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Esse campo deve ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisção não esteja vazio.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "o valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "o valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser númerico.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo deve ser preenchido.");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "o valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser númerico.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "o valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser númerico.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo deve ser preenchido.");


            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services;
        }
    }
}
