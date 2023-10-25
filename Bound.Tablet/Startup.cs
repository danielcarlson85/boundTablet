using Bound.Tablet.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bound.Tablet
{


    // To Run: make a call to:   [GET] "http://localhost:5000/send?text=Detta%20%C3%A4r%20textdfsdff&name=Dandffdfiel%20Carlsdff&machinename=ChestMachine&reps=45sdff"

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapGet("/send", async context =>
                {
                    var hubContext = context.RequestServices.GetRequiredService<IHubContext<ChatHub>>();

                    var name = context.Request.Query["name"];
                    var text = context.Request.Query["text"];
                    var reps = context.Request.Query["reps"];
                    var machineName = context.Request.Query["machinename"];

                    await hubContext.Clients.All.SendAsync("broadcastMessage", "Tablet", $"{name},{text},{reps},{machineName}");
                    await context.Response.WriteAsync("Data sent via HTTP.");
                });

            });
        }
    }
}
