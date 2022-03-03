using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForecastApp.ForecastAppModels;
using ForecastApp.OpenWeatherMapModels;
using ForecastApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApp.Controllers
{
    public class ForecastAppController : Controller
    {
        private readonly IForecastRepository _forecastRepository;

        public ForecastAppController(IForecastRepository forecastAppRepo)
        {
            _forecastRepository = forecastAppRepo;
        }

        public IActionResult SearchCity()
        {
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchCity(SearchCity model)
        {
            if (ModelState.IsValid) {
                return RedirectToAction("City", "ForecastApp", new { city = model.CityName });
            }
            return View(model);
        }

        public IActionResult City(string city)
        {
            WeatherResponse weatherResponse = _forecastRepository.GetForecast(city);
            City viewModel = new City();

            if (weatherResponse != null)
            {
                viewModel.Name = weatherResponse.Name;
                viewModel.Humidity = weatherResponse.Main.Humidity;
                viewModel.Pressure = weatherResponse.Main.Pressure;
                viewModel.Temp = weatherResponse.Main.Temp;
                viewModel.Weather = weatherResponse.Weather[0].Main;
                viewModel.Wind = weatherResponse.Wind.Speed;
                viewModel.Feels_like = weatherResponse.Main.Feels_like;
            } 
            return View(viewModel);
        }
    }
}