using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace License
{
    public class MyLicense : LicenseEntity
    {
        [Browsable(false)]
        [XmlElement("LicenseOwner")]
        public string LicenseOwner { get; set; }

        [Browsable(false)]
        [XmlElement("LicenseNumber")]
        public string LicenseNumber { get; set; }

        [Browsable(false)]
        [XmlElement("LogoOwner")]
        public string LogoOwner { get; set; }

        [Browsable(false)]
        [XmlElement("LicenseEnd")]
        public DateTime LicenseEnd { get; set; }
        
        public MyLicense()
        {
            //Initialize app name for the license
            AppName = "ScanHelper";
        }

        public override LicenseStatus DoExtraValidation(out string validationMsg)
        {
            LicenseStatus licStatus;

            switch (Type)
            {
                case LicenseTypes.Single:
                    //For Single License, check whether UID is matched
                    if (Uid == LicenseHandler.GenerateUid(AppName))
                    {
                        validationMsg = "Licencja poprawna";
                        licStatus = LicenseStatus.Valid;
                    }
                    else
                    {
                        validationMsg = "Nieprawidłowy plik licencji";
                        licStatus = LicenseStatus.Invalid;
                    }
                    break;

                case LicenseTypes.Volume:
                    //No UID checking for Volume License
                    validationMsg = "Licencja poprawna";
                    licStatus = LicenseStatus.Valid;
                    break;

                case LicenseTypes.Unknown:
                    validationMsg = "Licencja niepoprawna";
                    licStatus = LicenseStatus.Invalid;
                    break;

                default:
                    validationMsg = "Licencja niepoprawna";
                    licStatus = LicenseStatus.Invalid;
                    break;
            }

            if (licStatus == LicenseStatus.Valid)
            {
                DateTime ntpTime = new NetworkTime().GetDateTime();

                if (ntpTime > LicenseEnd)
                {
                    validationMsg = $"Licencja straciła ważność dnia: {LicenseEnd}";
                    licStatus = LicenseStatus.Expired;
                }
            }

            return licStatus;
        }
    }
}
