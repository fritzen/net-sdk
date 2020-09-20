using System;
using System.Diagnostics;
using MeliLibTools.MeliLibApi;
using MeliLibTools.Client;

namespace Example
{
    public class GenericResourceGetExample
    {
        public static void Mainext(){
            Configuration config = new Configuration();
            config.BasePath = "https://api.mercadolibre.com";
            var apiInstance = new MeliApi(config);

            try
            {
                RequestOptions ro = new RequestOptions();
                ro.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "status", "UNANSWERED"));
                // To see output in console
                Console.Write("Resultado get:" + apiInstance.Get("/my/received_questions/search", ro).Data);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling ItemsApi.ItemsIdGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }

}
