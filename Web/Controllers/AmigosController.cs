using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using System.Text;

namespace Web.Controllers
{
    public class AmigosController : Controller
    {

        public async Task<IActionResult> Index()
        {
            List<Amigos> amigosList = new List<Amigos>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62098/api/amigo"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    amigosList = JsonConvert.DeserializeObject<List<Amigos>>(apiResponse);
                }
            }
            return View(amigosList);
        }

        public ViewResult GetAmigos() => View();

        [HttpPost]
        public async Task<IActionResult> GetAmigos(int id)
        {
            Amigos amigos = new Amigos();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62098/api/amigo/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    amigos = JsonConvert.DeserializeObject<Amigos>(apiResponse);
                }
            }
            return View(amigos);
        }

        public ViewResult AddReservation() => View();

        [HttpPost]
        public async Task<IActionResult> AddReservation(Amigos reservation)
        {
            Amigos receivedReservation = new Amigos();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:62098/api/amigo/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedReservation = JsonConvert.DeserializeObject<Amigos>(apiResponse);
                }
            }
            return View(receivedReservation);
        }


        public async Task<IActionResult> UpdateReservation(int id)
        {
            Amigos reservation = new Amigos();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62098/api/amigo/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Amigos>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Amigos reservation)
        {
            Amigos receivedReservation = new Amigos();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(reservation.Id.ToString()), "Id");
                content.Add(new StringContent(reservation.Nome), "Nome");
                content.Add(new StringContent(reservation.Sobrenome), "Sobrenome");
                content.Add(new StringContent(reservation.Aniversario.ToString()), "Aniversário");

                using (var response = await httpClient.PutAsync("http://localhost:62098/api/amigo", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedReservation = JsonConvert.DeserializeObject<Amigos>(apiResponse);
                }
            }
            return View(receivedReservation);
        }

        public async Task<IActionResult> DeleteReservation(int ReservationId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:62098/api/amigo/" + ReservationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
