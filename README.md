
<h1 id="title" align="center">Blender Paradise</h1>


<p id="description">The idea of the project is all about free blender models sharing.</p>

  [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=dpS1lence_3D-It-&amp;metric=alert_status)](https://sonarcloud.io/summary/new_code?id=dpS1lence_3D-It-)
  
<h2>🧐 Features</h2>

Here're some of the project's best features:

*   Daily modeling challenge (updates daily)
*   User - friendly design

<h2>🛠️ Installation Steps:</h2>

<p>1. Before you start the application you must add your own appsettings.json with a valid connection string.</p>

<p>2. Also you must replace the AzureFileService with a LocalFileService if you dont want to use Azure.</p>

```
builder.Services.AddScoped<IFileService>(_ => new AzureFileService(builder.Configuration.GetConnectionString("BlobStorageConnection")));
```

<p>3. Here is the code example:</p>

```
builder.Services.AddScoped<IFileService>(_ => new LocalStorageFileService(builder.Environment.WebRootPath));
```
  
<h2>💻 Built with</h2>

Technologies used in the project:

*   ASP.Net
*   Azure
*   Bootstrap
*   Sonar Cloud
*   Entity Framework Core
*   NUnit

<h2>🛡️ License:</h2>

This project is licensed under the Apache License 2.0
