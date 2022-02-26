using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAgenda.Utils
{
    public static class Constants
    {
        #region firebase
        public const string FirebaseDataBaseUrl = "https://project001-ac43c.firebaseio.com";//"https://familyagenda-9dcc8.firebaseio.com"; // !
        public const string FirebaseFCMServerKey = "AAAAvyTKOag:APA91bG1TNJBF1U-MM1dOdHWcTgH5NPjJaU-MMuOij536j1LlIGrLImhhmL8pg4RVlby-bSScaOOi5v7KHs7A8V0EBmmXl5rJWOB_hcLbKxly2afmngILMQaq27hvksSiHLWjRP5achfaaj-X1MyJgbXEOZH6p2uFw";//"91173277938"; // !
        public const string FirebaseBasePostUrl = "https://project001-ac43c.firebaseio.com";//"https://familyagenda-9dcc8.firebaseio.com"; // !
        public const string FirebaseApiKey = "AIzaSyCrws0V4w2bJP60WSLWQXDWiFOIr6ISG_4";//"familyagenda-9dcc8"; // !

        #endregion

        //syncfusion
        //public const string Old_SyncfusionLicenseKey = "";
        public const string SyncfusionLicenseKey = "";

        //toast messages
        public const string ConnectivityLostMsg = "No internet connection";
        public const string NoConnectionMsg = "Couldn't refresh items, check your connection";
        public const string AuthConnectivityMsg = "Couldn't authenticate, check your connection";
        public const string CouldNotPerformActionMsg = "Couldn't perform action, check your connection";
        public const string AuthenticationFailedMsg = "Authentication failed: ";
    }
}
