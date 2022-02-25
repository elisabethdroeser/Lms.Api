using Bogus;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        public static async Task InitAsync(LmsApiContext db)
        {
            if (await db.Course.AnyAsync()) return;

            faker = new Faker("sv");

            var courses = GetCourses();
            await db.AddRangeAsync(courses);

            //var modules = GetModules();
            //await db.AddRangeAsync(modules);

            await db.SaveChangesAsync();
        }

        private static IList<Module> GetModules()
        {
            var modules = new List<Module>();
            var rnd = new Random();

            for (int i = 0; i < rnd.Next(2,6); i++)
            {
                var module = new Module
                {
                    Title = faker.Commerce.Product(),
                    StartDate = faker.Date.Soon(1)
                };
                modules.Add(module);
            }

            return modules;
        }

        private static IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();

            for (int i = 0; i < 20; i++)
            {
                var title = faker.Company.CompanyName();
                var startDate = faker.Date.Soon();
                var course = new Course(title, startDate);
                course.Modules = GetModules();

                courses.Add(course);
            }

            return courses;
        }


    }

}
   
