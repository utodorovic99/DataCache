﻿///////////////////////////////////////////////////////////
//  GeoRecord.cs
//  Implementation of the Class GeoRecord
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:23 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common_Project.Classes
{
    [Serializable]
    public class GeoRecord
    {

        private string gID;
        private string gName;

        public GeoRecord()
        {
            gID = "";
            gName = "";
        }

        public GeoRecord(string gID, string gName)
        {
            this.gID = gID;
            this.gName = gName;
        }

        ~GeoRecord()
        {

        }

        public string GID
        {
            get { return gID;  }
            set { gID = value; }
        }

        public string GName
        {
            get { return gName;  }
            set { gName = value; }
        }

        public bool IsEmpty()     { return gName == "" && gID == ""; }
        public bool IsComplete()  { return gName!="" && gID!="";     }
        public override string ToString()
        {
            return String.Format("Geographic entity: GID: {0}\tName: {1}", gID, gName);
        }
    }//end GeoRecord
}