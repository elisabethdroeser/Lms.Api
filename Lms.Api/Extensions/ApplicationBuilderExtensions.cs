using Bogus;
using Lms.Core.Entities;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static Faker faker;
   
        public static async Task InitAsync(LmsApiContext db)
        {
            if (await db.Course.AnyAsync()) return;

            faker = new Faker("sv");

            var courses = GetCourses();
            await db.AddRangeAsync(courses);

            var modules = GetModules();
            await db.AddRangeAsync(modules);

            await db.SaveChangesAsync();
        }

        private static IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();   
            for (int i = 0; i < 10; i++)
            {
                var course = new Course
                {
                    Title = faker.C();
                }
            }


        }

        private static object GetModules()
        {
            throw new NotImplementedException();
        }

     
    }
}
