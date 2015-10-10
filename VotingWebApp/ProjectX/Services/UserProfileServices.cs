using ProjectX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectX.Services
{
    public class UserProfileServices
    {

        private ApplicationDbContext db;
        public UserProfileServices()
        {
        db = new ApplicationDbContext();
        }
        public void CreateUserProfile(string firstName, string lastName, string userId, int intialPoints)
        {
            var colors = new List<string> { "AntiqueWhite", "Aqua", "Aquamarine", "Beige", "Black", "Blue", "BlueViolet", "Brown", "DarkGreen", "DarkViolet", "SlateGray" };
            Random random = new Random();
            int index = random.Next(colors.Count);
            var color = colors[index];

            var userProfile = new UserProfile { FirstName = firstName, LastName = lastName, Points = intialPoints, ApplicationUserId = userId, CanvasColor = color };
            db.UserProfiles.Add(userProfile);
            db.SaveChanges();

        }
    }
}