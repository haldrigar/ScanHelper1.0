using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace License
{
    public abstract class LicenseEntity
    {
        [Browsable(false)]
        [XmlIgnore]
        public string AppName { get; protected set; }

        [Browsable(false)]
        [XmlElement("Uid")]
        public string Uid { get; set; }

        [Browsable(false)]
        [XmlElement("Type")]
        public LicenseTypes Type { get; set; }

        [Browsable(false)]
        [XmlElement("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// For child class to do extra validation for those extended properties
        /// </summary>
        /// <param name="validationMsg"></param>
        /// <returns></returns>
        public abstract LicenseStatus DoExtraValidation(out string validationMsg);

    }
}
