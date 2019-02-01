namespace BicycleWorldShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BicycleWorldShop.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using BicycleWorldShop.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BicycleWorldShop.DAL.BicycleWorldShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BicycleWorldShop.DAL.BicycleWorldShopContext";
        }

        protected override void Seed(BicycleWorldShop.DAL.BicycleWorldShopContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var category = new List<Category>
            {
                new Category { Name = "Bikes", SubCategoryName="Road" },
                 new Category { Name = "Bikes", SubCategoryName="Mountain" },
                  new Category { Name = "Bikes", SubCategoryName="Commuter" },
                   new Category { Name = "Bikes", SubCategoryName="Comfort" },
                    new Category { Name = "Bikes", SubCategoryName="Cruiser" },
                     new Category { Name = "Bikes", SubCategoryName="Fitness" },
                      new Category { Name = "Bikes", SubCategoryName="Hybrid" },
                       new Category { Name = "Bikes", SubCategoryName="Electric" },
                        new Category { Name = "Bikes", SubCategoryName="Childrens" },

                        new Category { Name = "Parts", SubCategoryName="Handlebar grips" },
                        new Category { Name = "Parts", SubCategoryName="Pedals" },
                        new Category { Name = "Parts", SubCategoryName="Saddles" },

                           new Category { Name = "Shoes", SubCategoryName="Shoes" },

                              new Category { Name = "Tires", SubCategoryName="Tires" },

                                  new Category { Name = "Accessories", SubCategoryName="Body Care" },
                                   new Category { Name = "Accessories", SubCategoryName="Electronics" },
                                    new Category { Name = "Accessories", SubCategoryName="Eyewear" },
                                     new Category { Name = "Accessories", SubCategoryName="Hydratation" },
                                      new Category { Name = "Accessories", SubCategoryName="Lighting" },
                                       new Category { Name = "Accessories", SubCategoryName="Locks" },
                                        new Category { Name = "Accessories", SubCategoryName="Media" },
                                         new Category { Name = "Accessories", SubCategoryName="Nutrition" },
                                          new Category { Name = "Accessories", SubCategoryName="Packs" },
                                           new Category { Name = "Accessories", SubCategoryName="Pumps" },
                                            new Category { Name = "Accessories", SubCategoryName="Storage" },
                                            new Category { Name = "Accessories", SubCategoryName="Safety" },
                                             new Category { Name = "Accessories", SubCategoryName="Tools" },
                                              new Category { Name = "Accessories", SubCategoryName="Trailers" },


                                               new Category { Name = "Clothing", SubCategoryName="Gloves" },
                                                  new Category { Name = "Clothing", SubCategoryName="Jerseys Short" },
                                                     new Category { Name = "Clothing", SubCategoryName="Outwear" },
                                                        new Category { Name = "Clothing", SubCategoryName="Jerseys Long" },
                                                           new Category { Name = "Clothing", SubCategoryName="Shorts" },
                                                              new Category { Name = "Clothing", SubCategoryName="Undergarments" },
                                                                 new Category { Name = "Clothing", SubCategoryName="Socks" },

                                                                  new Category { Name = "Helmets", SubCategoryName="Adults" },
                                                                   new Category { Name = "Helmets", SubCategoryName="Kids" },

                                                                    new Category { Name = "Car Racks", SubCategoryName="Accessories Parts" },
                                                                     new Category { Name = "Car Racks", SubCategoryName="Hitch" },
                                                                      new Category { Name = "Car Racks", SubCategoryName="Roof" },
                                                                       new Category { Name = "Car Racks", SubCategoryName="Trunk" }

            };

            category.ForEach(s => context.Categories.AddOrUpdate(p => p.Name,s));
          context.SaveChanges();

   

            new List <Product>
      {

               new Product {Name="trek-fuel-ex-83",Price=decimal.Parse("350.12"), Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and green",Availability="yes",Brand="Trek",Size="54.6",Gender="male",LastUpdated=DateTime.Parse("12/1/2009 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Mountain").Id},
               new Product {Name="trek-x-caliber-81",Price=decimal.Parse("400.46"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="male",LastUpdated=DateTime.Parse("12/1/2010 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Mountain").Id },
                new Product {Name="trek-fuel-ex-8-29-263585-1",Price=decimal.Parse("4500.46"),Description="This is Description of the mountain bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="43.6",Gender="female",LastUpdated=DateTime.Parse("12/1/2010 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Mountain").Id },               


                   new Product {Name="trek-domane-commuter1",Price=decimal.Parse("1930.56"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Commuter").Id },
                    new Product {Name="trek-fx-commuter1",Price=decimal.Parse("5230.46"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="female",LastUpdated=DateTime.Parse("12/1/2004 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Commuter").Id },
                     new Product {Name="trek-fx-commuter3",Price=decimal.Parse("7200.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="female",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Commuter").Id },
                      new Product {Name="trek-fx-commuter4",Price=decimal.Parse("9300.46"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="male",LastUpdated=DateTime.Parse("12/1/2012 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Commuter").Id },

                      new Product {Name="trek-domane-road1",Price=decimal.Parse("2930.56"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Road").Id },
                    new Product {Name="trek-domane-road2",Price=decimal.Parse("230.46"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="female",LastUpdated=DateTime.Parse("12/1/2004 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Road").Id },
                     new Product {Name="trek-domane-road3",Price=decimal.Parse("1900.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="female",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Road").Id },
                      new Product {Name="trek-domane-road4",Price=decimal.Parse("3000.00"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="blue and white",Availability="yes",Brand="Trek",Size="44.6",Gender="male",LastUpdated=DateTime.Parse("12/1/2012 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Road").Id },

                    new Product {Name="bnt-xr2-13-z",   Price=decimal.Parse("30.56"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Tires").Id },
                    new Product {Name="bontrager-tire1",Price=decimal.Parse("40.26"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="27.5",Gender="male",LastUpdated=DateTime.Parse("12/1/2014 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Tires").Id },
                    new Product {Name="bontrager-tire2",Price=decimal.Parse("54.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Tires").Id },
                    new Product {Name="bontrager-tire3",Price=decimal.Parse("23.00"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="female",LastUpdated=DateTime.Parse("12/1/2012 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Tires").Id },
                    new Product {Name="bontrager-tire4",Price=decimal.Parse("134.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Tires").Id },
   
                     new Product {Name="bontrager-anara-shoes-womens-233326-1",Price=decimal.Parse("30.56"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Shoes").Id },
                    new Product {Name="bontrager-cambion-mtb-shoes-254641-1",Price=decimal.Parse("400.26"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="27.5",Gender="female",LastUpdated=DateTime.Parse("12/1/2014 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Shoes").Id },
                     new Product {Name="bontrager-evoke-wsd-mtb-shoes-copy-195173-1",Price=decimal.Parse("540.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="female",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Shoes").Id },
                      new Product {Name="bontrager-rhythm-mountain-shoe-286329-12",Price=decimal.Parse("235.00"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2012 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Shoes").Id },
                        new Product {Name="bontrager-rovv-mtb-shoe-womens-255179-1",Price=decimal.Parse("999.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="female",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Shoes").Id },


                                           new Product {Name="shim-pd-m324-11-s",Price=decimal.Parse("330.56"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="female",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                    new Product {Name="shimano-105-spd-sl-pedals-copy-225748-1",Price=decimal.Parse("43.26"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="27.5",Gender="female",LastUpdated=DateTime.Parse("12/1/2014 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                     new Product {Name="shimano-pd-m9020-xtr-pedals-copy-241471-1",Price=decimal.Parse("54.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                      new Product {Name="shimano-pd-9000-13-z",Price=decimal.Parse("24.00"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2012 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                        new Product {Name="shimano-pd-m520-black-07-s",Price=decimal.Parse("450.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                                       new Product {Name="shimano-pd-m540-07-s",Price=decimal.Parse("60.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                      new Product {Name="shimano-saint-pedals-13-z",Price=decimal.Parse("237.00"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="28.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2012 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id },
                        new Product {Name="shimano-saint-pedals-13-z",Price=decimal.Parse("1000.44"),Description="This is Description of the bike",Feature="No special features",Video="no for now",PartNumber="part numbers",Color="no",Availability="yes",Brand="Bontrager",Size="26.0",Gender="male",LastUpdated=DateTime.Parse("12/1/2011 12:00:00 AM"), Category_Id=context.Categories.FirstOrDefault(p=>p.SubCategoryName=="Pedals").Id }


            }.ForEach(a => context.Products.AddOrUpdate(p=>p.Name,a));
       
            context.SaveChanges();          

           

        }
    }
}
