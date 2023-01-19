using System.ComponentModel;

namespace Events.Common
{
    public static class Enums
    {
        public enum DocumentStatus
        {
            Received = 1,
            Processing = 2,
            Completed = 3,
            Rejected = 4
        }
        public enum RegistrationMethods
        {
            [Description("Email SignUp")]
            EmailSignUp = 1,
            [Description("Google")]
            Google = 2,
            [Description("Facebook")]
            Facebook = 3,
            [Description("LinkedIn")]
            LinkedIn = 4,
        }

        public enum ServiceTypes
        {
            [Description("Standard Invoice")]
            [DefaultValue("stdinv")]
            StandardInvoice = 1
        }

        public enum UserRoleTypes
        {
            Administrator = 1,
            Sender = 2
        }

        public enum UserTypes
        {
            SystemAdmin = 1

        }

        public enum EmailTemplateTypes
        {
            [Description("Welcome Email")]
            WelcomeEmail = 1,

            [Description("Email Authentication")]
            EmailAuthentication = 2,

            [Description("Forgot Password")]
            ForgotPassword = 3,

            [Description("Notification Rejection")]
            NotificationRejection = 4,

            [Description("Delivery Of Completed Request")]
            DeliveryOfCompletedRequest = 5,

            [Description("Receipt Email")]
            ReceiptEmail = 6,

            [Description("System Alert")]
            SystemAlert = 7,

            [Description("Billing Notice")]
            BillingNotice = 8,

            [Description("Account Freeze Notification Client")]
            AccountFreezeNotification_Client = 9,

            [Description("Account Freeze Notification Internal")]
            AccountFreezeNotification_Internal = 10,

            [Description("IACS AR for HVA")]
            IACSARforHVA = 11,

            [Description("IACS System Alert, SA qualifies for HVA")]
            SAQualifiesForHVA = 12,

            [Description("IACS Daily Operations Summary")]
            DailyOperationsInternalEmail = 13,

            [Description("Contact Us")]
            ContactUs = 14


        }

        public enum ExportOptions
        {
            CSV = 1,
            JSON = 2,
            NoImage = 3,
            OriginalImage = 4,
            PDFA = 5
        }
        public enum DataPurgeOptions
        {
            Immediate = 1,
            OneDay = 2,
            ThreeDays = 3,
            TwoWeeks = 4
        }

        public enum NotificationOptions
        {
            DonotSend = 1,
            FirstEveryWeek = 2,
            AllReceipte = 3,
            Every10Requests = 4
        }

        public enum PaymentMethodTypes
        {
            Strip = 1,
            Offline = 2           
        }

        public enum BillingTermTypes
        {
            Monthly = 1,
            HalfYearly = 2,
            Yearly = 3
        }

        public enum FromEntity
        {
            InboundAzureFunction = 1,
            StdInvoice_PreAzureFunction = 2,
            StdInvoice_PostAzureFunction = 3,
        }

    }
}
