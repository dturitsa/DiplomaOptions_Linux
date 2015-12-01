


dnx ef migrations add OptionsMigration --context OptionsContext

dnx ef database update --context OptionsContext

dnx ef migrations add IdentityMigration --context ApplicationDbContext

dnx ef database update --context ApplicationDbContext