# ToDo Tasks Back-End

**Setup Guide:**

1. Make sure you have dotnet SDK installed on your machine
[Installation guide here](https://docs.microsoft.com/en-us/dotnet/core/install/linux)
2. Clone repository code to your desired location via `git clone https://github.com/Krok-Todo-App/Todo-Back-End`
3. Go to src direcory `cd src/`
4. Restore packages via `dotnet restore`
5. Change connection strings in appsetting.json and appsetting.Development.json files to match you configuration
6. Publish the app via `dotnet publish -c Release -o 'Your output folder path'`

> Your app is now ready, but it needs to be configured with nginx web-server
You can refer to [this](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-5.0) article, but short instructions are listed below:

1. To configure Nginx as a reverse proxy to forward HTTP requests to your ASP.NET Core app, modify /etc/nginx/sites-available/default. Open it in a text editor, and replace the contents with the following snippet:

```
server {
    listen        80;
    server_name   todotask.dev *.todotask.dev;
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```
2. Once the Nginx configuration is established, run sudo nginx -t to verify the syntax of the configuration files. If the configuration file test is successful, force Nginx to pick up the changes by running sudo nginx -s reload.

> also, you can access api documentation by visiting todotask.dev/swagger/v1/swagger.json

To directly run the app on the server:
1. Navigate to the app's directory.
2. Run the app: dotnet <app_assembly.dll>, where app_assembly.dll is the assembly file name of the app.

Now - you should be done! <br>

**Good luck!**
