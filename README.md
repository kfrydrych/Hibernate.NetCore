# Hibernate.NetCore
NHibernate helpers for .NET Core

**Fluent NHibernate**
-----------------------------------
```
services.AddHibernate(Configuration) // Starting point allowing access to IConfiguration (Step 1)
        .UseSqlDatabaseWithConnectionStringName("DefaultConnection") // Sets SQL Driver and points to the right connection string (Step 2)
        .WithMappingsFromAssemblyOf<Startup>() // Uses class as a marker for assembly containing mappings (Step 3a)
        .WithMappingsFromAssembly(Assembly.GetExecutingAssembly()) // Or uses mappings from assembly provided (Step 3b)
        .ConfigureMappings(m =>  // Or uses custom configurations (Step 3c)
        {
            m.FluentMappings.AddFromAssemblyOf<Startup>()
                .Conventions.Add(ForeignKey.EndsWith("Id"))
                .Conventions.Add<HiLoConvention>();
        })
        .Build(); // Register ISessionFactory as singleton and ISession as scoped (Step 4)          
        // Use one version of step 3
        ```
