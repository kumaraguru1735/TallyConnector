﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TallyConnector.Models
{
    [XmlRoot(ElementName = "COSTCENTRE")]
    public class CostCenter:TallyXmlJson
    {
        
        [XmlAttribute(AttributeName = "ID")]
        public int TallyId { get; set; }

        [XmlAttribute(AttributeName = "NAME")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "REQNAME")]
        public string VName { get; set; }

        [XmlElement(ElementName = "CATEGORY")]
        public string Category { get; set; }

        [XmlElement(ElementName = "PARENT")]
        public string Parent { get; set; }

        [XmlIgnore]
        public string Alias
        {
            get
            {
                if (this.LanguageNameList.NameList.NAMES.Count > 0)
                {
                    this.LanguageNameList.NameList.NAMES[0] = this.Name;
                    return string.Join("\n", this.LanguageNameList.NameList.NAMES.GetRange(1, this.LanguageNameList.NameList.NAMES.Count - 1));
                }
                else
                {
                    this.LanguageNameList.NameList.NAMES.Add(this.Name);
                    return null;
                }

            }
            set
            {
                this.LanguageNameList = new LanguageNameList();

                if (value != null)
                {
                    LanguageNameList.NameList.NAMES.Add(Name);
                    if (value != "")
                    {
                        LanguageNameList.NameList.NAMES.AddRange(value.Split('\n').ToList());
                    }

                }
                else
                {
                    LanguageNameList.NameList.NAMES.Add(Name);
                }

            }
        }

        [JsonIgnore]
        [XmlElement(ElementName = "LANGUAGENAME.LIST")]
        public LanguageNameList LanguageNameList { get; set; }
        /// <summary>
        /// Accepted Values //Create, Alter, Delete
        /// </summary>
        [JsonIgnore]
        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }
    }

    [XmlRoot(ElementName = "ENVELOPE")]
    public class CostCentEnvelope : TallyXmlJson
    {

        [XmlElement(ElementName = "HEADER")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "BODY")]
        public CCentBody Body { get; set; } = new CCentBody();
    }

    [XmlRoot(ElementName = "BODY")]
    public class CCentBody
    {
        [XmlElement(ElementName = "DESC")]
        public Description Desc { get; set; } = new Description();

        [XmlElement(ElementName = "DATA")]
        public CCentData Data { get; set; } = new CCentData();
    }

    [XmlRoot(ElementName = "DATA")]
    public class CCentData
    {
        [XmlElement(ElementName = "TALLYMESSAGE")]
        public CCentMessage Message { get; set; } = new CCentMessage();
    }

    [XmlRoot(ElementName = "TALLYMESSAGE")]
    public class CCentMessage
    {
        [XmlElement(ElementName = "COSTCENTRE")]
        public CostCenter CostCenter { get; set; }
    }
}
