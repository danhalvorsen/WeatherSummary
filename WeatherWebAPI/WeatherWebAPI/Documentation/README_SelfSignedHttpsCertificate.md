# Make a Self Signed Https Certificate

In a terminal window in the root of your project, run the following commands to add a self signed certificate:

```cli
$ mkdir folderName
$ dotnet dev-certs https --clean
$ dotnet dev-certs https -ep ./folderName/https/certficateName.pfx -p YourPassword
$ dotnet dev-certs https --trust
```

**Remember to add the correct certificate name and password to the docker-compose.yml and docker-compose.override.yml files as shown in the [Visual Studio w/ docker-compose](/WeatherWebAPI/WeatherWebAPI/README_VisualStudioSetup.md) file.**

[Go back](/README.md/#backend-setup)