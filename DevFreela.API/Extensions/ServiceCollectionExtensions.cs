using DevFreela.API.Filter;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Consumers;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Service;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Payments;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectCommentRepository, ProjectCommentRepository>();

            services.AddHttpClient();
            services.AddMediatR(typeof(CreateProjectCommand));

            services.AddHostedService<PaymentApprovedConsumer>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMessageBusService, MessageBusService>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddControllers(
                        options => options.Filters.Add(typeof(ValidationFilter))
                        )
                        .AddFluentValidation(fluentValidation =>
                    fluentValidation.RegisterValidatorsFromAssemblyContaining<CreateUserInputModelValidator>());

            return services;
        }

    }
}
