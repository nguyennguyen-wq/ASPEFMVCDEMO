



using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebMVCDemo.Data;
using System;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMVCDemo.Data;
using WebMVCDemo.Models;

namespace WebMVCDemo.Models
{
    public static class CodeFile
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebMVCDemoContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WebMVCDemoContext>>()))
            {
                // Look for any movies.
                if (context.Medlem.Any())
                {
                    return;   // DB has been seeded
                }

                context.Medlem.AddRange(
                    new Medlem
                    {

                        Medlem_Id = Guid.NewGuid(),

                        //Medlem_Id = new Guid("d81e0829-55fa-4c37-b62f-f578c692af78"),

                        Fornavn = "Western",

                        Etternavn = "Western",

                        Bosted = "Western",

                        MobilTlf = 4654654,

                        Email = "Western@com",

                        Fodselsdato = DateTime.Parse("1959-4-15"),


                        CurrentKontintId = 2,

                    }



                );

                context.SaveChanges();
            }
        }
    }
}