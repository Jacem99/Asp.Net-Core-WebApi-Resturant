using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResturantWihtUOF.Core.Models;
using Microsoft.AspNetCore.Identity;
using ResturantWihtUOF.Core.Consts;
using ResturantWihtUOF.Core.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWihtUOF.EF.Services
{
   public class SeedingData
    {
        public async static void Seed(IApplicationBuilder applicationBuilder )
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if (!await context.Tables.AnyAsync())
                {
                    List<Tables> tables = new List<Tables>() {
                       new Tables (){Number =1 , TotalPeaple =1},
                       new Tables (){Number =2 , TotalPeaple =1},
                       new Tables (){Number =3 , TotalPeaple =2},
                       new Tables (){Number =4 , TotalPeaple =2},
                       new Tables (){Number =5 , TotalPeaple =2},
                       new Tables (){Number =6 , TotalPeaple =2},
                       new Tables (){Number =7 , TotalPeaple =3},
                       new Tables (){Number =8 , TotalPeaple =5},
                       new Tables (){Number =9 , TotalPeaple =7},
                       new Tables (){Number =10 , TotalPeaple =8},
                       new Tables (){Number =11 , TotalPeaple =8},
                       new Tables (){Number =12 , TotalPeaple =6},
                       new Tables (){Number =13 , TotalPeaple =4},
                       new Tables (){Number =14 , TotalPeaple =7},
                       new Tables (){Number =15 , TotalPeaple =6},
                       new Tables (){Number =16 , TotalPeaple =4},
                       new Tables (){Number =17 , TotalPeaple =4},
                       new Tables (){Number =18 , TotalPeaple =5},
                       new Tables (){Number =19 , TotalPeaple =6},
                       new Tables (){Number =20 , TotalPeaple =2},
                    };
                   await context.Tables.AddRangeAsync(tables);
                    await context.SaveChangesAsync();
                }

               if (!await context.Customers.AnyAsync())
                {
                    List<Customers> customers = new List<Customers>() {
                       new Customers (){Name ="ahmed" ,Phone="01245469795"},
                       new Customers (){Name ="yousef" ,Phone="01246548665"},
                   
                    };
                    await context.Customers.AddRangeAsync(customers);
                    await context.SaveChangesAsync();
                }
                /////////////////////
                if (!await context.Sections.AnyAsync())
                {
                    List<Sections> sections = new List<Sections>()
                    {
                        new Sections(){ Name ="المدير"},
                        new Sections(){ Name ="الاستقبال"},
                        new Sections(){ Name ="التوصيل"},
                        new Sections(){ Name ="الطلبات"},
                        new Sections(){ Name ="المطبخ"},
                        new Sections(){ Name ="التنظيف"},
                        new Sections(){ Name ="المستهلك"},
                    };
                    await context.Sections.AddRangeAsync(sections);
                    await context.SaveChangesAsync();
                }
                /////////////////////
                if (!await context.RolesResturant.AnyAsync())
                {
                    int admin = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "المدير")).Id;
                    int reciption = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "الاستقبال")).Id;
                    int delivery = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "التوصيل")).Id;
                    int orders = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "الطلبات")).Id;
                    int kitchen = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "المطبخ")).Id;
                    int clearing = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "التنظيف")).Id;
                    int cusomer = (await context.Sections.SingleOrDefaultAsync(i => i.Name == "المستهلك")).Id;

                    List<RolesResturant> rolesResturant = new List<RolesResturant>()
                    {
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "الأدمن" , NormalizedName ="الأدمن" ,ConcurrencyStamp=Guid.NewGuid().ToString() ,SectionsId = admin},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "كابتن صاله" ,NormalizedName ="كابتن صاله",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId =reciption},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "الكاشير",NormalizedName ="الكاشير",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId=reciption},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "الويتر",NormalizedName ="الويتر",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId=reciption},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "الشيف",NormalizedName ="الشيف",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId=kitchen},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "الدليفري",NormalizedName ="الدليفري",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId=delivery},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "استيور",NormalizedName ="استيور",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId=clearing},
                        new RolesResturant(){Id=Guid.NewGuid().ToString(),Name = "الزبون",NormalizedName ="الزبون",ConcurrencyStamp=Guid.NewGuid().ToString(),SectionsId=cusomer},
                    };
                   await context.RolesResturant.AddRangeAsync(rolesResturant);
                   
                }
                ///////////////////////
            /*    if (!await context.ApplicationUsers.AnyAsync())
                {
                    List<ApplicationUser> applicationUsers = new List<ApplicationUser>()
                    {
                        new ApplicationUser(){Id=Guid.NewGuid().ToString(),FirstName="Mustafa" ,LastName ="kasem",Email="mustafa12@gmail.com",PhoneNumber="01551184306"},
                        new ApplicationUser(){Id=Guid.NewGuid().ToString(),FirstName="Ahmed" ,LastName ="hassan",Email="ahmed12@gmail.com",PhoneNumber="01224556789"},
                        new ApplicationUser(){Id=Guid.NewGuid().ToString(),FirstName="khalid" ,LastName ="mustafa",Email="khalid12@gmail.com",PhoneNumber="01265856789"},
                        new ApplicationUser(){Id=Guid.NewGuid().ToString(),FirstName="Sameh" ,LastName ="mahamed",Email="sameh12@gmail.com",PhoneNumber="01287945789"},
                        new ApplicationUser(){Id=Guid.NewGuid().ToString(),FirstName="mahamed" ,LastName ="ahmed",Email="mahamed12@gmail.com",PhoneNumber="01224458969"},
                      };
                    foreach(var user in applicationUsers)
                    {
                        await _userManager.CreateAsync(user, "resturant_99");
                    }
                    
                }*/

               /* if (!await context.UserRoles.AnyAsync())
                {
                    ApplicationUser mustafa = await _userManager.FindByEmailAsync("mustafa12@gmail.com");
                    ApplicationUser ahmed = await _userManager.FindByEmailAsync("ahmed12@gmail.com");
                    ApplicationUser khalid = await _userManager.FindByEmailAsync("khalid12@gmail.com");
                    ApplicationUser mahamed = await _userManager.FindByEmailAsync("mahamed12@gmail.com");
                    ApplicationUser sameh = await _userManager.FindByEmailAsync("sameh12@gmail.com");

                   await _userManager.AddToRoleAsync(mustafa, RolesUser.Admin);
                   await _userManager.AddToRoleAsync(ahmed, RolesUser.Casheer);
                   await _userManager.AddToRoleAsync(khalid, RolesUser.Chief);
                   await _userManager.AddToRoleAsync(mahamed, RolesUser.Waiter);
                   await _userManager.AddToRoleAsync(sameh, RolesUser.Steior);
                  
                }*/
                /////////////////////
                if (!await context.Ingredient.AnyAsync())
               {
                   List<Ingredient> ingredients = new List<Ingredient>() {
                      new Ingredient (){Name ="طواجن" ,Description="الأكله المفضله لشبين الكوم"},
                      new Ingredient (){Name ="الكشري" , Description ="لما ملاقيش حاجه ناكلها"},
                      new Ingredient (){Name ="الكربات" ,Description="لما نيجي نغشم "},
                      new Ingredient (){Name ="الحلويات" ,Description ="دا للناس التوته دي مش لنا"},
                      new Ingredient (){Name ="الوجبات" ,Description="والله الواحد صعبان عليه حاله"},
                      new Ingredient (){Name ="المشروبات البارده" ,Description="دي للشتاء بس"},
                      new Ingredient (){Name ="المشروبات الغازيه" ,Description="دي معظمها في الشتاء برده"},
                      new Ingredient (){Name ="المشروبات الساخنه" ,Description="ايوه بقي دي الحاجه الدافيه اللي مهونه"},

                   };
                  await context.Ingredient.AddRangeAsync(ingredients);
                    await context.SaveChangesAsync();
                }
                /////////////////////
                if (!await context.Meal.AnyAsync())
                {
                    int tagen = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "طواجن")).Id;
                    int cosharie = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "الكشري")).Id;
                    int creep = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "الكربات")).Id;
                    int sweets = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "الحلويات")).Id;
                    int meal = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "الوجبات")).Id;
                    int bared = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "المشروبات البارده")).Id;
                    int gasy = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "المشروبات الغازيه")).Id;
                    int sakhen = (await context.Ingredient.SingleOrDefaultAsync(i => i.Name == "المشروبات الساخنه")).Id;
                  
                    List<Meal> meals = new List<Meal>() {
                      new Meal (){Name ="طاجن فراخ",Price=30,IngredientId =tagen },
                      new Meal (){Name ="طاجن كبده ",Price=20,IngredientId =tagen},
                      new Meal (){Name ="طاجن كبده مودزيريلا",Price=25,IngredientId = tagen},
                      new Meal (){Name ="طاجن لحمه",Price=20,IngredientId = tagen},
                      new Meal (){Name ="طاجن لحمه مودزيريلا",Price=25,IngredientId = tagen},
                      new Meal (){Name ="طاجن مكرونه بشاميل",Price=20,IngredientId = tagen},

                      new Meal (){Name ="كشري عادي",Price=15,IngredientId= cosharie},
                      new Meal (){Name ="كشري بالكبده",Price=20,IngredientId= cosharie},
                      new Meal (){Name ="كشري بالحمه",Price=20,IngredientId= cosharie},

                      new Meal (){Name ="كريب زنجر",Price=40,IngredientId= creep},
                      new Meal (){Name ="كريب شاورما",Price=40,IngredientId= creep},
                      new Meal (){Name ="كريب كريسبي",Price=40,IngredientId= creep},
                      new Meal (){Name ="كريب شيش طاوو",Price=45,IngredientId= creep},
                      new Meal (){Name ="كريب بانيه",Price=40,IngredientId= creep},
                      new Meal (){Name ="كريب الغشيم",Price=55,IngredientId= creep},

                      new Meal (){Name ="بسبوسه",Price=25,IngredientId= sweets},
                      new Meal (){Name ="رز بلبل",Price=15,IngredientId= sweets},
                      new Meal (){Name ="كوسكوسي",Price=25,IngredientId= sweets},
                      new Meal (){Name ="ديسباسيتو",Price=45,IngredientId= sweets},
                      new Meal (){Name ="كريم كراميل",Price=30,IngredientId= sweets},
                      new Meal (){Name ="كنافه بالمانجه",Price=25,IngredientId= sweets},
                      new Meal (){Name ="كنافه نابلسي",Price=25,IngredientId= sweets},


                      new Meal (){Name ="وجبه الغشيم",Price=80,IngredientId= meal},
                      new Meal (){Name ="وجبه فراخ",Price=75,IngredientId= meal},
                      new Meal (){Name ="وجبه لحمه",Price=75,IngredientId= meal},

                      new Meal (){Name ="قصب",Price=5,IngredientId= bared},
                      new Meal (){Name ="مانجه",Price=15,IngredientId= bared},
                      new Meal (){Name ="كركاريه",Price=10,IngredientId= bared},
                      new Meal (){Name ="عناب",Price=10,IngredientId= bared},
                      new Meal (){Name ="اناناس",Price=15,IngredientId= bared},
                      new Meal (){Name ="سموزي",Price=25,IngredientId= bared},

                      new Meal (){Name ="كانز",Price=10,IngredientId= gasy},
                      new Meal (){Name ="كوكاكولا",Price=10,IngredientId= gasy},
                      new Meal (){Name ="سيفن",Price=10,IngredientId= gasy},
                      new Meal (){Name ="استينج",Price=10,IngredientId= gasy},
                      new Meal (){Name ="فروتز",Price=10,IngredientId= gasy},

                       new Meal (){Name ="شاي",Price=5,IngredientId=sakhen},
                      new Meal (){Name ="يانسون",Price=5,IngredientId=sakhen},
                      new Meal (){Name ="قهوه",Price=20,IngredientId=sakhen},
                      new Meal (){Name ="قهوه فرنساوي",Price=25,IngredientId=sakhen},
                      new Meal (){Name ="نسكافيه عادي",Price=15,IngredientId=sakhen},
                      new Meal (){Name ="نسكافيه باللبن",Price=25,IngredientId=sakhen},
                      new Meal (){Name ="لاتيه",Price=20,IngredientId=sakhen},

                   };
                  await context.Meal.AddRangeAsync(meals);
                   
                }
                /////////////////////
                if (!await context.ResturantNeeds.AnyAsync())
                {
                    List<ResturantNeeds> resturantNeeds = new List<ResturantNeeds>() {
                       new ResturantNeeds (){Name ="أطباق الفيل",Kilo=250},
                       new ResturantNeeds (){Name ="أكياس",Kilo=250},
                       new ResturantNeeds (){Name ="ورق سولفان",Kilo=130},
                       new ResturantNeeds (){Name ="لحمه",Kilo=130},
                       new ResturantNeeds (){Name ="سكر",Kilo=300},
                       new ResturantNeeds (){Name ="كانز",Quantity=80},
                       new ResturantNeeds (){Name ="سيفن",Quantity=80},
                       new ResturantNeeds (){Name ="مانجه",Kilo = 70},
                       new ResturantNeeds (){Name ="قصب",Quantity=50},
                       new ResturantNeeds (){Name ="شاي",Kilo = 252},
                       new ResturantNeeds (){Name ="شوكلاته",Kilo = 30},
                    };
                   await context.ResturantNeeds.AddRangeAsync(resturantNeeds);
                    
                }
                /////////////////////
                if (!await context.TypeOfOrder.AnyAsync())
                {
                    List<TypeOfOrder> typeOfOrder = new List<TypeOfOrder>() {
                       new TypeOfOrder (){TypeOrder ="دليفري" },
                       new TypeOfOrder (){TypeOrder ="حجز" },
                      
                    };
                    await context.TypeOfOrder.AddRangeAsync(typeOfOrder);

                }
                ///////////////////////////////////////
                if (!await context.TypeOfReservation.AnyAsync())
                {
                    List<TypeOfReservation> typeOfReservations = new List<TypeOfReservation>() {
                       new TypeOfReservation (){TypeReservation ="في المطعم" },
                       new TypeOfReservation (){TypeReservation ="تيك اويي" },
                     
                    };
                    await context.TypeOfReservation.AddRangeAsync(typeOfReservations);

                }
                /////////////////////

                if (!await context.WorkingPeroid.AnyAsync())
                {
                    List<WorkingPeroid> workingPeroid = new List<WorkingPeroid>() {
                       new WorkingPeroid (){Period ="مسائي" },
                       new WorkingPeroid (){Period ="صباحي" },
                    };
                    await context.WorkingPeroid.AddRangeAsync(workingPeroid);
                }
                await context.SaveChangesAsync();
            }
        }

    }
}
