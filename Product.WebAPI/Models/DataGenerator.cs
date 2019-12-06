using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace Product.WebAPI.Models
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new POSStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<POSStoreDbContext>>()))
            {
                // Look for any board games.
                if (context.Products.Any())
                {
                    return;   // Data was already seeded
                }

                FileStream fs = File.Open("Database\\Product.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                if (!reader.EndOfStream)
                {
                    //Skip first line (Header column)
                    reader.ReadLine();
                }
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] items = line.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    context.Products.AddRange(
                        new Product
                        {
                            categoryId = items[0],
                            productId = items[1],
                            name = items[2],
                            price = Convert.ToDecimal(items[3]),
                            units = Convert.ToInt32(items[4]) ,
                            picutureUrl = items[5]
                        }
                    );
                }
                reader.Close();
                fs.Close();
                context.SaveChanges();
            }
        }
    }
}
