using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        private static int nextId = 1; // Static variable to generate unique IDs

        // GET: User
        public ActionResult Index()
        {
            return View(userlist); // Returns a view with the user list
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }
            return View(user); // Return the details view with the user data
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(); // Return the Create view
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = nextId++; // Assign a unique ID to the new user
                userlist.Add(user); // Add the new user to the list
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }
            return View(user); // Return the Create view with validation errors
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }
            return View(user); // Return the Edit view with the user data
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User updatedUser)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            if (ModelState.IsValid)
            {
                // Update the user's information
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                // Add other fields as necessary
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }
            return View(updatedUser); // Return the Edit view with validation errors
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }
            return View(user); // Return the Delete confirmation view
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            userlist.Remove(user); // Remove the user from the list
            return RedirectToAction(nameof(Index)); // Redirect to the Index action
        }

        // GET: User/Search
        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Index", userlist); // If no query is provided, return the full list
            }

            var searchResults = userlist
                .Where(u => u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                            u.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View("Index", searchResults); // Return the Index view with the filtered results
        }
    }
}
