using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace StarWars.API.Automation.Tests
{
    [TestFixture]
    public class SwapiTests
    {
        RestClient client;
        IRestResponse response;
        RestRequest request;
        
        public void GetRequestContent(string endpoint)
        {
            client = new RestClient($"http://swapi.co/api/{endpoint}");
            request = new RestRequest();
            response = client.Execute(request);
        }

        [Test]
        public void CheckIfNameIsCorrect()
        {
            GetRequestContent("people/1");
            People people = new JsonDeserializer().Deserialize<People>(response);
            people.Name.Should().Be("Luke Skywalker");
        }

        [Test]
        public void CheckIfAllContentIsCorrect()
        {
            GetRequestContent("planets/2");
            Planets actualPlanet = new JsonDeserializer().Deserialize<Planets>(response);
            Planets expectedPlanet = new Planets()
            {
                Name = "Alderaan",
                RotationPeriod = "24",
                OrbitalPeriod = "364",
                Diameter = "12500",
                Climate = "temperate",
                Gravity = "1 standard",
                Terrain = "grasslands, mountains",
                SurfaceWater = "40",
                Population = "2000000000",
                Residents = new string[] { "https://swapi.co/api/people/5/", "https://swapi.co/api/people/68/", "https://swapi.co/api/people/81/" }
            };
            actualPlanet.Should().Be(expectedPlanet);
        }
    }
}
