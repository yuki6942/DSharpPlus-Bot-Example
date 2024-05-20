

# Discord Bot Template Repository

This is a template repository for creating a Discord bot using C#. The bot is built with the following packages:

- DSharpPlus
- DSharpPlus.Commands
- DSharpPlus.Interactivity
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.Configuration.EnvironmentVariables
- Microsoft.Extensions.Configuration.UserSecrets
- Npgsql.EntityFrameworkCore.PostgreSQL

## Setup Instructions

To set up this template repository, follow these steps:

1. Clone the repository to your local machine.
2. Navigate to the project directory:
   ```
   cd DSharpPlus-Bot-Example/DSharpPlus-Bot-Example
   dotnet user-secrets init
   dotnet user-secrets set "DB_STRING" "YOUR_NPSQL_STRING"
   dotnet user-secrets set "DB_STRING" "YOUR_POSTGRES_STRING

### Contributing
Feel free to fork this repository and make improvements. Pull requests are welcome.

### License
This project is licensed under the [MIT License](https://github.com/yuki6942/DSharpPlus-Bot-Example/blob/main/LICENSE).
