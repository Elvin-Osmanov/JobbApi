using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jobb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Jobb.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {

        
        public async Task<IActionResult> Index()
        {
            List<Category> categoryList = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44391/api/category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoryList = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }
            return View(categoryList);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            Category newcategory = new Category();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44391/api/manage/category", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newcategory = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }
            return View(newcategory);
        }

        public ViewResult Detail() => View();

        [HttpPost]
        public async Task<IActionResult> Detail(int id)
        {
            Category category = new Category();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44391/api/manage/category" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        category = JsonConvert.DeserializeObject<Category>(apiResponse);
                    }


                }
            }

            return View(category);
        }

        public ViewResult Delete() => View();

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44391/api/manage/category/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            Category category = new Category();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44391/api/manage/category/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            Category categoryModel = new Category();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(category.Id.ToString()), "Id");
                content.Add(new StringContent(category.Name), "Name");
                content.Add(new StringContent(category.Icon), "Icon");



                using (var response = await httpClient.PutAsync("https://localhost:44324/api/manage/category", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    categoryModel = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }
            return View(categoryModel);
        }
    }
}

    

