# C# Forum Project

### Project Structure
<pre>
BBSforum
├─ .git
├─ .gitignore
├─ DbSet.session.sql
└─ forum
   ├─ appsettings.Development.json
   ├─ appsettings.json
   ├─ Controllers                     # APP CONTROLLERS
   │  ├─ AdminController.cs
   │  ├─ AuthController.cs
   │  ├─ CommentController.cs
   │  ├─ HomeController.cs
   │  ├─ MessageController.cs
   │  ├─ PostController.cs
   │  ├─ TagController.cs
   │  └─ UserController.cs
   ├─ Data                            # HANDLING CONNECTION WITH DATABASE
   │  └─ ForumDbContext.cs
   ├─ forum.csproj
   ├─ Forum.Data.db
   ├─ Forum.Data.db-shm
   ├─ Forum.Data.db-wal
   ├─ forum.sln
   ├─ Migrations
   │  ├─ 20230901212736_InitialCreate.cs
   │  ├─ 20230901212736_InitialCreate.Designer.cs
   │  └─ ForumDbContextModelSnapshot.cs
   ├─ Models                                  # APP MODELS 
   │  ├─ Comment.cs
   │  ├─ ErrorViewModel.cs
   │  ├─ Message.cs
   │  ├─ Post.cs
   │  ├─ Tag.cs
   │  └─ User.cs
   ├─ Program.cs                              # APP ENTRY 
   ├─ Properties                              # SOME SETTINGS  
   │  └─ launchSettings.json
   ├─ Views                                   # APP VIEWS
   │  ├─ Admin
   │  │  └─ Index.cshtml
   │  ├─ Auth
   │  │  ├─ AccountCreated.cshtml
   │  │  ├─ ChangePassword.cshtml
   │  │  ├─ Error.cshtml
   │  │  ├─ PasswordChanged.cshtml
   │  │  ├─ SignIn.cshtml
   │  │  └─ SignUp.cshtml
   │  ├─ Comment
   │  │  ├─ Create.cshtml
   │  │  ├─ Delete.cshtml
   │  │  ├─ Index.cshtml
   │  │  └─ PostComments.cshtml
   │  ├─ Home
   │  │  ├─ Index.cshtml
   │  │  ├─ IndexLoggedIn.cshtml
   │  │  └─ Search.cshtml
   │  ├─ Message
   │  │  ├─ Create.cshtml
   │  │  └─ Index.cshtml
   │  ├─ Post
   │  │  ├─ Create.cshtml
   │  │  ├─ Delete.cshtml
   │  │  ├─ Details.cshtml
   │  │  ├─ Edit.cshtml
   │  │  └─ Index.cshtml
   │  ├─ Shared
   │  │  ├─ Error.cshtml
   │  │  ├─ _Layout.cshtml
   │  │  ├─ _Layout.cshtml.css
   │  │  └─ _ValidationScriptsPartial.cshtml
   │  ├─ Tag
   │  │  ├─ Create.cshtml
   │  │  └─ Index.cshtml
   │  ├─ User
   │  │  ├─ Create.cshtml
   │  │  ├─ Delete.cshtml
   │  │  ├─ Details.cshtml
   │  │  ├─ Edit.cshtml
   │  │  ├─ Index.cshtml
   │  │  └─ Profile.cshtml
   │  ├─ _ViewImports.cshtml
   │  └─ _ViewStart.cshtml
   └─ wwwroot
      ├─ css
      │  ├─ js
      │  │  └─ site.js
      │  └─ site.css
      ├─ favicon.ico
      ├─ js
      │  └─ site.js
      └─ lib
         ├─ bootstrap
         ├─ jquery
         ├─ jquery-validation
         └─ jquery-validation-unobtrusive
  </pre>
  
  
### the introduction of structure
BBSforum (Root Directory):

This is the root directory of your project.
## .git:
This directory contains the version control information for your project, most likely managed with Git.

## .gitignore:
It's a configuration file for Git, specifying files and directories that should be ignored when tracking changes in your project.

## DbSet.session.sql:
This might be a database related file, possibly containing SQL queries or database setup information.

## forum (Application Directory):
This is the main application directory where your ASP.NET Core application resides.
## appsettings.Development.json and appsettings.json:
These JSON files are used to store configuration settings for your application. They can include database connection strings, API keys, and other settings.

## Controllers:
This directory contains the controllers for your application. Controllers handle incoming requests, process them, and return responses. Each controller corresponds to a different part of your application's functionality.

## Data:
This directory typically contains the database context class and other database-related configuration.

## forum.csproj:
This is the project file for your ASP.NET Core application. It contains metadata about your project and its dependencies.

## Forum.Data.db, Forum.Data.db-shm, and Forum.Data.db-wal:
These files might be related to a SQLite database used by your application.

## Migrations:
This directory is related to Entity Framework Migrations and contains database migration scripts that enable you to evolve your database schema over time.

## Models:
This directory contains the model classes for your application. Models represent the data structures used in your application.

## Program.cs:
This is the entry point for your application. It contains the Main method and is responsible for setting up and configuring the web host.

## Properties:
This directory may contain project properties and configurations.

## Views:
This directory contains the Razor views for your application. Razor is the templating engine used in ASP.NET Core for generating HTML.

## wwwroot:
This is the web root directory where you place static files like CSS, JavaScript, and images that can be served directly to clients.

## lib:
This directory appears to contain external libraries or packages used by your project, such as Bootstrap, jQuery, and jQuery-validation.


