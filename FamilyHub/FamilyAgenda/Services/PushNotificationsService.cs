using FamilyAgenda.Models;
using FamilyAgenda.Utils;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FamilyAgenda.Services
{
    public static class PushNotificationsService
    {
        public static void ConfigureFirebasePushNotifications()
        {
            // Handle when your app starts
            try
            {
                CrossFirebasePushNotification.Current.UnsubscribeAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }

            string user = null;

            try
            {
                user = Preferences.Get("user", string.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }

            if (!string.IsNullOrEmpty(user))
            {
                var topic = "Sofi";
                if (Preferences.Get("user", "") == "Sofi")
                {
                    topic = "Panos";
                }

                CrossFirebasePushNotification.Current.Subscribe(topic);
            }

            try
            {
                CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                {
                    //Debug.WriteLine($"TOKEN REC: {p.Token}");

                    try
                    {
                        //Debug.WriteLine("TOKEN REC");

                        Debug.WriteLine($"TOKEN REC: {p.Token}");
                    }
                    catch (Exception ex1)
                    {
                        Debug.WriteLine("Exception : " + ex1.Message);
                    }

                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : " + ex.Message);
            }

            try
            {
                Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : " + ex.Message);
            }



            // On notification received
            try
            {
                CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
                {
                    try
                    {
                        Debug.WriteLine("Received");
                        if (p.Data.ContainsKey("body"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                            //App.Current.MainPage.Message = $"{p.Data["body"]}";
                        });

                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception : " + ex.Message);
                    }

                };
            }
            catch (Exception ex2)
            {
                Debug.WriteLine("Exception : " + ex2.Message);
            }


            // On notification opened
            try
            {
                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Opened");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                    if (!string.IsNullOrEmpty(p.Identifier))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                        //App.Current.MainPage.Message = p.Identifier;
                    });
                    }
                    else if (p.Data.ContainsKey("color"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Current.MainPage.Navigation.PushAsync(new ContentPage()
                            {
                                BackgroundColor = Color.FromHex($"{p.Data["color"]}")

                            });
                        });

                    }
                    else if (p.Data.ContainsKey("aps.alert.title"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                        //App.Current.MainPage.Message = $"{p.Data["aps.alert.title"]}";
                    });

                    }
                };
            }
            catch (Exception ex3)
            {
                Debug.WriteLine("Exception : " + ex3.Message);
            }

            // ................
            try
            {
                CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Action");

                    if (!string.IsNullOrEmpty(p.Identifier))
                    {
                        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                        foreach (var data in p.Data)
                        {
                            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                        }

                    }

                };
            }
            catch (Exception ex4)
            {
                Debug.WriteLine("Exception : " + ex4.Message);
            }


            // On notification deleted
            try
            {
                CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Dismissed");
                };
            }
            catch (Exception ex5)
            {
                Debug.WriteLine("Exception : " + ex5.Message);
            }
        }

        public static async void SendNotificationAsync(string mesage, string username)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.FirebaseBasePostUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "key", "=" + Constants.FirebaseFCMServerKey);

                try
                {
                    string jsonString = JsonConvert.SerializeObject(new NotificationMessage
                    {
                        To =  $"/topics/{username}",
                        Data = new Data { Message = mesage }
                    });

                    var response = await httpClient.PostAsync("send", new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Helpers.ShowToastMessage(response.StatusCode.ToString());
                    }
                }

                catch (Exception e)
                {
                    Helpers.ShowToastMessage(e.Message);
                }
            }
        }
    }
}
