using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SummaryScreen.Models;

public class MyObeservableObject : ObservableObject
{
    public HttpClient httpClient = new HttpClient();

    public MyObeservableObject()
    {
        string serverAddress = "http://localhost/";
        httpClient.BaseAddress = new Uri(serverAddress);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


    }
}
