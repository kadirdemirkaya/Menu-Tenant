# Menu-Tenant

This project is a multi-tenant example project

In the project scenario, more than one business wants a menu service and also we purpose to develop in one project base

## Features

- Users can a register and during register can want personal database or shared database service.

- Registered people can create up to 10 menus and there can only be one active menu

- Menu requests can be distinguished by company names added during registration

- Thanks to the token or company name, it is known which database path it will work on

## Library Used

- **EventBusDomain**

  - **Explanation**: This event based library generally was used for apply cqrs and working event based

  - **Usage Example**: You can give an assembly reference and then can inject:
    ```csharp
    services.AddEventBus(assemblies);
    ```

- **Base.Caching**

  - **Explanation**: This library does caching and supports both in-memory and distributed caching.

  - **Usage Example**: You can give an assembly reference and then inject the caching services as follows:
    ```csharp
    services.AddCaching(configuration, new CacheConfiguration()
    {
        baseCacheConfiguration = new()
        {
            DefaultCacheTime = 60,
            ShortTermCacheTime = 3
        },
        baseDistributedCacheConfiguration = new()
        {
            Enabled = true,
            ConnectionString = "",
            InstanceName = "Caching"
        }
    });
    ```

- **SecretManagement**

  - **Explanation**: A lightweight library for managing secrets, supporting AWS Secrets Manager and HashiCorp Vault for secure storage and retrieval.

  - **Usage Example**: You can access and use it directly from the service or factory service:
    ```csharp
    services.AddSingleton<ISecretManagementFactory, SecretManagementFactory>()
    services.AddSingleton<ISecretsManagerService, AwsSecretsManagerService>();
    ```

## Katmanlar

#### 1. **Shared**

- Contains properties, models, and utility functions that are used across all layers of the application. This layer helps to avoid code duplication and ensures consistency in shared data structures and logic
- Redis stream is develop for distributed events
- There is a project for health control

#### Auth

- Includes users will receive menu service
- There is a job service in auth for personal database

#### Tenant

- There is menu relevant jobs

### Database

- All database operations (creation, deletion, etc.) take place here

## Current Shortcomings

- [ ] The project can be supported with email services. User can be informed instantly.
- [ ] QueryFilter errors in the repository service should be fixed and it can be developed as a common layer.
- [ ] CompanyName is empty and if not come tenantid we are didn't matching to urls
- [ ] Shareddb tenantId value not equals connectionpools tenantId value
- [ ] Unit Tests fell short
- [ ] No distribution testing done
