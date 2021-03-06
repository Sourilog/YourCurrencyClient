﻿// client for Currency

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Currency.Models;


namespace CurrencyClient
{
    class Client
    {
        // async call
        static async Task DoWork()
        { string currencyname = "";
           
            try
            {
				// http client creates an object
                using (HttpClient client = new HttpClient())
                {
					// connects to uri 
					//requires to be changed to Azure 
                    client.BaseAddress = new Uri("http://yourcurrency.azurewebsites.net");            // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //1
                    //read all current currency information
                  //GET  calling  all currency
                    HttpResponseMessage response = await client.GetAsync("Currency/all");
                 if (response.IsSuccessStatusCode)
                    {
                       
                        var Allcurrency = await response.Content.ReadAsAsync<IEnumerable<CurrencyInformation>>();
                                        foreach (var k in Allcurrency)
                                        {
                                            Console.WriteLine(k.Currencyname + "  " + k.CurrencyRate + "   " +k.RateDate.ToShortDateString());
                                        }
                                    }
                                    else {
                                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                                    }
                 //2
                    //Change currency from one to another // this is hardcoded for test purposes
                    //Euro or Pound or Yen
                    currencyname = "Euro";
                    double price = 0.876;
                    var w = new CurrencyInformation() { Currencyname = currencyname, CurrencyRate = price, RateDate = DateTime.Now };
                    //POST
					response = await client.PostAsJsonAsync("Currency/SaveNewCurrency",w);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Write("\n\n\n Change ...\n");
                        // read result 
                        var result = await response.Content.ReadAsAsync<String>();
                        Console.WriteLine(result.ToString());
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    
                    //3
                    //find           
                    //Get Method   
					
                    CurrencyInformation t = new CurrencyInformation();
                    //Euro or Pound or Yen - hardcoded as euro for test purposes
                    currencyname = "Euro";
                    response = await client.GetAsync("Currency/Find/"+currencyname);
                                    if (response.IsSuccessStatusCode)
                                    {
                        Console.Write("\n\n\n Find ...\n");
                                    var   e= await response.Content.ReadAsAsync< IEnumerable<CurrencyInformation>>();
                                        foreach (var l in e)
                                        {
                                            Console.WriteLine(l.Currencyname + "  " + l.CurrencyRate + "   " + l.RateDate.ToShortDateString());
                                        }
                                    }
                                    else

                                    {
                                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                                    }


                    //4
                    //Convert          
                    //Get Method

                    //Euro or Pound or Yen - hardcoded for test purposes

                    response = await client.GetAsync("Currency/Convert?currncyname_in=Yen&p_in=3&currncyname_to=Euro");
                    if (response.IsSuccessStatusCode)
                    {
                        
                        Console.Write("\n\n\n Convert ...\n");
                        var r = await response.Content.ReadAsAsync<String>();

                        Console.Write("3 Yen="+r+ " Euro");
                    }
                    else

                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }



                }
            }
            catch { }
                   
        }

        static void Main(string[] args)
        {
            DoWork().Wait();
            Console.ReadKey();
        }
    }
}
