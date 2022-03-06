using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System.Text.Json;

namespace StarWars.API.Automation.Tests
{
    [TestFixture]
    public class SwapiTests
    {
        RestClient client;
        RestResponse response;
        RestRequest request;

        string BASE_URL = "https://swapi.dev/api/";


        public void GetRequestContent(string endpoint)
        {
            client = new RestClient($"{BASE_URL}{endpoint}");
            request = new RestRequest();
            response = client.ExecuteGetAsync(request).GetAwaiter().GetResult();
        }

        [Test]
        public void CheckIfNameIsCorrect()
        {
            GetRequestContent("people/1");
            People people = JsonSerializer.Deserialize<People>(response.Content); // <People>(response);
            people.name.Should().Be("Luke Skywalker");
        }

        [Test]
        public void CheckIfAllContentIsCorrect()
        {
            GetRequestContent("planets/2");
            Planets actualPlanet = JsonSerializer.Deserialize<Planets>(response.Content);
            Planets expectedPlanet = new Planets()
            {
                name = "Alderaan",
                created = "2014-12-10T11:35:48.479000Z",
                edited = "2014-12-20T20:58:18.420000Z",
                rotation_period = "24",
                orbital_period = "364",
                diameter = "12500",
                climate = "temperate",
                gravity = "1 standard",
                terrain = "grasslands, mountains",
                surface_water = "40",
                population = "2000000000",
                residents = new string[] { $"{BASE_URL}people/5/", $"{BASE_URL}people/68/", $"{BASE_URL}people/81/" },
                films = new string[] {$"{BASE_URL}films/1/", $"{BASE_URL}films/6/" },
                url = $"{BASE_URL}planets/2/"
            };
            actualPlanet.Should().Equals(expectedPlanet);
        }
    }
}
