using Bound.Tablet.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bound.Tablet
{


    // To Run: make a call to:   [GET] ""

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<ChatHub>(); // Register your custom service

            services.AddTransient<ChatHub>();


        }

        //https://boundhub.azurewebsites.net/send?name=Daniel%20Carlson%20H%C3%B6rnlund&weight=333&reps=35&machinename=ChestMachine&status=online

        //http://localhost:5000/send?text=Detta%20%C3%A4r%20textdfsdff&name=Dandffdfiel%20Carlsdff&machinename=ChestMachine&reps=45sdff

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ChatHub chatHub)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/myHub");
                endpoints.MapGet("/send", async context =>
                {
                    var hubContext = context.RequestServices.GetRequiredService<IHubContext<ChatHub>>();

                    var name = context.Request.Query["name"];
                    var reps = context.Request.Query["reps"];
                    var status = context.Request.Query["status"];
                    var machineName = context.Request.Query["machinename"];
                    var weight = context.Request.Query["weight"];

                    await hubContext.Clients.All.SendAsync("broadcastMessage", "Tablet", $"{name},{reps},{machineName},{status},{weight}");
                    await context.Response.WriteAsync("Data sent via HTTP.");
                });

            });
        }

    }
}
