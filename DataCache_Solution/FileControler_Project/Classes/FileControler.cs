﻿///////////////////////////////////////////////////////////
//  FileControler.cs
//  Implementation of the Class FileControler
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:23 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Linq;
using FileControler_Project.Services;
using Common_Project.Classes;
using FileControler_Project.Enums;
using FileControler_Project.Handlers.XMLHandler.Classes;

namespace FileControler_Project.Classes
{
    public class FileControler : IFControlerOps
    {

        private FileControlerAgent m_ConnectionControler;
        private XMLHandler m_XMLHandler;
        Dictionary<string, ELoadDataType> supportedTypes;


        public FileControler()
        {
            m_ConnectionControler = new FileControlerAgent();
            m_XMLHandler = new XMLHandler();

            supportedTypes = new Dictionary<string, ELoadDataType>();
            supportedTypes["ostv"] = ELoadDataType.Consumption;
        }

        ~FileControler()
        {

        }

        //// Method originates from distributed DB, placed here for testing purposes only, reloacate it later
        //// consumptionRecords should be merged with DB records
        //private ConsumptionUpdate GenerateConsumptionUpdate(List<ConsumptionRecord> consumptionRecords)
        //{
        //    ConsumptionUpdate retVal = new ConsumptionUpdate();
        //    int tmpHour = 0;
        //    Tuple<int, int> tmpTup;

        //    foreach (var currConsumprionRecord in consumptionRecords )
        //    {
        //        if(!retVal.NewGeos.Contains(currConsumprionRecord.GID)) // Handle new geo
        //        { 
        //            retVal.NewGeos.Add(currConsumprionRecord.GID);
        //        }
                
        //        if(currConsumprionRecord.MWh==-1)   // Handle miss           
        //        {
        //            tmpHour = currConsumprionRecord.GetHour();

        //            if (!retVal.DupsAndMisses[currConsumprionRecord.GID].Item2.Contains(tmpHour)) // Not recorded?
        //            {
        //                retVal.DupsAndMisses[currConsumprionRecord.GID].Item2.Add(tmpHour);       // Record it
        //            }
        //        }
        //        else // If duplicate handle it                       
        //        {
        //            // First one is recorded others for auditing
        //            var first = consumptionRecords.Find(x => x.GID == currConsumprionRecord.GID &&              // Same gID
        //                                                     x.TimeStamp == currConsumprionRecord.TimeStamp &&  // Same timestamp
        //                                                     x.MWh!=-1);                                        // Not a miss
        //            // Dictionary<string, Tuple<List<Tuple<int, int>>,int[]>>
        //            tmpHour = currConsumprionRecord.GetHour();
        //            tmpTup = new Tuple<int, int>(tmpHour, currConsumprionRecord.MWh);
        //            if(!currConsumprionRecord.Equals(first))    // Is a duplicate not the origininal one
        //            {
        //                if (!retVal.DupsAndMisses.ContainsKey(currConsumprionRecord.GID))   // First
        //                {
        //                    // First duplicate or miss for that country
        //                    retVal.DupsAndMisses[currConsumprionRecord.GID] = 
        //                        new Tuple<List<Tuple<int, int>>, List<int>>(new List<Tuple<int, int>>(), new List<int>(24));
        //                }
        //                else if(!retVal.DupsAndMisses[currConsumprionRecord.GID].Item1.Contains(tmpTup)) // Not the first one, not recorded
        //                {
        //                    retVal.DupsAndMisses[currConsumprionRecord.GID].Item1.Add(tmpTup);
        //                }
        //            }
        //            else  //Not a duplicate, has value, check if it is a miss deny
        //            {
        //                if(retVal.DupsAndMisses.ContainsKey(currConsumprionRecord.GID) && 
        //                    retVal.DupsAndMisses[currConsumprionRecord.GID].Item2.Contains(currConsumprionRecord.GetHour()))
        //                {
        //                    retVal.DupsAndMisses[currConsumprionRecord.GID].Item2.Remove(currConsumprionRecord.GetHour());
        //                }
        //            }
        //        }
        //    }
        //    Console.WriteLine(retVal.ToString());   // For testing purposes only
        //    return retVal;
        //}

        /// 
        /// <param name="consumptionRecord"></param>
        private Tuple<EFileLoadStatus, ConsumptionUpdate> InitDBConsumptionWrite(List<ConsumptionRecord> consumptionRecord)
        {
            try
            {
                ConsumptionUpdate update = m_ConnectionControler.OstvConsumptionDBWrite(consumptionRecord);
                return new Tuple<EFileLoadStatus, ConsumptionUpdate>(EFileLoadStatus.Success, update);
            }
            catch
            {
                return new Tuple<EFileLoadStatus, ConsumptionUpdate>(EFileLoadStatus.DBWriteFailed,  new ConsumptionUpdate());    // Risky
            }
            
       
        }

        /// 
        /// <param name="path"></param>
        private Tuple<EFileLoadStatus, ConsumptionUpdate> LoadOstvConsumptionStoreDB(FileInfo fileInfo)
        {
            Tuple<EFileLoadStatus, ConsumptionUpdate> retVal;

            // Add new extension handler here
            switch (fileInfo.Extension)         // Wich handler to call?
            {
                case ".xml":
                    {
                        var loaded = m_XMLHandler.XMLOstvConsumptionRead(fileInfo);
                        retVal = new Tuple<EFileLoadStatus, ConsumptionUpdate>(loaded.Item1, null);
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        //// For testing purposes																			//
                        //Console.WriteLine("\r\n++++++++++ LOADED XML ELEMENTS +++++++++++\n\r");                        //
                        //if (loaded.Item1 != EFileLoadStatus.Success) Console.WriteLine(loaded.Item1.ToString());        //
                        //else                                                                                            //
                        //{                                                                                               //
                        //    foreach (var el in loaded.Item2) Console.WriteLine(el);                                     //																							
                        //}                                                                                               //
                        //Console.WriteLine("\r\n++++++++++++++++++++++++++++++++++++++++++\n\r");                        //
                        //                                                                                                //                                                                                                
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                       
                        return this.InitDBConsumptionWrite(loaded.Item2);  // Write loaded content to distributed DB
                    }
                default:
                    {
                        return new Tuple<EFileLoadStatus, ConsumptionUpdate>(EFileLoadStatus.InvalidFileExtension, new ConsumptionUpdate());
                    }
            }
        }

        private bool IsValidDate(string year, string month, string day, ELoadDataType type)
        {
            DateTime testDateTime;
            try { testDateTime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day)); }
            catch { return false; } // Not a Valid date

            switch (type)                        // //Place to add new supported types in future
            {
                case ELoadDataType.Consumption: // ostv cannot be in future
                    {
                        if (testDateTime <= DateTime.Now) return true;
                        return false;
                    }
                default: { return false; }
            }

        }

        /// 
        /// <param name="path"></param>
        /// <param name="dataType"></param>
        public Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> LoadFileStoreDB(string path, ELoadDataType dataType)
        {

            FileInfo fileInfo = new FileInfo(path);
            char[] splitWordsBy = "_.".ToArray();
            string[] splitParts = fileInfo.Name.Split(splitWordsBy, StringSplitOptions.RemoveEmptyEntries);
            string timeStampBase = splitParts[1]+"-"+splitParts[2]+"-"+splitParts[3];

            if (supportedTypes.ContainsKey(splitParts[0]) &&
                supportedTypes[splitParts[0]] == dataType &&                // Is valid data type? (ostv)
                IsValidDate(splitParts[1], splitParts[2], splitParts[3], dataType)) // Is valid date	
            {
                switch (dataType)
                {
                    //Place to add new supported types in future
                    case ELoadDataType.Consumption:
                        {
                            return new Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>>
                                (timeStampBase, LoadOstvConsumptionStoreDB(fileInfo));
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            return new Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>>
                (timeStampBase, new Tuple<EFileLoadStatus, ConsumptionUpdate>
                (EFileLoadStatus.FileTypeNotSupported, new ConsumptionUpdate()));// Avoiding null as retVal
        }

    }//end FileControler
}