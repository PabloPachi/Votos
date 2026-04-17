using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Mvc.Models;

namespace VotingSystem.Mvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var ip = ObtenerIpLocal();
        ViewBag.IP = ip;

        return View();
    }
    private string ObtenerIpLocal()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork &&
                !IPAddress.IsLoopback(ip))
            {
                return ip.ToString();
            }
        }

        return "No encontrada";
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
