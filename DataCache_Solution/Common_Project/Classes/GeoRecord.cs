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
    public class GeoRecord
    {

        private string gID;
        private string gName;

        public GeoRecord()
        {

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
            get
            {
                return gID;
            }
            set
            {
                gID = value;
            }
        }

        public string GName
        {
            get
            {
                return gName;
            }
            set
            {
                gName = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Geographic entity: GID: {0}\tName: {1}", gID, gName);
        }
    }//end GeoRecord
}