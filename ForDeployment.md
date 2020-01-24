# **Steps To Deploy**
## Class Project
1. In the directory `\TheAlphabytes\class_project\class_project\Web.config` change
    ```xml
    <connectionStrings>
    </connectionStrings>
    ```
    to
    ```xml
    <connectionStrings configSource="Web_deploy.config">
    </connectionStrings>
    ```

1. In the directory `\TheAlphabytes\class_project\class_project\DAL\CPDBContext.cs` change
    ```csharp
    public CPDBContext()
        : base("name=CPDBContext")
    {
    }
    ```
    to
    ```csharp
    public CPDBContext()
        : base("name=CPDBContext_Azure")
    {
    }
    ```
1. In the directory `\TheAlphabytes\class_project\class_project\Models\IdentityModels.cs` change
    ```csharp
    public ApplicationDbContext()
        : base("CPDBContext", throwIfV1Schema: false)
    {
        Database.SetInitializer<ApplicationDbContext>(null);
    }
    ```
    to
    ```csharp
    public ApplicationDbContext()
        : base("CPDBContext_Azure", throwIfV1Schema: false)
    {
        Database.SetInitializer<ApplicationDbContext>(null);
    }
    ```
